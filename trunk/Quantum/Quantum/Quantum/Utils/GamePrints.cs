using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quantum.Quantum.Utils
{
    class GamePrints
    {
        public static int gameIteration;

        public static void WriteLine(String message)
        {
            if (gameIteration % 100 != 0) return;

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
