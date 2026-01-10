using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using HtmlAgilityPack;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;
using System.Net.Http;
using System.Threading.Tasks;
using System.Configuration;

namespace DEV.Common
{
    public class CommonHelper
    {
        public string ToXML(Object oObject)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlSerializer xmlSerializer = new XmlSerializer(oObject.GetType());
            using (MemoryStream xmlStream = new MemoryStream())
            {
                xmlSerializer.Serialize(xmlStream, oObject);
                xmlStream.Position = 0;
                xmlDoc.Load(xmlStream);
                return xmlDoc.InnerXml.Replace("encoding=\"utf-8\"", "");
            }
        }
        /// <summary>
        /// URLShorten
        /// </summary>
        /// <param name="DynamicLink"></param>
        /// <param name="APIKey"></param>
        /// <returns></returns>
        public static async Task<string> URLShorten(string DynamicLink, string APIKey)
        {
            try
            {
                var objitem = new PostGoogleUrl
                {
                    longDynamicLink = APIKey.Split('|')[1] + DynamicLink,
                    suffix = new Suffix { option = "SHORT" }
                };

                string requestUrl = ApplicationConstants.FireBaseUrl + APIKey.Split('|')[0];
                string json = JsonConvert.SerializeObject(objitem);

                using (var client = new HttpClient())
                using (var content = new StringContent(json, Encoding.UTF8, "application/json"))
                {
                    var response = await client.PostAsync(requestUrl, content);

                    if (!response.IsSuccessStatusCode)
                    {
                        return DynamicLink;
                    }

                    var responseText = await response.Content.ReadAsStringAsync();
                    var objResult = JsonConvert.DeserializeObject<GoogleResponse>(responseText);

                    if (objResult == null || string.IsNullOrEmpty(objResult.shortLink))
                    {
                        return DynamicLink;
                    }

                    return objResult.shortLink;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "URLShorten", ExceptionPriority.Low, ApplicationType.APPSERVICE, 0, 0, 0);
                return DynamicLink;
            }
        }

        public async static Task<string> SendSMS(string SMSURL, string secretkey, string mobileno)
        {
            string responseText = string.Empty;
            string postdata = "tar_num=" + mobileno;
            postdata += "&tar_msg=" + secretkey;
            postdata += "label=RMGOTP";
            var data = Encoding.ASCII.GetBytes(postdata);
            var content = new ByteArrayContent(data);
            var sendQuickSMSURL = SMSURL;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("ContentType", "application/x-www-form-urlencoded");
                using (var requestmessage = new HttpRequestMessage(HttpMethod.Post, sendQuickSMSURL) { Content = content })
                {
                    var response = await client.SendAsync(requestmessage);
                    responseText = await response.Content.ReadAsStringAsync();
                }
            }
            return responseText;
        }
        public static int GenerateRandomNo()
        {
            int _min = 1000;
            int _max = 9999;
            Random _rdm = new Random();
            return _rdm.Next(_min, _max);
        }
        /// <summary>
        /// CKEditor Image Style changes
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string _CkeditorFigureTag(string html)
        {
            try
            {
                HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();
                htmlDoc.LoadHtml(html);
                var imgrepeaters = htmlDoc.DocumentNode.SelectNodes("//img");
                if (imgrepeaters != null)
                {
                    foreach (HtmlNode node in htmlDoc.DocumentNode.SelectNodes("//img"))
                    {
                        var styles = node.GetAttributeValue("style", null);
                        if (styles != null)
                        {
                            var wd = styles.Split(':')[1].Replace("%", "").Replace(";", "");
                            var widthd = Math.Round(Convert.ToInt16(1000) * (Convert.ToDouble(wd) / Convert.ToInt16(100)), 2);
                            string styleoutput = "width:" + widthd + "px";
                            node.SetAttributeValue("style", styleoutput);
                        }

                    }
                }
                var repeaters = htmlDoc.DocumentNode.SelectNodes("//figure");
                if (repeaters != null)
                {
                    foreach (HtmlNode node in htmlDoc.DocumentNode.SelectNodes("//figure"))
                    {
                        var styles = node.GetAttributeValue("style", null);
                        if (styles != null)
                        {
                            foreach (var child in node.ChildNodes)
                            {
                                if (child.Name == "img")
                                {
                                    var wd = styles.Split(':')[1].Replace("%", "").Replace(";", "");
                                    var widthd = Math.Round(Convert.ToInt16(1240) * (Convert.ToDouble(wd) / Convert.ToInt16(100)), 2);
                                    string styleoutput = "width:" + widthd + "px";
                                    child.SetAttributeValue("style", styleoutput);
                                }
                            }
                        }
                    }
                }
                return htmlDoc.DocumentNode.InnerHtml;
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "_CkeditorFigureTag", ExceptionPriority.Low, ApplicationType.APPSERVICE, 0, 0, 0);
                return html;
            }
        }
        public static T GetEnumValue<T>(int intValue) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new Exception("T must be an Enumeration type.");
            }
            T val = ((T[])Enum.GetValues(typeof(T)))[0];

            foreach (T enumValue in (T[])Enum.GetValues(typeof(T)))
            {
                if (Convert.ToInt32(enumValue).Equals(intValue))
                {
                    val = enumValue;
                    break;
                }
            }
            return val;
        }
    }
}
