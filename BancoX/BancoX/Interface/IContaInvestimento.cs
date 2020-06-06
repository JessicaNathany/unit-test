using System;
using System.Collections.Generic;
namespace BancoX.Interface
{
    public interface IContaInvestimento
    {
        void Resgate(int idTitulo, decimal valor,  DateTime dataAtual, out string mensagemErro);

        void Investit(int idTitulo, decimal valor, out string mensagemErro);

        List<ExtratoInvetimento>Extrato(int idTitulo, int idCarteira, out string mensagemErro);

        void Retirada(double valorRetirada, int idAgencia, int contaCorrente, string nomeBanco, string mensagemErro);

        bool Deposito(int idAgencia, int numero, string banco, decimal valor, out string mensagemErro);

        IList<Investimentos> ListarMeusInvestimentos();
    }
}
