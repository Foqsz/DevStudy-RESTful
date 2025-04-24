using DevStudy.FrontEnd.DevStudyFrontEnd.Application.Interface;
using DevStudy.FrontEnd.DevStudyFrontEnd.Core.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace DevStudy.FrontEnd.DevStudyFrontEnd.Application.Service;

public class ProfessorService : IProfessorService
{
    private readonly HttpClient _httpClient;
    private const string url = "https://localhost:7238/api/Instrutor";
    private readonly JsonSerializerOptions _serializerOptions;

    public ProfessorService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _serializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<IEnumerable<InstrutorViewModel>> GetProfessores()
    {
        HttpClient client = HttpClient();

        var response = await client.GetAsync($"{url}");

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var professores = JsonConvert.DeserializeObject<IEnumerable<InstrutorViewModel>>(content);
            if (professores == null)
            {
                return null;
            }
            return professores;
        }

        return null;
    }

    public async Task<InstrutorViewModel> GetProfessorById(int id)
    {
        HttpClient client = HttpClient();

        var response = await client.GetAsync($"{url}/{id}");

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var professor = JsonConvert.DeserializeObject<InstrutorViewModel>(content);
            if (professor == null)
            {
                throw new HttpRequestException("Erro ao desserializar o professor.");
            }
            return professor;
        }
        return null;
    }

    public async Task<InstrutorViewModel> CreateProfessor(InstrutorViewModel professor)
    {
        HttpClient client = HttpClient();

        var response = await client.PostAsJsonAsync($"{url}", professor);

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var professorCriado = JsonConvert.DeserializeObject<InstrutorViewModel>(content);
            if (professorCriado == null)
            {
                throw new HttpRequestException("Erro ao desserializar o professor.");
            }
            return professorCriado;
        }

        throw new HttpRequestException($"Erro ao criar o professor. {response.StatusCode}");
    }

    public async Task<InstrutorViewModel> UpdateProfessor(int id, InstrutorViewModel professor)
    {
        HttpClient client = HttpClient();

        var professorJson = new StringContent(JsonConvert.SerializeObject(professor), Encoding.UTF8, "application/json");

        var response = await client.PutAsync($"{url}/{id}", professorJson);

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var professorAtualizado = JsonConvert.DeserializeObject<InstrutorViewModel>(content);
            if (professorAtualizado == null)
            {
                throw new HttpRequestException("Erro ao desserializar o professor.");
            }
            return professorAtualizado;
        }
        throw new HttpRequestException($"Erro ao atualizar o professor. {response.StatusCode}");
    } 

    public Task<bool> DeleteProfessor(int id)
    {
        HttpClient client = HttpClient();
        var response = client.DeleteAsync($"{url}/{id}");

        if (response.Result.IsSuccessStatusCode)
        {
            return Task.FromResult(true);
        }
        else
        {
            throw new HttpRequestException($"Erro ao deletar o professor. {response.Result.StatusCode}");
        } 
    }

    private HttpClient HttpClient()
    {
        return _httpClient;
    }
}
