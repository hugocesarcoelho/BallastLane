using System;
using System.Security.Cryptography;
using System.Text;

namespace BallastLane.ApplicationService.Service
{
    public class PasswordHasher
    {
        public static string HashPassword(string password, string salt)
        {
            string combined = string.Concat(password, salt);

            byte[] combinedBytes = Encoding.UTF8.GetBytes(combined);

            using (SHA512 sha512 = SHA512.Create())
            {
                byte[] hashBytes = sha512.ComputeHash(combinedBytes);

                StringBuilder hashStringBuilder = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    hashStringBuilder.Append(b.ToString("x2"));
                }

                return hashStringBuilder.ToString();
            }
        }
    }

}
