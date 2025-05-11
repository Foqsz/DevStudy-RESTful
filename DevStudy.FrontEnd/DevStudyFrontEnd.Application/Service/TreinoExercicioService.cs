using DevStudy.FrontEnd.DevStudyFrontEnd.Application.Interface;
using DevStudy.FrontEnd.DevStudyFrontEnd.Core.Models;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json;

namespace DevStudy.FrontEnd.DevStudyFrontEnd.Application.Service;

public class TreinoExercicioService : ITreinoExercicioService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<TreinoExercicioService> _logger;
    private const string url = "https://localhost:7238/api/TreinoExercicio";
    private readonly JsonSerializerOptions _serializerOptions;

    public TreinoExercicioService(HttpClient httpClient, ILogger<TreinoExercicioService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
        _serializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<IEnumerable<TreinoExercicioViewModel>> GetTreinoExercicios()
    {
        _logger.LogInformation("Fetching all TreinoExercicios from API.");
        var client = _httpClient;

        var response = await client.GetAsync($"{url}");

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var treinos = JsonConvert.DeserializeObject<IEnumerable<TreinoExercicioViewModel>>(content);
            if (treinos == null)
            {
                _logger.LogWarning("No TreinoExercicios found.");
                return null;
            }
            _logger.LogInformation("Successfully fetched TreinoExercicios.");
            return treinos;
        }

        _logger.LogError($"Failed to fetch TreinoExercicios. Status Code: {response.StatusCode}");
        return null;
    }

    public async Task<TreinoExercicioViewModel> GetTreinoExercicioById(int id)
    {
        _logger.LogInformation($"Fetching TreinoExercicio with ID {id}.");
        var client = _httpClient;

        var response = await client.GetAsync($"{url}/{id}");

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var treino = JsonConvert.DeserializeObject<TreinoExercicioViewModel>(content);
            if (treino == null)
            {
                _logger.LogError("Failed to deserialize TreinoExercicio.");
                throw new HttpRequestException("Erro ao desserializar o treino.");
            }
            _logger.LogInformation($"Successfully fetched TreinoExercicio with ID {id}.");
            return treino;
        }

        _logger.LogError($"Failed to fetch TreinoExercicio with ID {id}. Status Code: {response.StatusCode}");
        throw new HttpRequestException($"Erro ao buscar o treino. {response.StatusCode}");
    }

    public async Task<TreinoExercicioViewModel> CreateTreinoExercicio(TreinoExercicioViewModel treinoExercicio)
    {
        _logger.LogInformation("Creating a new TreinoExercicio.");
        var client = _httpClient;

        var response = await client.PostAsJsonAsync($"{url}", treinoExercicio);

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var addTreino = JsonConvert.DeserializeObject<TreinoExercicioViewModel>(content);
            if (addTreino == null)
            {
                _logger.LogError("Failed to deserialize the created TreinoExercicio.");
                throw new HttpRequestException("Erro ao desserializar o treino adicionado.");
            }
            _logger.LogInformation("Successfully created a new TreinoExercicio.");
            return addTreino;
        }

        _logger.LogError($"Failed to create TreinoExercicio. Status Code: {response.StatusCode}");
        throw new HttpRequestException($"Erro ao adicionar o treino. {response.StatusCode}");
    }

    public async Task<TreinoExercicioViewModel> UpdateTreinoExercicio(int id, TreinoExercicioViewModel treinoExercicio)
    {
        _logger.LogInformation($"Updating TreinoExercicio with ID {id}.");
        var client = _httpClient;
        var treinoJson = new StringContent(JsonConvert.SerializeObject(treinoExercicio), Encoding.UTF8, "application/json");

        var response = await client.PatchAsync($"{url}/{id}", treinoJson);

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var updateTreino = JsonConvert.DeserializeObject<TreinoExercicioViewModel>(content);
            if (updateTreino == null)
            {
                _logger.LogError("Failed to deserialize the updated TreinoExercicio.");
                throw new HttpRequestException("Erro ao desserializar o aluno atualizado.");
            }
            _logger.LogInformation($"Successfully updated TreinoExercicio with ID {id}.");
            return updateTreino;
        }

        _logger.LogError($"Failed to update TreinoExercicio with ID {id}. Status Code: {response.StatusCode}");
        throw new HttpRequestException($"Erro ao fazer update no aluno. {response.StatusCode}");
    }

    public async Task<bool> DeleteTreinoExercicio(int id)
    {
        _logger.LogInformation($"Deleting TreinoExercicio with ID {id}.");
        var client = _httpClient;

        var response = await client.DeleteAsync($"{url}/{id}");

        if (response.IsSuccessStatusCode)
        {
            _logger.LogInformation($"Successfully deleted TreinoExercicio with ID {id}.");
            return true;
        }

        _logger.LogError($"Failed to delete TreinoExercicio with ID {id}. Status Code: {response.StatusCode}");
        return false;
    }
}
