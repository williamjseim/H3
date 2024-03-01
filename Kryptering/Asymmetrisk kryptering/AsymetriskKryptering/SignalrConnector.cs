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
        RSA rsa = RSA.Create();
        RSA serverRsa = RSA.Create();
        public SignalrConnector()
        {
            rsa.KeySize = 2048;
            serverRsa.KeySize = 2048;
            string url = $"https://localhost:7247/Chathub";
            hubConnection = new HubConnectionBuilder().WithUrl(url).Build();
            hubConnection.StartAsync().Wait();
            hubConnection.On<byte[]>("Connected", Message);
        }

        public void Message(byte[] encryptedMessage)
        {
        }

        public async Task RequestKey()
        {
            var key = await hubConnection.InvokeAsync<byte[]>("RequestKey", rsa.ExportRSAPublicKey());
            this.serverRsa.ImportRSAPublicKey(key, out int bytesread);
        }

        public async void SendMessage(object sender, EventArgs args)
        {
            var message = serverRsa.Encrypt(Encoding.UTF8.GetBytes("Text"), RSAEncryptionPadding.OaepSHA256);
            var encryptedMessage = await hubConnection.InvokeAsync<byte[]>("Message", message);
        }

        public async Task ReceiveMessage(byte[] encryptedMessage)
        {
            
        }

    }
}
