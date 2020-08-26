using System;
using System.Collections.Generic;
namespace BancoX.Interface
{
    public interface IContaInvestimentoService
    {
        bool Investir(int idTitulo, decimal valor, out string mensagemErro);

        List<ExtratoInvetimento>Extrato(int idTitulo, int idCarteira, out string mensagemErro);

        bool ResgateInvestimento(double valorRetirada, int idAgencia, int contaCorrente, string nomeBanco, DateTime dataAtualResgate, DateTime dataVencimento, out string mensagemErro);

        bool Deposito(int idAgencia, int numero, string banco, decimal valor, out string mensagemErro);

        bool Transferencia(int agenciaOrigem, int contaOrigem, decimal valor, int agenciaDestino, int contaDestino, out string mensagemErro);
    }
}
