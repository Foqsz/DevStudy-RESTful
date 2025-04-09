using DevStudy.FrontEnd.DevStudyFrontEnd.Application.Interface;
using DevStudy.FrontEnd.DevStudyFrontEnd.Core.Models;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json;

namespace DevStudy.FrontEnd.DevStudyFrontEnd.Application.Service
{
    public class AvaliacaoFisicaService : IAvaliacaoFisicaService
    {
        private readonly HttpClient _httpClient;
        private const string url = "https://localhost:7238/api/AvaliacaoFisica";
        private readonly JsonSerializerOptions _serializerOptions;

        public AvaliacaoFisicaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _serializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<IEnumerable<AvaliacaoFisicaViewModel>> GetAvaliacoesFisicas()
        {
            var client = _httpClient;

            var response = await client.GetAsync($"{url}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var avaliacoes = JsonConvert.DeserializeObject<IEnumerable<AvaliacaoFisicaViewModel>>(content);
                if (avaliacoes == null)
                {
                    throw new HttpRequestException("Erro ao desserializar a lista de avaliações físicas.");
                }
                return avaliacoes;
            }
            throw new HttpRequestException($"Erro ao buscar as avaliações físicas. {response.StatusCode}");

        }

        public async Task<AvaliacaoFisicaViewModel> GetAvaliacaoFisica(int id)
        {
            var client = _httpClient;

            var response = await client.GetAsync($"{url}/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var avaliacoes = JsonConvert.DeserializeObject<AvaliacaoFisicaViewModel>(content);
                if (avaliacoes == null)
                {
                    throw new HttpRequestException($"Erro ao desserializar a avaliação física id={id}.");
                }
                return avaliacoes;
            }
            throw new HttpRequestException($"Erro ao buscar a avaliação física id={id}. {response.StatusCode}");
        }

        public async Task<AvaliacaoFisicaViewModel> CreateAvaliacaoFisica(AvaliacaoFisicaViewModel avaliacaoFisica)
        {
            var client = _httpClient;

            Console.WriteLine("=== INÍCIO DO PROCESSO DE ENVIO DA AVALIAÇÃO FÍSICA ===");
            Console.WriteLine("Dados do objeto antes da serialização:");
            Console.WriteLine($"Id: {avaliacaoFisica.Id}");
            Console.WriteLine($"AlunoId: {avaliacaoFisica.AlunoId}");
            Console.WriteLine($"Data: {avaliacaoFisica.Data}");
            Console.WriteLine($"Peso: {avaliacaoFisica.Peso}");
            Console.WriteLine($"Altura: {avaliacaoFisica.Altura}");

            // Ensure decimal values are correctly formatted for JSON serialization
            var jsonContent = JsonConvert.SerializeObject(avaliacaoFisica, new JsonSerializerSettings
            {
                Culture = System.Globalization.CultureInfo.InvariantCulture
            });

            Console.WriteLine("JSON enviado:");
            Console.WriteLine(jsonContent);

            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"{url}", content);

            Console.WriteLine("Status da resposta HTTP: " + response.StatusCode);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                Console.WriteLine("JSON recebido:");
                Console.WriteLine(responseContent);

                var avaliacao = JsonConvert.DeserializeObject<AvaliacaoFisicaViewModel>(responseContent);
                if (avaliacao == null)
                {
                    throw new HttpRequestException("Erro ao desserializar a avaliação física.");
                }

                Console.WriteLine("Dados Recebidos após desserialização:");
                Console.WriteLine($"Id: {avaliacao.Id}");
                Console.WriteLine($"AlunoId: {avaliacao.AlunoId}");
                Console.WriteLine($"Data: {avaliacao.Data}");
                Console.WriteLine($"Peso: {avaliacao.Peso}");
                Console.WriteLine($"Altura: {avaliacao.Altura}");

                Console.WriteLine("=== FIM DO PROCESSO ===");

                return avaliacao;
            }

            throw new HttpRequestException($"Erro ao criar a avaliação física. {response.StatusCode}");
        }


        public async Task<AvaliacaoFisicaViewModel> UpdateAvaliacaoFisica(int id, AvaliacaoFisicaViewModel avaliacaoFisica)
        {
            var client = _httpClient;

            var response = await client.PutAsJsonAsync($"{url}/{id}", avaliacaoFisica);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var avaliacao = JsonConvert.DeserializeObject<AvaliacaoFisicaViewModel>(content);
                if (avaliacao == null)
                {
                    throw new HttpRequestException("Erro ao desserializar a avaliação física.");
                }
                return avaliacao;
            }
            throw new HttpRequestException($"Erro ao atualizar a avaliação física. {response.StatusCode}");
        }

        public async Task<bool> DeleteAvaliacaoFisica(int id)
        {
            var client = _httpClient;

            var response = await client.DeleteAsync($"{url}/{id}");

            if(response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                throw new HttpRequestException($"Erro ao deletar a avaliação física. {response.StatusCode}");
            } 
        }
    }
}
