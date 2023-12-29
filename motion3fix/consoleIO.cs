using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using c = motion3fix.constants;
using t = motion3fix.constants.eText;

namespace motion3fix {
    public enum msgType { info, warning, abort, error, normal};
    internal class consoleIO {
        public static void sendMSG(msgType level, string msg = "", bool critical = false) {
            ConsoleColor cc = Console.ForegroundColor;
            switch(level) {
                case msgType.info:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(c.getText(t.info) + msg);
                    break;
                case msgType.warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(c.getText(t.warning) + msg);
                    break;
                case msgType.error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(c.getText(t.error) + msg);
                    if(critical) {
                        Console.WriteLine(c.getText(t.iErrorExit));
                        Console.ReadKey();
                        Environment.Exit(0);
                    }
                    break;
                case msgType.abort:
                    Console.WriteLine(c.getText(t.iAbortExit));
                    Console.ReadKey();
                    Environment.Exit(0);
                    break;
                case msgType.normal: 
                    Console.WriteLine(msg);
                    break;
                default: throw new ArgumentOutOfRangeException(nameof(level));
            }
            Console.ForegroundColor = cc;
        }

        public static int requestUserInput(string msg) {
            int result = -1;

            Console.WriteLine();
            Console.Write(msg+ "\n\n");

            do {
                Console.Write(c.getText(t.iAwaitUserInput));
                ConsoleKeyInfo key = Console.ReadKey();
                switch(key.Key) {
                    case ConsoleKey.N: result = 1; break;
                    case ConsoleKey.Y: result = 0; break;
                    default: Console.WriteLine(); break;
                }
            } while(result < 0);
            Console.WriteLine();
            return result;
        }

        public static int requestUserInputNumeric(string msg, params int[] numbers ) {
            int result = -1;

            Console.WriteLine();
            Console.Write(msg + "\n\n");

            do {
                Console.Write(c.getText(t.iAwaitUserInputNumeric));
                ConsoleKeyInfo key = Console.ReadKey();
                switch(key.Key) {
                    case ConsoleKey.D0: if(numbers.Contains(0)) result = 0; break;
                    case ConsoleKey.D1: if(numbers.Contains(1)) result = 1; break;
                    case ConsoleKey.D2: if(numbers.Contains(2)) result = 2; break;
                    case ConsoleKey.D3: if(numbers.Contains(3)) result = 3; break;
                    case ConsoleKey.D4: if(numbers.Contains(4)) result = 4; break;
                    case ConsoleKey.D5: if(numbers.Contains(5)) result = 5; break;
                    case ConsoleKey.D6: if(numbers.Contains(6)) result = 6; break;
                    case ConsoleKey.D7: if(numbers.Contains(7)) result = 7; break;
                    case ConsoleKey.D8: if(numbers.Contains(8)) result = 8; break;
                    case ConsoleKey.D9: if(numbers.Contains(9)) result = 9; break;
                    default: Console.WriteLine(); break;
                }
            } while(result < 0);
            Console.WriteLine();
            return result;
        }
    }
}
