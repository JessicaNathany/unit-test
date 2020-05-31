﻿using System;
using BancoX;
using BancoX.Interface;
using FizzWare.NBuilder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;

namespace BanxoX.UnitTest
{
    [TestClass]
    public class ContaCorrenteTest
    {
        private ContaCorrente GetContaCorrente()
        {
            var contaCorrente = new ContaCorrente(
                Mock.Of<IAgenciaRepository>(),
                Mock.Of<IContaRepository>(),
                Mock.Of<IExtratoRepository>()

                );

            var agencia = new Agencia() { Id = 8792, Nome = "Agência Zona Oeste" };
            var agencia2 = new Agencia() { Id = 200, Nome = "Agência Zona Leste" };

            Mock.Get(contaCorrente.AgenciaRepository).Setup(x => x.GetById(8792)).Returns(agencia);
            Mock.Get(contaCorrente.AgenciaRepository).Setup(x => x.GetById(200)).Returns(agencia);

            var conta = new Conta() { Id = 3621, AgenciaId = 8792, NomeCliente = "Jéssica", CPFCliente = "004.887.380-24", Saldo = 100m };
            var conta2 = new Conta() { Id = 700, AgenciaId = 200, NomeCliente = "Peter Pan", CPFCliente = "004.111.350-24", Saldo = 700m };

            Mock.Get(contaCorrente.ContaRepository).Setup(c => c.GetById(8792, 3621)).Returns(conta);
            Mock.Get(contaCorrente.ContaRepository).Setup(c => c.GetById(200, 700)).Returns(conta2);

            return contaCorrente;
        }

        [TestMethod]
        public void ContaCorrente_Deposito_RetornaTrueSeRealiadoComSucesso() 
        {
            // arrange
            var contaCorrente = GetContaCorrente();

            var agencia = 8792;
            var conta = 3621;

            // act
            string msgErro;
            var result = contaCorrente.Deposito(agencia, conta, 100m, out msgErro);

            //assert
            Assert.IsTrue(result);
        }


        [TestMethod]
        public void ContaCorrente_Deposito_ErroSeAgenciaNaoExistir()
        {
            // arrange
            var contaCorrente = GetContaCorrente();

            // act
            string msgErro;
            var result = contaCorrente.Deposito(000, 100, 100m, out msgErro);

            //assert
            Assert.IsFalse(result);
            Assert.AreEqual("Agência inválida!", msgErro);
        }

        [TestMethod]
        public void ContaCorrente_Deposito_ErroSeContaNaoExistir()
        {
            var agencia = 8792;

            // arrange
            var contaCorrente = GetContaCorrente();

            // act
            string msgErro;
            var result = contaCorrente.Deposito(agencia, 000, 100m, out msgErro);

            //asserta
            Assert.IsFalse(result);
            Assert.AreEqual("Conta inválida!", msgErro); 
        }

        [TestMethod]
        public void ContaCorrente_Deposito_ErroSeContaNaoExistirNaAgencia()
        {
            // arrange
            var contaCorrente = GetContaCorrente();

            // act
            string msgErro;
            var result = contaCorrente.Deposito(8792, 100, 100m, out msgErro); // conta não existe

            //assert
            Assert.IsFalse(result);
            Assert.AreEqual("Conta inválida!", msgErro);
        }

        [TestMethod]
        public void ContaCorrente_Deposito_ErroSeValorMenorOuIgualZero()
        {
            // arrange
            var contaCorrente = GetContaCorrente();

            // act
            string msgErro;
            var result = contaCorrente.Deposito(8792, 3621, 0m, out msgErro); // valor <= 0

            //assert
            Assert.IsFalse(result);
            Assert.AreEqual("Valor do depósito deve ser maior do que 0!", msgErro);
        }

        [TestMethod]
        public void ContaCorrente_Deposito_RetornaTrueSeRealizadoComSucesso()
        {
            // arrange
            var contaCorrente = GetContaCorrente();

            // act
            string msgErro;
            var result = contaCorrente.Deposito(8792, 3621, 50m, out msgErro);

            //assert
            Assert.IsTrue(result);
            Mock.Get(contaCorrente.ContaRepository).Verify(x => x.Save(It.Is<Conta>(c => c.AgenciaId.Equals(8792) && c.Id == 3621 && c.Saldo == 150m)));
            Mock.Get(contaCorrente.ExtratoRepository).Verify(x => x.Save(It.Is<Extrato>(e => e.AgenciaId == 8792 && e.ContaId == 3621 && e.Descricao == "Depósito" && e.Valor == 50m && e.Saldo == 150m && e.DataRegistro == DateTime.Today)));
        }

