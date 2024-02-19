using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Management;
using System.Diagnostics;

namespace SecureChatApp.Logic
{
    public static class Encryption
    {
        /// <summary>
        /// Hashes a string using SHA512
        /// </summary>
        /// <returns>string</returns>
        public static string Encrypt512(string phrase)
        {

            UTF8Encoding encoder = new UTF8Encoding();
            SHA512 sha512hasher = SHA512.Create();
            byte[] hashedDataBytes = sha512hasher.ComputeHash(encoder.GetBytes(phrase));
            string hashedPwd = byteArrayToString(hashedDataBytes);
            return hashedPwd;
        }

        /// <summary>
        /// Creates a byte array after hashing a string using SHA256 used as an aes key by 
        /// </summary>
        /// <returns>byte[]</returns>
        public static byte[] CreateAESKey256(string phrase)
        {

            UTF8Encoding encoder = new UTF8Encoding();
            SHA256 sha256hasher = SHA256.Create();
            byte[] hashedDataBytes = sha256hasher.ComputeHash(encoder.GetBytes(phrase));
            return hashedDataBytes;
        }

        /// <summary>
        /// Turns a byte array into a usable string
        /// </summary>
        /// <returns>string</returns>
        private static string byteArrayToString(byte[] inputArray)
        {
            StringBuilder output = new StringBuilder("");
            for (int i = 0; i < inputArray.Length; i++)
            {
                output.Append(inputArray[i].ToString("X2"));
            }
            return output.ToString();
        }

        static string cpuID = GetCpuId();
        static string mbID = GetMotherboardInfo();
        static string cmID = GetCacheMemoryInfo();

        //TODO: Upgrade the encryption to include salt and perhaps more

        /// <summary>
        /// Encrypts a text input using the password input
        /// </summary>
        /// <returns></returns>
        public static void EncryptFile(string input, string password)
        {

            string hwFingerprint = $"{cpuID}-{mbID}-{cmID}-{Encrypt512(password)}";

            byte[] hashedHID = CreateAESKey256(hwFingerprint);

            using (Aes aes = Aes.Create())
            {
                aes.Key = hashedHID;
                aes.GenerateIV();
                aes.Padding = PaddingMode.PKCS7;

                // Get the initialization vector (IV) and write it to the file.
                byte[] iv = aes.IV;
                using (FileStream fileStream = new FileStream("ImportantData.txt", FileMode.Create))
                {
                    fileStream.Write(iv, 0, iv.Length);
                }

                // Encrypt the data.
                ICryptoTransform encryptor = aes.CreateEncryptor();
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter writer = new StreamWriter(cryptoStream))
                        {
                            string padding = "1234567890123456"; //This was necessary so that no data was lost
                            writer.Write(padding + input);
                        }
                    }
                    byte[] encryptedData = memoryStream.ToArray();

                    // Save the encrypted data to a file.
                    File.WriteAllBytes("ImportantData.txt", encryptedData);
                }
            }
        }

        /// <summary>
        /// Decrypts a file using the password input
        /// </summary>
        /// <returns></returns>
        public static string DecryptFile(string password)
        {
            string hwFingerprint = $"{cpuID}-{mbID}-{cmID}-{Encrypt512(password)}";

            byte[] hashedHID = CreateAESKey256(hwFingerprint);

            byte[] iv = new byte[16]; // IV size for AES is 16 bytes
            byte[] encryptedData;

            // Read the initialization vector (IV) and the encrypted data from the file
            using (FileStream fileStream = new FileStream("ImportantData.txt", FileMode.Open))
            {
                fileStream.Read(iv, 0, iv.Length);

                encryptedData = new byte[fileStream.Length - iv.Length];
                fileStream.Read(encryptedData, 0, encryptedData.Length);
            }

            try
            {
                using (Aes aes = Aes.Create())
                {
                    aes.Key = hashedHID;
                    aes.IV = iv;
                    aes.Padding = PaddingMode.PKCS7; // Set the same padding mode used during encryption

                    ICryptoTransform decryptor = aes.CreateDecryptor();

                    using (MemoryStream decryptedStream = new MemoryStream())
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(decryptedStream, decryptor, CryptoStreamMode.Write))
                        {
                            cryptoStream.Write(encryptedData, 0, encryptedData.Length);
                        }

                        byte[] decryptedBytes = decryptedStream.ToArray();

                        string decryptedText = Encoding.UTF8.GetString(decryptedBytes);
                        //File.WriteAllText("ImportantData.txt", decryptedText);
                        return decryptedText;
                    }
                }
            }
            catch (CryptographicException ex)
            {
                Debug.WriteLine($"Decryption error: {ex.Message}");
            }

            return "Unable to decrypt data, did you enter the correct username/password?";
        }

        /// <summary>
        /// Decrypts a file using the password input to verify if the user successfully signed in
        /// </summary>
        /// <returns>bool</returns>
        public static bool VerifyAccount(string password)
        {
            string hwFingerprint = $"{cpuID}-{mbID}-{cmID}-{Encrypt512(password)}";

            byte[] hashedHID = CreateAESKey256(hwFingerprint);

            byte[] iv = new byte[16]; // IV size for AES is 16 bytes
            byte[] encryptedData;

            // Read the initialization vector (IV) and the encrypted data from the file
            using (FileStream fileStream = new FileStream("ImportantData.txt", FileMode.Open))
            {
                fileStream.Read(iv, 0, iv.Length);

                encryptedData = new byte[fileStream.Length - iv.Length];
                fileStream.Read(encryptedData, 0, encryptedData.Length);
            }

            try
            {
                using (Aes aes = Aes.Create())
                {
                    aes.Key = hashedHID;
                    aes.IV = iv;
                    aes.Padding = PaddingMode.PKCS7; // Set the same padding mode used during encryption

                    ICryptoTransform decryptor = aes.CreateDecryptor();

                    using (MemoryStream decryptedStream = new MemoryStream())
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(decryptedStream, decryptor, CryptoStreamMode.Write))
                        {
                            cryptoStream.Write(encryptedData, 0, encryptedData.Length);
                        }
                        return true;
                    }
                }
            }
            catch (CryptographicException ex)
            {
                Debug.WriteLine($"Decryption error: {ex.Message}");
            }

            return false;
        }

        /// <summary>
        /// Gets ProcessorID, Manufacturer and Number of logical processors and concats them into a single string
        /// </summary>
        /// <returns>string</returns>
        static string GetCpuId()
        {
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Processor"))
            {
                foreach (ManagementObject obj in searcher.Get())
                {
                    return obj["ProcessorId"].ToString() + "-" + obj["Manufacturer"].ToString() + "-" + obj["NumberOfLogicalProcessors"].ToString();
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// Gets Serialnumber and Manufacturer of the motherboard and concats them into a single string
        /// </summary>
        /// <returns>string</returns>
        static string GetMotherboardInfo()
        {
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_BaseBoard"))
            {
                foreach (ManagementObject obj in searcher.Get())
                {
                    return obj["SerialNumber"].ToString() + "-" + obj["Manufacturer"].ToString();
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// Gets Name, Manufacturer and Serialnumber of the RAM and concats them into a single string
        /// </summary>
        /// <returns>string</returns>
        static string GetCacheMemoryInfo()
        {
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMemory"))
            {
                foreach (ManagementObject obj in searcher.Get())
                {
                    return obj["Name"].ToString() + "-" + obj["Manufacturer"].ToString() + "-" + obj["SerialNumber"].ToString();
                }
            }
            return string.Empty;
        }
    }
}
