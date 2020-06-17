using System;
using System.Collections.Generic;
namespace BancoX.Interface
{
    public interface IContaInvestimento
    {
        void Investit(int idTitulo, decimal valor, out string mensagemErro);

        List<ExtratoInvetimento>Extrato(int idTitulo, int idCarteira, out string mensagemErro);

        void ResgateTitulo(double valorRetirada, int idAgencia, int contaCorrente, string nomeBanco, DateTime dataTitulo, DateTime dataVencimento, string mensagemErro);

        bool Deposito(int idAgencia, int numero, string banco, decimal valor, out string mensagemErro);

        IList<Investimentos> ListarMeusInvestimentos();

        double DescontoImpostoRenda(double valorTitulo, DateTime dataAplicacao, DateTime dataVencimento, out string mensagemErro);

        bool Transferencia(int agenciaOrigem, int contaOrigem, decimal valor, int agenciaDestino, int contaDestino, out string mensagemErro);
    }
}
