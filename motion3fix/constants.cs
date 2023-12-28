using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace motion3fix {
    internal class constants {
        const double version = 1.0;
        const string name = "Baro";
        internal static string dir() {
            return AppContext.BaseDirectory;
        }

        internal static string motionDir() {
            return dir() + "motions\\";
        }

        internal static string fixedName() {
            return "[FIXED]";
        }

        internal static string physicsPath() {
            return "physics3.json";
        }

        internal static string intro() {
            return "\tStart of application\n\tThis Programm is for fixing live2D models for web-applications like SillyTavern\n\tVersion:" + version + " by " + name;
        }
    }
}
