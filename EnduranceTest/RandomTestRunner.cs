using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace EnduranceTest
{
    public class RandomTestRunner: ITestRunner
    {
        private TestedMethods _methods;
        private int _testMax;
        private long _testDuration;
        System.IO.TextWriter _writer;

        public void Run(TestedMethods methods, int minutes, System.IO.TextWriter writer)
        {
            _writer = writer;
            _methods = methods;
            _testDuration = (long)minutes * 60_000;
            _testMax = _methods.Methods.Count;
            Stopwatch testDuration = Stopwatch.StartNew();
            Random random = new Random();
            while (testDuration.ElapsedMilliseconds < _testDuration)
            {
                InvokeNextTest(random);
            }
        }

        private void InvokeNextTest(Random random)
        {
            int index = random.Next(0, _testMax);
            var method = _methods.Methods[index];
            Stopwatch methodSw = Stopwatch.StartNew();
            method.Invoke(_writer);
            method.Duration = methodSw.Elapsed.TotalSeconds;
            //Console.WriteLine($"[{method.ClassFullName}] [{method.Name}]  Duration {method.Duration}");
            Console.WriteLine(method.TestDurationFormatted(_methods.TestNameWidth));
        }
    }
}
