using BancoX.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Transactions;

namespace BancoX
{
    [TestClass]
    public class ContaCorrente : IContaCorrente
    {
        public ContaCorrente(IAgenciaRepository agenciaRepository, IContaRepository contaRepository, IExtratoRepository extratoRepository)
        {
            AgenciaRepository = agenciaRepository;
            ContaRepository = contaRepository;
            ExtratoRepository = extratoRepository;
        }

        public IAgenciaRepository AgenciaRepository { get; set; }

        public IContaRepository ContaRepository { get; set; }

        public IExtratoRepository ExtratoRepository { get; set; }

        public bool Deposito(int idAgencia, int conta, decimal valor, out string mensagemErro)
        {
            mensagemErro = "";

            var agencia = AgenciaRepository.GetById(idAgencia);

            if (agencia == null)
            {
                mensagemErro = "Agência inválida!";
                return false;
            }

            var contaCorrente = ContaRepository.GetById(idAgencia, conta);

            if (contaCorrente == null)
            {
                mensagemErro = "Conta inválida!";
                return false;
            }

            if (valor <= 0)
            {
                mensagemErro = "Valor do depósito deve ser maior do que 0!";
                return false;
            }

            contaCorrente.Saldo = contaCorrente.Saldo + valor;

            var extrato = new Extrato()
            {
                DataRegistro = DateTime.Today,
                AgenciaId = idAgencia,
                ContaId = conta,
                Valor = valor,
                Saldo = contaCorrente.Saldo,
                Descricao = "Depósito"
            };

            try
            {
                using (var transaction = new TransactionScope())
                {
                    ContaRepository.Save(contaCorrente);
                    ExtratoRepository.Save(extrato);
                    transaction.Complete();
                }
            }
            catch (Exception)
            {
                // incluir isso em um log...
                mensagemErro = "Ocorreu um erro ao fazer o depósito!";
                return false;
            }
            return true;
        }

        public bool Saque(int idAgencia, int conta, decimal valor, out string mensagemErro)
        {
            mensagemErro = "";

            var agencia = AgenciaRepository.GetById(idAgencia);

            if (agencia == null)
            {
                mensagemErro = "Agência inválida!";
                return false;
            }

            var contaCorrente = ContaRepository.GetById(idAgencia, conta);

            if (contaCorrente == null)
            {
                mensagemErro = "Conta inválida!";
                return false;
            }

            if (valor <= 0)
            {
                mensagemErro = "O valor do saque precisa ser maior que zero!";
                return false;
            }

            if (valor > contaCorrente.Saldo)
            {
                mensagemErro = "O valor do saque precisa ser menor ou igual ao saldo da conta!";
                return false;
            }

            contaCorrente.Saldo = contaCorrente.Saldo - valor;

            var extrato = new Extrato()
            {
                DataRegistro = DateTime.Now,
                AgenciaId = idAgencia,
                ContaId = conta,
                Valor = valor * - 1,
                Saldo = contaCorrente.Saldo,
                Descricao = "Saque"
            };

            try
            {
                using (var transaction = new TransactionScope())
                {
                    ContaRepository.Save(contaCorrente);
                    ExtratoRepository.Save(extrato);
                    transaction.Complete();
                }
            }
            catch (Exception)
            {
                // incluir isso em um log...
                mensagemErro = "Ocorreu um erro ao fazer o saque!";
                return false;
            }
            return true;

        }

        public decimal Saldo(int agencia, int conta, out string mensagemErro)
        {
            throw new NotImplementedException();
        }

        public bool Transferencia(int agenciaOrigem, int contaOrigem, decimal valor, int agenciaDestino, int contaDestino, out string MensagemErro)
        {
            throw new NotImplementedException();
        }

        public List<Extrato> Extrato(int agencia, int conta, DateTime dataInicio, DateTime dataFim, out string mensagemErro)
        {
            throw new NotImplementedException();
        }
    }
}
