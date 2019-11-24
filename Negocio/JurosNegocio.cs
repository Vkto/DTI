using Apresentacao;
using Dados;
using Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Negocio
{
    public class JurosNegocio : IJurosNegocio
    {
        public async Task<JurosApresentacao> CalcularJurosComposto(JurosDados entrada)
        {
            decimal montante = entrada.ValorAporteMensal;
            decimal jurosSimples = 0;
            decimal jurosComposto = 0;
            decimal valorInicialMontante = 0;

            jurosSimples = CalcularJurosSimples(entrada, montante, jurosSimples);

            montante = entrada.ValorAporteMensal;

            jurosComposto = CalcularJurosComposto(entrada, montante, jurosComposto, valorInicialMontante);

            var jurosRetorno = new JurosApresentacao
            {
                JurosComposto = decimal.Parse(jurosComposto.ToString()),
                JurosSimples = decimal.Parse(jurosSimples.ToString()),
                TempoResgate = entrada.TempoResgate
            };

            return jurosRetorno;
        }

        private static decimal CalcularJurosComposto(JurosDados entrada, decimal montante, decimal jurosComposto, decimal valorInicialMontante)
        {
            for (int i = 0; i < CalcularMeses(entrada); i++)
            {
                valorInicialMontante = i == 0 ? (CalcularPorcentagemSobreMontante(montante, decimal.Parse(entrada.TaxaJurosMensal.ToString()))) + entrada.ValorAporteMensal : valorInicialMontante;
                jurosComposto += i == 0 ? valorInicialMontante : (montante * decimal.Parse(entrada.TaxaJurosMensal.ToString()) / 100) + valorInicialMontante;

                montante = jurosComposto;
            }
            return jurosComposto;
        }

        private static decimal CalcularJurosSimples(JurosDados entrada, decimal montante, decimal jurosSimples)
        {
            for (int i = 0; i < CalcularMeses(entrada); i++)
            {
                jurosSimples += (CalcularPorcentagemSobreMontante(montante, decimal.Parse(entrada.TaxaJurosMensal.ToString()))) + montante;
            }

            return jurosSimples;
        }

        private static decimal CalcularPorcentagemSobreMontante(decimal montante, decimal taxaJurosMensal)
        {
            return montante * taxaJurosMensal / 100;
        }

        private static int CalcularMeses(JurosDados entrada)
        {
            return entrada.TempoResgate * 12;
        }

    }
}
