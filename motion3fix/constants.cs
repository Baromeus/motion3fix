using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace motion3fix {
    internal class constants {
        private static string ModelDir = ""; //not a constant, but for consistency usability of folder i but it in here.

        public static Dictionary<eText, string> text;
        public static Dictionary<eConst, string> constant;
        public enum language { english, german}; //any language that is added
        public enum eText { 
            iIntro, iLoadingMoc, iFoundMotions, iFixMotions, iFixModelPaths, iSuccesfullExit, iErrorExit, iAbortExit, iAwaitUserInput, iAwaitUserInputNumeric, 
            iLoadingMotions, iSuccessLoading, iCurrentFixMotion, iSavedAs, iChangeMotionPath, iPathChanged, iAvailibleModes, 
            qSelectMode, qFixFoundMotions, qApplyFixedPaths,
            eModelJsonNotFound,eModelMocNotFound, eMotionFolderNotFound, eMotionFilesNotFound, ePathAlreadyFixed, eUnknownMode,
            info, warning, error
        }
        public enum eConst {
            dirRoot, dirMotion, dirExpression, dirModel,
            tagFixed,
            filePhysics, fileExtMoc, fileExtModelJson, fileExtMotionJson,
            tParamater, tPartOpacity
        }
        const double version = 1.1;
        const string name = "Baro";

        internal static void initConstants() {
            constant = new Dictionary<eConst, string>();
            constant.Add(eConst.dirRoot, AppContext.BaseDirectory);
            constant.Add(eConst.dirMotion, "motions\\");
            constant.Add(eConst.dirExpression, "expressions\\");
            constant.Add(eConst.tagFixed, "[FIXED]");
            constant.Add(eConst.filePhysics, "physics3.json");
            constant.Add(eConst.fileExtMoc, ".moc3");
            constant.Add(eConst.fileExtModelJson, ".model3.json");
            constant.Add(eConst.fileExtMotionJson, ".motion3.json");
            constant.Add(eConst.tParamater, "Parameter");
            constant.Add(eConst.tPartOpacity, "PartOpacity");
        }

        internal static void setLanguage(language l) {
            switch(l) {
                default: 
                    setEnglish();  
                    break;
            }
        }

        internal static string getText(eText key) {
            string t;
            if(text.TryGetValue(key,out t))
                return t;
            return "";
        }

        internal static string getConst(eConst key) {
            string c;
            if(key == eConst.dirModel) {
                return ModelDir;
            }
            if(constant.TryGetValue(key, out c))
                return c;
            return "";
        }

        internal static void setModelDir(string dir) {
            ModelDir = dir;
        }

        private static void setEnglish() {
            text = new Dictionary<eText, string>();
            text.Add(eText.iIntro, "\tStart of application\n\tThis Programm is for fixing live2D models for web - applications like SillyTavern\n\tVersion:" + version + " by " + name);
            text.Add(eText.iLoadingMoc, "loading of .moc files...");
            text.Add(eText.iLoadingMotions, "loading of .motion3.json files...");
            text.Add(eText.iSuccessLoading, " successfully loaded.");
            text.Add(eText.iFoundMotions, "Following motions where Found:\n");
            text.Add(eText.iFixMotions, "fixing motions...");
            text.Add(eText.iCurrentFixMotion, "fixing motion: ");
            text.Add(eText.iSavedAs, " -> Saved as ");
            text.Add(eText.iFixModelPaths,  "Motion paths in model3.json sucessfully set and saved.");
            text.Add(eText.iSuccesfullExit, "Fixing successfull, animations should now work correctly, press any key to close the application.");
            text.Add(eText.iErrorExit, "The application will now shut down.\npress any key.");
            text.Add(eText.iAbortExit, "Operation canceled by user.\npress any key to close application.\n");
            text.Add(eText.iAwaitUserInput, "user input (y/n): ");
            text.Add(eText.iAwaitUserInputNumeric, "user input [number]: ");
            text.Add(eText.iChangeMotionPath, "changing the motion path for: ");
            text.Add(eText.iPathChanged, "path sucessfully changed.");
            text.Add(eText.iAvailibleModes, "follwing Modes are availible for this Programm:\n" +
                                           "[1] Normal - The executable is in the model folder - only this model will be altered.\n" +
                                           "[2] Bulk - The executable is in the root folder of all models, every model in this folder will be altered.\n");

            text.Add(eText.eModelJsonNotFound, ".model3.json file not found, can't apply changes.");
            text.Add(eText.eModelMocNotFound, "No .moc3 file found, is this executable inside a modelfolder?.");
            text.Add(eText.eMotionFolderNotFound, "Motions folder not found! make sure that the motionfolder is named 'motion'.");
            text.Add(eText.eMotionFilesNotFound, "No motion data found, make sure you have motion3.json files.");
            text.Add(eText.ePathAlreadyFixed, "The path for this file is already correctly set.");
            text.Add(eText.eUnknownMode, "A unknown mode was selected (This should not be possible!) get in contact with the Programmer and descripe what happened.\n Programm will Shut down.");

            text.Add(eText.qSelectMode, "Select the mode you want to operate in.");   
            text.Add(eText.qFixFoundMotions, "\nDo you want to try to fix those files?");
            text.Add(eText.qApplyFixedPaths, "Do you alos want to apply those fixes to the model3.json?");

            text.Add(eText.info, "[INFO]");
            text.Add(eText.warning, "[WARNING]");
            text.Add(eText.error, "[ERROR]");
        }
    }
}
