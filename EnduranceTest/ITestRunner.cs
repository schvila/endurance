using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnduranceTest
{
    interface ITestRunner
    {
        void Run(TestedMethods methods, int minutes, System.IO.TextWriter writer);
    }
}
