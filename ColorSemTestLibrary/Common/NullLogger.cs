namespace ColorSemTestLibrary.Common
{
    public class NullLogger : ILogger
    {
        public NullLogger()
        {
        }
        public void Log(string message)
        {
        }

        public void Error(string message)
        {
        }

        public void Info(string message)
        {
        }

        public void Warning(string message)
        {
        }
    }
}
