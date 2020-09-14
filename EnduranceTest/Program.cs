using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EnduranceTest
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length < 3)
            {
                ReportUsage();
                return;
            }
            CmdLine cmdLine = new CmdLine(args);
            if(string.IsNullOrEmpty(cmdLine.AssemblyPathName) || cmdLine.Minutes == 0)
            {
                ReportUsage();
                return;
            }

            if (!File.Exists(cmdLine.AssemblyPathName))
            {
                Console.WriteLine($"Assembly file not found: '{cmdLine.AssemblyPathName}'");
                Console.ReadLine();
                return;
            }
            TestedMethods testedMethods = new TestedMethods(cmdLine.AssemblyPathName);
            Console.WriteLine($"Methods to test:  {testedMethods.Methods.Count}");

            ITestRunner runner = null;
            switch (cmdLine.TestType)
            {
                case CmdLine.TestTypes.Random:
                    runner = new RandomTestRunner();
                    break;
                case CmdLine.TestTypes.All:
                    runner = new AllTestsRunner();
                    break;
                case CmdLine.TestTypes.DurationAdjusted:
                    runner = new DurationAdjustedTestRunner();
                    break;
                default:
                    Console.WriteLine("UNKNOWN test runner");
                    break;
            }

            if(runner != null)
                runner.Run(testedMethods, cmdLine.Minutes);
            Console.WriteLine("*** Test finished press any key...");
            Console.ReadLine();
        }

        private static void ReportUsage()
        {
            Console.WriteLine(
@"Usage : 
EnduranceTest.exe TestAssembly.dll -test all|random|durationadjusted -minutes 1
mandatory: 
    assembly.dll name or PathName
    -minutes greater than zero
default:
    -test all");
            Console.ReadLine();

        }
    }
    public class CmdLine
    {
        public enum TestTypes
        {
            All,
            Random,
            DurationAdjusted
        }
        private Dictionary<string, string> _cmdDict = new Dictionary<string, string>();
        private string _assemblyPathName;
        public string AssemblyPathName
        {
            get
            {
                return _assemblyPathName;
            }
            set
            {
                // ensure dll in current directory
                string asmPath = value;
                var dir = Path.GetDirectoryName(asmPath);
                if (string.IsNullOrEmpty(dir))
                {
                    asmPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), asmPath);
                }

                _assemblyPathName = asmPath;
            }
        }
        public CmdLine(string[] args)
        {
            Parse(args);
        }
        private void Parse(string[] args)
        {
            if (args.Length == 0)
                throw new ArgumentException("Command line arguments missing");
            AssemblyPathName = args[0];
            for (int i = 1; i < args.Length - 1; i++)
            {
                if (args[i].StartsWith("-"))
                    if (SetCommand(args[i], args[i + 1]))
                        i++;

            }
        }
        public int Minutes
        {
            get
            {
                var key = _cmdDict.Keys.FirstOrDefault(k => (k == "m" || k == "minutes"));
                if (!string.IsNullOrEmpty(key))
                    return Convert.ToInt32(_cmdDict[key]);
                return 0;
            }
        }
        public TestTypes TestType
        {
            get
            {
                var key = _cmdDict.Keys.FirstOrDefault(k => (k == "t" || k == "test"));
                if (!string.IsNullOrEmpty(key))
                {
                    switch (_cmdDict[key].ToLowerInvariant())
                    {
                        case "all":
                            {
                                return TestTypes.All;
                            }
                        case "random":
                            {
                                return TestTypes.Random;
                            }
                        case "durationadjusted":
                            {
                                return TestTypes.DurationAdjusted;
                            }
                    }
                }
                return TestTypes.All;
            }
        }

        private bool SetCommand(string k, string v)
        {
            var key = k.Remove(0, 1).ToLowerInvariant();
            if (!_cmdDict.ContainsKey(key))
                _cmdDict.Add(key, v);
            else
                _cmdDict[key] = v;

            return true;
        }
    }

}
