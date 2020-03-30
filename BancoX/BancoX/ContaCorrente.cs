using BancoX.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

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

        public bool Transferencia(int agenciaOrigem, int contaOrigem, decimal valor, int agenciaDestino, int contaDestino, string MensagemErro)
        {
            throw new NotImplementedException();
        }

        public List<Extrato> Extrato(int agencia, int conta, DateTime dataInicio, DateTime dataFim, out string mensagemErro)
        {
            throw new NotImplementedException();
        }
    }
}
