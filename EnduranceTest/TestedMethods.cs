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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="methodInfo"></param>
        /// <returns> -1 if no Duration attribuet property found, will be not tested</returns>
        //private long GetExpectedDuration(MethodInfo methodInfo)
        //{
        //    long expectedDuration = -1;
        //    var customAttributes = methodInfo.GetCustomAttributes(false);
        //    var customAttribute = customAttributes[0];
        //    var attributeType = customAttribute.GetType();
        //    var propertyInfo = attributeType.GetProperty(TEST_DURATION);
        //    if(propertyInfo != null)
        //    {
        //        object duration = propertyInfo.GetValue(customAttribute, null);
        //        if (duration != null && long.TryParse(duration.ToString(), out expectedDuration))
        //        {
        //        }
        //        else
        //        {
        //            expectedDuration = -1;
        //        }
        //    }
        //    return expectedDuration;
        //}
    }
}
