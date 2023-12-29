using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using motion3fix.classes;
using CIO = motion3fix.consoleIO;
using c = motion3fix.constants;
using t = motion3fix.constants.eText;
using cc = motion3fix.constants.eConst;

namespace motion3fix {
    internal class utils {
        private static string getModelDir() {
            string dir = c.getConst(cc.dirRoot);
            if(dir != c.getConst(cc.dirModel))
                dir += c.getConst(cc.dirModel);
            return dir;
        }
        public static string getMoc() {
            string dir = getModelDir();
            string[] files = Directory.GetFiles(dir, "*" + c.getConst(cc.fileExtMoc)); //TODO change with variable modelDir
            if(files.Count() == 0) 
                CIO.sendMSG(msgType.error, c.getText(t.eModelMocNotFound), true);
            try {
                string text =  File.ReadAllText(files[0]);
                CIO.sendMSG(msgType.info, files[0] + c.getText(t.iSuccessLoading));
                return text;
            } catch(IOException e) {
                CIO.sendMSG(msgType.error, e.Message, true);
            }
            return ""; //Code should not reach this point
        }

        public static string getModelJson() {
            string dir = getModelDir();

            string[] files = Directory.GetFiles(dir, "*" + c.getConst(cc.fileExtModelJson));
            if(files.Count() == 0) {
                CIO.sendMSG(msgType.warning, c.getText(t.eModelJsonNotFound));
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
            string dir = getModelDir() + c.getConst(cc.dirMotion);
            if(!Directory.Exists(dir)) {
                CIO.sendMSG(msgType.error, c.getText(t.eMotionFolderNotFound));
            }

            string[] files = Directory.GetFiles(dir, "*" + c.getConst(cc.fileExtMotionJson));
            if(files.Count() == 0) {
                CIO.sendMSG(msgType.warning, c.getText(t.eMotionFilesNotFound));
            }

            TMotion[] motions = new TMotion[files.Length];
            int index = 0;

            foreach(string file in files) {
                if(!file.Contains(c.getConst(cc.tagFixed))) {
                    TMotion motion = JsonFileReader.Read<TMotion>(file);
                    motion.path = file.Replace(dir, "");
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

        //public static bool getCorrectPhysics() {
        //  string[] files = Directory.GetFiles(c.dir(), c.physicsPath());
        //  return files.Count() > 0;
        //}

        public static string fixModelPath(string model, string path) {
            CIO.sendMSG(msgType.info, c.getText(t.iChangeMotionPath) + path);
            if(model.Contains(c.getConst(cc.tagFixed) + path)) {
                CIO.sendMSG(msgType.warning, c.getText(t.ePathAlreadyFixed));
                return model;
            }
            model = model.Replace(path, c.getConst(cc.tagFixed) + path);
            CIO.sendMSG(msgType.info, c.getText(t.iPathChanged));
            return model;
        }

        public static void fixMotion(TMotion motion, string moc) {
            CIO.sendMSG(msgType.info, c.getText(t.iCurrentFixMotion) + motion.path);
            foreach(TCurve curve in motion.Curves) {
                if(curve.Target == c.getConst(cc.tParamater)) {
                    CIO.sendMSG(msgType.info, " -> " + curve.Id);
                    curve.pos = moc.IndexOf(curve.Id);
                } else {
                    curve.pos = int.MaxValue;
                }
            }

            TCurve[] SortedList = motion.Curves.OrderBy(o => o.pos).ToArray<TCurve>();
            motion.Curves = SortedList;
        }

        public static void saveMotion(TMotion motion) {
            string dir = getModelDir() + c.getConst(cc.dirMotion);
            string filename = c.getConst(cc.tagFixed) + motion.path;
            string json = JsonFileReader.Write(motion);
            File.WriteAllText(dir + filename, json);
            CIO.sendMSG(msgType.info, c.getText(t.iSavedAs) + dir + filename);
        }

        public static void saveModelJson(string model) {
            string dir = getModelDir();
            string[] files = Directory.GetFiles(dir, "*" + c.getConst(cc.fileExtModelJson));
            try {
                File.Copy(files[0], files[0]+".backup");
            } catch (IOException e) {
                CIO.sendMSG(msgType.error, e.Message);
                return;
            }

            File.WriteAllText(files[0], model); //TODO that's dirty, have to make it clean later...
        }

        public static string[] searchRootForModels() {
            string[] fulldirs = Directory.GetDirectories(c.getConst(cc.dirRoot));
            string[] dirs = new string[fulldirs.Length];

            for(int i = 0; i < fulldirs.Length; i++) {
                dirs[i] = new DirectoryInfo(fulldirs[i]).Name + "\\";
            }

            return dirs;
        }

        //public static void renamePhysics() {
        //    File.Move(c.dir() + "." + c.physicsPath(), c.dir() + c.physicsPath());
        //    File.Move(c.dir() + "*." + c.physicsPath(), c.dir() + c.physicsPath());
        //}
    }
}
