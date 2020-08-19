using System;
using BancoX;
using BancoX.Interface;
using BancoX.Model;
using Moq;
using Xunit;

namespace BanxoX.UnitTest
{
    public class ContaInvestimentoServiceTest
    {
        private ContaInvestimentoService GetContaInvestimento()
        {
            var contaInvestimento = new ContaInvestimentoService(
                 Mock.Of<IExtratoInvetimentoRepository>(),
                 Mock.Of<IContaInvestimentoRepository>(),
                 Mock.Of<IAgenciaRepository>(),
                 Mock.Of<IContaRepository>()
                );

            var agenciaNorte = new Agencia() { Id = 8792, Nome = "Agência Norte", Banco = "Banco Nacional" };
            var agenciaSul = new Agencia() { Id = 200, Nome = "Agência Sul", Banco = "Banco Sul" };

            var conta = new Conta() { Id = 3621, AgenciaId = 8792, Cliente = new Cliente() { Nome = "Jéssica Nathany", CPF = "004.887.380-24" }, Saldo = 100m };
            var conta2 = new Conta() { Id = 700, AgenciaId = 200, Cliente = new Cliente() { Nome = "Peter Pan", CPF = "014.121.350-50" }, Saldo = 700m };

            var contaInvest = new ContaInvestimento() { Id = 1, AgenciaId = 0001, Banco = "Easyinvest", Saldo = 1000M, Numero = 1040 };
            var contaInvest2 = new ContaInvestimento() { Id = 2, AgenciaId = 0002, Banco = "XP", Saldo = 2000M, Numero = 1050 };

            Mock.Get(contaInvestimento._AgenciaRepository).Setup(c => c.GetById(agenciaNorte.Id)).Returns(agenciaNorte);
            Mock.Get(contaInvestimento._AgenciaRepository).Setup(c => c.GetById(agenciaSul.Id)).Returns(agenciaSul);

            Mock.Get(contaInvestimento._IContaInvestimentoRepository).Setup(c => c.GetById(contaInvest.Id)).Returns(contaInvest);
            Mock.Get(contaInvestimento._IContaInvestimentoRepository).Setup(c => c.GetById(contaInvest2.Id)).Returns(contaInvest2);

            Mock.Get(contaInvestimento._ContaRepository).Setup(c => c.GetById(conta.Id, conta.AgenciaId)).Returns(conta);
            Mock.Get(contaInvestimento._ContaRepository).Setup(c => c.GetById(conta2.Id, conta2.AgenciaId)).Returns(conta2);

            return contaInvestimento;
        }

        [Fact(DisplayName = "Agência não existe")]
        [Trait("Categoria", "Conta Investimento")]
        public void ContaInvestimento_Deposito_AgenciaNaoExiste_Erro()
        {
            var numero = 2000;
            var banco = "EasyInvest";
            var valor = 1000M;

            // Arrange
            var contaInvestimento = GetContaInvestimento();

            // Act
            string msgErro;
         
            var result = contaInvestimento.Deposito(0, numero, banco, valor, out msgErro);

            // Assert
            Assert.False(result);
            Assert.Equal("Agência inválida!", msgErro);
        }

        
        [Fact(DisplayName = "Conta Investimento não existe")]
        [Trait("Categoria", "Conta Investimento")]
        public void ContaInvestimento_Deposito_ContaInvestidorNaoExiste_Erro()
        {
            var banco = "EasyInvest";
            var valor = 1000M;
            var idAgencia = 8792;

            // arrange
            var contaInvestimento = GetContaInvestimento();

            // act
            string msgErro;
            var result = contaInvestimento.Deposito(idAgencia, 0, banco, valor, out msgErro);

            // assert
            Assert.False(result);
            Assert.Equal("Conta inválida!", msgErro);
        }
        
        [Fact(DisplayName = "Valor do depósito maior ou igual 50")]
        [Trait("Categoria", "Conta Investimento")]
        public void ContaInvestimento_Deposito_ValorMaiorOuIgualCinquenta_Erro()
        {
            var valorDeposito = 30M;

            // Arrange
            var contaInvestimento = GetContaInvestimento();

            // Act
            string msgErro;
            var result = contaInvestimento.Deposito(2, 1050, "XP", valorDeposito, out msgErro);

            // Assert
            Assert.False(result);
            Assert.Equal("O valor do depósito precisa ser maior ou igual a 50!", msgErro);
        }
        
        [Fact(DisplayName = "Calcula agência de origem não existe")]
        [Trait("Categoria", "Conta Investimento")]
        public void ContaInvestimento_Transferencia_ErroAgenciaOrigemNaoExiste()
        {
            var contaInvestOrigem = 2;
            var valor = 500;
            var agenciaDestino = 8792;

            // Arrange
            var contaInvestimento = GetContaInvestimento();

            // Act
            string msgErro;
            var result = contaInvestimento.Transferencia(0, contaInvestOrigem, valor, agenciaDestino, 0, out msgErro);

            // Assert
            Assert.False(result);
            Assert.Equal("Agência origem inválida!", msgErro);
        }

