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
                if (MapHub.Instance.pages[context.Request.Url.Segments[1]] != null)
                {
                    object test = typeof(Program).GetMethod(MapHub.Instance.pages[request.Url.Segments[2]]).Invoke(new Program(), null);
                    if(test is Tuple<int, string> ass)
                    {
                        response.StatusCode = ass.Item1;
                        responseString = ass.Item2;
                    }
                }
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                response.ContentLength64 = buffer.Length;
                output.Write(buffer, 0, buffer.Length);
                output.Close();
                listener.Stop();
            }
        }

        [Mapping("index", HttpType.Get)]
        public Tuple<int, string> Index()
        {

            return Tuple.Create(404, "error");

        }

        
        public void Full()
        {

        }
    }

    public enum HttpType
    {
        Get,
        Post
    }

    public class Mapping : Attribute
    {
        public Mapping(string page, HttpType type, [CallerMemberName] string owner = "") 
        {
            MapHub.Instance.pages.Add(page, owner);
        }
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
            
        }

        public Dictionary<string, string> pages = new Dictionary<string, string>();
    }
}