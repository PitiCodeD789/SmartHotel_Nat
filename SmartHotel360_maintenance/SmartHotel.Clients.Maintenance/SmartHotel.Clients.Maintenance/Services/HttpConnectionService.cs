using Newtonsoft.Json;
using SmartHotel.Clients.Maintenance.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace SmartHotel.Clients.Maintenance.Services
{
    public class HttpConnectionService
    {
        public async Task<ResultModel<T>> HttpGet<T>(ServiceEnumerables.Url enumerables, string valueToGet = null) where T : class
        {
            ResultModel<T> result = new ResultModel<T>();
            try
            {
                var client = new HttpClient();
                string accessToken = null;
                try
                {
                    accessToken = await SecureStorage.GetAsync("AccessToken");
                }
                catch (Exception e)
                {
                    result.IsError = true;
                    result.Message = ServiceEnumerables.HttpConnectionError.CannotRetrieveSecureStorage;
                    return result;
                }
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                string url = ServiceEnumerables.UrlString[(int)enumerables] + valueToGet;
                client.Timeout = TimeSpan.FromSeconds(30);
                HttpResponseMessage response = client.GetAsync(url).Result;
                result.HttpStatusCode = response.StatusCode;
                T resultModel;
                try
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        result.IsError = true;
                        result.Message = ServiceEnumerables.HttpConnectionError.NotSuccess;
                        return result;
                    }
                    string responseBody = await response.Content.ReadAsStringAsync();
                    resultModel = JsonConvert.DeserializeObject<T>(responseBody);
                }
                catch (Exception e)
                {
                    result.IsError = true;
                    result.Message = ServiceEnumerables.HttpConnectionError.DeserializeJsonError;
                    return result;
                }

                result.Model = resultModel;
                return result;
            }
            catch (Exception e)
            {
                result.IsError = true;
                result.Message = ServiceEnumerables.HttpConnectionError.UnknownError;
                return result;
            }
        }


        public async Task<ResultModel<T>> HttpPost<T>(ServiceEnumerables.Url enumerables, object postObject) where T : class
        {
            ResultModel<T> result = new ResultModel<T>();
            try
            {
                var client = new HttpClient();
                string accessToken = null;
                try
                {
                    accessToken = await SecureStorage.GetAsync("AccessToken");
                }
                catch (Exception e)
                {
                    result.IsError = true;
                    result.Message = ServiceEnumerables.HttpConnectionError.CannotRetrieveSecureStorage;
                    return result;
                }
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                string url = ServiceEnumerables.UrlString[(int)enumerables];
                client.Timeout = TimeSpan.FromSeconds(5);
                var json = JsonConvert.SerializeObject(postObject);
                var requestBody = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(url, requestBody).Result;
                result.HttpStatusCode = response.StatusCode;
                T resultModel;
                try
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        result.IsError = true;
                        result.Message = ServiceEnumerables.HttpConnectionError.NotSuccess;
                        return result;
                    }
                    string responseBody = await response.Content.ReadAsStringAsync();
                    resultModel = JsonConvert.DeserializeObject<T>(responseBody);
                }
                catch (Exception e)
                {
                    result.IsError = true;
                    result.Message = ServiceEnumerables.HttpConnectionError.DeserializeJsonError;
                    return result;
                }

                result.Model = resultModel;
                return result;
            }
            catch (Exception e)
            {
                result.IsError = true;
                result.Message = ServiceEnumerables.HttpConnectionError.UnknownError;
                return result;
            }
        }
    }
}
