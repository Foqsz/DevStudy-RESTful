using DevStudy.FrontEnd.DevStudyFrontEnd.Application.Interface;
using DevStudy.FrontEnd.DevStudyFrontEnd.Core.Models;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Text.Json;

namespace DevStudy.FrontEnd.DevStudyFrontEnd.Application.Service
{
    public class AvaliacaoFisicaService : IAvaliacaoFisicaService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<AvaliacaoFisicaService> _logger;
        private const string url = "https://localhost:7238/api/AvaliacaoFisica";
        private readonly JsonSerializerOptions _serializerOptions;

        public AvaliacaoFisicaService(HttpClient httpClient, ILogger<AvaliacaoFisicaService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
            _serializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<IEnumerable<AvaliacaoFisicaViewModel>> GetAvaliacoesFisicas()
        {
            _logger.LogInformation("Fetching all Avaliacoes Fisicas from {Url}", url);

            var client = _httpClient;
            var response = await client.GetAsync($"{url}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var avaliacoes = JsonConvert.DeserializeObject<IEnumerable<AvaliacaoFisicaViewModel>>(content);

                if (avaliacoes == null)
                {
                    _logger.LogWarning("No Avaliacoes Fisicas found.");
                    return null;
                }

                _logger.LogInformation("Successfully fetched Avaliacoes Fisicas.");
                return avaliacoes;
            }

            _logger.LogError("Failed to fetch Avaliacoes Fisicas. Status Code: {StatusCode}", response.StatusCode);
            return null;
        }

        public async Task<AvaliacaoFisicaViewModel> GetAvaliacaoFisica(int id)
        {
            _logger.LogInformation("Fetching Avaliacao Fisica with ID {Id} from {Url}", id, url);

            var client = _httpClient;
            var response = await client.GetAsync($"{url}/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var avaliacao = JsonConvert.DeserializeObject<AvaliacaoFisicaViewModel>(content);

                if (avaliacao == null)
                {
                    _logger.LogWarning("Avaliacao Fisica with ID {Id} not found.", id);
                    return null;
                }

                _logger.LogInformation("Successfully fetched Avaliacao Fisica with ID {Id}.", id);
                return avaliacao;
            }

            _logger.LogError("Failed to fetch Avaliacao Fisica with ID {Id}. Status Code: {StatusCode}", id, response.StatusCode);
            return null;
        }

        public async Task<AvaliacaoFisicaViewModel> CreateAvaliacaoFisica(AvaliacaoFisicaViewModel avaliacaoFisica)
        {
            _logger.LogInformation("Creating a new Avaliacao Fisica.");

            var client = _httpClient;
            var jsonContent = JsonConvert.SerializeObject(avaliacaoFisica, new JsonSerializerSettings
            {
                Culture = System.Globalization.CultureInfo.InvariantCulture
            });

            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{url}", content);

            _logger.LogInformation("HTTP response status: {StatusCode}", response.StatusCode);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var avaliacao = JsonConvert.DeserializeObject<AvaliacaoFisicaViewModel>(responseContent);

                if (avaliacao == null)
                {
                    _logger.LogWarning("Failed to deserialize the created Avaliacao Fisica.");
                    return null;
                }

                _logger.LogInformation("Successfully created a new Avaliacao Fisica.");
                return avaliacao;
            }

            _logger.LogError("Failed to create a new Avaliacao Fisica. Status Code: {StatusCode}", response.StatusCode);
            return null;
        }

        public async Task<AvaliacaoFisicaViewModel> UpdateAvaliacaoFisica(int id, AvaliacaoFisicaViewModel avaliacaoFisica)
        {
            _logger.LogInformation("Updating Avaliacao Fisica with ID {Id}.", id);

            var client = _httpClient;
            var response = await client.PutAsJsonAsync($"{url}/{id}", avaliacaoFisica);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var avaliacao = JsonConvert.DeserializeObject<AvaliacaoFisicaViewModel>(content);

                if (avaliacao == null)
                {
                    _logger.LogWarning("Failed to deserialize the updated Avaliacao Fisica with ID {Id}.", id);
                    return null;
                }

                _logger.LogInformation("Successfully updated Avaliacao Fisica with ID {Id}.", id);
                return avaliacao;
            }

            _logger.LogError("Failed to update Avaliacao Fisica with ID {Id}. Status Code: {StatusCode}", id, response.StatusCode);
            return null;
        }

        public async Task<bool> DeleteAvaliacaoFisica(int id)
        {
            _logger.LogInformation("Deleting Avaliacao Fisica with ID {Id}.", id);

            var client = _httpClient;
            var response = await client.DeleteAsync($"{url}?id={id}");

            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("Successfully deleted Avaliacao Fisica with ID {Id}.", id);
                return true;
            }

            _logger.LogError("Failed to delete Avaliacao Fisica with ID {Id}. Status Code: {StatusCode}", id, response.StatusCode);
            return false;
        }
    }
}
