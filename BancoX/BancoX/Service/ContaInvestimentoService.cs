using System;
using System.Collections.Generic;
using System.Transactions;
using BancoX.Interface;
using FluentValidation.Results;

namespace BancoX
{
    public class ContaInvestimentoService : IContaInvestimentoService
    {
        public IAgenciaRepository AgenciaRepository { get; private set; }

        public IContaRepository ContaRepository { get; private set; }

        public IExtratoInvetimentoRepository ExtratoInvestimentoRepository { get; private set; }

        public IContaInvestimentoRepository ContaInvestimentoRepository { get; private set; }

        public ContaInvestimentoService(IExtratoInvetimentoRepository extratoInvestimentoRepository, IContaInvestimentoRepository contaInvestimentoRepository, IAgenciaRepository agenciaRepository, IContaRepository contaRepository)
        {
            ExtratoInvestimentoRepository = extratoInvestimentoRepository;
            ContaInvestimentoRepository = contaInvestimentoRepository;
            AgenciaRepository = agenciaRepository;
            ContaRepository = contaRepository;
        }
        
        public bool Deposito(int idAgencia, int numero, string banco, decimal valor, out string mensagemErro)
        {
            mensagemErro = "";

            var agencia = AgenciaRepository.GetById(idAgencia);

            var contaInvestimento = ContaInvestimentoRepository.GetById(idAgencia, numero);

            if (agencia == null)
            {
                mensagemErro = "Agência inválida!";
                return false;
            }

            if (contaInvestimento == null)
            {
                mensagemErro = "Conta inválida!";
                return false;
            }

            if(valor < 50)
            {
                mensagemErro = "O valor do depósito precisa ser maior ou igual a 50!";
                return false;
            }

            contaInvestimento.Saldo = contaInvestimento.Saldo + valor;

            var extrato = new ExtratoInvetimento
            {
                DataRegistro = DateTime.Now,
                Saldo = contaInvestimento.Saldo,
                IdAgencia = 0002,
                IdConta = 1050,
                Valor = 300m,
                Descricao = "Depósito"
            };

            try
            {
                using(var transaction = new TransactionScope())
                {
                   ContaInvestimentoRepository.Save(contaInvestimento);
                   ExtratoInvestimentoRepository.Save(extrato);
                   transaction.Complete();
                }
            }
            catch (Exception)
            {
                mensagemErro = "Ocorreu um erro ao fazer o depósito!";
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

        public bool ResgateInvestimento(double valorRetirada, int idAgencia, int contaCorrente, string nomeBanco, DateTime dataAtualResgate, DateTime dataVencimento, out string mensagemErro)
        {
            // retirada da conta investimentos para uma outra conta particular

            // resgate do título => transferência para uma conta corrente

            throw new NotImplementedException();
        }

        public double CalculaDescontoImpostoRenda(double valorTitulo, DateTime dataAplicacao, DateTime dataVencimento, out string mensagemErro)
        {
            throw new NotImplementedException();

            // refactor esse método deverá estar no service TesouroDiretoService e o teste também
        }

        public bool Transferencia(int agenciaOrigem, int contaOrigem, decimal valor, int agenciaDestino, int contaDestino, out string mensagemErro)
        {
            throw new NotImplementedException();

            // transferência do valor da carteira para a conta corrente
        }
    }
}
