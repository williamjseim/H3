using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AsymetriskKryptering
{
    class SignalrConnector
    {
        public static HubConnection hubConnection;
        RSA userRsa = RSA.Create();
        RSA serverRsa = RSA.Create();
        public SignalrConnector()
        {
            userRsa.KeySize = 2048;
            serverRsa.KeySize = 2048;
            string url = $"https://localhost:7247/Chathub";
            hubConnection = new HubConnectionBuilder().WithUrl(url).Build();
            hubConnection.StartAsync().Wait();
            hubConnection.On<string>("RequestKey", RequestKey);
            hubConnection.On<byte[]>("ReceiveMessage", ReceiveMessage);
        }

        public void Reconnect(object sender, EventArgs e)
        {
            hubConnection.StopAsync().Wait();
            hubConnection.StartAsync().Wait();
        }

        public async Task RequestKey(string key)
        {
            this.serverRsa.ImportRSAPublicKey(Convert.FromBase64String(key), out int bytesread);
            await hubConnection.SendAsync("RequestKey", Convert.ToBase64String(userRsa.ExportRSAPublicKey()));
            Debug.WriteLine(Convert.ToBase64String(userRsa.ExportRSAPublicKey()));
            Debug.WriteLine("Key send");
        }

        public async void SendMessage(object sender, EventArgs args)
        {
            byte[] message = serverRsa.Encrypt(Encoding.UTF8.GetBytes("Text"), RSAEncryptionPadding.OaepSHA1);
            await hubConnection.SendAsync("Message", message);
        }

        public async Task ReceiveMessage(byte[] encryptedMessage)
        {
            var decrypt = this.userRsa.Decrypt(encryptedMessage, RSAEncryptionPadding.OaepSHA1);
            
            
            Debug.WriteLine(Encoding.UTF8.GetString(decrypt));
        }

    }
}
