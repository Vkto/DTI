using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Apresentacao;
using Dados;
using Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Negocio;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JurosController : ControllerBase
    {
        private IJurosNegocio _jurosNegocio;
        public JurosController(IJurosNegocio jurosNegocio)
        {
            _jurosNegocio = jurosNegocio;
        }
        [HttpPost]
        public async Task<JurosApresentacao> Get(JurosDados juros)
        {
            return _jurosNegocio.CalcularJurosComposto(juros).GetAwaiter().GetResult();
        }
    }
}