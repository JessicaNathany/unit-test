using BancoX.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoX.Service
{
    public class TesouroDiretoService : ITesouroDiretoService
    {
        public double CalculaDescontoImpostoRenda(double valorTitulo, DateTime dataAplicacao, DateTime dataVencimento, out string mensagemErro)
        {
            throw new NotImplementedException();
        }
    }
}
