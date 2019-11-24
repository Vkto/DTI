using System;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Apresentacao;
using Dados;
using Fachada;
using Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Repositorio;
using System.Net.Http;

namespace Teste
{
    [TestClass]
    public class VerificarInputMenu
    {
        private IJurosFachada _fachada;
        private IJurosRepositorio _jurosRepositorio;

        public VerificarInputMenu()
        {
            _jurosRepositorio = new JurosRepositorio();
            _fachada = new JurosFachada();
        }
        [TestMethod]
        public void VerificarItens()
        {
            var fachada = new JurosFachada();

            Task<JurosApresentacao> retorno = InserirItens();

            Assert.IsNotNull(retorno);
        }

        private async Task<JurosApresentacao> InserirItens()
        {
            var fachada = new Fachada.JurosFachada();
            var dados = new JurosDados { TaxaJurosMensal = 2, TempoResgate = 1, ValorAporteMensal = 100 };
            return  await fachada.GetJurosComposto(dados);
        }

        [TestMethod]
        public void ValidarRetornoComItens()
        {
           
            var insert = InserirItens();
            var retorno = _jurosRepositorio.ListarHistorico();
            Assert.IsNotNull(retorno);
        }
        public void validarRetornoSemItens()
        {
            var retorno = _jurosRepositorio.ListarHistorico();
            Assert.IsNull(retorno);
        }
    }
}
