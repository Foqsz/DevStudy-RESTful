using DevStudy.FrontEnd.DevStudyFrontEnd.Application.Interface;
using DevStudy.FrontEnd.DevStudyFrontEnd.Core.Models;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json;

namespace DevStudy.FrontEnd.DevStudyFrontEnd.Application.Service;

public class ExerciciosService : IExercicioService
{
    private readonly HttpClient _httpClient;
    private const string url = "https://localhost:7238/api/Exercicios";
    private readonly JsonSerializerOptions _serializerOptions;

    public ExerciciosService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _serializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<IEnumerable<ExercicioViewModel>> GetExercicios()
    {
        var client = _httpClient;

        var response = await client.GetAsync($"{url}");

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var exercicios = JsonConvert.DeserializeObject<IEnumerable<ExercicioViewModel>>(content);
            if (exercicios == null)
            {
                throw new HttpRequestException("Erro ao desserializar a lista de exercicios.");
            }
            return exercicios;
        }

        throw new HttpRequestException($"Erro ao buscar os exercicios. {response.StatusCode}");
    }

    public async Task<ExercicioViewModel> GetExercicioById(int id)
    {
        var client = _httpClient;

        var response = await client.GetAsync($"{url}/{id}");

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var exercicio = JsonConvert.DeserializeObject<ExercicioViewModel>(content);
            if (exercicio == null)
            {
                throw new HttpRequestException("Erro ao desserializar o aluno.");
            }
            return exercicio;
        }

        throw new HttpRequestException($"Erro ao buscar o aluno. {response.StatusCode}");
    }

    public async Task<ExercicioViewModel> CreateExercicio(ExercicioViewModel exercicio)
    {
        var client = _httpClient;

        var response = await client.PostAsJsonAsync($"{url}", exercicio);

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var addExercicio = JsonConvert.DeserializeObject<ExercicioViewModel>(content);
            if (addExercicio == null)
            {
                throw new HttpRequestException("Erro ao desserializar o exercicio adicionado.");
            }
            return addExercicio;
        }

        throw new HttpRequestException($"Erro ao adicionar o exercicio. {response.StatusCode}");
    }

    public async Task<ExercicioViewModel> UpdateExercicio(int id, ExercicioViewModel exercicio)
    {
        var client = _httpClient;
        var exercicioJson = new StringContent(JsonConvert.SerializeObject(exercicio), Encoding.UTF8, "application/json");

        var response = await client.PatchAsync($"{url}/{id}", exercicioJson);

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var updatedExercicio = JsonConvert.DeserializeObject<ExercicioViewModel>(content);
            if (updatedExercicio == null)
            {
                throw new HttpRequestException("Erro ao desserializar o exercicio atualizado.");
            }
            return updatedExercicio;
        }

        throw new HttpRequestException($"Erro ao fazer update no exercicio. {response.StatusCode}");
    }

    public async Task<bool> DeleteExercicio(int id)
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
 
 