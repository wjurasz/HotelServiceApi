using HotelService.Promotion.CrossCutting.Dtos;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace HotelService.ReservationApi.Resolvers
{
    public class PromotionResolver
    {
        private readonly HttpClient _httpClient;

        public PromotionResolver(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:5142/");
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<PromotionDto.Read?> GetPromotionByCode(string code)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/promotions/{code}");
                if (!response.IsSuccessStatusCode)
                    return null;

                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<PromotionDto.Read>(json);
            }
            catch
            {
                return null;
            }
        }

        public async Task<PromotionDto.Read?> GetPromotionById(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/promotions/id/{id}");
                if (!response.IsSuccessStatusCode)
                    return null;

                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<PromotionDto.Read>(json);
            }
            catch
            {
                return null;
            }
        }
    }
}
