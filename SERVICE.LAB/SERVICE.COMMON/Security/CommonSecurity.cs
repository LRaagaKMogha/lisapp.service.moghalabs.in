using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace DEV.Common
{
    public static class CommonSecurity
    {      
        public static string GeneratePassword(int length) //length of salt    
        {           
            return "YMXYURT1";
        }
        public static string EncodePassword(string pass, string salt) //encrypt password    
        {
            // Convert password and salt to byte arrays using Unicode encoding
            byte[] passBytes = Encoding.Unicode.GetBytes(pass);
            byte[] saltBytes = Encoding.Unicode.GetBytes(salt);

            // Concatenate saltBytes + passBytes into one byte array
            byte[] combinedBytes = new byte[saltBytes.Length + passBytes.Length];
            Buffer.BlockCopy(saltBytes, 0, combinedBytes, 0, saltBytes.Length);
            Buffer.BlockCopy(passBytes, 0, combinedBytes, saltBytes.Length, passBytes.Length);

            // Compute SHA1 hash over the combined byte array
            using (SHA1 sha1 = SHA1.Create())
            {
                byte[] sha1Hash = sha1.ComputeHash(combinedBytes);

                // Convert SHA1 hash to Base64 string
                string base64Sha1 = Convert.ToBase64String(sha1Hash);

                // Compute MD5 hash over the Base64 string
                using (MD5 md5 = MD5.Create())
                {
                    byte[] md5Hash = md5.ComputeHash(Encoding.UTF8.GetBytes(base64Sha1));

                    string md5StringWithDashes = BitConverter.ToString(md5Hash);
                    return md5StringWithDashes;
                }
            }

            //byte[] bytes = Encoding.Unicode.GetBytes(pass);
            //byte[] src = Encoding.Unicode.GetBytes(salt);
            //byte[] dst = new byte[src.Length + bytes.Length];
            //System.Buffer.BlockCopy(src, 0, dst, 0, src.Length);
            //System.Buffer.BlockCopy(bytes, 0, dst, src.Length, bytes.Length);
            //HashAlgorithm algorithm = HashAlgorithm.Create("SHA1");
            //byte[] inArray = algorithm.ComputeHash(dst);
            //return EncodePasswordMd5(Convert.ToBase64String(inArray));
        }
        public static string EncodePasswordMd5(string pass) //Encrypt using MD5    
        {
            Byte[] originalBytes;
            Byte[] encodedBytes;
          
            //Instantiate MD5CryptoServiceProvider, get bytes for original password and compute hash (encoded password)    
            
            using (MD5 md5 = MD5.Create())
            {
                originalBytes = Encoding.Default.GetBytes(pass);
                encodedBytes = md5.ComputeHash(originalBytes);
                return BitConverter.ToString(encodedBytes);
            }
        }
        public static string base64Encode(string sData) // Encode    
        {
            string encodedData = string.Empty;
            try
            {
                byte[] encData_byte = System.Text.Encoding.UTF8.GetBytes(sData);
                encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "CommonSecurity.base64Decode", ExceptionPriority.Medium, ApplicationType.APPSERVICE, 0, 0, 0);
            }
            return encodedData;
        }
        public static string base64Decode(string sData) //Decode    
        {
            string result = string.Empty;
            try
            {
                var encoder = new System.Text.UTF8Encoding();
                System.Text.Decoder utf8Decode = encoder.GetDecoder();
                byte[] todecodeByte = Convert.FromBase64String(sData);
                int charCount = utf8Decode.GetCharCount(todecodeByte, 0, todecodeByte.Length);
                char[] decodedChar = new char[charCount];
                utf8Decode.GetChars(todecodeByte, 0, todecodeByte.Length, decodedChar, 0);
                result = new String(decodedChar);
                return result;
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "CommonSecurity.base64Decode", ExceptionPriority.Medium, ApplicationType.APPSERVICE, 0, 0, 0);
            }
            return result;

        }
    }
}
