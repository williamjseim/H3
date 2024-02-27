using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;

namespace Hashing;

class Program
{
    static void Main(string[] args)
    {
        System.Console.WriteLine("write text to hash");
        var plainText = "Test";
        plainText = Console.ReadLine();
        var sha = SHA256.Create();
        var plainBytes = Encoding.UTF8.GetBytes(plainText!);
        var hmac = new HMACSHA256(Encoding.UTF8.GetBytes("Key"));
        var hashedBytes = sha.ComputeHash(Encoding.UTF8.GetBytes(plainText!));
        var hmacBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(plainText!));
        string hashedText = ToString(hashedBytes);
        string hmacText = ToString(hmacBytes);
        var asciiText = Encoding.ASCII.GetBytes(plainText!);
        Console.ForegroundColor = ConsoleColor.Yellow;
        System.Console.WriteLine("Sha256 algorithm");
        Console.WriteLine(hashedText);
        Console.ForegroundColor = ConsoleColor.Red;
        System.Console.WriteLine("AScii hash");
        System.Console.WriteLine(ToString(asciiText));
        Console.ForegroundColor = ConsoleColor.Blue;
        System.Console.WriteLine("Plain text and hashed text to hex");
        System.Console.WriteLine(Convert.ToHexString(plainBytes));
        System.Console.WriteLine(Convert.ToHexString(hashedBytes));
        Console.ForegroundColor = ConsoleColor.Green;
        System.Console.WriteLine("Hmac algorithm");
        Console.WriteLine(hmacText);
    }

    public static string ToString(byte[] bytes){
        string text = "";
        foreach (var item in bytes)
        {
            text+=item;
        }
        return text;
    }

    public static void Write(string text, ConsoleColor consoleColor = ConsoleColor.White){
        Console.ForegroundColor = consoleColor;
        System.Console.WriteLine(text);
        Console.ResetColor();
    }
}
