using System;
using System.Security.Cryptography;
using System.Text;

namespace ATIS.Ui.Helper
{
    internal static class Crypt
    {
        public static string CalculateHash(string clearTextPassword, string salt)
        {
            // Convert the salted password to a byte array
            var saltedHashBytes = Encoding.UTF8.GetBytes(clearTextPassword + salt);
            // Use the hash algorithm to calculate the hash
            //   HashAlgorithm algorithm = new SHA256Managed();
            HashAlgorithm algorithm = new SHA512Managed();
            var hash = algorithm.ComputeHash(saltedHashBytes);
            // Return the hash as a base64 encoded string to be compared to the stored password
            return Convert.ToBase64String(hash);
        }

    }
}
