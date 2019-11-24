using Apresentacao;
using Dados;
using Fachada;
using Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositorio
{
    public class JurosRepositorio : IJurosRepositorio
    {
        private IJurosFachada _jurosFachada;
        private List<JurosSaida> _historico;


        public JurosRepositorio()
        {
            _jurosFachada = new JurosFachada();
            _historico = new List<JurosSaida>();
        }
        public async Task<JurosApresentacao> PostJurosRepositorio(JurosDados entrada)
        {
            return await _jurosFachada.GetJurosComposto(entrada);
        }
        public void Salvar(JurosSaida juros)
        {
            this._historico.Add(juros);
        }
        public List<JurosSaida> ListarHistorico()
        {
            return this._historico;
        }
    }
}