        [TestMethod]
        public void ContaCorrente_Saque_RetornaTrueSeSaqueRealizadoComSucesso()
        {
            // arrange
            var contaCorrente = GetContaCorrente();

            // act
            string msgErro;
            var result = contaCorrente.Saque(8792, 3621, 50m, out msgErro);

            //assert
            Assert.IsTrue(result);
            Mock.Get(contaCorrente.ContaRepository).Verify(x => x.Save(It.Is<Conta>(c => c.AgenciaId.Equals(8792) && c.Id == 3621 && c.Saldo == 50m)));
            Mock.Get(contaCorrente.ExtratoRepository).Verify(x => x.Save(It.Is<Extrato>(e => e.AgenciaId == 8792 && e.ContaId == 3621 && e.Descricao == "Saque" && e.Valor == -50m && e.Saldo == -50m && e.DataRegistro == DateTime.Now)));
        }

        [TestMethod]
        public void ContaCorrente_Saque_ErroSeAgenciaNaoExistir()
        {
            // arrange
            var contaCorrente = GetContaCorrente();

            // act
            string msgErro;
            var result = contaCorrente.Saque(666, 36921, 50m, out msgErro); // conta não existe

            //assert
            Assert.IsFalse(result);
            Assert.AreEqual("Agência inválida!", msgErro);
        }

        [TestMethod]
        public void ContaCorrente_Saque_ErroSeContaNaoExistirNaAgencia()
        {
            // arrange
            var contaCorrente = GetContaCorrente();

            // act
            string msgErro;
            var result = contaCorrente.Saque(8792, 666, 50m, out msgErro); // conta não existe

            //assert
            Assert.IsFalse(result);
            Assert.AreEqual("Conta inválida!", msgErro);
        }

        [TestMethod]
        public void ContaCorrente_Saque_ErroSeValorMenorOuIgualZero()
        {
            // arrange
            var contaCorrente = GetContaCorrente();

            // act
            string msgErro;
            var result = contaCorrente.Saque(8792, 3621, -1m, out msgErro); // valor <= 0

            //assert
            Assert.IsFalse(result);
            Assert.AreEqual("O valor do saque precisa ser maior que zero!", msgErro);
        }

        [TestMethod]
        public void ContaCorrente_Saque_ErroSeValorMaiorQueSaldoConta()
        {
            // arrange
            var contaCorrente = GetContaCorrente();

            // act
            string msgErro;
            var result = contaCorrente.Saque(200, 700, 800m, out msgErro); // valor > que o saldo

            //assert
            Assert.IsFalse(result);
            Assert.AreEqual("O valor do saque precisa ser menor ou igual ao saldo da conta!", msgErro);
        }

        [TestMethod]
        public void ContaCorrente_Transferencia_RetornaTrueSeRealizadoComSucesso()
        {
            var agenciaOrigem = 8792;
            var agenciaDestino = 200;
            var contaOrigem = 3621;
            var contaDestino = 700;
            var valor = 50m;

            // arrange
            var contaCorrente = GetContaCorrente();

            // act
            string erro;
            var result = contaCorrente.Transferencia(agenciaOrigem, contaOrigem, valor, agenciaDestino, contaDestino, out erro);

            // assert
            Assert.IsTrue(result);

            // conta origem
            Mock.Get(contaCorrente.ContaRepository).Verify(x => x.Save(It.Is<Conta>(c => c.AgenciaId.Equals(agenciaOrigem) && c.Id == contaOrigem && c.Saldo == valor)));
            Mock.Get(contaCorrente.ExtratoRepository).Verify(x => x.Save(It.Is<Extrato>(e => e.AgenciaId == agenciaOrigem && e.ContaId == contaOrigem && e.Descricao == "Transferência para agência 200 conta 700" && e.Valor == -50m && e.Saldo == 50m && e.DataRegistro == DateTime.Today)));

            // conta destino
            Mock.Get(contaCorrente.ContaRepository).Verify(x => x.Save(It.Is<Conta>(c => c.AgenciaId.Equals(agenciaDestino) && c.Id == contaDestino && c.Saldo == valor)));
            Mock.Get(contaCorrente.ExtratoRepository).Verify(x => x.Save(It.Is<Extrato>(e => e.AgenciaId == agenciaDestino && e.ContaId == contaDestino && e.Descricao == "Transferência de agencia 8692 conta 3621" && e.Valor == -50m && e.Saldo == 750m && e.DataRegistro == DateTime.Today)));
        }

        [TestMethod]
        public void ContaCorrente_Transferencia_ErroSeAgenciaOrigemNaoExistir()
        {
            // arrange
            var contaCorrente = GetContaCorrente();

            // act
            string msgErro;
            var result = contaCorrente.Transferencia(666, 8792, 50m, 200, 700, out msgErro);

            //assert
            Assert.IsFalse(result);
            Assert.AreEqual("Agência de origem inválida!", msgErro);
        }

