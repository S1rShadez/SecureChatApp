using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace SecureChatApp.Logic
{
    public static class Encryption
    {
        public static string Encrypt512(string phrase)
        {
            
            UTF8Encoding encoder = new UTF8Encoding();
            SHA512 sha512hasher = SHA512.Create();
            byte[] hashedDataBytes = sha512hasher.ComputeHash(encoder.GetBytes(phrase));
            string hashedPwd = byteArrayToString(hashedDataBytes);
            return hashedPwd;
        }

        private static string byteArrayToString(byte[] inputArray)
        {
            StringBuilder output = new StringBuilder("");
            for (int i = 0; i < inputArray.Length; i++)
            {
                output.Append(inputArray[i].ToString("X2"));
            }
            return output.ToString();
        }
    }
}
