using System;
using BancoX;
using BancoX.Interface;
using Moq;
using Xunit;

namespace BanxoX.UnitTest
{
  
    public class TesouroDiretoTest
    {
        private Carteira GetCarteira()
        {
            var carteira = new Carteira(
                 Mock.Of<IExtratoInvetimentoRepository>(),
                 Mock.Of<ICarteiraRepository>()
                );

            return carteira;
        }

        [Fact]
        public void Erro_TituloNaoExiste()
        {
            
        }

        [Fact]
        public void Investir_CarteiraInvestidorNaoExiste_Erro()
        {
            // arrange


            // act

            // assert
        }

        [Fact]
        public void CarteiraInvestidorSaldoInferior_Erro()
        {
            throw new NotImplementedException();

            // carteira do investidor sem saldo
        }

        [Fact]
        public void Investir_ValorMaiorQueValorMinimoDoTitulo()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void ResgateNaoDeveSerMaiorQueSeisMesesDoValorDoTitulo_Erro()
        {

            throw new NotImplementedException();
            //quando tentar sacar menos de seis meses da data da aplicação
        }

        [Fact]
        public void CobrancaImpostoRendaAteSeisMeses_Sucess()
        {
            throw new NotImplementedException();

            // regra do desconto do imposto de renda ao resgatar em até seis meses da data da aplicação

            // desconto IR 22,5%
        }

        [Fact]
        public void CobrancaImpostoRendaAteUmAno_Sucess()
        {
            throw new NotImplementedException();

            // regra do desconto do imposto de renda ao resgatar em até um ano da data da aplicação

            // desconto IR 20%
        }

        [Fact]
        public void CobrancaImpostoRendaAteDoisAno_Sucess()
        {
            throw new NotImplementedException();

            // regra do desconto do imposto de renda ao resgatar em até dois anos da data da aplicação

            // desconto IR 17, 5%
        }

        [Fact]
        public void CobrancaImpostoRendaAcimaDoisAno_Sucess()
        {
            throw new NotImplementedException();

            // regra do desconto do imposto de renda ao resgatar em até dois anos da data da aplicação

            // desconto IR 15%
        }

    }
}
