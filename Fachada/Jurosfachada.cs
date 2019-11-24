using Apresentacao;
using Dados;
using Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Fachada
{
    public class JurosFachada : IJurosFachada
    {
        HttpClient client = new HttpClient();

        public JurosFachada()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            client = new HttpClient(clientHandler);
            client.DefaultRequestHeaders.ExpectContinue = false;
            client.BaseAddress = new Uri("https://localhost:5001/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        }
        public async Task<JurosApresentacao> GetJurosComposto(JurosDados entrada)
        {
            JurosApresentacao retorno = new JurosApresentacao();

            if (entrada.TaxaJurosMensal <= 0 || entrada.TempoResgate <= 0 || entrada.ValorAporteMensal <= 0)
            {
                retorno.Erro = "Os valores informados devem ser maiores que 0.";
                throw new Exception("Os valores informados devem ser maiores que 0.");
            }
            else
            {
                HttpResponseMessage response = client.PostAsJsonAsync(
                      "api/juros", entrada).GetAwaiter().GetResult();

                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    retorno.Erro = null;
                    var dados = await response.Content.ReadAsStringAsync();
                    retorno = JsonConvert.DeserializeObject<JurosApresentacao>(dados);
                }
            }
            return retorno;
        }
        public async Task<List<JurosApresentacao>> GetHistoricoSimulacoes()
        {
            var retorno = new List<JurosApresentacao>();

            HttpResponseMessage response = client.GetAsync(
                "api/juros").GetAwaiter().GetResult();
            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                var dados = await response.Content.ReadAsStringAsync();
                retorno = JsonConvert.DeserializeObject<List<JurosApresentacao>>(dados);
            }
            return retorno;
        }
    }
}
