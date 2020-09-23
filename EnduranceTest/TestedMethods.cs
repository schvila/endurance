using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EnduranceTest
{
    public class TestedMethods
    {
        private static string TEST_ATTRIBUTE = "EnduranceTestAttribute";
        private List<TestedMethod> _methodsList = new List<TestedMethod>();
        public List<TestedMethod> Methods => _methodsList;
        public IEnumerable<TestedType> TestedTypes => _methodsList.Select(s => s.MethodOwnerType);


        private Assembly _testedAssembly;
        public TestedMethods(string assemblyPath)
        {
            _testedAssembly = Assembly.LoadFile(assemblyPath);
            InitList();

        }
        public TestedMethods(Assembly assembly)
        {
            _testedAssembly = assembly;
            InitList();
        }
        private void InitList()
        {
            foreach (var testtyp in _testedAssembly.GetTypes().Where(t => (CustomAttributeData.GetCustomAttributes(t).SingleOrDefault(cad => cad.AttributeType.FullName.Contains(TEST_ATTRIBUTE)) != null)))
            {
                foreach (var method in testtyp.GetMethods().Where(t => (CustomAttributeData.GetCustomAttributes(t).SingleOrDefault(cad => cad.AttributeType.FullName.Contains(TEST_ATTRIBUTE)) != null)))
                {
                    var cad = method.GetCustomAttributes(false);
                    if (cad.Length > 0)
                    {
                        //double expectedDuration = GetExpectedDuration(method);
                        if(TestedMethod.IsTestable(method))
                            _methodsList.Add(new TestedMethod(method, new TestedType(testtyp)));
                    }
                }
            }
        }
        private int _testNameWidth = -1;
        public int TestNameWidth
        {
            get
            {
                if(_testNameWidth == -1)
                {
                    if (_methodsList.Count() > 0)
                        _testNameWidth = _methodsList.Max(m => m.TestFullName.Length);
                    else
                        _testNameWidth = 50;
                }
                return _testNameWidth;
            }
        }
        public void SimpleReport()
        {
            Console.WriteLine($"{"Test name".FormatWidth(TestNameWidth)} Duration");

            foreach (var test in _methodsList)
            {
                Console.WriteLine(test.TestDurationFormatted(TestNameWidth));
            }
        }

    }

}
