using System;
using System.Collections.Generic;
using System.Text;

namespace Telas
{
    public abstract class TelasConsole<Item>
    {
        public Dictionary<int, TelaItem<Item>> Itens { get; protected set; }
    }
    public class TelaItem<I>
    {
        public string Descricao { get; set; }
        public Func<I> TelaSelecionada { get; set; }
    }

}
