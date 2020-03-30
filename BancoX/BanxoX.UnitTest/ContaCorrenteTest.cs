using System;
using BancoX;
using BancoX.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BanxoX.UnitTest
{
    [TestClass]
    public class ContaCorrenteTest
    {
        private ContaCorrente GetContaCorrente()
        {
            var contaCorrente = new ContaCorrente(           
                Mock.Of<IAgenciaRepository>(),
                Mock.Of<IContaRepository>()
                
                );

            return contaCorrente;
        }

        [TestMethod]
        public void Deposito_RetornaTrueSeRealiadoComSucesso()
        {
            Assert.Inconclusive();
        }


        [TestMethod]
        public void Deposito_Erro_SeAgenciaNaoExistir()
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
        public void Deposito_Erro_SeContaNaoExistir()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void Deposito_Erro_SeContaNaoExistirNaAgencia()
        {
            // arrange
            var contaCorrente = GetContaCorrente();


            var agencia = new Agencia() { Id = 8792, Nome = "Agência Zona Oeste" };
            Mock.Get(contaCorrente._IAgenciaRepository).Setup(x=> x.GetById(100)).Returns(agencia);


            // act
            string msgErro;
            var result = contaCorrente.Deposito(8792, 100, 100m, out msgErro); // conta não existe

            //assert
            Assert.IsFalse(result);
            Assert.AreEqual("Conta inválida!", msgErro);
        }

        [TestMethod]
        public void Deposito_Erro_SeValorMenorOuIgualZero()
        {
            // arrange
            var contaCorrente = GetContaCorrente();


            var agencia = new Agencia() { Id = 8792, Nome = "Agência Zona Oeste" };
            Mock.Get(contaCorrente._IAgenciaRepository).Setup(x => x.GetById(100)).Returns(agencia);

            var conta = new Conta() { Id = 3621, AgenciaId = 8792, NomeCliente = "Jéssica", CPFCliente = "004.887.380-24", Saldo = 100m };
            Mock.Get(contaCorrente._IContaRepository).Setup(c => c.GetById(100, 8792)).Returns(conta);
           
            // act
            string msgErro;
            var result = contaCorrente.Deposito(8792, 3621, 0m, out msgErro); // valor menor igual 0

            //assert
            Assert.IsFalse(result);
            Assert.AreEqual("Valor do depósito deverá ser maior do que 0", msgErro);
        }

        [TestMethod]
        public void Deposito_RetornaTrueSeRealizadoComSucesso()
        {
            // arrange
            var contaCorrente = GetContaCorrente();


            var agencia = new Agencia() { Id = 8792, Nome = "Agência Zona Oeste" };
            Mock.Get(contaCorrente._IAgenciaRepository).Setup(x => x.GetById(100)).Returns(agencia);

            var conta = new Conta() { Id = 3621, AgenciaId = 8792, NomeCliente = "Jéssica", CPFCliente = "004.887.380-24", Saldo = 100m };
            Mock.Get(contaCorrente._IContaRepository).Setup(c => c.GetById(100, 8792)).Returns(conta);

            // act
            string msgErro;
            var result = contaCorrente.Deposito(8792, 3621, 50m, out msgErro); 

            //assert
            Assert.IsTrue(result);
            Assert.
        }

        [TestMethod]
        public void Saque_Erro_SeAgenciaNaoExistir()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void Saque_ErroSeContaNaoExistir()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void Saque_ErroSeValorMenorOuIgualZero()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void Saque_Erro_SEValorMaiorQueSaldoConta()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void Transferencia_Retorna_True_SeRealizadoComSucesso()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void Transferencia_Erro_SeAgenciaOrigemNaoExistir()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void Transferencia_Erro_SeContaOrigemNaoExistirNaAgencia()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void Transferencia_Erro_SeAgenciaDestinoNaoExistir()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void Transferencia_Erro_SeContaDestinoNaoExistirNaAgencia()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void Transferencia_Erro_SeValorMenorOuIgualZero()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void Transferencia_Erro_SeValorMaiorQueSaldoContaOrigem()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void SaldoErro_SeAgenciaNaoExistir()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void SaldoErro_SeContaNaoEgistirNaAgencia()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void Extrato_RetornaRegistroExtrato()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void Extrato_Erro_SeContaNaoExistirNaAgencia()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void Extrato_Erro_SeDataInicioMaiorDataFim()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void Extrato_Erro_SePeriodoMaior120Dias()
        {
            Assert.Inconclusive();
        }
        [TestMethod]
        public void Extrato_PrimeiraLinhaContemSaldoAnterior()
        {
            Assert.Inconclusive();
        }
    }
}
