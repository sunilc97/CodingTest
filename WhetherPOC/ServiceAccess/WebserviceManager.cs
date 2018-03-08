using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WhetherPOC.ServiceAccess
{
    public  static class WebserviceManager<T>
    {
        public static HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }

        public static async Task<T> GetWhetherData(string ZipCode)
        {
            try
            {
                var address = Uri.EscapeUriString(Constants.whetherUrl + ZipCode+"&appid=b6907d289e10d714a6e88b30761fae22");
                using (var client = GetClient())
                {
                    var response = await client.GetAsync(address);
                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<T>(content);
                    }
                    else
                    {
                        return default(T);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return default(T);
            }
        }


        public static class JsonContentFactory
        {
            public static StringContent CreateJsonContent(object obj)
            {
                var json = JsonConvert.SerializeObject(obj);
                var content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
                return content;
            }
        }


    }




}
