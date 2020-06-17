using System;
using BancoX;
using BancoX.Interface;
using BancoX.Model;
using Moq;
using Xunit;

namespace BanxoX.UnitTest
{
    public class ContaInvestimentoTest
    {
        private ContaInvestimento GetContaInvestimento()
        {
            var contaInvestimento = new ContaInvestimento(
                 Mock.Of<IExtratoInvetimentoRepository>(),
                 Mock.Of<IContaInvestimentoRepository>(),
                 Mock.Of<IAgenciaRepository>()
                );

            var agenciaNorte = new Agencia() { Id = 8792, Nome = "Agência Norte" };
            var agenciaSul = new Agencia() { Id = 200, Nome = "Agência Sul" };

            var conta = new Conta() { Id = 3621, AgenciaId = 8792, Cliente = new Cliente() { Nome = "Jéssica Nathany", CPF = "004.887.380-24" }, Saldo = 100m };
            var conta2 = new Conta() { Id = 700, AgenciaId = 200, Cliente = new Cliente() { Nome = "Peter Pan", CPF = "014.121.350-50" }, Saldo = 700m };

            return contaInvestimento;
        }

        [Fact]
        public void ContaInvestimento_Deposito_AgenciaNaoExiste()
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

        [Fact]
        public void ContaInvestimento_Deposito_ContaInvestidorNaoExiste()
        {
            var numero = 0;
            var banco = "EasyInvest";
            var valor = 1000M;
            var idAgencia = 8792;

            // arrange
            var contaInvestimento = GetContaInvestimento();

            // act
            string msgErro;
            var result = contaInvestimento.Deposito(idAgencia, numero, banco, valor, out msgErro);

            // assert
            Assert.False(result);
            Assert.Equal("Conta inválida!", msgErro);
        }

        [Fact]
        public void ContaInvestimento_Deposito_ValorInferior50()
        {
            throw new NotImplementedException();

            // valor do depósito na carteira tem que ser maior ou igual a 50.00
        }

        [Fact]
        public void ContaInvestimento_Resgate_ValorMaiorQueValorMinimoDoTitulo_Erro()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void ContaInvestimento_Resgate_ValorNaoDeveSerMaiorQueSeisMesesDoValorDoTitulo_Erro()
        {

            throw new NotImplementedException();
            //quando tentar sacar menos de seis meses da data da aplicação
        }

        [Fact]
        public void ContaInvestimento_CobrancaImpostoRendaAteSeisMeses_Sucess()
        {
            throw new NotImplementedException();

            // regra do desconto do imposto de renda ao resgatar em até seis meses da data da aplicação

            // desconto IR 22,5%
        }

        [Fact]
        public void ContaInvestimento_DescontoImpostoRenda_ImpostoRendaAteUmAno()
        {
            throw new NotImplementedException();

            // regra do desconto do imposto de renda ao resgatar em até um ano da data da aplicação

            // desconto IR 20%
        }

        [Fact]
        public void ContaInvestimento_DescontoImpostoRenda_CobrancaImpostoRendaAteDoisAno()
        {
            throw new NotImplementedException();

            // regra do desconto do imposto de renda ao resgatar em até dois anos da data da aplicação

            // desconto IR 17, 5%
        }

        [Fact]
        public void ContaInvestimento_CobrancaImpostoRendaAcimaDoisAno()
        {
            throw new NotImplementedException();

            // regra do desconto do imposto de renda ao resgatar em até dois anos da data da aplicação

            // desconto IR 15%
        }

    }
}
