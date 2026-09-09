using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.IO;
using System.Security.Cryptography;

namespace ManagerWall
{
    internal class Crypto
    {
        public static byte[] GenerateSalt()
        {
            byte[] salt = new byte[32];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            return salt;
        }
        public static byte[] CreateKey(string masterCode, byte[] salt)
        {
            using (var pdkdf2 = new Rfc2898DeriveBytes(masterCode, salt, 600000, HashAlgorithmName.SHA256))
            {
                return pdkdf2.GetBytes(32);
            }
        }
        public static byte[] EncryptPlainText(string plainText, byte[] key, byte[] salt)
        {

            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.GenerateIV();

                using (MemoryStream ms = new MemoryStream())
                {
                    ms.Write(salt, 0, salt.Length);
                    ms.Write(aes.IV, 0, aes.IV.Length);

                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    using (StreamWriter sw = new StreamWriter(cs))
                    {
                        sw.Write(plainText);
                    }
                    return ms.ToArray();
                }
            }
        }

        public static string DecryptCipherText(byte[] cipherText, byte[] key)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;

                byte[] iv = new byte[16];
                Array.Copy(cipherText, 32, iv, 0, iv.Length);
                aes.IV = iv;

                using (MemoryStream ms = new MemoryStream())
                {
                    ms.Write(cipherText, 48, cipherText.Length - 48);
                    ms.Position = 0;

                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read))
                    using (StreamReader sr = new StreamReader(cs))
                    {
                        return sr.ReadToEnd();
                    }
                }
            }
        }
    }
}
