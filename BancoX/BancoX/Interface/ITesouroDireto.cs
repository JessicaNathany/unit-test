using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoX.Interface
{
    public interface ITesouroDireto
    {
        decimal Resgate(DateTime dataCompra);

        void Investir(decimal valorTitulo, decimal valorCarteira);
    }
}
