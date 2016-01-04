using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiChainLib.Harness
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var task = Task.Run(async () =>
                {
                    await new Complete().RunAsync();
//                    await new Example().RunAsync();
                });
                task.Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine("**********************");
                Console.WriteLine(ex);
            }
            finally
            {
                if (Debugger.IsAttached)
                    Console.ReadLine();
            }
        }
    }
}
