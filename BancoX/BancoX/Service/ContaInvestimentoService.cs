using System;
using System.Collections.Generic;
using BancoX.Interface;
namespace BancoX
{
    public class ContaInvestimentoService : IContaInvestimentoService
    {
        public IAgenciaRepository _AgenciaRepository { get; private set; }

        public IContaRepository _ContaRepository { get; private set; }

        public IExtratoInvetimentoRepository _IExtratoInvestimentoRepository { get; private set; }

        public IContaInvestimentoRepository _IContaInvestimentoRepository { get; private set; }

        public ContaInvestimentoService(IExtratoInvetimentoRepository extratoInvestimentoRepository, IContaInvestimentoRepository contaInvestimentoRepository, IAgenciaRepository agenciaRepository, IContaRepository contaRepository)
        {
            _IExtratoInvestimentoRepository = extratoInvestimentoRepository;
            _IContaInvestimentoRepository = contaInvestimentoRepository;
            _AgenciaRepository = agenciaRepository;
            _ContaRepository = contaRepository;
        }
        
        public bool Deposito(int idAgencia, int numero, string banco, decimal valor, out string mensagemErro)
        {
            mensagemErro = "";

            var agencia = _AgenciaRepository.GetById(idAgencia);

            var conta = _IContaInvestimentoRepository.GetById(numero);

            if (agencia == null)
            {
                mensagemErro = "Agência inválida!";
                return false;
            }

            if (conta == null)
            {
                mensagemErro = "Conta inválida!";
                return false;
            }

            return true;
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

        public double CalculaDescontoImpostoRenda(double valorTitulo, DateTime dataAplicacao, DateTime dataVencimento, out string mensagemErro)
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
