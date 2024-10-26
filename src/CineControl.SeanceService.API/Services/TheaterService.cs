using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using CineControl.SeanceService.API.Models;

namespace CineControl.SeanceService.API.Services
{
    public class TheaterService : ITheaterService
    {
        private readonly HttpClient _httpClient;

        public TheaterService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<TheaterDto> GetTheaterByIdAsync(int theaterId)
        {
            var response = await _httpClient.GetAsync($"api/theaters/{theaterId}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var theater = JsonSerializer.Deserialize<TheaterDto>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return theater;
            }
            return null;
        }
    }
}
