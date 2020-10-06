using System;
using BancoX;
using BancoX.Interface;
using BancoX.Model;
using BancoX.Service;
using Moq;
using Xunit;

namespace BanxoX.UnitTest.ServiceTest
{
    public class TesouroDiretoServiceTest
    {
        //private TesouroDiretoService GetTesouroDiretoServiceTest()
        //{

        //}



        //[Fact(DisplayName = "Calcula Imposto de Renda até 6 meses")]
        //[Trait("Categoria", "Conta Investimento")]
        //public void ContaInvestimento_CalculoImpostoRenda_AteSeisMeses()
        //{
        //    double valorTitulo = 500.00;
        //    DateTime dataAplicacao = DateTime.Now.AddMonths(-6);
        //    DateTime dataVencimento = dataAplicacao.AddYears(4);

        //    var calcIR = (22.5 / 100) * valorTitulo;

        //    // Arrange
        //    var contaInvestimento = GetContaInvestimento();

        //    // Act
        //    string msgErro;
        //    var result = contaInvestimento.CalculaDescontoImpostoRenda(valorTitulo, dataAplicacao, dataVencimento, out msgErro);

        //    // Assert
        //    Assert.Equal(result, calcIR);
        //    Assert.Equal("Será cobrado 22,5% de imposto de renda do seu lucro!", msgErro);
        //}


        //[Fact(DisplayName = "Calcula Imposto de Renda acima de 1 anos")]
        //[Trait("Categoria", "Conta Investimento")]
        //public void ContaInvestimento_CalculoImpostoRenda_AteUmAno()
        //{
        //    double valorTitulo = 500.00;
        //    DateTime dataAplicacao = DateTime.Now.AddMonths(-6);
        //    DateTime dataVencimento = dataAplicacao.AddYears(4);

        //    var calcIR = (20 / 100) * valorTitulo;

        //    // Arrange
        //    var contaInvestimento = GetContaInvestimento();

        //    // Act
        //    string msgErro;
        //    var result = contaInvestimento.CalculaDescontoImpostoRenda(valorTitulo, dataAplicacao, dataVencimento, out msgErro);

        //    // Assert
        //    Assert.Equal(result, calcIR);
        //    Assert.Equal("Será cobrado 20% de imposto de renda do seu lucro!", msgErro);
        //}


        //[Fact(DisplayName = "Calcula Imposto de Renda acima de 2 anos")]
        //[Trait("Categoria", "Conta Investimento")]
        //public void ContaInvestimento_CalculoImpostoRenda_AcimaDoisAno()
        //{
        //    double valorTitulo = 680.00;
        //    DateTime dataAplicacao = DateTime.Now.AddMonths(-6);
        //    DateTime dataVencimento = dataAplicacao.AddYears(4);

        //    var calcIR = (15 / 100) * valorTitulo;

        //    // Arrange
        //    var contaInvestimento = GetContaInvestimento();

        //    // Act
        //    string msgErro;
        //    var result = contaInvestimento.CalculaDescontoImpostoRenda(valorTitulo, dataAplicacao, dataVencimento, out msgErro);

        //    // Assert
        //    Assert.Equal(result, calcIR);
        //    Assert.Equal("Será cobrado 15% de imposto de renda do seu lucro!", msgErro);
        //}
    }
}
