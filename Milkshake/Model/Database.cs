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
        //loads the database
        private Database()
        {
            IEnumerable<GalacticRoute> routes;
            JsonSerializer serializer = new JsonSerializer();
            string routepath = "galacticRoutes.json";
            using (StreamReader sr = new StreamReader(routepath))
            using (JsonReader jsonReader = new JsonTextReader(sr))
            {
                //serializer.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;'
                routes = serializer.Deserialize<IEnumerable<GalacticRoute>>(jsonReader);
                Console.WriteLine(routes.First().ToString()) ;
                this.routes = routes.ToArray();
            }
        }

        //database of all api keys
        public Dictionary<string, ApiKey> Keys = new()
        {
            {"Kaptajn", new ApiKey(10, 0, true) },
            {"Rex", new ApiKey(5, 0) },
            {"99", new ApiKey(5, 0) }
        };

        //checks if apikey exists
        public bool IsApiKeyValid(string apiKey)
        {
            if (Keys.ContainsKey(apiKey))
            {
                return Keys[apiKey].CallsLeft();
            }
            return false;
        }

        //tick one up the api calls
        public bool ApiCall(string apikey)
        {
            if (Keys[apikey].maxApiCalls <= Keys[apikey].apiCalls)
                return false;

            Keys[apikey].apiCalls++;
            return true;
        }
        public GalacticRoute[] routes { get; private set; }
    }
}
