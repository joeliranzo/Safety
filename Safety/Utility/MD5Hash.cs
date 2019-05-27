using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;

namespace Safety.Models
{
    public class MD5Hash
    {
        public static string EncryptPassword(string password)
        {
            MD5 md5Hash = MD5.Create();
            string hash = GetMd5Hash(md5Hash, password);

            return hash;
        }

        public static bool VerifyPassword(string source, string hash)
        {
            MD5 md5Hash = MD5.Create();
            hash = GetMd5Hash(md5Hash, source);

            if (VerifyMd5Hash(md5Hash, source, hash))
            {
                return true;
            }
            return false;
        }

        public static bool IsValidMD5(string md5)
        {
            if (md5 == null || md5.Length != 32) return false;
            foreach (var x in md5)
            {
                if ((x < '0' || x > '9') && (x < 'a' || x > 'f') && (x < 'A' || x > 'F'))
                {
                    return false;
                }
            }
            return true;
        }

        private static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        // Verify a hash against a string.
        private static bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
        {
            // Hash the input.
            string hashOfInput = GetMd5Hash(md5Hash, input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}