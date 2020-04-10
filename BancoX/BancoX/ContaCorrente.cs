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
        public ContaCorrente(IAgenciaRepository agenciaRepo, IContaRepository contaRepo, IExtratoRepository extratoRepository)
        {
            AgenciaRepository = agenciaRepo;
            ContaRepository = contaRepo;
            ExtratoRepository = extratoRepository;
        }   

        public IAgenciaRepository AgenciaRepository { get; set; }
        public IContaRepository ContaRepository { get; set; }
        public IExtratoRepository ExtratoRepository { get; set; }


        public bool Deposito(int agencia, int conta, decimal valor, out string mensagemErro)
        {
            var ag = AgenciaRepository.GetById(agencia);

            if(ag == null)
            {
                mensagemErro = "Agência inválida!";
                return false;
            }

            var contaCorrente = ContaRepository.GetById(agencia, conta);

            if(contaCorrente == null)
            {
                mensagemErro = "Conta inválida!";
                return false;
            }

            if(valor <= 0)
            {
                mensagemErro = "O valor~do depósito deve ser maior que zero!";
                return false;
            }

            contaCorrente.Saldo = contaCorrente.Saldo + valor;

            var extrato = new Extrato()
            {
                DataRegistro = DateTime.Now,
                AgenciaId = agencia,
                ContaId = conta,
                Valor = valor,
                Saldo = contaCorrente.Saldo,
                Descricao = "Depoósito"
            };

            using (var transaction = new TransactionScope())
            {
                ContaRepository.Save(contaCorrente);
                ExtratoRepository.Save(extrato);
            }
                

            throw new NotImplementedException();
        }

        public bool Saque(int agencia, int conta, decimal valor, out string mensagemErro)
        {
            throw new NotImplementedException();
        }

        public decimal Saldo(int agencia, int conta, out string mensagemErro)
        {
            throw new NotImplementedException();
        }

        public bool Transferencia(int agenciaOrigem, int contaOrigem, decimal valor, int agenciaDestino, int contaDestino, out  string MensagemErro)
        {
            throw new NotImplementedException();
        }

        public List<Extrato> Extrato(int agencia, int conta, DateTime dataInicio, DateTime dataFim, out string mensagemErro)
        {
            throw new NotImplementedException();
        }
    }
}
