using Microsoft.AspNetCore.SignalR;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace HackGame.Api.Hubs
{
    public class Chathub : Hub
    {
        RSA rsa;
        RSA userRsa;
        public Chathub()
        {
            this.rsa = RSA.Create();
            userRsa = RSA.Create();
            rsa.KeySize = 2048;
            userRsa.KeySize = 2048;
        }
        public override async Task OnConnectedAsync()
        {
            await Console.Out.WriteLineAsync("connected");
            await Clients.Caller.SendAsync("RequestKey", Convert.ToBase64String(this.rsa.ExportRSAPublicKey()));
            return;
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            return base.OnDisconnectedAsync(exception);
        }

        public async Task RequestKey(string key)
        {
            Console.WriteLine(Convert.ToBase64String(userRsa.ExportRSAPublicKey()));
            this.userRsa.ImportRSAPublicKey(Convert.FromBase64String(key), out int byasdw);
            return;
        }

        public async Task Message(byte[] text)
        {
            Console.WriteLine(Encoding.UTF8.GetString(text));
            /*await Clients.Caller.SendAsync("ReceiveMessage", text);
            return;//*/
            byte[] decrypt = rsa.Decrypt(text, RSAEncryptionPadding.OaepSHA1);
            byte[] message = userRsa.Encrypt(decrypt, RSAEncryptionPadding.OaepSHA1);
            //await Console.Out.WriteLineAsync(Encoding.UTF8.GetString(decrypt));
            await Clients.Caller.SendAsync("ReceiveMessage", message);
            return;
        }
    }
}
