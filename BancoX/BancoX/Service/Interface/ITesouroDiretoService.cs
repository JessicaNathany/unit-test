using System;
namespace BancoX.Service.Interface
{
    public interface ITesouroDiretoService
    {
        double CalculaDescontoImpostoRenda(int idTesouro, double valorTitulo, DateTime dataAplicacao, DateTime dataVencimento, out string mensagemErro);
    }
}
