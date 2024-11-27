using BookingService.API.Models;
using BookingService.API.Models.Results;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace BookingService.API.Services
{
    public class ExternalApiService : IExternalApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<ExternalApiService> _logger;
        private readonly string _seanceApiUrl;
        private readonly string _theaterApiUrl;

        public ExternalApiService(
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration,
            ILogger<ExternalApiService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;

            // Pobierz adresy URL z konfiguracji
            _seanceApiUrl = configuration.GetValue<string>("ExternalApis:Seances");
            _theaterApiUrl = configuration.GetValue<string>("ExternalApis:Theaters");
        }

        public async Task<SeanceDto> GetSeanceAsync(int seanceId)
        {
            var httpClient = _httpClientFactory.CreateClient();

            var response = await httpClient.GetAsync($"{_seanceApiUrl}/{seanceId}");
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"Nie udało się pobrać seansu o ID {seanceId}. Status Code: {response.StatusCode}");
                return null;
            }

            var content = await response.Content.ReadAsStringAsync();
            var seance = JsonConvert.DeserializeObject<SeanceDto>(content);
            return seance;
        }

        public async Task<Theater> GetTheaterAsync(int theaterId)
        {
            var httpClient = _httpClientFactory.CreateClient();

            var response = await httpClient.GetAsync($"{_theaterApiUrl}/{theaterId}");
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"Nie udało się pobrać teatru o ID {theaterId}. Status Code: {response.StatusCode}");
                return null;
            }

            var content = await response.Content.ReadAsStringAsync();
            var theater = JsonConvert.DeserializeObject<Theater>(content);
            return theater;
        }
    }
}
