using DevStudy.FrontEnd.DevStudyFrontEnd.Application.Interface;
using DevStudy.FrontEnd.DevStudyFrontEnd.Core.Models;
using Newtonsoft.Json;
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
            return JsonConvert.DeserializeObject<IEnumerable<AlunoViewModel>>(content);
        }

        throw new HttpRequestException($"Erro ao buscar os veículos. {response.StatusCode}");
    }

    public Task<AlunoViewModel> GetAluno(int id)
    {
        throw new NotImplementedException();
    }

    public Task<AlunoViewModel> GetAlunoByEmail(string email)
    {
        throw new NotImplementedException();
    }

    public Task<AlunoViewModel> AddAluno(AlunoViewModel aluno)
    {
        throw new NotImplementedException();
    }

    public Task<AlunoViewModel> UpdateAluno(int id, AlunoViewModel aluno)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAluno(int id)
    {
        throw new NotImplementedException();
    } 
}
