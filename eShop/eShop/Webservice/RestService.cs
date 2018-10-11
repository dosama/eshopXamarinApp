using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using eShop.Constants;
using Newtonsoft.Json;

namespace eShop.Webservice
{
    public class RestService
    {
        private readonly HttpClient _client;

        public string ServiceName { get; set; }


        public RestService()
        {
            _client = new HttpClient {MaxResponseContentBufferSize = 256000};
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
        }

        public async Task<T> GetDataAsync<T>(string path)
        {
            var restUrl = AppConstants.WEBSERVICE_URL + ServiceName +'/'+ path;
            var uri = new Uri(string.Format(restUrl, string.Empty));

            try
            {
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }
            return default(T);
        }
        public async Task<List<T>> GetDataAsync<T>()
        {
            var restUrl = AppConstants.WEBSERVICE_URL + ServiceName;
            var uri = new Uri(string.Format(restUrl, string.Empty));

            try
            {
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<T>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

            return null;
        }

        public async Task SaveTodoItemAsync<T>(T item, bool isNewItem = false)
        {
            var restUrl = AppConstants.WEBSERVICE_URL + ServiceName;
            var uri = new Uri(string.Format(restUrl, string.Empty));

            try
            {
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                if (isNewItem)
                {
                    response = await _client.PostAsync(uri, content);
                }
                else
                {
                    response = await _client.PutAsync(uri, content);
                }

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"				TodoItem successfully saved.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }
        }

        public async Task DeleteTodoItemAsync(string id)
        {
            var restUrl = AppConstants.WEBSERVICE_URL + ServiceName;
            var uri = new Uri(string.Format(restUrl, string.Empty));


            try
            {
                var response = await _client.DeleteAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"				TodoItem successfully deleted.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }
        }
    }
}