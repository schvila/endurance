using System;
using System.Diagnostics;

namespace EnduranceTest
{
    public class AllTestsRunner: ITestRunner
    {
        private TestedMethods _methods;
        private bool _durationOnly;
        public AllTestsRunner(bool durationOnly = false)
        {
            _durationOnly = durationOnly;
        }
        public void Run(TestedMethods methods, int minutes, System.IO.TextWriter writer)
        {
            _methods = methods;
            Stopwatch methodSw = new Stopwatch();
            foreach (var testMethod in _methods.Methods)
            {
                methodSw.Restart();
                testMethod.Invoke(_durationOnly? null: writer);
                testMethod.Duration = methodSw.Elapsed.TotalMilliseconds;
                if (!_durationOnly)
                {
                    testMethod.Share += testMethod.Duration;
                    testMethod.TimesExecuted++;
                    Console.WriteLine(testMethod.ToShortDurationString(_methods.TestNameWidth));
                }
            }
        }
    }
}
