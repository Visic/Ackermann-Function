using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AckermannFunction
{
    public static class SW
    {
        static Dictionary<string, Stopwatch> _stopwatches = new Dictionary<string, Stopwatch>();

        public static void Restart(string name)
        {
            if (!_stopwatches.ContainsKey(name)) _stopwatches[name] = new Stopwatch();
            _stopwatches[name].Restart();
        }

        public static void LapAndPrint(string name, string msg = "", long threshholdInMS = 0)
        {
            var ms = _stopwatches[name].ElapsedMilliseconds;
            if (ms >= threshholdInMS) Debug.Print("{0} -- Elapsed {1}ms", msg, ms);
            _stopwatches[name].Restart();
        }

        public static void StopAndPrint(string name, string msg = "", long threshholdInMS = 0)
        {
            _stopwatches[name].Stop();
            var ms = _stopwatches[name].ElapsedMilliseconds;
            if(ms >= threshholdInMS) Console.WriteLine("{0} -- Elapsed {1}ms", msg, ms);//Debug.Print("{0} -- Elapsed {1}ms", msg, ms);
        }
    }
}