        [TestMethod]
        public void ContaCorrente_Transferencia_ErroSeContaOrigemNaoExistirNaAgencia()
        {
            // arrange
            var contaCorrente = GetContaCorrente();
            var agOrigem = 8792;
            var contaOrigem = 0;
            var agDestino = 200;
            var contaDestino = 700;

            // act
            string msgErro;
            var result = contaCorrente.Transferencia(agOrigem, contaOrigem, 50m, agDestino, contaDestino, out msgErro); // conta não existe

            //assert
            Assert.IsFalse(result);
            Assert.AreEqual("Conta de origem inválida!", msgErro);
        }

        [TestMethod]
        public void ContaCorrente_Transferencia_ErroSeAgenciaDestinoNaoExiste()
        {
            // arrange
            var contaCorrente = GetContaCorrente();
            var agOrigem = 8792;
            var contaOrigem = 3621;
            var agDestino = 0;
            var contaDestino = 666;

            // act
            string msgErro;
            var result = contaCorrente.Transferencia(agOrigem, contaOrigem, 50m, agDestino, contaDestino, out msgErro); // ag destino inválida

            //assert
            Assert.IsFalse(result);
            Assert.AreEqual("Agência de destino inválida!", msgErro);
        }

        [TestMethod]
        public void ContaCorrente_Transferencia_ErroSeContaDestinoNaoExistirNaAgencia()
        {
            // arrange
            var contaCorrente = GetContaCorrente();
            var agOrigem = 8792;
            var contaOrigem = 3621;
            var agDestino = 200;
            var contaDestino = 0;

            // act
            string msgErro;
            var result = contaCorrente.Transferencia(agOrigem, contaOrigem, 50m, agDestino, contaDestino, out msgErro); 

            //assert
            Assert.IsFalse(result);
            Assert.AreEqual("Conta de destino inválida!", msgErro);
        }

        [TestMethod]
        public void ContaCorrente_Transferencia_ErroSeValorMenorOuIgualZero()
        {
            // arrange
            var contaCorrente = GetContaCorrente();

            // act
            string msgErro;
            var result = contaCorrente.Transferencia(8792, 3621, 0m, 200, 700, out msgErro);

            //assert
            Assert.IsFalse(result);
            Assert.AreEqual("O valor deve ser maior do que zero!", msgErro);
        }

        [TestMethod]
        public void ContaCorrente_Transferencia_ErroSeValorMaiorQueSaldoContaOrigem()
        {
            // arrange
            var contaCorrente = GetContaCorrente();

            // act
            string msgErro;
            var result = contaCorrente.Saque(8792, 3621, 1000m, out msgErro); 

            //assert
            Assert.IsFalse(result);
            Assert.AreEqual("O valor deve ser maior ou igual ao saldo da conta de origem!", msgErro);
        }

        [TestMethod]
        public void ContaCorrente_Saldo_RetornaSaldoDaConta()
        {
            var conta = 8792;
            var agencia = 3621;

            // arrange
            var contaCorrente = GetContaCorrente();

            // act
            string msgErro;
            var result = contaCorrente.Saldo(conta, agencia, out msgErro);

            //assert
            Assert.AreEqual(100m, result);
        }

        [TestMethod]
        public void ContaCorrente_Saldo_ErroSeAgenciaNaoExistir()
        {
            // arrange
            var contaCorrente = GetContaCorrente();

            // act
            string msgErro;
            var result = contaCorrente.Saldo(0, 8792, out msgErro);

            //assert
            Assert.AreEqual(0m, result);
            Assert.AreEqual("Agência inválida!", msgErro);
        }

        [TestMethod]
        public void ContaCorrente_Saldo_ErroSeContaNaoExistirNaAgencia()
        {
            // arrange
            var contaCorrente = GetContaCorrente();

            // act
            string msgErro;
            var result = contaCorrente.Saldo(8792, 666, out msgErro); // conta não existe

            //assert
            Assert.AreEqual(0m, result);
            Assert.AreEqual("Conta inválida!", msgErro);
        }

        [TestMethod]
        public void ContaCorrente_Extrato_RetornaRegistrosDoExtrato()
        {
            // arrange
            var contaCorrente = GetContaCorrente();
            var dataInicio = new DateTime(2020, 02, 01);
            var dataFim = new DateTime(2020, 02, 16);
            var agencia = 8792;
            var conta = 8792;

            // cria uma lista de Extrato
            var listaExtrato = Builder<Extrato>.CreateListOfSize(10).All()
                         .With(x => x.AgenciaId = conta).With(x => x.ContaId = conta).Build();

            Mock.Get(contaCorrente.ExtratoRepository).Setup(x => x.GetByPeriodo(agencia, conta, dataInicio, dataFim)).Returns(listaExtrato);

            // act
            string msgErro;
            var result = contaCorrente.Extrato(agencia, conta, dataInicio, dataFim, out msgErro);

            //assert
            Assert.IsNull(result);
            Assert.AreEqual(11,  result.Count);
            Assert.AreEqual(listaExtrato.Sum(e=> e.Valor), result.Sum(r=> r.Valor));
        }

