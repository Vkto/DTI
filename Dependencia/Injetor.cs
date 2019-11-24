using Dados;
using Interface;
using Repositorio;
using System;
using System.ComponentModel.Design;
using Telas;

namespace Dependencia
{
    public class Injetor
    {
        public static ServiceContainer Container()
        {
            var container = new ServiceContainer();

            container.AddService(typeof(IJurosRepositorio), new JurosRepositorio());

            return container;
        }
        public static ITela PreencherTela()
        {
            return new Telas.Telas(Container());
        }
    }
}
