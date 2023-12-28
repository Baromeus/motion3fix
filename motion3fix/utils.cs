using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using motion3fix.classes;
using CIO = motion3fix.consoleIO;
using c = motion3fix.constants;

namespace motion3fix {
    internal class utils {
        public static string getMoc() {
            string[] files = Directory.GetFiles(c.dir(), "*.moc3");
            if(files.Count() == 0) 
                CIO.sendMSG(msgType.error, "No .moc3 file found, is this executable inside a modelfolder?.");
            try {
                return File.ReadAllText(files[0]);
            } catch(IOException e) {
                CIO.sendMSG(msgType.error, e.Message);
            }
            return ""; //Code should not reach this point
        }

        public static string getModelJson() {
            string[] files = Directory.GetFiles(c.dir(), "*.model3.json");
            if(files.Count() == 0) {
                CIO.sendMSG(msgType.warning, ".model3.json file not found, can't apply changes.");
                return "";
            } 
            try {
                return File.ReadAllText(files[0]);
            } catch(IOException e) {
                CIO.sendMSG(msgType.warning, e.Message);
            }
            return "";
        }

        public static TMotion[] getMotions() {
            if(!Directory.Exists(c.motionDir())) {
                CIO.sendMSG(msgType.error, "motions folder not found! make sure that the motionfolder is named 'motion'.");
            }

            string[] files = Directory.GetFiles(AppContext.BaseDirectory + "motions\\", "*.motion3.json");
            if(files.Count() == 0) {
                CIO.sendMSG(msgType.error, "no motion data found, make sure you have motion3.json files.");
            }

            TMotion[] motions = new TMotion[files.Length];
            int index = 0;

            foreach(string file in files) {
                if(!file.Contains(c.fixedName())) {
                    TMotion motion = JsonFileReader.Read<TMotion>(file);
                    motion.path = file.Replace(c.motionDir(), "");
                    motions[index++] = motion;
                }
            }

            if(index < files.Length) {
                TMotion[] temp = new TMotion[index];
                for(int i = 0; i < index; i++) 
                    temp[i] = motions[i];
                motions = temp;
            }

            return motions;
        }

        public static bool getCorrectPhysics() {
            string[] files = Directory.GetFiles(c.dir(), c.physicsPath());
            return files.Count() > 0;
        }

        public static string fixModelPath(string model, string path) {
            // TODO check if already fixed
            model = model.Replace(path, c.fixedName() + path);
            return model;
        }

        public static void fixMotion(TMotion motion, string moc) {
            foreach(TCurve curve in motion.Curves) {
                if(curve.Target == "Parameter") {
                    curve.pos = moc.IndexOf(curve.Id);
                } else {
                    curve.pos = int.MaxValue;
                }
            }

            TCurve[] SortedList = motion.Curves.OrderBy(o => o.pos).ToArray<TCurve>();
            motion.Curves = SortedList;
        }

        public static void saveMotion(TMotion motion) {
            string json = JsonFileReader.Write(motion);
            File.WriteAllText(c.motionDir() + c.fixedName() + motion.path, json);
        }

        public static void saveModelJson(string model) {
            string[] files = Directory.GetFiles(c.dir(), "*.model3.json");
            File.WriteAllText(files[0], model); //TODO that's dirty, have to make it clean later...
        }

        //public static void renamePhysics() {
        //    File.Move(c.dir() + "." + c.physicsPath(), c.dir() + c.physicsPath());
        //    File.Move(c.dir() + "*." + c.physicsPath(), c.dir() + c.physicsPath());
        //}
    }
}
