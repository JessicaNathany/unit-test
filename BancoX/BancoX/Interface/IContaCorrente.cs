using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoX
{
    public interface IContaCorrente
    {
        bool Deposito(int agencia, int conta, decimal valor, out string mensagemErro);

        bool Saque(int agencia, int conta, decimal valor, out string mensagemErro);

        decimal Saldo(int agencia, int conta, out string mensagemErro);

        bool Transferencia(int agenciaOrigem, int contaOrigem, decimal valor, int agenciaDestino, int contaDestino, string MensagemErro);;

        List<Extrato> Extrato(int agencia, int conta, DateTime dataInicio, DateTime dataFim, out string mensagemErro);
    }
}
