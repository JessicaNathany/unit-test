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

            var agenciaDigital = new Agencia() { Id = 0001, Nome = "Easyinvest SC", Banco = "Easyinvest", Tipo = Tipo.Digital };
            var agenciaDigital2 = new Agencia() { Id = 0002, Nome = "XP Ltda.", Banco = "XP Investimentos", Tipo = Tipo.Digital };

            var conta = new Conta() { Id = 3621, AgenciaId = 8792, Cliente = new Cliente() { Nome = "Jéssica Nathany", CPF = "004.887.380-24" }, Saldo = 100m };
            var conta2 = new Conta() { Id = 700, AgenciaId = 200, Cliente = new Cliente() { Nome = "Peter Pan", CPF = "014.121.350-50" }, Saldo = 700m };

            var contaInvest = new ContaInvestimento() { Id = 1, AgenciaId = 0001, Banco = "Easyinvest", Saldo = 300m, Numero = 1040 };
            var contaInvest2 = new ContaInvestimento() { Id = 2, AgenciaId = 0002, Banco = "XP Investimentos", Saldo = 2000M, Numero = 1050 };

            // Mockup dos dados (simulando base, em memória)
            Mock.Get(contaInvestimento.AgenciaRepository).Setup(c => c.GetById(agenciaDigital.Id)).Returns(agenciaDigital);
            Mock.Get(contaInvestimento.AgenciaRepository).Setup(c => c.GetById(agenciaDigital2.Id)).Returns(agenciaDigital2);

            Mock.Get(contaInvestimento.ContaInvestimentoRepository).Setup(c => c.GetById(contaInvest.AgenciaId, contaInvest.Numero)).Returns(contaInvest);
            Mock.Get(contaInvestimento.ContaInvestimentoRepository).Setup(c => c.GetById(contaInvest2.AgenciaId, contaInvest.Numero)).Returns(contaInvest2);

            Mock.Get(contaInvestimento.ContaRepository).Setup(c => c.GetById(conta.Id, conta.AgenciaId)).Returns(conta);
            Mock.Get(contaInvestimento.ContaRepository).Setup(c => c.GetById(conta2.Id, conta2.AgenciaId)).Returns(conta2);

            return contaInvestimento;
        }

        [Fact(DisplayName = "Agência não existe")]
        [Trait("Categoria", "Conta Investimento")]
        public void ContaInvestimento_Deposito_AgenciaNaoExiste_Erro()
        {
            var numero = 1040;
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
            var idAgencia = 0001;

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
            // Arrange
            var contaInvestimento = GetContaInvestimento();

            // Act
            string msgErro;
            var result = contaInvestimento.Deposito(0001, 1040, "Easyinvest", 30M, out msgErro);

            // Assert
            Assert.False(result);
            Assert.Equal("O valor do depósito precisa ser maior ou igual a 50!", msgErro);
        }

        [Fact(DisplayName = "Conta Investimento - Depósito realizado com sucesso")]
        [Trait("Categoria", "Conta Investimento")]
        public void ContaInvestimento_Deposito_RetornaSuccess()
        {
            // Arrange
            var contaInvestimento = GetContaInvestimento();

            // Act
            string msgErro;
            var result = contaInvestimento.Deposito(0001, 1040, "Easyinvest", 300m, out msgErro);

            // Assert
            Assert.True(result);
            Mock.Get(contaInvestimento.ContaInvestimentoRepository).Verify(x => x.Save(It.Is<ContaInvestimento>(c => c.AgenciaId == 0001 && c.Numero.Equals(1040) && c.Banco == "Easyinvest" && c.Saldo == 300m)));
            Mock.Get(contaInvestimento.ExtratoInvestimentoRepository).Verify(x => x.Save(It.Is<ExtratoInvetimento>(e => e.IdCarteira == 1 && e.IdAgencia == 0001 && e.IdConta == 1040 && e.Valor == 300m && e.Saldo == 600m && e.Descricao == "Depósito")));
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
