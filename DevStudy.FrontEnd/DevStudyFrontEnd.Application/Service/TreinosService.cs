using DevStudy.FrontEnd.DevStudyFrontEnd.Application.Interface;
using DevStudy.FrontEnd.DevStudyFrontEnd.Core.Models;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json;

namespace DevStudy.FrontEnd.DevStudyFrontEnd.Application.Service;

public class TreinosService : ITreinosService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<TreinosService> _logger;
    private const string url = "https://localhost:7238/api/Treinos";
    private readonly JsonSerializerOptions _serializerOptions;

    public TreinosService(HttpClient httpClient, ILogger<TreinosService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
        _serializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<IEnumerable<TreinoViewModel>> GetTreinos()
    {
        _logger.LogInformation("Fetching all treinos from API.");
        var client = _httpClient;
        var response = await client.GetAsync($"{url}");
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var treinos = JsonConvert.DeserializeObject<IEnumerable<TreinoViewModel>>(content);
            if (treinos == null)
            {
                _logger.LogWarning("No treinos found in the response.");
                return null;
            }
            _logger.LogInformation("Successfully fetched treinos.");
            return treinos;
        }
        _logger.LogError($"Failed to fetch treinos. Status Code: {response.StatusCode}");
        return null;
    }

    public async Task<TreinoViewModel> GetTreinoById(int id)
    {
        _logger.LogInformation($"Fetching treino with ID {id} from API.");
        var client = _httpClient;
        var response = await client.GetAsync($"{url}/{id}");
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var treino = JsonConvert.DeserializeObject<TreinoViewModel>(content);
            if (treino == null)
            {
                _logger.LogError("Failed to deserialize treino.");
                throw new HttpRequestException("Erro ao desserializar o treino.");
            }
            _logger.LogInformation($"Successfully fetched treino with ID {id}.");
            return treino;
        }
        _logger.LogError($"Failed to fetch treino with ID {id}. Status Code: {response.StatusCode}");
        throw new HttpRequestException($"Erro ao buscar o treino. {response.StatusCode}");
    }

    public async Task<TreinoViewModel> CreateTreino(TreinoViewModel treino)
    {
        _logger.LogInformation("Creating a new treino.");
        var client = _httpClient;
        var response = await client.PostAsJsonAsync($"{url}", treino);

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var addTreino = JsonConvert.DeserializeObject<TreinoViewModel>(content);
            if (addTreino == null)
            {
                _logger.LogError("Failed to deserialize the created treino.");
                throw new HttpRequestException("Erro ao desserializar o treino adicionado.");
            }
            _logger.LogInformation("Successfully created a new treino.");
            return addTreino;
        }
        _logger.LogError($"Failed to create a new treino. Status Code: {response.StatusCode}");
        throw new HttpRequestException($"Erro ao adicionar o treino. {response.StatusCode}");
    }

    public async Task<TreinoViewModel> UpdateTreino(int id, TreinoViewModel treino)
    {
        _logger.LogInformation($"Updating treino with ID {id}.");
        var client = _httpClient;
        var content = new StringContent(JsonConvert.SerializeObject(treino), Encoding.UTF8, "application/json");
        var response = await client.PutAsync($"{url}/{id}", content);
        if (response.IsSuccessStatusCode)
        {
            var updateTreino = JsonConvert.DeserializeObject<TreinoViewModel>(await response.Content.ReadAsStringAsync());
            if (updateTreino == null)
            {
                _logger.LogError("Failed to deserialize the updated treino.");
                throw new HttpRequestException("Erro ao desserializar o treino atualizado.");
            }
            _logger.LogInformation($"Successfully updated treino with ID {id}.");
            return updateTreino;
        }
        _logger.LogError($"Failed to update treino with ID {id}. Status Code: {response.StatusCode}");
        throw new HttpRequestException($"Erro ao atualizar o treino. {response.StatusCode}");
    }

    public async Task<bool> DeleteTreino(int id)
    {
        _logger.LogInformation($"Deleting treino with ID {id}.");
        var client = _httpClient;
        var response = await client.DeleteAsync($"{url}/{id}");
        if (response.IsSuccessStatusCode)
        {
            _logger.LogInformation($"Successfully deleted treino with ID {id}.");
            return true;
        }
        _logger.LogError($"Failed to delete treino with ID {id}. Status Code: {response.StatusCode}");
        throw new HttpRequestException($"Erro ao deletar o treino. {response.StatusCode}");
    }
}
