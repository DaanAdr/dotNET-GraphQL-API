using System.Security.Cryptography;
using System.Text;

namespace graphql_api.Helperclasses
{
    public static class PasswordHashLogic
    {
        private const int _saltSize = 16;
        private const int _hashSize = 32;
        private const int _iterations = 100000;
        private static readonly HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

        public static string HashPassword(string input)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(_saltSize);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(input, salt, _iterations, hashAlgorithm, _hashSize);

            string hashedPassword = $"{Convert.ToHexString(hash)}-{Convert.ToHexString(salt)}";
            return hashedPassword;
        }

        public static bool VerifyPassword(string plainTextPassword, string hashedPassword)
        {
            // Get hash and salt from hashed password
            string[] hashedPasswordParts = hashedPassword.Split("-");
            byte[] hash = Convert.FromHexString(hashedPasswordParts[0]);
            byte[] salt = Convert.FromHexString(hashedPasswordParts[1]);

            // Hash plaintext password
            byte[] inputHash = Rfc2898DeriveBytes.Pbkdf2(plainTextPassword, salt, _iterations, hashAlgorithm, _hashSize);

            // Compare hash values
            return CryptographicOperations.FixedTimeEquals(hash, inputHash);
        }
    }
}
