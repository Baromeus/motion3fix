using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace motion3fix {
    public enum msgType { info, warning, abort, error };
    internal class consoleIO {
        public static void sendMSG(msgType level, string msg = "") {
            ConsoleColor cc = Console.ForegroundColor;
            switch(level) {
                case msgType.info:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("[INFO] " + msg);
                    break;
                case msgType.warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("[WARING] " + msg);
                    break;
                case msgType.error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("[ERROR] " + msg);
                    Console.WriteLine("The application will now shut down.\npress any key.");
                    Console.ReadKey();
                    Environment.Exit(0);
                    break;
                case msgType.abort:
                    Console.WriteLine("Operation canceled by user.\npress any key to close application.\n");
                    Console.ReadKey();
                    Environment.Exit(0);
                    break;
                default: throw new ArgumentOutOfRangeException(nameof(level));
            }
            Console.ForegroundColor = cc;
        }

        public static int requestUserInput(string msg) {
            int result = -1;

            Console.WriteLine("");
            Console.Write(msg+ "\n\n");

            do {
                Console.Write("user input (y/n): ");
                ConsoleKeyInfo key = Console.ReadKey();
                switch(key.Key) {
                    case ConsoleKey.N: result = 1; break;
                    case ConsoleKey.Y: result = 0; break;
                    default: Console.WriteLine(""); break;
                }
            } while(result < 0);
            Console.WriteLine("");
            return result;
        }
    }
}
