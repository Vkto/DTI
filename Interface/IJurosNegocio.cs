using Apresentacao;
using Dados;
using System;
using System.Threading.Tasks;

namespace Interface
{
    public interface IJurosNegocio
    {
        Task<JurosApresentacao> CalcularJurosComposto(JurosDados entrada);
    }
}
