﻿using System;
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
        public double Duration { get; set; }
        public string Name => _methodInfo.Name;
        public double Probability { get; set; }
        public int TimesExecuted { get; set; }

        public TestedMethod(MethodInfo methodInfo, TestedType testedType)
        {
            _methodInfo = methodInfo;
            MethodOwnerType = testedType;
        }
        private TestedMethod() { }

        public object Invoke()
        {
            var p = _methodInfo.GetParameters();
            object res = null;
            if (p.Length == 0)
            {
                res = TestMethod.Invoke(MethodOwnerType.Instance, null);
            }
            else if (p.Length == 1 && p[0].ParameterType.FullName == "System.IO.TextWriter")
            {
                //p[0].ParameterType.Equals(typeof(int)).Dump("isINT");
                object[] parametersArray = new object[] { Console.Out };
                res = TestMethod.Invoke(MethodOwnerType.Instance, parametersArray);
            }


            return res;
        }


    }
}