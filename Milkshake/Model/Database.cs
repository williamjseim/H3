using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace Milkshake.Model
{
    public class Database
    {
        private static Database instance = null;
        public static Database Instance { get { if (instance == null) instance = new(); return instance; } }
        private Database()
        {
            IEnumerable<GalacticRoute> routes;
            JsonSerializer serializer = new JsonSerializer();
            string routepath = "C:\\Users\\zbcwise\\Documents\\GitHub\\H3\\Milkshake\\galacticRoutes.json";
            using (StreamReader sr = new StreamReader(routepath))
            using (JsonReader jsonReader = new JsonTextReader(sr))
            {
                //serializer.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;'
                routes = serializer.Deserialize<IEnumerable<GalacticRoute>>(jsonReader);
                Console.WriteLine(routes.First().ToString()) ;
                this.routes = routes.ToArray();
            }
        }

        public Dictionary<string, ApiKey> Keys = new()
        {
            {"Kaptajn", new ApiKey(10, 0, true) },
            {"Rex", new ApiKey(5, 0) },
            {"99", new ApiKey(5, 0) }
        };

        public bool ApiCall(string apikey)
        {
            if (Keys[apikey].maxApiCalls <= Keys[apikey].apiCalls)
                return false;

            Keys[apikey].apiCalls++;
            return true;
        }
        public GalacticRoute[] routes { get; private set; }
    }

    public class ApiKey
    {
        public int maxApiCalls;
        public int apiCalls;
        public bool isKaptain = false;
        public ApiKey(int maxApi, int apicalls, bool isKaptain = false)
        {
            this.maxApiCalls = maxApi;
            this.apiCalls = apicalls;
            this.isKaptain = isKaptain;

        }
    }

    public class GalacticRoute
    {
        public GalacticRoute()
        {
            
        }

        public string name;
        public string start;
        public string end;
        public string[] navigationPoints;
        public string duration;
        public string[] dangers;
        public string fuelUsage;
        public string descriptions;

        public override string ToString()
        {
            return $"{name} {start}";
        }
    }
}
