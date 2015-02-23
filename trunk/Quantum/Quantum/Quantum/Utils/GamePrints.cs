using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quantum.Quantum.Utils
{
    class GamePrints
    {
        public static bool enablePrint;

        public static void WriteLine(String message)
        {
            if (!enablePrint) return;

            Console.WriteLine(message);
        }

        public static void NextFrame()
        {
            WriteLine("");
        }

        public static void PrintPerformance(String message)
        {
            WriteLine(message);
        }
    }
}
