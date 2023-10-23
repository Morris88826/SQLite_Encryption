using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using System.Globalization;

namespace DatabaseEncryption
{
    public class Utilities
    {
        public static string Key = "neurobit_data";
        public static string Encrypt(string data, string key)
            {
                using (Aes aes = Aes.Create())
                {
                    aes.Key = Encoding.UTF8.GetBytes(key); // Ensure key length is appropriate
                    aes.IV = new byte[16]; // Initialization vector of 16 zeroes

                    ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                    using (MemoryStream ms = new MemoryStream())
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                        {
                            sw.Write(data);
                        }
                        return Convert.ToBase64String(ms.ToArray());
                    }
                }
            }

        public static string Decrypt(string encryptedData, string key)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = new byte[16];

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(encryptedData)))
                using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                using (StreamReader sr = new StreamReader(cs))
                {
                    return sr.ReadToEnd();
                }
            }
        }

        public static string EncryptDouble(double data, string key)
        {
            return Encrypt(data.ToString(CultureInfo.InvariantCulture), key);
        }

        public static double DecryptDouble(string encryptedData, string key)
        {
            var decryptedString = Decrypt(encryptedData, key);
            return double.Parse(decryptedString, CultureInfo.InvariantCulture);
        }

    }   
}
