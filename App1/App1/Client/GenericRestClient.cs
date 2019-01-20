using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using App1.Models;
using Newtonsoft.Json;

namespace RestClient.Client
{
    /// <summary>
    /// RestClient implements methods for calling CRUD operations
    /// using HTTP.
    /// </summary>
    public class GenericRestClient<T>
    {
        string WebServiceUrl = "http://matdhaya3nich2.azurewebsites.net/api/" + typeof(T).Name + "s/";

        public async Task<List<T>> GetAsync()
        {
            var httpClient = new HttpClient();

            var json = await httpClient.GetStringAsync(WebServiceUrl);

            var taskModels = JsonConvert.DeserializeObject<List<T>>(json);

            return taskModels;
        }

        public async Task<T> GetbyIdAsync(int id)
        {
            string WebServiceUrl1 = "http://matdhaya3nich2.azurewebsites.net/api/" + typeof(T).Name + "s/";
            var httpClient = new HttpClient();

            var json = await httpClient.GetStringAsync(WebServiceUrl1+id);

            var taskModels = JsonConvert.DeserializeObject<T>(json);

            return taskModels;
        }

        public async Task<bool> PostAsync(T t)
        {

            var httpClient = new HttpClient();

            var json = JsonConvert.SerializeObject(t);

            HttpContent httpContent = new StringContent(json);

            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var result = await httpClient.PostAsync(WebServiceUrl, httpContent);

            return result.IsSuccessStatusCode;
        }

        public async Task<bool> PutAsync(int id, T t)
        {
            var httpClient = new HttpClient();

            var json = JsonConvert.SerializeObject(t);

            HttpContent httpContent = new StringContent(json);

            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var result = await httpClient.PutAsync(WebServiceUrl + id, httpContent);

            return result.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id, T t)
        {
            var httpClient = new HttpClient();

            var response = await httpClient.DeleteAsync(WebServiceUrl + id);

            return response.IsSuccessStatusCode;
        }

        // api/UserComptes/GetUserCompte

        public async Task<List<UserCompte>> GetUserCompteAsync(string name)
        {
            string WebServiceUrl1 = "http://matdhaya3nich2.azurewebsites.net/api/UserComptes/GetUserCompte/";
            var httpClient = new HttpClient();

            var json = await httpClient.GetStringAsync(WebServiceUrl1+name);

            var taskModels = JsonConvert.DeserializeObject<List<UserCompte>>(json);

            return taskModels;
        }

        public async Task<T> GetUserCompteByEmail(String Email)
        {
            string WebServiceUrl1 = "http://matdhaya3nich2.azurewebsites.net/api/UserComptes/GetUserCompteByEmail/";
            var httpClient = new HttpClient();

            var json = await httpClient.GetStringAsync(WebServiceUrl1 + Email);

            var taskModels = JsonConvert.DeserializeObject<T>(json);

            return taskModels;
        }

        public async Task<T> GetGuideCompteByEmail(String Email)
        {
            string WebServiceUrl1 = "http://matdhaya3nich2.azurewebsites.net/api/GuideComptes/GetGuideCompteByEmail/";
            var httpClient = new HttpClient();

            var json = await httpClient.GetStringAsync(WebServiceUrl1 + Email);

            var taskModels = JsonConvert.DeserializeObject<T>(json);

            return taskModels;
        }
        public async Task<List<T>> GetGuideCompteBycalendar(int id)
        {
            string WebServiceUrl1 = "http://matdhaya3nich2.azurewebsites.net/api/" + typeof(T).Name + "s/";
            var httpClient = new HttpClient();

            var json = await httpClient.GetStringAsync(WebServiceUrl1 + id);

            var taskModels = JsonConvert.DeserializeObject<List<T>>(json);

            return taskModels;
        }
    }
}
