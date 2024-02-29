using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Kryptering.Encrypters
{
    internal class RsaEncrypter : EncrypterBase
    {
        RSA rsa;
        public RsaEncrypter()
        {
            rsa = RSA.Create();
            rsa.KeySize = 2048;
        }
        public override bool Decrypt(string text, out byte[] EncryptedBytes)
        {
            throw new NotImplementedException();
        }

        public override bool Encrypt(string text, out byte[] EncryptedBytes)
        {
            EncryptedBytes = rsa.Encrypt(Encoding.UTF8.GetBytes(text), RSAEncryptionPadding.OaepSHA256);
            return true;
        }

        public override Tuple<byte[], byte[]> GenerateKeyAndIv()
        {
            throw new NotImplementedException();
        }
    }
}
