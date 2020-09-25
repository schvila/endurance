using System;
using System.Diagnostics;

namespace EnduranceTest
{
    public class AllTestsRunner: ITestRunner
    {
        private TestedMethods _methods;
        public void Run(TestedMethods methods, int minutes, System.IO.TextWriter writer)
        {
            _methods = methods;
            Stopwatch methodSw = new Stopwatch();
            foreach (var testMethod in _methods.Methods)
            {
                methodSw.Restart();
                testMethod.Invoke(writer);
                testMethod.Duration = methodSw.Elapsed.;
                Console.WriteLine(testMethod.ToShortDurationString(_methods.TestNameWidth));

            }
        }
    }
}
