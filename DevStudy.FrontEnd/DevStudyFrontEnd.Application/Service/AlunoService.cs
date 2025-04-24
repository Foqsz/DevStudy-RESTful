using DevStudy.FrontEnd.DevStudyFrontEnd.Application.Interface;
using DevStudy.FrontEnd.DevStudyFrontEnd.Core.Models;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json;

namespace DevStudy.FrontEnd.DevStudyFrontEnd.Application.Service;

public class AlunoService : IAlunoService
{
    private readonly HttpClient _httpClient;
    private const string url = "https://localhost:7238/api/Aluno";
    private readonly JsonSerializerOptions _serializerOptions;

    public AlunoService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _serializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<IEnumerable<AlunoViewModel>> GetAlunos()
    {
        var client = _httpClient;

        var response = await client.GetAsync($"{url}");

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var alunos = JsonConvert.DeserializeObject<IEnumerable<AlunoViewModel>>(content);
            if (alunos == null)
            {
                throw new HttpRequestException("Erro ao desserializar a lista de alunos.");
            }
            return alunos;
        }

        throw new HttpRequestException($"Erro ao buscar os alunos. {response.StatusCode}");
    }

    public async Task<AlunoViewModel> GetAluno(int id)
    {
        var client = _httpClient;

        var response = await client.GetAsync($"{url}/{id}");

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var aluno = JsonConvert.DeserializeObject<AlunoViewModel>(content);
            if (aluno == null)
            {
                throw new HttpRequestException("Erro ao desserializar o aluno.");
            }
            return aluno;
        }

        return null;
    }

    public async Task<AlunoViewModel> GetAlunoByEmail(string email)
    {
        var client = _httpClient;

        var response = await client.GetAsync($"{url}/email/{email}");

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var aluno = JsonConvert.DeserializeObject<AlunoViewModel>(content);
            if (aluno == null)
            {
                throw new HttpRequestException("Erro ao desserializar o aluno.");
            }
            return aluno;
        }

        return null;
    }

    public async Task<AlunoViewModel> AddAluno(AlunoViewModel aluno)
    {
        var client = _httpClient;

        var response = await client.PostAsJsonAsync($"{url}", aluno);

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var addedAluno = JsonConvert.DeserializeObject<AlunoViewModel>(content);
            if (addedAluno == null)
            {
                throw new HttpRequestException("Erro ao desserializar o aluno adicionado.");
            }
            return addedAluno;
        }

        throw new HttpRequestException($"Erro ao adicionar o aluno. {response.StatusCode}");
    }

    public async Task<AlunoViewModel> UpdateAluno(int id, AlunoViewModel aluno)
    {
        var client = _httpClient;
        var alunoJson = new StringContent(JsonConvert.SerializeObject(aluno), Encoding.UTF8, "application/json");

        var response = await client.PatchAsync($"{url}/{id}", alunoJson);

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var updatedAluno = JsonConvert.DeserializeObject<AlunoViewModel>(content);
            if (updatedAluno == null)
            {
                throw new HttpRequestException("Erro ao desserializar o aluno atualizado.");
            }
            return updatedAluno;
        }

        throw new HttpRequestException($"Erro ao fazer update no aluno. {response.StatusCode}");
    }

    public async Task<bool> DeleteAluno(int id)
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
