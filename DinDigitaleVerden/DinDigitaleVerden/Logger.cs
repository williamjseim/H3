using System.Text;

namespace DinDigitaleVerden
{
    public class Logger
    {
        public static object syncObj = new();
        private StringBuilder logger = new StringBuilder();
        public async Task Log(string log)
        {
            lock (syncObj)
            {
                logger.AppendLine(log + DateTime.Now);
            }
        }
    }
}