        [TestMethod]
        public void ContaCorrente_Extrato_ErroSeAgenciaNaoExistir()
        {
            // arrange
            var contaCorrente = GetContaCorrente();

            // act
            string msgErro;
            var result = contaCorrente.Extrato(0, 8792, new DateTime(2020, 02, 01), new DateTime(2020, 02, 16), out msgErro);

            //assert
            Assert.IsNull(result);
            Assert.AreEqual("Agência inválida!", msgErro);
        }

        [TestMethod]
        public void ContaCorrente_Extrato_RetornaRegistroDoExtrato()
        {
            // arrange
            var contaCorrente = GetContaCorrente();
            var dataInicio = new DateTime(2020, 01, 01);
            var dataFim = new DateTime(2020, 01, 15);

            var extratoLista = Builder<Extrato>.CreateListOfSize(10)
                .All()
                .With(x => x.AgenciaId = 8792)
                .With(x => x.ContaId = 3621)
                .Build();

            Mock.Get(contaCorrente.ExtratoRepository).Setup(c => c.GetByPeriodo(8792, 3621, dataInicio, dataFim)).Returns(extratoLista);

            // act
            string mesgErro;
            var result = contaCorrente.Extrato(666, 8792, dataInicio, dataFim, out mesgErro);

            // assert
            Assert.IsNotNull(result);
            Assert.AreEqual(11, result.Count);
            Assert.AreEqual(extratoLista.Sum(e=> e.Valor), result.Sum(r=> r.Valor));
        }

        [TestMethod]
        public void ContaCorrente_Extrato_ErroSeContaNaoExistirNaAgencia()
        {
            // arrange
            var contaCorrente = GetContaCorrente();

            // act
            string msgErro;
            var result = contaCorrente.Extrato(8792, 0, new DateTime(2020, 01, 01), new DateTime(2020, 01, 15), out msgErro); // conta não existe

            //assert
            Assert.IsNull(result);
            Assert.AreEqual("Conta de origem é invalida!", msgErro);
        }

        [TestMethod]
        public void ContaCorrente_Extrato_ErroSeDataInicioMaiorDataFim()
        {
            var agencia = 8792;
            var conta = 3621;

            // arrange
            var contaCorrente = GetContaCorrente();

            // act
            string msgErro;
            var result = contaCorrente.Extrato(agencia, conta, new DateTime(2021, 01, 01), new DateTime(2020, 01, 15), out msgErro); // dataIni > dataFim

            //assert
            Assert.IsNull(result);
            Assert.AreEqual("A data de inicio deve ser menor que a data fim!", msgErro);
        }

        [TestMethod]
        public void ContaCorrente_Extrato_ErroSePeriodoMaior120Dias()
        {
            var agencia = 8792;
            var conta = 3621;

            // arrange
            var contaCorrente = GetContaCorrente();

            // act
            string msgErro;
            var result = contaCorrente.Extrato(agencia, conta, new DateTime(2020, 01, 01), (new DateTime(2020, 01, 15)).AddDays(121), out msgErro); // periodo > 120 dias

            //assert
            Assert.IsNull(result);
            Assert.AreEqual("O periodo não deve ser supeior há 120 dias!", msgErro);
        }
        [TestMethod]
        public void ContaCorrente_Extrato_PrimeiraLinhaContemSaldoAnterior()
        {
            // arrange
            var contaCorrente = GetContaCorrente();
            var dataInicio = new DateTime(2020, 02, 01);
            var dataFim = new DateTime(2020, 02, 16);
            var agencia = 8792;
            var conta = 8792;

            // cria uma lista de Extrato
            var listaExtrato = Builder<Extrato>.CreateListOfSize(10).All()
                         .With(x => x.AgenciaId = conta).With(x => x.ContaId = conta).Build();

            Mock.Get(contaCorrente.ExtratoRepository).Setup(x => x.GetByPeriodo(agencia, conta, dataInicio, dataFim)).Returns(listaExtrato);
            Mock.Get(contaCorrente.ExtratoRepository).Setup(x => x.GetSaldoAnterior(agencia, conta, dataInicio, dataFim)).Returns(30);

            // act
            string msgErro;
            var result = contaCorrente.Extrato(agencia, conta, dataInicio, dataFim, out msgErro);

            //assert
            Assert.AreEqual("Saldo anterior", result.First().Descricao);
            Assert.AreEqual(30m, result.First().Saldo);
        }
    }
}
