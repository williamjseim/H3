using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;

namespace Hashing;

class Program
{
    static void Main(string[] args)
    {
        MenuManager program = new();
        program.Setup();
    }
}
