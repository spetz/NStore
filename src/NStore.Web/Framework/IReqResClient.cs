using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NStore.Web.Framework
{
    public interface IReqResClient
    {
        Task<IEnumerable<UserData>> BrowseAsync(int page = 1);
    }

    public class ReqResClient : IReqResClient
    {
        private readonly HttpClient _httpClient;

        public ReqResClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        public async Task<IEnumerable<UserData>> BrowseAsync(int page = 1)
        {
            var response = await _httpClient.GetAsync($"users?page={page}");
            if (!response.IsSuccessStatusCode)
            {
                return Enumerable.Empty<UserData>();
            }

            var data = await response.Content.ReadAsAsync<UsersResponse>();

            return data.Data;
        }

        private class UsersResponse
        {
            public IEnumerable<UserData> Data { get; set; }
        }
    }

    public class UserData
    {
        public int Id { get; set; }
        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        [JsonProperty("last_name")]
        public string LastName { get; set; }
    }
}