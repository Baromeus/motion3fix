using System;
using motion3fix.classes;
using CIO = motion3fix.consoleIO;
using c = motion3fix.constants;


namespace motion3fix{
    internal class Program{
        static void Main(string[] args){

            CIO.sendMSG(msgType.info, c.intro());

            CIO.sendMSG(msgType.info, "loading of .moc and .motion3.json files...");
            string moc = utils.getMoc();
            TMotion[] motions = utils.getMotions();

            string s = "Following motions where Found:\n";
            foreach(TMotion motion in motions) {
                s += motion.path + "\n";
            }

            s += "\nDo you want to try to fix those files?";
            if(CIO.requestUserInput(s) == 1) {
                CIO.sendMSG(msgType.abort);
            }

            CIO.sendMSG(msgType.info, "fixing models...");
            foreach(TMotion motion in motions) {
                utils.fixMotion(motion, moc);
                utils.saveMotion(motion);
            }

            if(CIO.requestUserInput("Do you alos want to apply those fixes to the model3.json?") == 0) {
                CIO.sendMSG(msgType.info, "add fices paths to models.json...");
                s = utils.getModelJson();
                foreach(TMotion motion in motions) {
                    s = utils.fixModelPath(s, motion.path);
                }
                utils.saveModelJson(s);
            }

            //CIO.sendMSG(msgType.info, "check if physics3.json is correct...");
            //if(!utils.getCorrectPhysics()) {
            //    if(CIO.requestUserInput("No fixed physics found, do you want to fix it now?") == 0) {
            //        CIO.sendMSG(msgType.info, "fixing physics");
            //    }
            //}

            CIO.sendMSG(msgType.info, "Fixing successfull, animations should now work correctly, press any key to close the application.");
            Console.ReadKey();
            return;
        }
    }
}