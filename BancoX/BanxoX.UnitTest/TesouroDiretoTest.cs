using System;
using BancoX;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BanxoX.UnitTest
{
    [TestClass]
    public class TesouroDiretoTest
    {
        [TestMethod]
        public void Erro_TituloNaoExiste()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void CarteiraInvestidorNaoExiste_Erro()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void CarteiraInvestidorVazia_Erro()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void Investir_ValorMaiorQueValorMinimoTitulo()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void ResgateNaoDeveSerMaiorQueSeisMesesDoValorDoTitulo_Erro()
        {
            Assert.Inconclusive();

            //quando tentar sacar menos de seis meses da data da aplicação
        }

        [TestMethod]
        public void CobrancaImpostoRendaAteSeisMeses_Sucess()
        {
            Assert.Inconclusive();

            // regra do desconto do imposto de renda ao resgatar em até seis meses da data da aplicação

            // desconto IR 22,5%
        }

        [TestMethod]
        public void CobrancaImpostoRendaAteUmAno_Sucess()
        {
            Assert.Inconclusive();

            // regra do desconto do imposto de renda ao resgatar em até um ano da data da aplicação

            // desconto IR 20%
        }

        [TestMethod]
        public void CobrancaImpostoRendaAteDoisAno_Sucess()
        {
            Assert.Inconclusive();

            // regra do desconto do imposto de renda ao resgatar em até dois anos da data da aplicação

            // desconto IR 17, 5%
        }

        [TestMethod]
        public void CobrancaImpostoRendaAcimaDoisAno_Sucess()
        {
            Assert.Inconclusive();

            // regra do desconto do imposto de renda ao resgatar em até dois anos da data da aplicação

            // desconto IR 15%
        }

    }
}
