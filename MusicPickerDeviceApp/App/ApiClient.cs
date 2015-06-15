using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace MusicPickerDeviceApp.App
{
    public class ApiClient
    {
        private Uri endpoint;
        private string bearer;
        private bool authenticated;

        public ApiClient(Uri endpoint)
        {
            this.endpoint = endpoint;
        }

        public bool SignUp(string username, string password)
        {
            Uri uri = new Uri(endpoint, "/api/Account/Register");
            HttpContent content = new FormUrlEncodedContent(new []
            {
                new KeyValuePair<string, string>("Username", username),
                new KeyValuePair<string, string>("Password", password),
                new KeyValuePair<string, string>("ConfirmPassword", password), 
            });

            HttpResponseMessage result = (new HttpClient()).PostAsync(uri, content).Result;

            if (result.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public void ProvideBearer(string bearer)
        {
            this.bearer = bearer;
        }

        public bool LogIn(string username, string password)
        {
            Uri uri = new Uri(endpoint, "/oauth/token");
            HttpContent content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("username", username),
                new KeyValuePair<string, string>("password", password),
                new KeyValuePair<string, string>("grant_type", "password")
            });

            HttpResponseMessage result = (new HttpClient()).PostAsync(uri, content).Result;
            if (!result.IsSuccessStatusCode)
            {
                return false;
            }

            Dictionary<string, string> data =
                JsonConvert.DeserializeObject<Dictionary<string, string>>(result.Content.ReadAsStringAsync().Result);
           
            ProvideBearer(data["access_token"]);
            return true;
        }

        public int DeviceAdd(string name)
        {
            Uri uri = new Uri(endpoint, "/api/Devices");
            HttpContent content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("name", name),
            });

            HttpResponseMessage result = (new HttpClient()
            {
                DefaultRequestHeaders = { Authorization = new AuthenticationHeaderValue("Bearer", this.bearer)}
            }).PostAsync(uri, content).Result;
            if (!result.IsSuccessStatusCode)
            {
                return -1; // Exception @TODO
            }

            Dictionary<string, string> data =
               JsonConvert.DeserializeObject<Dictionary<string, string>>(result.Content.ReadAsStringAsync().Result);

            return Convert.ToInt32(data["Id"]);
        }

        public bool DeviceCollectionSubmit(int deviceId, string collection)
        {
            Uri uri = new Uri(endpoint, string.Format("/api/Devices/{0}/Submit", deviceId));

            HttpContent content = new StringContent(collection)
            {
                Headers = { ContentType = new MediaTypeHeaderValue("application/json")}
            };

            HttpResponseMessage result = (new HttpClient()
            {
                DefaultRequestHeaders = { Authorization = new AuthenticationHeaderValue("Bearer", this.bearer) }
            }).PostAsync(uri, content).Result;

            if (!result.IsSuccessStatusCode)
            {
                return false;
            }

            return true;
        }

        public List<Device> DevicesGet()
        {
            Uri uri = new Uri(endpoint, "/api/Devices");

            HttpResponseMessage result = (new HttpClient()
            {
                DefaultRequestHeaders = { Authorization = new AuthenticationHeaderValue("Bearer", this.bearer) }
            }).GetAsync(uri).Result;

            if (!result.IsSuccessStatusCode)
            {
                return null; // @TODO exception
            }

            return JsonConvert.DeserializeObject<List<Device>>(result.Content.ReadAsStringAsync().Result);
        }


    }
}
