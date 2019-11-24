using System;
using System.Collections.Generic;
using System.Text;

namespace Apresentacao
{
    public class JurosSaida
    {
        public decimal AporteMensal { get; set; }
        public decimal PatrimonioJurosSimples { get; set; }
        public decimal PatrimonioJurosComposto { get; set; }
        public int Meses { get; set; }
        public DateTime HorasSimulado { get; set; }
    }
}
