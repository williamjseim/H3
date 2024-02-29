using Microsoft.AspNetCore.SignalR;
using System.Security.Cryptography;
using System.Text;

namespace HackGame.Api.Hubs
{
    public class Chathub : Hub
    {
        RSA rsa = RSA.Create();
        RSA userRsa = RSA.Create();
        public Chathub()
        {
            rsa.KeySize = 2048;
            userRsa.KeySize = 2048;
        }
        public override async Task OnConnectedAsync()
        {
            byte[] userData = await Clients.Caller.InvokeAsync("RequestKey", rsa.ExportRSAPublicKey());
            return;
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            return base.OnDisconnectedAsync(exception);
        }

        public async Task<byte[]> ExchangeRsaKeys(byte[] key)
        {
            return this.rsa.ExportRSAPublicKey();
        }

        public async Task<byte[]> Message(string text, byte[] key)
        {
            userRsa.ImportRSAPublicKey(key, out int bytes);
            var message = userRsa.Encrypt(Encoding.UTF8.GetBytes(text), RSAEncryptionPadding.OaepSHA256);
            await Console.Out.WriteLineAsync(text);
            await Console.Out.WriteLineAsync(Encoding.UTF8.GetString(message));
            return message;
        }
    }
}
