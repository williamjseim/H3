namespace Milkshake.Model
{
    public class GalacticRoute
    {
        public GalacticRoute(){ }

        public string name;
        public string start;
        public string end;
        public string[] navigationPoints;
        public string duration;
        public string[] dangers;
        public string fuelUsage;
        public string descriptions;
        public bool secret;

        public override string ToString()
        {
            return $"{name} {start}";
        }
    }
}
