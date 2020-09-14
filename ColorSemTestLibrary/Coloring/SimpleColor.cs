using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ColorSemTestLibrary.Common;

namespace ColoringColorSemTestLibrary
{
    [EnduranceTest("Simple coloring test")]
    public class SimpleColor
    {
        [EnduranceTest]
        public void TestSimpleColor()
        {
            WaitSeconds(0.3451);
        }
        public void NoTested1() { }
        public void NoTested2() { }

        [EnduranceTest]
        public void BlendingNoDuration()
        {
            WaitSeconds(0.0497869);
        }
        [EnduranceTest]
        public void LoggerTest(System.IO.TextWriter logWriter)
        {
            TextWriterLogger log = new TextWriterLogger(logWriter);
            log.Info("SimpleColor, LoggerTest ok");
            WaitSeconds(0.0197869, log);
        }

        private void WaitSeconds(double secs, TextWriterLogger log = null)
        {
            Stopwatch sw = Stopwatch.StartNew();
            string ws = string.Empty;
            int i = 0;
            do
            {
                i++;
                ws += "ElapsedTicks" + sw.ElapsedTicks.ToString();
                if (log != null && i % 100 == 0)
                    log.Info($"Elapse {sw.Elapsed.TotalSeconds} seconds {i}");
            }
            while (sw.Elapsed.TotalSeconds < secs);
        }
    }
}
