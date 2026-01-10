using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Dev.Win.Common
{
    public static class CommonSecurity
    {      
      
        public static string base64Encode(string sData) // Encode    
        {
            try
            {
                byte[] encData_byte = new byte[sData.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(sData);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }
        public static string base64Decode(string sData) //Decode    
        {
            try
            {
                var encoder = new System.Text.UTF8Encoding();
                System.Text.Decoder utf8Decode = encoder.GetDecoder();
                byte[] todecodeByte = Convert.FromBase64String(sData);
                int charCount = utf8Decode.GetCharCount(todecodeByte, 0, todecodeByte.Length);
                char[] decodedChar = new char[charCount];
                utf8Decode.GetChars(todecodeByte, 0, todecodeByte.Length, decodedChar, 0);
                string result = new String(decodedChar);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Decode" + ex.Message);
            }
        }
        public static string ReduceImageSize(string Base64ImageByte, int width, int Height)
        {
            string result = string.Empty;
            byte[] bytes = System.Convert.FromBase64String(Base64ImageByte);
            MemoryStream ms = new MemoryStream(bytes, 0, bytes.Length);
            using (var image = System.Drawing.Image.FromStream(ms))
            {
                var newWidth = (int)(width * 0.5);
                var newHeight = (int)(Height * 0.5);
                var thumbnailImg = new Bitmap(newWidth, newHeight);
                var thumbGraph = Graphics.FromImage(thumbnailImg);
                thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
                thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
                thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                var imageRectangle = new Rectangle(0, 0, newWidth, newHeight);
                thumbGraph.DrawImage(image, imageRectangle);
                using (var stream = new MemoryStream())
                {
                    thumbnailImg.Save(stream, image.RawFormat);
                    byte[] resultbyte = stream.ToArray();
                    result = "data:image/jpeg;base64," + Convert.ToBase64String(resultbyte);
                }
            }
            return result;
        }

    }

}
