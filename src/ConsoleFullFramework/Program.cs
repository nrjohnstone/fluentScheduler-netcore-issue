using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetStandardLibDapper;
using NetStandardLibFluentScheduler;

namespace ConsoleFullFramework
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                SerilogConfig.Main();
                FluentSchedulerConfig.Main();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
