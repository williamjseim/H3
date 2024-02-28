using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kryptering.Encrypters
{
    internal abstract class EncrypterBase
    {
        public abstract bool Encrypt(string text, out byte[] EncryptedBytes);
        public abstract Tuple<byte[], byte[]> GenerateKeyAndIv();
    }
}
