
using System.Diagnostics;
using System.Security.Cryptography;

var text = AKEncrypter.Encrypt("Vi kom, Vi så, Vi sejrede.");
Console.WriteLine(text);
Console.WriteLine(AKEncrypter.DeCrypt(text));

/*static void Random(){
    Stopwatch stopwatch = new();
    stopwatch.Start();
    Random random = new();
    for (int i = 0; i < 100; i++)
    {
        var num = random.Next();
        //Console.WriteLine(num);
    }
    stopwatch.Stop();
    Console.WriteLine(stopwatch.ElapsedTicks+" random time");
}

static void RandomNumber(){
    Stopwatch stopwatch = new();
    stopwatch.Start();
    using(var random = RandomNumberGenerator.Create()){
        for (int i = 0; i < 100; i++)
        {
            var bytes = new byte[4];
            random.GetBytes(bytes);
            var num = BitConverter.ToUInt32(bytes,0);
            //Console.WriteLine(num);
        }
    }
    stopwatch.Stop();
    Console.WriteLine(stopwatch.ElapsedTicks+ " number time");
}//*/
