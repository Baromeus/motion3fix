using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace motion3fix.classes {
    internal static class JsonFileReader {
        public static T Read<T>(string filePath) {
            string text = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<T>(text);
        }

        public static string Write<T>(T obj) {
            return JsonSerializer.Serialize(obj);
        }
    }
}
