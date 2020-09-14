using System;
using System.Diagnostics;

namespace EnduranceTest
{
    public class AllTestsRunner: ITestRunner
    {
        private TestedMethods _methods;
        public void Run(TestedMethods methods, int minutes)
        {
            _methods = methods;
            foreach (var testMethod in _methods.Methods)
            {
                Stopwatch methodSw = new Stopwatch();
                methodSw.Restart();
                testMethod.Invoke();
                testMethod.Duration = methodSw.Elapsed.TotalSeconds;
                Console.WriteLine($"[{testMethod.Name}] duration {testMethod.Duration}");
            }
        }
    }
}
