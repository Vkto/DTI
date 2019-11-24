using System;
using System.Collections.Generic;
using System.Text;

namespace Interface
{
    public interface ITela
    {
        void Renderizar();
        ITela TratarEntradaValor(string linha);
    }
}
