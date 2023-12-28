using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace motion3fix.classes {
    public class TMeta {
        public double Duration { get; set; }
        public double Fps { get; set; }
        public bool Loop { get; set; }
        public bool AreBeziersRestricted { get; set; }
        public int CurveCount { get; set; }
        public int TotalSegmentCount { get; set; }
        public int TotalPointCount { get; set; }
        public int UserDataCount { get; set; }
        public int TotalUserDataSize { get; set; }
    }

    public class TCurve {
        public string Target { get; set; }
        public string Id { get; set; }
        public double[] Segments { get; set; }
        [JsonIgnore]
        public int pos { get; set; }
    }

    public class TUserData {
    }

    public class TMotion {
        public int Version { get; set; }
        public TMeta Meta { get; set; }
        public TCurve[] Curves { get; set; }
        public TUserData[] UserData { get; set; }
        [JsonIgnore]
        public string path { get; set; }
    }
}
