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
                Mock.Of<IContaRepository>(),
                Mock.Of<IExtratoRepository>()
                
                );

            var agencia = new Agencia() { Id = 8792, Nome = "Agência Zona Oeste" };
            var agencia2 = new Agencia() { Id = 200, Nome = "Agência Zona Leste" };

            Mock.Get(contaCorrente.AgenciaRepository).Setup(x => x.GetById(100)).Returns(agencia);
            Mock.Get(contaCorrente.AgenciaRepository).Setup(x => x.GetById(200)).Returns(agencia);

            var conta = new Conta() { Id = 3621, AgenciaId = 8792, NomeCliente = "Jéssica", CPFCliente = "004.887.380-24", Saldo = 100m };
            var conta2 = new Conta() { Id = 700, AgenciaId = 200, NomeCliente = "Peter Pan", CPFCliente = "004.111.350-24", Saldo = 700m };

            Mock.Get(contaCorrente.ContaRepository).Setup(c => c.GetById(100, 8792)).Returns(conta);
            Mock.Get(contaCorrente.ContaRepository).Setup(c => c.GetById(200, 700)).Returns(conta2);

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
            Mock.Get(contaCorrente.AgenciaRepository).Setup(x => x.GetById(100)).Returns(agencia);

            var conta = new Conta() { Id = 3621, AgenciaId = 8792, NomeCliente = "Jéssica", CPFCliente = "004.887.380-24", Saldo = 100m };
            Mock.Get(contaCorrente.ContaRepository).Setup(c => c.GetById(100, 8792)).Returns(conta);
           
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
           
            // act
            string msgErro;
            var result = contaCorrente.Deposito(8792, 3621, 50m, out msgErro); 

            //assert
            Assert.IsTrue(result);
            Mock.Get(contaCorrente.ContaRepository).Verify(x => x.Save(It.Is<Conta>(c => c.AgenciaId.Equals(8792) && c.Id == 3621 && c.Saldo == 150m)));
            Mock.Get(contaCorrente.ExtratoRepository).Verify(x => x.Save(It.Is<Extrato>(e => e.AgenciaId == 8792 && e.ContaId == 3621 && e.Descricao == "Depósito" && e.Valor == 50m && e.Saldo == 150m && e.DataRegistro == DateTime.Today)));
        }

        [TestMethod]
        public void Saque_Erro_SeAgenciaNaoExistir()
        {
            // arrange
            var contaCorrente = GetContaCorrente();

            // act
            string msgErro;
            var result = contaCorrente.Saque(8792, 666, 50m, out msgErro); // conta não existe

            //assert
            Assert.IsFalse(result);
            Assert.AreEqual("Agência é invalida!", msgErro);
        }

        [TestMethod]
        public void Saque_ErroSeContaNaoExistirNaAgencia()
        {
            // arrange
            var contaCorrente = GetContaCorrente();

            // act
            string msgErro;
            var result = contaCorrente.Saque(666, 3621, 50m, out msgErro); // conta não existe

            //assert
            Assert.IsFalse(result);
            Assert.AreEqual("Conta é invalida!", msgErro);
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
        public void Transferencia_Erro_SeContaOrigemNaoExistirNaAgencia()
        {
            // arrange
            var contaCorrente = GetContaCorrente();

            // act
            string msgErro;
            var result = contaCorrente.Transferencia(8792, 666, 50m, 200,700, out msgErro); // conta não existe

            //assert
            Assert.IsFalse(result);
            Assert.AreEqual("Conta de origem é invalida!", msgErro);
        }

        [TestMethod]
        public void Transferencia_Erro_SeAgenciaDestinoNaoExistir()
        {
            // arrange
            var contaCorrente = GetContaCorrente();

            // act
            string msgErro;
            var result = contaCorrente.Transferencia(8792, 3621, 50m, 200,666, out msgErro); // ag destino inválida

            //assert
            Assert.IsFalse(result);
            Assert.AreEqual("Conta de destino inv", msgErro);
        }

        [TestMethod]
        public void Transferencia_Erro_SeContaDestinoNaoExistirNaAgencia()
        {
            // arrange
            var contaCorrente = GetContaCorrente();

            // act
            string msgErro;
            var result = contaCorrente.Transferencia(8792, 3621, 50m, 200, 666, out msgErro); // valor menor igual 0

            //assert
            Assert.IsFalse(result);
            Assert.AreEqual("Valor do depósito deverá ser maior do que 0", msgErro);
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
            // arrange
            var contaCorrente = GetContaCorrente();

            // act
            string msgErro;
            var result = contaCorrente.Saldo(666, 8792, out msgErro);

            //assert
            Assert.AreEqual(0m, result);
            Assert.AreEqual("Agência inválida!", msgErro);
        }

        [TestMethod]
        public void Extrato_erro_SeAgenciaNaoExistir()
        {
            // arrange
            var contaCorrente = GetContaCorrente();

            // act
            string msgErro;
            var result = contaCorrente.Extrato(666, 8792, new DateTime(2020,02,01), new DateTime(2020, 02, 16), out msgErro);

            //assert
            Assert.IsNull(result);
            Assert.AreEqual("Agência inválida!", msgErro);
        }

        [TestMethod]
        public void SaldoErro_SeContaNaoExistirNaAgencia()
        {
            // arrange
            var contaCorrente = GetContaCorrente();

            // act
            string msgErro;
            var result = contaCorrente.Saldo(8792, 666, out msgErro); // conta não existe

            //assert
            Assert.AreEqual(0m, result);
            Assert.AreEqual("Conta de origem é invalida!", msgErro);
        }

        [TestMethod]
        public void Extrato_RetornaRegistroExtrato()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void Extrato_Erro_SeContaNaoExistirNaAgencia()
        {
            // arrange
            var contaCorrente = GetContaCorrente();

            // act
            string msgErro;
            var result = contaCorrente.Extrato(8792, 55, new DateTime(2020,01,01), new DateTime(2020, 01, 15), out msgErro); // conta não existe

            //assert
            Assert.IsNull(result);
            Assert.AreEqual("Conta de origem é invalida!", msgErro);
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
