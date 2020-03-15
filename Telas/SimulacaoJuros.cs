using Apresentacao;
using Dados;
using Enumerador;
using Interface;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Telas
{
    public class SimulacaoJuros : ITela
    {
        IJurosRepositorio _jurosRepositorio;
        private InfoJurosDados _dadoEditado;
        private bool _dadosSendoEditados;
        private bool _dadosProntosParaSalvar;
        private string _erro;

        private InfoJurosDados[] _dadosJuros = new InfoJurosDados[]
        {
            new InfoJurosDados(){ Descricao = "Valor Aporte Mensal.", NumeroMenu=1},
            new InfoJurosDados(){ Descricao = "Taxa de juros mensal do investimento contratado.", NumeroMenu =2},
            new InfoJurosDados(){ Descricao = "Tempo previsto para pagamento (em ano).", NumeroMenu=3}
        };
        public SimulacaoJuros(IJurosRepositorio jurosRepositorio)
        {
            _jurosRepositorio = jurosRepositorio;
        }

        public void Renderizar()
        {
            _dadosJuros.ToList().ForEach(i =>
            Console.WriteLine($"{i.NumeroMenu} - {i.Descricao} {i.Valor}")
            );
            if (_dadosJuros.Select(i => i.Valor).Where(i => string.IsNullOrEmpty(i)).Count() == 0 && !_dadosSendoEditados)
            {
                _dadosProntosParaSalvar = true;
                Console.WriteLine();
                Console.WriteLine("Deseja realizar a simulação de Juros? Digite SIM para simular.");
            }
            if (_dadosSendoEditados)
            {
                Console.WriteLine();
                Console.WriteLine(_dadoEditado.Descricao);
            }
            Console.WriteLine();
        }
        public ITela TratarEntradaValor(string linha)
        {
            PreencherEdicao(linha);
            VerificarSalvamento(linha);
            return null;
        }

        private void VerificarSalvamento(string linha)
        {
            if (_dadosProntosParaSalvar && linha?.ToUpperInvariant() == BotoesEnum.SIM.ToString())
            {
                Console.WriteLine();
                Console.WriteLine("Simulando...");
                Console.WriteLine();
                var simulacaoJuros = new JurosDados();
                try
                {
                    simulacaoJuros.ValorAporteMensal = decimal.Parse(_dadosJuros[0].Valor);
                    simulacaoJuros.TaxaJurosMensal = double.Parse(_dadosJuros[1].Valor);
                    simulacaoJuros.TempoResgate = int.Parse(_dadosJuros[2].Valor);
                }
                catch (Exception e)
                {
                    _erro = e.Message;
                    Console.WriteLine("Erro ao persistir dados. Verifique os valores informados e tente novamente.");
                    Console.ReadLine();
                }
                try
                {
                    var retorno = this._jurosRepositorio.PostJurosRepositorio(simulacaoJuros).GetAwaiter().GetResult();

                    this._jurosRepositorio.Salvar(new JurosSaida
                    {
                        Meses = retorno.TempoResgate,
                        PatrimonioJurosComposto = retorno.JurosComposto,
                        PatrimonioJurosSimples = retorno.JurosSimples,
                        AporteMensal = simulacaoJuros.ValorAporteMensal,
                        HorasSimulado = DateTime.Now
                    });

                    Console.WriteLine("Simulação Efetuada com sucesso!");

                    Console.WriteLine($"Em {retorno.TempoResgate} ano(s) seu Patrimônio Acumulado em Juros Compostos terá um valor de" +
                        $" R${Math.Round(retorno.JurosComposto, 2)} ");

                    Console.WriteLine($"Em {retorno.TempoResgate} ano(s) seu Patrimônio Acumulado em Juros Simples terá um valor " +
                        $"de R${Math.Round(retorno.JurosSimples, 2)} ");

                    Console.WriteLine();
                    Console.WriteLine("Para simular novamente, basta pressionar ENTER.");
                    Console.ReadLine();
                }
                catch (Exception e)
                {
                    if (e.Message == "Os valores informados devem ser maiores que 0.")
                    {
                        Console.WriteLine(e.Message);
                        Console.WriteLine("Para simular novamente, basta pressionar ENTER.");
                        Console.ReadLine();
                    }
                    else
                    {
                        throw e;
                    }
                }
            }
        }

        private void PreencherEdicao(string linha)
        {
            if (_dadosSendoEditados)
            {
                _dadosSendoEditados = false;
                _dadoEditado.Valor = linha;
            }
            else if (int.TryParse(linha, out int opcao) && opcao - 1 < _dadosJuros.Length && opcao - 1 >= 0)
            {
                _dadosSendoEditados = true;
                _dadoEditado = _dadosJuros.ToArray()[opcao - 1];
            }
        }
    }
}
