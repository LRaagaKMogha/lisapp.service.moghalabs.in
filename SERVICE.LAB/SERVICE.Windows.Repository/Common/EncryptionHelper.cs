using System.IO;
using System.Text;
using System.Security.Cryptography;
using System;
using System.Configuration;

namespace DEV.Windows.Repository
{
    public static class EncryptionHelper
    {

        public static string Encrypt(string plainText)
        {
            string EncryptionKey = ConfigurationManager.AppSettings["MKey"].ToString();
            string saltString = ConfigurationManager.AppSettings["Salt"].ToString();
            byte[] clearBytes = Encoding.Unicode.GetBytes(plainText);
            using (Aes encryptor = Aes.Create())
            {
                var salt = Encoding.UTF8.GetBytes(saltString);
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, salt);
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
            string EncryptionKey = ConfigurationManager.AppSettings["MKey"].ToString();
            string saltString = ConfigurationManager.AppSettings["Salt"].ToString();
            plainText = plainText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(plainText);
            using (Aes encryptor = Aes.Create())
            {
                var salt = Encoding.UTF8.GetBytes(saltString);
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, salt);
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
