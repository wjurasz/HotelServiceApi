using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace HotelService.ReservationApi.Resolvers
{
    public class ClientResolver
    {
        private readonly HttpClient _httpClient;

        public ClientResolver(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:5084/"); 
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<ClientDto?> ResolveClient(int clientId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"client/{clientId}");

                if (!response.IsSuccessStatusCode)
                    return null;

                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ClientDto>(json);
            }
            catch
            {
                return null;
            }
        }

    }

    public class ClientDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
    }

}
