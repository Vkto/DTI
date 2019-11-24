using Apresentacao;
using Dados;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Interface
{
    public interface IJurosFachada
    {
        Task<JurosApresentacao> GetJurosComposto(JurosDados entrada);
    }
}
