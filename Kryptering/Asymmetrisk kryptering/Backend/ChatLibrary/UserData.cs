using System.Security.Cryptography;

namespace ChatLibrary
{
    [Serializable]
    public class UserData
    {
        public string connectionId;
        public string name;
        public byte[] aesKey;
        public byte[] iv;
    }
}
