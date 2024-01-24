namespace Milkshake.Model
{
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

        //check  if the apikey has any calls left
        public bool CallsLeft()
        {
            return this.maxApiCalls > apiCalls;
        }
    }
}
