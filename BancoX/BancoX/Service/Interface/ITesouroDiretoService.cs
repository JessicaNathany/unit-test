using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoX.Service.Interface
{
    public interface ITesouroDiretoService
    {
        double CalculaDescontoImpostoRenda(double valorTitulo, DateTime dataAplicacao, DateTime dataVencimento, out string mensagemErro);
    }
}
