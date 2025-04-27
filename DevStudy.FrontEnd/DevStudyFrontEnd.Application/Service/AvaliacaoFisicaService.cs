using DevStudy.FrontEnd.DevStudyFrontEnd.Application.Interface;
using DevStudy.FrontEnd.DevStudyFrontEnd.Core.Models;
using Newtonsoft.Json;
using System.Net;
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
                    return null; 
                }
                return avaliacoes;
            }
            return null;

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
                    return null;
                }
                return avaliacoes;
            }
           return null; 
        }

        public async Task<AvaliacaoFisicaViewModel> CreateAvaliacaoFisica(AvaliacaoFisicaViewModel avaliacaoFisica)
        {
            var client = _httpClient;

            // Ensure decimal values are correctly formatted for JSON serialization
            var jsonContent = JsonConvert.SerializeObject(avaliacaoFisica, new JsonSerializerSettings
            {
                Culture = System.Globalization.CultureInfo.InvariantCulture
            });

            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"{url}", content);

            Console.WriteLine("Status da resposta HTTP: " + response.StatusCode);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                var avaliacao = JsonConvert.DeserializeObject<AvaliacaoFisicaViewModel>(responseContent);
                if (avaliacao == null)
                {
                    return null;
                }

                return avaliacao;
            }

            return null;
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
                    return null;
                }
                return avaliacao;
            }
            return null;
        }

        public async Task<bool> DeleteAvaliacaoFisica(int id)
        {
            var client = _httpClient;

            var response = await client.DeleteAsync($"{url}?id={id}");

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }
    }
}
