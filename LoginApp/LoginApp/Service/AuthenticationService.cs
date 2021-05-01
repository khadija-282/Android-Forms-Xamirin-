using LoginApp.ViewModels;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace LoginApp.Service
{
    public class AuthenticationService
    {
        private HttpClient _client;

        public AuthenticationService(HttpClient httpClient)
        {
            _client = httpClient;
        }
        public async Task<bool> MakeGetRequest(string resource)
        {
            bool isAuthenticated = false;
            try
            {
                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri(_client.BaseAddress, resource),
                    Method = HttpMethod.Get
                };
                var response = await _client.GetAsync(resource);
                var testresp = response;
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    var res = JsonConvert.DeserializeObject<Response>(responseString);
                    isAuthenticated = res.result;
                }
                return isAuthenticated;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
