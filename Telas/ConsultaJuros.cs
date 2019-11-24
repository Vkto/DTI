using Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Telas
{
    public class ConsultaJuros : ITela
    {
        IJurosRepositorio _jurosRepositorio;

        public ConsultaJuros(IJurosRepositorio jurosRepositorio)
        {
            _jurosRepositorio = jurosRepositorio;
        }
        public void Renderizar()
        {
            var historico = this._jurosRepositorio.ListarHistorico();
            if (historico.Count == 0)
            {
                Console.WriteLine("Não existe histórico de simulações para ser mostrado");
                
                Console.WriteLine("Digite a opção de retorno conforme o desejado (SAIR - VOLTAR)");
            }
            else
            {
                historico.ForEach(i =>
                {
                    Console.WriteLine($"Simulação efetuada às { i.HorasSimulado}");
                    Console.WriteLine();
                    Console.WriteLine($"Aporte Mensal de R${Math.Round(i.AporteMensal),2} em {i.Meses} mes(es) que resultou num Patrimônio Acumulado em Juros Compostos de R${Math.Round(i.PatrimonioJurosComposto,2)} " +
                        $"e um Patrimônio Acumulado em Juros Simples de R${Math.Round(i.PatrimonioJurosSimples,2)} ");
                    Console.WriteLine();
                });
                Console.WriteLine();
                Console.WriteLine("Digite a opção de retorno conforme o desejado (SAIR - VOLTAR)");
            }
        }
        public ITela TratarEntradaValor(string linha)
        {
            return null;
        }
    }
}
