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
        Aes aes;
        public SignalrConnector()
        {
            aes = Aes.Create();
            string url = $"https://localhost:7247/Chathub";
            hubConnection = new HubConnectionBuilder().WithUrl(url).Build();
            hubConnection.StartAsync().Wait();
            hubConnection.On<byte[]>("Connected", Message);
            hubConnection.On<byte[]>("ReceiveIv", ReceiveIv);
        }

        public void Message(byte[] encryptedMessage)
        {
        }

        public async Task RequestKey()
        {
        }

        public async Task ReceiveIv(byte[] iv)
        {
            this.aes.IV = iv;
        }

        public async void SendMessage(object sender, EventArgs args)
        {
            var encryptedMessage = await hubConnection.InvokeAsync<byte[]>("Message", "text");
            var decrypt = aes.DecryptCbc(encryptedMessage);
            Debug.WriteLine(Encoding.UTF8.GetString(decrypt));
        }

        public async Task ReceiveMessage(byte[] encryptedMessage)
        {
            
        }

    }
}
