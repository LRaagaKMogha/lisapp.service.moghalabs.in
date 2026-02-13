using Service.Model.Integration;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace Service.Repository.Integration.externalservices
{
    public class NotificationDetailsService
    {
        public async void PushNotification(NotifyResponse notifyResponse,string url)
        {           
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                
                var payload = JsonSerializer.Serialize(notifyResponse);
                var content = new StringContent(payload,Encoding.UTF8, "application/json");
                var returndata = await client.PostAsync(client.BaseAddress,content);
            }
        }
    }
}
