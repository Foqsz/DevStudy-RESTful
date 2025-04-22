using DevStudy.FrontEnd.DevStudyFrontEnd.Application.Interface;
using DevStudy.FrontEnd.DevStudyFrontEnd.Core.Models;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json;

namespace DevStudy.FrontEnd.DevStudyFrontEnd.Application.Service;

public class TreinoExercicioService : ITreinoExercicioService
{
    private readonly HttpClient _httpClient;
    private const string url = "https://localhost:7238/api/TreinoExercicio";
    private readonly JsonSerializerOptions _serializerOptions;

    public TreinoExercicioService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _serializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    } 

    public async Task<IEnumerable<TreinoExercicioViewModel>> GetTreinoExercicios()
    {
        var client = _httpClient;

        var response = await client.GetAsync($"{url}");

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var treinos = JsonConvert.DeserializeObject<IEnumerable<TreinoExercicioViewModel>>(content);
            if (treinos == null)
            {
                return null;
            }
            return treinos;
        }

        return null;
    }

    public async Task<TreinoExercicioViewModel> GetTreinoExercicioById(int id)
    {
        var client = _httpClient;

        var response = await client.GetAsync($"{url}/{id}");

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var treino = JsonConvert.DeserializeObject<TreinoExercicioViewModel>(content);
            if (treino == null)
            {
                throw new HttpRequestException("Erro ao desserializar o treino.");
            }
            return treino;
        }

        throw new HttpRequestException($"Erro ao buscar o treino. {response.StatusCode}");
    }

    public async Task<TreinoExercicioViewModel> CreateTreinoExercicio(TreinoExercicioViewModel treinoExercicio)
    {
        var client = _httpClient;

        var response = await client.PostAsJsonAsync($"{url}", treinoExercicio);

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var addTreino = JsonConvert.DeserializeObject<TreinoExercicioViewModel>(content);
            if (addTreino == null)
            {
                throw new HttpRequestException("Erro ao desserializar o treino adicionado.");
            }
            return addTreino;
        }

        throw new HttpRequestException($"Erro ao adicionar o treino. {response.StatusCode}");
    }

    public async Task<TreinoExercicioViewModel> UpdateTreinoExercicio(int id, TreinoExercicioViewModel treinoExercicio)
    {
        var client = _httpClient;
        var treinoJson = new StringContent(JsonConvert.SerializeObject(treinoExercicio), Encoding.UTF8, "application/json");

        var response = await client.PatchAsync($"{url}/{id}", treinoJson);

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var updateTreino = JsonConvert.DeserializeObject<TreinoExercicioViewModel>(content);
            if (updateTreino == null)
            {
                throw new HttpRequestException("Erro ao desserializar o aluno atualizado.");
            }
            return updateTreino;
        }

        throw new HttpRequestException($"Erro ao fazer update no aluno. {response.StatusCode}");
    }
    public async Task<bool> DeleteTreinoExercicio(int id)
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
