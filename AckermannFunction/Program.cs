using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AckermannFunction
{
    class Program
    {
        static void Main(string[] args)
        {
            while(true)
            {
                var thread = new Thread(() => RunAck(args), 1073741824);
                thread.Start();
                thread.Join();
                Console.ReadKey();
            }
        }

        static void RunAck(string[] args)
        {
            SW.Restart("");
            var result = Ack(long.Parse(args[0]), long.Parse(args[1]));
            SW.StopAndPrint("", $"Ack({args[0]}, {args[1]}): {result}");
        }

        static long Ack(long n, long m)
        {
            if(n == 0) return m + 1;
            if(m == 0) return Ack(n - 1, 1);
            return Ack(n - 1, Ack(n, m - 1));
        }
    }
}
