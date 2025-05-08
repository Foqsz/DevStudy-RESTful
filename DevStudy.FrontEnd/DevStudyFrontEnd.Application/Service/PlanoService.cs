using DevStudy.FrontEnd.DevStudyFrontEnd.Application.Interface;
using DevStudy.FrontEnd.DevStudyFrontEnd.Core.Models;
using Newtonsoft.Json;
using System.Text.Json;

namespace DevStudy.FrontEnd.DevStudyFrontEnd.Application.Service;

public class PlanoService : IPlanoService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<PlanoService> _logger;
    private const string url = "https://localhost:7238/api/Plano";
    private readonly JsonSerializerOptions _serializerOptions;

    public PlanoService(HttpClient httpClient, ILogger<PlanoService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
        _serializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<IEnumerable<PlanoViewModel>> GetPlanos()
    {
        _logger.LogInformation("Fetching all planos.");
        var client = _httpClient;

        var response = await client.GetAsync($"{url}");

        if (response.IsSuccessStatusCode)
        {
            var content = response.Content.ReadAsStringAsync();
            var planos = JsonConvert.DeserializeObject<IEnumerable<PlanoViewModel>>(content.Result);
            if (planos == null)
            {
                _logger.LogWarning("No planos found.");
                return null;
            }
            _logger.LogInformation("Successfully fetched planos.");
            return planos;
        }
        _logger.LogError($"Failed to fetch planos. StatusCode: {response.StatusCode}");
        return null;
    }

    public async Task<PlanoViewModel> GetPlano(int id)
    {
        _logger.LogInformation($"Fetching plano with ID: {id}");
        var client = _httpClient;
        var response = client.GetAsync($"{url}/{id}");

        if (response.Result.IsSuccessStatusCode)
        {
            var content = response.Result.Content.ReadAsStringAsync();
            var plano = JsonConvert.DeserializeObject<PlanoViewModel>(content.Result);
            if (plano == null)
            {
                _logger.LogError("Failed to deserialize the fetched plano.");
                throw new HttpRequestException("Erro ao desserializar o plano.");
            }
            _logger.LogInformation($"Successfully fetched plano with ID: {id}");
            return plano;
        }
        _logger.LogError($"Failed to fetch plano with ID: {id}. StatusCode: {response.Result.StatusCode}");
        throw new HttpRequestException($"Erro ao buscar o plano. {response.Result.StatusCode}");
    }

    public async Task<PlanoViewModel> CreatePlano(PlanoViewModel plano)
    {
        _logger.LogInformation("Creating a new plano.");
        var client = _httpClient;
        var response = await client.PostAsJsonAsync($"{url}", plano);

        if (response.IsSuccessStatusCode)
        {
            var content = response.Content.ReadAsStringAsync();
            var planoCriado = JsonConvert.DeserializeObject<PlanoViewModel>(content.Result);
            if (planoCriado == null)
            {
                _logger.LogError("Failed to deserialize the created plano.");
                throw new HttpRequestException("Erro ao desserializar o plano criado.");
            }
            _logger.LogInformation("Successfully created a new plano.");
            return planoCriado;
        }
        _logger.LogError($"Failed to create a new plano. StatusCode: {response.StatusCode}");
        throw new HttpRequestException($"Erro ao criar o plano. {response.StatusCode}");
    }

    public async Task<PlanoViewModel> UpdatePlano(int id, PlanoViewModel plano)
    {
        _logger.LogInformation($"Updating plano with ID: {id}");
        var client = _httpClient;

        var response = await client.PutAsJsonAsync($"{url}/{id}", plano);

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();

            var planoAtualizado = JsonConvert.DeserializeObject<PlanoViewModel>(content);
            if (planoAtualizado == null)
            {
                _logger.LogError("Failed to deserialize the updated plano.");
                throw new HttpRequestException("Erro ao desserializar o plano atualizado.");
            }
            _logger.LogInformation($"Successfully updated plano with ID: {id}");
            return planoAtualizado;
        }
        _logger.LogError($"Failed to update plano with ID: {id}. StatusCode: {response.StatusCode}");
        throw new HttpRequestException($"Erro ao atualizar o plano. {response.StatusCode}");
    }

    public async Task<bool> DeletePlano(int id)
    {
        _logger.LogInformation($"Deleting plano with ID: {id}");
        var client = _httpClient;

        var response = await client.DeleteAsync($"{url}/{id}");

        if (response.IsSuccessStatusCode)
        {
            _logger.LogInformation($"Successfully deleted plano with ID: {id}");
            return true;
        }
        _logger.LogError($"Failed to delete plano with ID: {id}. StatusCode: {response.StatusCode}");
        return false;
    }
}