        [Fact(DisplayName = "Calcula agência não existe")]
        [Trait("Categoria", "Conta Investimento")]
        public void ContaInvestimento_Transferencia_ErroContaOrigemNaoExisteNaAgencia()
        {
            var agenciaInvestOrigem = 0002;
            var contaInvestOrigem = 2;
            var valor = 500;
            var agenciaDestino = 8792;

            // Arrange
            var contaInvestimento = GetContaInvestimento();

            // Act
            string msgErro;
            var result = contaInvestimento.Transferencia(agenciaInvestOrigem, contaInvestOrigem, valor, agenciaDestino, 0, out msgErro);

            // Assert
            Assert.False(result);
            Assert.Equal("Conta origem inválida!", msgErro);
        }

        
        [Fact(DisplayName = "Calcula agêcnia destino não existe")]
        [Trait("Categoria", "Conta Investimento")]
        public void ContaInvestimento_Transferencia_ErroSeAgenciaDestinoNaoExiste()
        {
            var agenciaInvestOrigem = 0002;
            var contaInvestOrigem = 2;
            var contaDestino = 3621;
            var valor = 500;
         

            // Arrange
            var contaInvestimento = GetContaInvestimento();

            // Act
            string msgErro;
            var result = contaInvestimento.Transferencia(agenciaInvestOrigem, contaInvestOrigem, valor, 0, contaDestino, out msgErro);

            // Assert
            Assert.False(result);
            Assert.Equal("Agência destino inválida!", msgErro);
        }

        
        [Fact(DisplayName = "Calcula Conta não existe")]
        [Trait("Categoria", "Conta Investimento")]
        public void ContaInvestimento_Transferencia_ErroSeContaDestinoNaoExiste()
        {
            var agenciaInvestOrigem = 0002;
            var contaInvestOrigem = 2;
            var agenciaDestino = 8792;
            var valor = 500;


            // Arrange
            var contaInvestimento = GetContaInvestimento();

            // Act
            string msgErro;
            var result = contaInvestimento.Transferencia(agenciaInvestOrigem, contaInvestOrigem, valor, agenciaDestino, 0, out msgErro);

            // Assert
            Assert.False(result);
            Assert.Equal("Conta destino inválida!", msgErro);
        }

        
        [Fact(DisplayName = "Calcula data maior ou igual do que data minima do resgate")]
        [Trait("Categoria", "Conta Investimento")]
        public void ContaInvestimento_Resgate_DataDeveraSerMaiorOuIgualQueDataMininaDoResgate_Erro()
        {
            double valorResgate = 5000;
            int idAgencia = 0001;
            int conta = 1;
            string nomeBanco = "Easyinvest";
            DateTime dataRetirada = DateTime.Now;
            DateTime dataVenci = DateTime.Now.AddDays(60);

            // Arrange
            var contaInvestimento = GetContaInvestimento();
            
            // Act
            string msgErro;
            var result = contaInvestimento.ResgateInvestimento(valorResgate, idAgencia, conta, nomeBanco, dataRetirada, dataVenci, out msgErro);

            // Assert
            Assert.False(result);
            Assert.Equal("A data de resgate deve ser maior ou igual a data de vencimento do título!", msgErro);
        }

        
        [Fact(DisplayName = "Calcula Imposto de Renda até 6 meses")]
        [Trait("Categoria", "Conta Investimento")]
        public void ContaInvestimento_CalculoImpostoRenda_AteSeisMeses()
        {
            double valorTitulo = 500.00;
            DateTime dataAplicacao = DateTime.Now.AddMonths(-6);
            DateTime dataVencimento = dataAplicacao.AddYears(4);

            var calcIR = (22.5 / 100) * valorTitulo; 

            // Arrange
            var contaInvestimento = GetContaInvestimento();

            // Act
            string msgErro;
            var result = contaInvestimento.CalculaDescontoImpostoRenda(valorTitulo, dataAplicacao, dataVencimento, out msgErro);

            // Assert
            Assert.Equal(result, calcIR);
            Assert.Equal("Será cobrado 22,5% de imposto de renda do seu lucro!", msgErro);
        }

        
        [Fact(DisplayName = "Calcula Imposto de Renda acima de 1 anos")]
        [Trait("Categoria", "Conta Investimento")]
        public void ContaInvestimento_CalculoImpostoRenda_AteUmAno()
        {
            double valorTitulo = 500.00;
            DateTime dataAplicacao = DateTime.Now.AddMonths(-6);
            DateTime dataVencimento = dataAplicacao.AddYears(4);

            var calcIR = (20/100) * valorTitulo; 

            // Arrange
            var contaInvestimento = GetContaInvestimento();

            // Act
            string msgErro;
            var result = contaInvestimento.CalculaDescontoImpostoRenda(valorTitulo, dataAplicacao, dataVencimento, out msgErro);

            // Assert
            Assert.Equal(result, calcIR);
            Assert.Equal("Será cobrado 20% de imposto de renda do seu lucro!", msgErro);
        }

        
        [Fact(DisplayName = "Calcula Imposto de Renda acima de 2 anos")]
        [Trait("Categoria", "Conta Investimento")]
        public void ContaInvestimento_CalculoImpostoRenda_AcimaDoisAno()
        {
            double valorTitulo = 680.00;
            DateTime dataAplicacao = DateTime.Now.AddMonths(-6);
            DateTime dataVencimento = dataAplicacao.AddYears(4);

            var calcIR = (15 / 100) * valorTitulo;

            // Arrange
            var contaInvestimento = GetContaInvestimento();

            // Act
            string msgErro;
            var result = contaInvestimento.CalculaDescontoImpostoRenda(valorTitulo, dataAplicacao, dataVencimento, out msgErro);

            // Assert
            Assert.Equal(result, calcIR);
            Assert.Equal("Será cobrado 15% de imposto de renda do seu lucro!", msgErro);
        }
    }
}
