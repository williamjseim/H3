using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Kryptering.Encrypters
{
    class DesEncrypter : EncrypterBase
    {
        TripleDES des;
        public DesEncrypter()
        {
            this.des = TripleDES.Create();
        }

        public override bool Decrypt(string text, out byte[] EncryptedBytes)
        {
            throw new NotImplementedException();
        }

        public override bool Encrypt(string text, out byte[] encryptedBytes)
        {
            try
            {
                if (des.IV == null || des.Key == null)
                {
                    encryptedBytes = new byte[0];
                    return false;
                }
                byte[] plainText = Encoding.UTF8.GetBytes(text);
                byte[] ciphertext = new byte[plainText.Length];
                encryptedBytes = des.EncryptCfb(plainText, des.IV);
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
            this.des.GenerateKey();
            this.des.GenerateIV();
            return Tuple.Create(des.Key, des.IV);
        }
    }
}
