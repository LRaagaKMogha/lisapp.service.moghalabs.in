using System.IO;
using System.Text;
using System.Security.Cryptography;
using System;
using System.Configuration;

namespace DEV.Common
{
    public static class EncryptionHelper
    {
        public static string Encrypt(string plainText)
        {
            string encryptionKey = ConfigurationManager.AppSettings["MKey"]
                ?? throw new InvalidOperationException("Encryption key (MKey) is missing in AppSettings.");

            string saltString = ConfigurationManager.AppSettings["Salt"]
                ?? throw new InvalidOperationException("Salt is missing in AppSettings.");

            byte[] clearBytes = Encoding.Unicode.GetBytes(plainText);
            using (Aes encryptor = Aes.Create())
            {
                var salt = Encoding.UTF8.GetBytes(saltString);
                using var pdb = new Rfc2898DeriveBytes(
                    encryptionKey,
                    salt,
                    1000,
                    HashAlgorithmName.SHA1
                );

                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    plainText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return plainText;
        }
        public static string Decrypt(string plainText)
        {
            if(plainText == null)
            {
                plainText = string.Empty;
            }
            string encryptionKey = ConfigurationManager.AppSettings["MKey"]
                ?? throw new InvalidOperationException("Encryption key (MKey) is missing in AppSettings.");

            string saltString = ConfigurationManager.AppSettings["Salt"]
                ?? throw new InvalidOperationException("Salt is missing in AppSettings.");

            plainText = plainText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(plainText);
            using (Aes encryptor = Aes.Create())
            {
                var salt = Encoding.UTF8.GetBytes(saltString);
                using var pdb = new Rfc2898DeriveBytes(
                    encryptionKey,
                    salt,
                    1000,
                    HashAlgorithmName.SHA1
                );

                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    plainText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return plainText;
        }
    }
}
