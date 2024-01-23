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
                    object test = typeof(Program).GetMethod(obj).Invoke(new Program(), null);
                    if(test is Tuple<int, string> ass)
                    {
                        response.StatusCode = ass.Item1;
                        responseString = $"<body style=\"background:black; color:white; display:flex; justify-content: center;\">{ass.Item2}</body>";
                    }
                }
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                response.ContentLength64 = buffer.Length;
                response.Cookies.Add(new Cookie("fuckyou", "suck dick"));
                output.Write(buffer, 0, buffer.Length);
                output.Close();
                listener.Stop();
            }
        }

        public Tuple<int, string> Index()
        {
            return Tuple.Create(404, "error");
        }

        
        public Tuple<int, string> Full()
        {
            string page = 
                @"<span>
                Fuck you
                </span>";
            return Tuple.Create(200, page);
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
            pages.Add("index", nameof(Program.Index));
            pages.Add("full", nameof(Program.Full));
        }

        public Dictionary<string, string> pages = new Dictionary<string, string>();
    }
}