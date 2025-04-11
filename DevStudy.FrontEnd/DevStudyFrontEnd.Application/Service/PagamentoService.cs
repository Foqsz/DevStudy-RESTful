using DevStudy.FrontEnd.DevStudyFrontEnd.Application.Interface;
using DevStudy.FrontEnd.DevStudyFrontEnd.Core.Models;
using Newtonsoft.Json;
using System.Net;
using System.Text.Json;

namespace DevStudy.FrontEnd.DevStudyFrontEnd.Application.Service;

public class PagamentoService : IPagamentoService
{
    private readonly HttpClient _httpClient;
    private const string url = "https://localhost:7238/api/Pagamento";
    private readonly JsonSerializerOptions _serializerOptions;

    public PagamentoService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _serializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<IEnumerable<PagamentoViewModel>> GetPagamentos()
    {
        var client = _httpClient;

        var response = await client.GetAsync($"{url}");

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            // Nenhum pagamento encontrado, mas isso não é um erro fatal
            return Enumerable.Empty<PagamentoViewModel>();
        }

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();

            var pagamentos = JsonConvert.DeserializeObject<IEnumerable<PagamentoViewModel>>(content);

            return pagamentos ?? Enumerable.Empty<PagamentoViewModel>();
        }

        // Outros erros (500, 403, etc)
        throw new HttpRequestException($"Erro ao buscar os pagamentos. {response.StatusCode}");
    }


    public async Task<PagamentoViewModel> GetPagamento(int id)
    {
        var client = _httpClient;

        var response = await client.GetAsync($"{url}/{id}");

        if (response.IsSuccessStatusCode)
        {
            var pagamentoId = await ReadContent<PagamentoViewModel>(response);

            return pagamentoId;
        }

        throw new HttpRequestException($"Erro ao buscar o pagamento com ID {id}. {response.StatusCode}");
    } 

    public async Task<PagamentoViewModel> CreatePagamento(PagamentoViewModel pagamento)
    {
        var client = _httpClient; 

        var response = await client.PostAsJsonAsync($"{url}", pagamento);

        if (response.IsSuccessStatusCode)
        {
            var pagamentoCreate = await ReadContent<PagamentoViewModel>(response);
            return pagamentoCreate;
        }
        throw new Exception($"Erro ao criar o pagamento. {response.StatusCode}");
    }

    public async Task<PagamentoViewModel> UpdatePagamento(int id, PagamentoViewModel pagamento)
    {
        var client = _httpClient;

        var response = await client.PutAsJsonAsync($"{url}/{id}", pagamento);

        if (response.IsSuccessStatusCode)
        {
            var pagamentoUpdate = await ReadContent<PagamentoViewModel>(response);
            return pagamentoUpdate;
        }
        throw new Exception($"Erro ao atualizar o pagamento com ID {id}. {response.StatusCode}");
    }

    public async Task<bool> DeletePagamento(int id)
    {
        var client = _httpClient;
        var response = await client.DeleteAsync($"{url}/{id}");

        if (response.IsSuccessStatusCode)
        {
            return true;
        }
        return false;
    }

    private async Task<T?> ReadContent<T>(HttpResponseMessage response)
    {
        var content = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(content);
    }

}
