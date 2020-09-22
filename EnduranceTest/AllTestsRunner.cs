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
            foreach (var testMethod in _methods.Methods)
            {
                Stopwatch methodSw = new Stopwatch();
                methodSw.Restart();
                testMethod.Invoke(writer);
                testMethod.Duration = methodSw.Elapsed.TotalSeconds;
                Console.WriteLine($"[{testMethod.Name}] duration {testMethod.Duration}");
            }
        }
    }
}
