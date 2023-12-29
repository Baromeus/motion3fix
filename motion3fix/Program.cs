using System;
using motion3fix.classes;
using CIO = motion3fix.consoleIO;
using c = motion3fix.constants;
using t = motion3fix.constants.eText;
using cc = motion3fix.constants.eConst;


namespace motion3fix{
    internal class Program{
        static void Main(string[] args){
            c.initConstants();
            c.setLanguage(c.language.english); //TODO language selector (in cmd or as arg?)
            CIO.sendMSG(msgType.info, c.getText(t.iIntro));
            CIO.sendMSG(msgType.normal, c.getText(t.iAvailibleModes));

            int Mode = CIO.requestUserInputNumeric(c.getText(t.qSelectMode), 1, 2);
            switch(Mode) {
                case 1:
                    c.setModelDir(c.getConst(cc.dirRoot));
                    break;
                case 2: break;
                default: 
                    CIO.sendMSG(msgType.error, c.getText(t.eUnknownMode), true);
                    break;
            }

            if(Mode == 1) {
                runModelFix();
            } else if(Mode == 2) {
                string[] dirs = utils.searchRootForModels();

                foreach(string dir in dirs) {
                    c.setModelDir(dir);
                    runModelFix();
                }
            }

            //CIO.sendMSG(msgType.info, "check if physics3.json is correct...");
            //if(!utils.getCorrectPhysics()) {
            //    if(CIO.requestUserInput("No fixed physics found, do you want to fix it now?") == 0) {
            //        CIO.sendMSG(msgType.info, "fixing physics");
            //    }
            //}

            CIO.sendMSG(msgType.info, c.getText(t.iSuccesfullExit));
            Console.ReadKey();
            return;
        }

        private static void runModelFix() {
            CIO.sendMSG(msgType.info, c.getText(t.iLoadingMoc));
            string moc = utils.getMoc();

            CIO.sendMSG(msgType.info, c.getText(t.iLoadingMotions));
            TMotion[] motions = utils.getMotions();

            string s = c.getText(t.iFoundMotions);
            foreach(TMotion motion in motions) {
                s += motion.path + "\n";
            }

            s += c.getText(t.qFixFoundMotions);
            if(CIO.requestUserInput(s) == 1) {
                CIO.sendMSG(msgType.abort);
            }

            CIO.sendMSG(msgType.info, c.getText(t.iFixMotions));
            foreach(TMotion motion in motions) {
                utils.fixMotion(motion, moc);
                utils.saveMotion(motion);
            }

            if(CIO.requestUserInput(c.getText(t.qApplyFixedPaths)) == 0) {
                s = utils.getModelJson();
                foreach(TMotion motion in motions) {
                    s = utils.fixModelPath(s, motion.path);
                }
                utils.saveModelJson(s);
                CIO.sendMSG(msgType.info, c.getText(t.iFixModelPaths));
            }
        }
    }
}