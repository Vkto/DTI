using Interface;
using Repositorio;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;

namespace Telas
{
    public class Telas : TelasConsole<ITela>, ITela
    {
        public Telas(ServiceContainer container)
        {
            Itens = new Dictionary<int, TelaItem<ITela>>() {
                {1,new TelaItem<ITela>(){Descricao = "1 - Simular Juros", TelaSelecionada = () => new SimulacaoJuros(container.GetService(typeof(IJurosRepositorio)) as IJurosRepositorio)} },
                {2,new TelaItem<ITela>(){Descricao = "2 - Consultar Juros", TelaSelecionada = () => new ConsultaJuros(container.GetService(typeof(IJurosRepositorio))as IJurosRepositorio)} },
              
            };
        }
        public void Renderizar()
        {
            foreach (var menuItens in Itens)
            {
                Console.WriteLine($"{menuItens.Value.Descricao}");
            }
        }

        public ITela TratarEntradaValor(string linha)
        {
            VerificarValorInformado(linha);
            if (int.TryParse(linha, out int opcao) && Itens.TryGetValue(opcao, out TelaItem<ITela> item))
            {
                return item.TelaSelecionada();
            }
            return null;
        }

        private void VerificarValorInformado(string linha)
        {
            try
            {
                int.Parse(linha);
            }
            catch (Exception)
            {
                Console.WriteLine();
                Console.WriteLine("Favor digitar um valor válido");
                Console.WriteLine("Pressione ENTER para retornar");
                Console.ReadLine();
            }
        }
    }
}
