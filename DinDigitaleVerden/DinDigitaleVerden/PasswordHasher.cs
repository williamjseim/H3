using System.Security.Cryptography;
using System.Text;

namespace DinDigitaleVerden
{
    public static class PasswordHasher
    {
        public static string Hash(string password)
        {
            HashAlgorithm sha = SHA256.Create();
            byte[] hashedPassword = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < hashedPassword.Length; i++)
            {
                builder.Append(hashedPassword[i]);
            }
            return builder.ToString();
        }
    }
}
