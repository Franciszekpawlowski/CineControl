using System.Net.Http;
using System.Net.Http.Json; // Dodaj to using
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using CineControl.SeanceService.API.Models;

namespace CineControl.SeanceService.API.Services
{
    public class FilmService : IFilmService
    {
        private readonly HttpClient _httpClient;
        private readonly string _filmServiceBaseUrl;

        public FilmService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _filmServiceBaseUrl = configuration["FilmServiceBaseUrl"] ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<Film> GetFilmById(int id)
        {
            var response = await _httpClient.GetAsync($"{_filmServiceBaseUrl}/api/films/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Film>();
        }
    }
}
