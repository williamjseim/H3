using System.Net;
using System.Runtime.CompilerServices;

namespace Httpconsol
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                HttpListener listener = new HttpListener();
                listener.Prefixes.Add("http://Localhost:8080/");
                listener.Start();
                string responseString = "<body style=\"background:black; color:white; display:flex; justify-content: center;\"><span>The Bird of Hermes is my name<br>Eat my wings to keep me tame</span></body>";
                HttpListenerContext context = listener.GetContext();
                HttpListenerRequest request = context.Request;
                HttpListenerResponse response = context.Response;
                Stream output = response.OutputStream;
                if (MapHub.Instance.pages.TryGetValue(context.Request.Url.Segments[1].ToLower(), out string? obj))
                {
                    object test = typeof(Program).GetMethod(obj).Invoke(new Program(), new object?[]{request.Url.Query});
                    if(test is Tuple<int, string> ass)
                    {
                        byte[] buffer = System.Text.Encoding.UTF8.GetBytes($"<body style=\"background:black; color:white; display:flex; justify-content: center;\">{ass.Item2}</body>");
                        response.StatusCode = ass.Item1;
                        response.ContentLength64 = buffer.Length;
                        response.Cookies.Append(new Cookie("fuckyou", "suck dick"));
                        output.Write(buffer, 0, buffer.Length);
                        output.Close();
                    }
                }
                else{
                    byte[] buffer = System.Text.Encoding.UTF8.GetBytes($"<body style=\"background:black; color:white; display:flex; justify-content: center;\">Error</body>");
                        response.ContentLength64 = buffer.Length;
                        response.Cookies.Append(new Cookie("fuckyou", "suck dick"));
                        output.Write(buffer, 0, buffer.Length);
                        output.Close();
                }
                listener.Stop();
            }
        }

        /*public Tuple<int, HttpListenerResponse> Index(Object?[]? ar)
        {
            return Tuple.Create(404, "error");
        }*/

        public Tuple<int, string> Full(string quary)
        {
            return Tuple.Create(200, "response");
        }
        Dictionary<int, string> milkshakes = new()
        {
            {1, "Chocolet"},
            {2, "vanilla"},
            {34, "strawbarry"},
        };
        public Tuple<int, string> MilkShake(string quary)
        {
            string li = "";
            foreach (var item in milkshakes)
            {
                li += $"<li>{item.Key} {item.Value}</li>";
            }
            string page = $"<ol>{li}</ol>";
            return Tuple.Create(200, li);
        }
    }

    public enum HttpType
    {
        Get,
        Post
    }

    public class MapHub
    {
        private static MapHub? instance = null;
        public static MapHub Instance { get 
            { 
                if (instance == null) 
                    instance = new();

                return instance; 
            } 
        }

        public MapHub()
        {
            pages.Add("full", nameof(Program.Full));
            pages.Add("milkshakes", nameof(Program.MilkShake));
        }

        public Dictionary<string, string> pages = new Dictionary<string, string>();
    }
}