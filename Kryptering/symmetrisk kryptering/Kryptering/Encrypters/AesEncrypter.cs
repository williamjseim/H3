using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Kryptering.Encrypters
{
    internal class AesEncrypter : EncrypterBase
    {
        Aes aes;
        public AesEncrypter()
        {
            aes = Aes.Create();
        }

        public override bool Encrypt(string text, out byte[] encryptedBytes)
        {
            try
            {
                Debug.WriteLine(aes.IV.Length+" "+aes.Key.Length);
                if(aes.IV == null || aes.Key == null)
                {
                    encryptedBytes = new byte[0];
                    return false;
                }
                byte[] plainText = Encoding.UTF8.GetBytes(text);
                byte[] ciphertext = new byte[plainText.Length];
                encryptedBytes = aes.EncryptCfb(plainText, aes.IV);
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                encryptedBytes = new byte[0];
                return false;
            }
        }

        public override Tuple<byte[], byte[]> GenerateKeyAndIv()
        {
            aes.GenerateKey();
            aes.GenerateIV();
            return Tuple.Create(aes.Key, aes.IV);
        }
    }
}
