using Kryptering.Encrypters;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Input;
using System.Windows.Media;

namespace Kryptering
{
    public class Manager(MainWindow main)
    {
        public MainWindow main = main;

        byte[] encrytedBytes = new byte[0];

        EncrypterBase[] encrypters;

        public virtual void Setup()
        {
            //main.CryptoBox.
            main.KeyGenerateButton.Click += GenerateKey;
            main.Encrypt.Click += Encrypt;
            encrypters = new EncrypterBase[]
            {
                new AesEncrypter(),
            };
        }

        protected virtual void GenerateKey(object sender, EventArgs e)
        {
            int i = main.CryptoBox.SelectedIndex;
            main.KeyGenerateButton.Background = new SolidColorBrush(Colors.LightGray);
            var keyAndIv = this.encrypters[i].GenerateKeyAndIv();
            this.main.IvTextBox.Text = Encoding.UTF8.GetString(keyAndIv.Item2);
            this.main.KeyTextBox.Text = Encoding.UTF8.GetString(keyAndIv.Item1);
            /*this.aes = System.Security.Cryptography.Aes.Create();
            aes.GenerateIV();
            aes.GenerateKey();
            this.main.IvTextBox.Text = Encoding.UTF8.GetString(aes.IV);
            this.main.KeyTextBox.Text = Encoding.UTF8.GetString(aes.Key);*/
        }

        protected virtual void Encrypt(object sender, EventArgs e)
        {
            try
            {
                int i = main.CryptoBox.SelectedIndex;
                Debug.WriteLine(i);
                /*if(aes == null)
                {
                    return;
                }*/
                string text = main.PlainTextASCII.Text;
                if(this.encrypters[i].Encrypt(text, out byte[] encrytedBytes))
                {
                    this.encrytedBytes = encrytedBytes;
                    UpdateText(encrytedBytes);
                }
                else
                {
                    main.KeyGenerateButton.Background = new SolidColorBrush(Colors.Red);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        protected void UpdateText(byte[] encryptedBytes)
        {
            var encryptedText = Encoding.ASCII.GetString(encrytedBytes);
            main.CipherASCII.Text = encryptedText;
            main.CipherHex.Text = Convert.ToHexString(encryptedBytes);
            Debug.WriteLine(encryptedText);
        }
    }
}
