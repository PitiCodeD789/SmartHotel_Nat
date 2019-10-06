using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SmartHotel.Services.Hotels.Services
{
    public class NotificationService
    {
        public string CallDeskNotification(int hotelId, string user, string message)
        {
            try
            {
                var model = new
                {
                    to = "/topics/" + hotelId.ToString(),
                    notnotification = new
                    {
                        title = user,
                        body = message,
                        click_action = "message"
                    }
                };

                HttpClient client = new HttpClient();
                client.Timeout = TimeSpan.FromSeconds(20);
                HttpContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                string url = @"https://fcm.googleapis.com/fcm/send";
                string token = @"AAAASbggBW8:APA91bF5OIUgPZInwsYU8ro0Nmx0NfGGrYcT4Elciwkw_D404Gjt2TclP51qz15VdCKw5FJFT2Ro_TDLVz3lpS1w_6OoySk2dy5wWVzHY5NA2OOKdn24gN0r_De5c9fiACN7lKjoCp3L";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("key", token);
                var result = client.PostAsync(url, content).Result;
                if (result.IsSuccessStatusCode)
                {
                    var json_result = result.Content.ReadAsStringAsync().Result;

                    string wordResult = JsonConvert.DeserializeObject<string>(json_result);

                    return wordResult;
                }
                else
                {
                    client.Dispose();
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error : " + e.Message);
                return null;
            }

        }
    }
}
