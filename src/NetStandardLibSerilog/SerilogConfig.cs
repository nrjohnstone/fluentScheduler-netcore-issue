using System;
using Serilog;

namespace NetStandardLibDapper
{
    public static class SerilogConfig
    {
        public static void Main()
        {
           Log.Logger = new LoggerConfiguration().CreateLogger();
        }
    }
}
