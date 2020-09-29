using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnduranceTest
{
    class DurationAdjustedTestRunner : ITestRunner
    {
        private TestedMethods _methods;
        private int _testMax;
        private long _testDuration;
        private Random _random = new Random();
        System.IO.TextWriter _writer;

        public void Run(TestedMethods methods, int minutes, System.IO.TextWriter writer)
        {
            _writer = writer;
            //
            var allTestsRunner = new AllTestsRunner(true);
            allTestsRunner.Run(methods, minutes, _writer);
            _methods = methods;
            _testDuration = (long)minutes * 60_000;
            _testMax = _methods.Methods.Count;
            EvaluateProbabilities();
            Stopwatch testDuration = Stopwatch.StartNew();
            while (testDuration.ElapsedMilliseconds < _testDuration)
            {
                InvokeNextTest();
            }



        }

        private void InvokeNextTest()
        {
           while(true)
            {
                int index = _random.Next(0, _testMax);
                var randomProbability = _random.NextDouble();
                var test = _methods.Methods[index];
                if (randomProbability <= test.Probability)
                {
                    Stopwatch methodSw = Stopwatch.StartNew();
                    test.Invoke( _writer );

                    test.Duration = methodSw.Elapsed.TotalMilliseconds;
                    test.Share += test.Duration;
                    test.TimesExecuted++;

                    Console.WriteLine(test.ToShortDurationString(_methods.TestNameWidth));

                    EvaluateProbabilities();
                    return;
                }

            }
        }

        private void EvaluateProbabilities()
        {
            double allTestDuration = _methods.Methods.Select(m => m.Duration).Sum();
            foreach(var test in _methods.Methods)
            {
                var share = test.Duration / allTestDuration;
                test.Probability = (1 - share) / (_testMax - 1);
            }
        }
    }
}
