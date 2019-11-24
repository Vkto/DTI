using Dados;
using Dependencia;
using Enumerador;
using Interface;
using Repositorio;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace DTI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("pt-BR");

            var stackTelas = new Stack<ITela>(2);

            stackTelas.Push(Injetor.PreencherTela());

            var telaAtual = stackTelas.Peek();

            while (stackTelas.TryPeek(out telaAtual))
            {
                Console.Clear();

                Console.WriteLine("SIMULAÇÂO DE JUROS COMPOSTO : A qualquer instante digite \"SAIR\" para sair do sistema e \"VOLTAR\" para voltar à tela anterior");
                Console.WriteLine();
                Console.WriteLine("Digite a opção do menu desejado e der ENTER");
                Console.WriteLine();
                telaAtual.Renderizar();
                Console.WriteLine();

                var entrada = Console.ReadLine();

                if (entrada.ToUpperInvariant() == BotoesEnum.SAIR.ToString())
                {
                    return;
                }
                if (entrada.ToUpperInvariant() == BotoesEnum.VOLTAR.ToString())
                {
                    if (!stackTelas.TryPop(out telaAtual))
                        return;
                    continue;
                }
                var telaAcao = telaAtual.TratarEntradaValor(entrada);

                if (telaAcao != null)
                {
                    stackTelas.Push(telaAcao);
                }
            }
        }
    }
}
