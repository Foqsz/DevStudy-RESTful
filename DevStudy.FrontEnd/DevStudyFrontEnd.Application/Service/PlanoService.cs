using DevStudy.FrontEnd.DevStudyFrontEnd.Application.Interface;
using DevStudy.FrontEnd.DevStudyFrontEnd.Core.Models;
using Newtonsoft.Json;
using System.Text.Json;

namespace DevStudy.FrontEnd.DevStudyFrontEnd.Application.Service;

public class PlanoService : IPlanoService
{
    private readonly HttpClient _httpClient;
    private const string url = "https://localhost:7238/api/Plano";
    private readonly JsonSerializerOptions _serializerOptions;

    public PlanoService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _serializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<IEnumerable<PlanoViewModel>> GetPlanos()
    {
        var client = _httpClient;

        var response = await client.GetAsync($"{url}");

        if(response.IsSuccessStatusCode)
        {
            var content = response.Content.ReadAsStringAsync();
            var planos = JsonConvert.DeserializeObject<IEnumerable<PlanoViewModel>>(content.Result);
            if (planos == null)
            {
                return null;
            }
            return planos;
        }
        return null;
    }

    public async Task<PlanoViewModel> GetPlano(int id)
    {
        var client = _httpClient;
        var response = client.GetAsync($"{url}/{id}");

        if (response.Result.IsSuccessStatusCode)
        {
            var content = response.Result.Content.ReadAsStringAsync();
            var plano = JsonConvert.DeserializeObject<PlanoViewModel>(content.Result);
            if (plano == null)
            {
                throw new HttpRequestException("Erro ao desserializar o plano.");
            }
            return plano;
        }
        throw new HttpRequestException($"Erro ao buscar o plano. {response.Result.StatusCode}");
    }  

    public async Task<PlanoViewModel> CreatePlano(PlanoViewModel plano)
    {
        var client = _httpClient;
        var response = await client.PostAsJsonAsync($"{url}", plano);

        if (response.IsSuccessStatusCode)
        {
            var content = response.Content.ReadAsStringAsync();
            var planoCriado = JsonConvert.DeserializeObject<PlanoViewModel>(content.Result);
            if (planoCriado == null)
            {
                throw new HttpRequestException("Erro ao desserializar o plano criado.");
            }
            return planoCriado;
        }
        throw new HttpRequestException($"Erro ao criar o plano. {response.StatusCode}");
    }

    public async Task<PlanoViewModel> UpdatePlano(int id, PlanoViewModel plano)
    {
        var client = _httpClient;

        var response = await client.PutAsJsonAsync($"{url}/{id}", plano);

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();

            var planoAtualizado = JsonConvert.DeserializeObject<PlanoViewModel>(content);
            if (planoAtualizado == null)
            {
                throw new HttpRequestException("Erro ao desserializar o plano atualizado.");
            }
            return planoAtualizado;
        }
        throw new HttpRequestException($"Erro ao atualizar o plano. {response.StatusCode}");
    }

    public async Task<bool> DeletePlano(int id)
    {
        var client = _httpClient;

        var response = await client.DeleteAsync($"{url}/{id}");

        if (response.IsSuccessStatusCode)
        {
            return true;
        } 
        return false;
    }   
}
