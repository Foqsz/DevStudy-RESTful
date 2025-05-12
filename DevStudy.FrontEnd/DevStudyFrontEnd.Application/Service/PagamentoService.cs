using DevStudy.FrontEnd.DevStudyFrontEnd.Application.Interface;
using DevStudy.FrontEnd.DevStudyFrontEnd.Core.Models;
using Newtonsoft.Json;
using System.Net;
using System.Text.Json;

namespace DevStudy.FrontEnd.DevStudyFrontEnd.Application.Service;

public class PagamentoService : IPagamentoService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<PagamentoService> _logger;
    private const string url = "https://localhost:7238/api/Pagamento";
    private readonly JsonSerializerOptions _serializerOptions;

    public PagamentoService(HttpClient httpClient, ILogger<PagamentoService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
        _serializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<IEnumerable<PagamentoViewModel>> GetPagamentos()
    {
        _logger.LogInformation("Iniciando a busca de todos os pagamentos.");
        var client = _httpClient;

        var response = await client.GetAsync($"{url}");

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            _logger.LogWarning("Nenhum pagamento encontrado.");
            return Enumerable.Empty<PagamentoViewModel>();
        }

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var pagamentos = JsonConvert.DeserializeObject<IEnumerable<PagamentoViewModel>>(content);
            _logger.LogInformation("Pagamentos encontrados com sucesso.");
            return pagamentos ?? Enumerable.Empty<PagamentoViewModel>();
        }

        _logger.LogError("Erro ao buscar pagamentos. StatusCode: {StatusCode}", response.StatusCode);
        return null;
    }

    public async Task<PagamentoViewModel> GetPagamento(int id)
    {
        _logger.LogInformation("Iniciando a busca do pagamento com ID {Id}.", id);
        var client = _httpClient;

        var response = await client.GetAsync($"{url}/{id}");

        if (response.IsSuccessStatusCode)
        {
            var pagamentoId = await ReadContent<PagamentoViewModel>(response);
            _logger.LogInformation("Pagamento com ID {Id} encontrado com sucesso.", id);
            return pagamentoId;
        }

        _logger.LogError("Erro ao buscar o pagamento com ID {Id}. StatusCode: {StatusCode}", id, response.StatusCode);
        throw new HttpRequestException($"Erro ao buscar o pagamento com ID {id}. {response.StatusCode}");
    }

    public async Task<PagamentoViewModel> CreatePagamento(PagamentoViewModel pagamento)
    {
        _logger.LogInformation("Iniciando a criação de um novo pagamento.");
        var client = _httpClient;

        var response = await client.PostAsJsonAsync($"{url}", pagamento);

        if (response.IsSuccessStatusCode)
        {
            var pagamentoCreate = await ReadContent<PagamentoViewModel>(response);
            _logger.LogInformation("Pagamento criado com sucesso.");
            return pagamentoCreate;
        }

        _logger.LogError("Erro ao criar o pagamento. StatusCode: {StatusCode}", response.StatusCode);
        throw new Exception($"Erro ao criar o pagamento. {response.StatusCode}");
    }

    public async Task<PagamentoViewModel> UpdatePagamento(int id, PagamentoViewModel pagamento)
    {
        _logger.LogInformation("Iniciando a atualização do pagamento com ID {Id}.", id);
        var client = _httpClient;

        var response = await client.PutAsJsonAsync($"{url}/{id}", pagamento);

        if (response.IsSuccessStatusCode)
        {
            var pagamentoUpdate = await ReadContent<PagamentoViewModel>(response);
            _logger.LogInformation("Pagamento com ID {Id} atualizado com sucesso.", id);
            return pagamentoUpdate;
        }

        _logger.LogError("Erro ao atualizar o pagamento com ID {Id}. StatusCode: {StatusCode}", id, response.StatusCode);
        throw new Exception($"Erro ao atualizar o pagamento com ID {id}. {response.StatusCode}");
    }

    public async Task<bool> DeletePagamento(int id)
    {
        _logger.LogInformation("Iniciando a exclusão do pagamento com ID {Id}.", id);
        var client = _httpClient;
        var response = await client.DeleteAsync($"{url}/{id}");

        if (response.IsSuccessStatusCode)
        {
            _logger.LogInformation("Pagamento com ID {Id} excluído com sucesso.", id);
            return true;
        }

        _logger.LogError("Erro ao excluir o pagamento com ID {Id}. StatusCode: {StatusCode}", id, response.StatusCode);
        return false;
    }

    private async Task<T?> ReadContent<T>(HttpResponseMessage response)
    {
        var content = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(content);
    }
}
