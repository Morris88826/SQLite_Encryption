using System;
using System.Globalization;
using System.IO;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Markup;


namespace DatabaseEncryption
{
    public class AESCryptography
    {
        private const int SaltSize = 8;
        private const int Iterations = 1000;
        private const int KeySize = 32; // AES-256 key size in bytes
        private const int IVSize = 16;  // AES block size in bytes
        private static string password = "neurobit_data";  // Always use a strong password
        public static string Encrypt(string data)
        {
            using (Aes aes = Aes.Create())
            {
                byte[] salt = GenerateRandomSalt();
                byte[] key = DeriveKey(password, salt, KeySize);
                byte[] iv = GenerateRandomIV();

                aes.Key = key;
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream())
                {
                    // Prepend salt and IV to the encrypted data for use during decryption
                    ms.Write(salt, 0, salt.Length);
                    ms.Write(iv, 0, iv.Length);

                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                        {
                            sw.Write(data);
                        }
                    }
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }
        public static string EncryptDouble(double data)
        {
            return Encrypt(data.ToString(CultureInfo.InvariantCulture));
        }
        public static string EncryptDateTime(DateTime data)
        {
            return Encrypt(data.ToString("o"));  // Using ISO 8601 format for precision
        }

        public static string EncryptInt(int data)
        {
            return Encrypt(data.ToString());
        }

        public static string Decrypt(string encryptedData)
        {
            byte[] encryptedBytes = Convert.FromBase64String(encryptedData);
            byte[] salt = new byte[SaltSize];
            Array.Copy(encryptedBytes, salt, SaltSize);

            byte[] iv = new byte[IVSize];
            Array.Copy(encryptedBytes, SaltSize, iv, 0, IVSize);

            byte[] actualEncryptedData = new byte[encryptedBytes.Length - SaltSize - IVSize];
            Array.Copy(encryptedBytes, SaltSize + IVSize, actualEncryptedData, 0, actualEncryptedData.Length);

            using (Aes aes = Aes.Create())
            {
                byte[] key = DeriveKey(password, salt, KeySize);

                aes.Key = key;
                aes.IV = iv;

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream(actualEncryptedData))
                using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                using (StreamReader sr = new StreamReader(cs))
                {
                    return sr.ReadToEnd();
                }
            }
        }
        public static double DecryptDouble(string encryptedData)
        {
            var decryptedString = Decrypt(encryptedData);
            return double.Parse(decryptedString, CultureInfo.InvariantCulture);
        }

        public static DateTime DecryptDateTime(string encryptedData)
        {
            var decryptedString = Decrypt(encryptedData);
            return DateTime.ParseExact(decryptedString, "o", CultureInfo.InvariantCulture);
        }
        public static int DecryptInt(string encryptedData)
        {
            var decryptedString = Decrypt(encryptedData);
            return int.Parse(decryptedString);
        }


        private static byte[] GenerateRandomSalt()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] salt = new byte[SaltSize];
                rng.GetBytes(salt);
                return salt;
            }
        }

        private static byte[] GenerateRandomIV()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] iv = new byte[IVSize];
                rng.GetBytes(iv);
                return iv;
            }
        }

        private static byte[] DeriveKey(string password, byte[] salt, int keySizeInBytes)
        {
            using (var deriveBytes = new Rfc2898DeriveBytes(password, salt, Iterations))
            {
                return deriveBytes.GetBytes(keySizeInBytes);
            }
        }
    }
}
