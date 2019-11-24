using Apresentacao;
using Dados;
using Fachada;
using Interface;
using Repositorio;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace TesteConsole
{
    public class VerificarInput
    {
        private IJurosFachada _fachada;
        private IJurosRepositorio _jurosRepositorio;

        public VerificarInput()
        {
            _jurosRepositorio = new JurosRepositorio();
            _fachada = new JurosFachada();
        }
        [Fact]
        public void VerificarInsert()
        {
            var fachada = new JurosFachada();

            JurosApresentacao retorno = InserirItens().GetAwaiter().GetResult();

            Assert.NotNull(retorno);

        }
        [Fact]
        public void VerificarListaRetornoNotNull()
        {
            var retorno = InserirItens().GetAwaiter().GetResult();
            var saida = new JurosSaida
            {
                Meses = retorno.TempoResgate,
                AporteMensal = 100,
                HorasSimulado = DateTime.Now,
                PatrimonioJurosComposto = retorno.JurosComposto,
                PatrimonioJurosSimples = retorno.JurosSimples,
            };
            this._jurosRepositorio.Salvar(saida);

            var historico = _jurosRepositorio.ListarHistorico().Count() > 0;

            Assert.True(historico);
        }
        [Fact]
        public void VerificarListaRetornoNull()
        {
            var historico = _jurosRepositorio.ListarHistorico().Count() > 0;

            Assert.False(historico);
        }
        private async Task<JurosApresentacao> InserirItens()
        {
            var dados = new JurosDados { TaxaJurosMensal = 2, TempoResgate = 1, ValorAporteMensal = 100 };
            return await _fachada.GetJurosComposto(dados);
        }


    }
}
