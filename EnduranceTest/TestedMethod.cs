using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace EnduranceTest
{
    public class TestedMethod
    {
        private MethodInfo _methodInfo;
        public MethodInfo TestMethod => _methodInfo;
        public object MethodInstnce { get; set; }
        public TestedType MethodOwnerType;
        public string ClassName => MethodOwnerType.TestType.Name;
        public string ClassFullName => MethodOwnerType.TestType.FullName;
        public string TestFullName => $"[{ClassFullName}] [{Name}]";
        public double Duration { get; set; }
        public double Share { get; set; }
        public string Name => _methodInfo.Name;
        public double Probability { get; set; }
        public int TimesExecuted { get; set; }

        public TestedMethod(MethodInfo methodInfo, TestedType testedType)
        {
            _methodInfo = methodInfo;
            MethodOwnerType = testedType;
        }
        private TestedMethod() { }

        public object Invoke(System.IO.TextWriter writer)
        {
            var p = _methodInfo.GetParameters();
            object res = null;
            if (p.Length == 0)
            {
                res = TestMethod.Invoke(MethodOwnerType.Instance, null);
            }
            else if (p.Length == 1 && p[0].ParameterType.FullName == "System.IO.TextWriter")
            {
                object[] parametersArray = new object[] { writer };
                res = TestMethod.Invoke(MethodOwnerType.Instance, parametersArray);
            }


            return res;
        }
        public static bool IsTestable(MethodInfo methodInfo)
        {
            var p = methodInfo.GetParameters();
            return ((p.Length == 0) ||
                (p.Length == 1 && p[0].ParameterType.FullName == "System.IO.TextWriter"));

        }
        /*
                    Console.WriteLine($"{"Test name".FormatWidth(TestNameWidth)} {0, 15} {1,15} {2,15}",
                        "Share[min]", "Executed", "Avg. dur.[ms]");
         */
        public string ToReportString(int width)
        {
            return string.Format($"{TestFullName.FormatWidth(width)} {{0,-15:N5}} {{1,-15}} {{2,-15:N5}}",
                Share/60_000,
                TimesExecuted,
                Share/TimesExecuted);
        }
        public string ToShortDurationString(int width)
        {
            return $"{TestFullName.FormatWidth(width)} {Duration}";
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
