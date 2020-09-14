using System;
using System.Diagnostics;
using ColorSemTestLibrary.Common;

namespace ColorSemTestLibrary.Coloring
{
    [EnduranceTest("Quant coloring tests")]
    public class Quant
    {
        public string QuantVersion { get; set; }
        [EnduranceTest]
        public void TestQuantMapping()
        {
            WaitSeconds(0.854144);
        }
        private void WaitSeconds(double secs)
        {
            Stopwatch sw = Stopwatch.StartNew();
            string ws = string.Empty;
            do
            {
                ws += "ElapsedTicks" + sw.ElapsedTicks.ToString();
            }
            while (sw.Elapsed.TotalSeconds < secs);
        }

    }
}
