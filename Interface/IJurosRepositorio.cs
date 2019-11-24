using Apresentacao;
using Dados;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Interface
{
    public interface IJurosRepositorio
    {
        Task<JurosApresentacao> PostJurosRepositorio(JurosDados entrada);
        List<JurosSaida> ListarHistorico();
        void Salvar(JurosSaida juros);
    }
}
