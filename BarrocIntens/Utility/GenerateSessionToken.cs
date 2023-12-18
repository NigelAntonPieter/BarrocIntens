using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BarrocIntens.Utility
{
    public static class GenerateSessionToken
    {
        public static string SessionTokenGenerator(int length)
        {
            const string validChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] bytes = new byte[length];
                rng.GetBytes(bytes);
                StringBuilder sb = new StringBuilder(length);
                foreach (byte b in bytes)
                {
                    sb.Append(validChars[b % validChars.Length]);
                }
                return sb.ToString();
            }

        }
    }
}
