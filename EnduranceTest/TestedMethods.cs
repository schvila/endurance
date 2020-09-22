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
                        _methodsList.Add(new TestedMethod(method, new TestedType(testtyp)));
                    }
                }
            }
        }
        public void SimpleReport()
        {
            int width = _methodsList.Max(m => m.TestFullName.Length);
            Console.WriteLine($"{"Test name".FormatWidth(width)} Duration");

            foreach (var test in _methodsList)
            {
                Console.WriteLine($"{test.TestFullName.FormatWidth(width)} {test.Duration}");
            }
        }

    }
    public static class ExtensionUtils
    {
        public static string FormatWidth(this string text, int width)
        {
            string fmt = "{0,-" + width + "}";
            var arg = text.Length > width ? text.Substring(0, width) : text;
            return string.Format(fmt, arg);
        }
    }

}
