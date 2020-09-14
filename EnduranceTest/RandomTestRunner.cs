﻿using System;
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

        public void Run(TestedMethods methods, int minutes)
        {
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
            method.Invoke();
            method.Duration = methodSw.Elapsed.TotalSeconds;
            Console.WriteLine($"[{method.ClassFullName}] [{method.Name}]  Duration {method.Duration}");


        }
    }
}