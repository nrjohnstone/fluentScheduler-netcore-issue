using System;
using FluentScheduler;

namespace NetStandardLibFluentScheduler
{
    public static class FluentSchedulerConfig
    {
        public static void Main()
        {
            Console.WriteLine("Executing NetStandardLib");
            JobManager.Start();

        }
    }
}
