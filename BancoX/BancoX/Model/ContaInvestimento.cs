using System;
using System.Collections.Generic;
using BancoX.Interface;
namespace BancoX
{
    // Conta de investimento do cliente
    public class ContaInvestimento : IContaInvestimento
    {
        public ContaInvestimento(IExtratoInvetimentoRepository extratoInvestimentoRepository, IContaInvestimentoRepository contaInvestimentoRepository, IAgenciaRepository agenciaRepository)
        {
            _IExtratoInvestimentoRepository = extratoInvestimentoRepository;
            _IContaInvestimentoRepository = contaInvestimentoRepository;
            _AgenciaRepository = agenciaRepository;
        }

        public IAgenciaRepository _AgenciaRepository { get; set; }
        public IExtratoInvetimentoRepository _IExtratoInvestimentoRepository { get; set; }
        public IContaInvestimentoRepository _IContaInvestimentoRepository { get; set; }

        public bool Deposito(int idAgencia, int numero, string banco, decimal valor, out string mensagemErro)
        {
            //mensagemErro = "";

            //var agencia = _AgenciaRepository.GetById(idAgencia);

            //if(agencia == null)
            //{
            //    mensagemErro = "Agência inválida!";
            //    return false;
            //}
            //return true;

            throw new NotImplementedException();
        }

        public List<ExtratoInvetimento> Extrato(int idTitulo, int idCarteira, out string mensagemErro)
        {
            throw new NotImplementedException();
        }

        public bool Investir(int idTitulo, decimal valor, out string mensagemErro)
        {
            throw new NotImplementedException();
        }

        public IList<Investimentos> ListarMeusInvestimentos()
        {
            throw new NotImplementedException();
        }

        public bool ResgateTitulo(double valorRetirada, int idAgencia, int contaCorrente, string nomeBanco, DateTime dataAtualResgate, DateTime dataVencimento, out string mensagemErro)
        {
            // retirada da conta investimentos para uma outra conta particular

            // resgate do título => transferência para uma conta corrente

            throw new NotImplementedException();
        }

        public double DescontoImpostoRenda(double valorTitulo, DateTime dataAplicacao, DateTime dataVencimento, out string mensagemErro)
        {
            throw new NotImplementedException();
        }

        public bool Transferencia(int agenciaOrigem, int contaOrigem, decimal valor, int agenciaDestino, int contaDestino, out string mensagemErro)
        {
            throw new NotImplementedException();

            // transferência do valor da carteira para a conta corrente
        }
    }
}
