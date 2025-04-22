using DevStudy.FrontEnd.DevStudyFrontEnd.Application.Interface;
using DevStudy.FrontEnd.DevStudyFrontEnd.Core.Models;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json;

namespace DevStudy.FrontEnd.DevStudyFrontEnd.Application.Service;

public class TreinosService : ITreinosService
{
    private readonly HttpClient _httpClient;
    private const string url = "https://localhost:7238/api/Treinos";
    private readonly JsonSerializerOptions _serializerOptions;

    public TreinosService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _serializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<IEnumerable<TreinoViewModel>> GetTreinos()
    {
        var client = _httpClient;
        var response = await client.GetAsync($"{url}");
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var treinos = JsonConvert.DeserializeObject<IEnumerable<TreinoViewModel>>(content);
            if (treinos == null)
            {
                return null;
            }
            return treinos;
        }
        return null;
    }

    public async Task<TreinoViewModel> GetTreinoById(int id)
    {
        var client = _httpClient;
        var response = await client.GetAsync($"{url}/{id}");
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var treino = JsonConvert.DeserializeObject<TreinoViewModel>(content);
            if (treino == null)
            {
                throw new HttpRequestException("Erro ao desserializar o treino.");
            }
            return treino;
        }
        throw new HttpRequestException($"Erro ao buscar o treino. {response.StatusCode}");
    }

    public async Task<TreinoViewModel> CreateTreino(TreinoViewModel treino)
    { 
        var client = _httpClient; 

        var response = await client.PostAsJsonAsync($"{url}", treino);

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var addTreino = JsonConvert.DeserializeObject<TreinoViewModel>(content);
            if (addTreino == null)
            {
                throw new HttpRequestException("Erro ao desserializar o treino adicionado.");
            }
            return addTreino;
        }
        throw new HttpRequestException($"Erro ao adicionar o treino. {response.StatusCode}");
    }

    public async Task<TreinoViewModel> UpdateTreino(int id, TreinoViewModel treino)
    {
        var client = _httpClient;
        var content = new StringContent(JsonConvert.SerializeObject(treino), Encoding.UTF8, "application/json");
        var response = await client.PutAsync($"{url}/{id}", content);
        if (response.IsSuccessStatusCode)
        {
            var updateTreino = JsonConvert.DeserializeObject<TreinoViewModel>(await response.Content.ReadAsStringAsync());
            if (updateTreino == null)
            {
                throw new HttpRequestException("Erro ao desserializar o treino atualizado.");
            }
            return updateTreino;
        }
        throw new HttpRequestException($"Erro ao atualizar o treino. {response.StatusCode}");
    }

    public async Task<bool> DeleteTreino(int id)
    {
        var client = _httpClient;
        var response = await client.DeleteAsync($"{url}/{id}");
        if (response.IsSuccessStatusCode)
        {
            return true;
        }
        throw new HttpRequestException($"Erro ao deletar o treino. {response.StatusCode}");
    }
}
