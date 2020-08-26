﻿using System;
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

        [Fact(DisplayName = "Conta Investimento - Depósito agência não existe")]
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
        
        [Fact(DisplayName = "Conta Investimento - Depósito conta não existe")]
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
        
        [Fact(DisplayName = "Conta Investimento - Depósito maior ou igual 50")]
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
            Mock.Get(contaInvestimento.ContaInvestimentoRepository).Verify(x => x.Save(It.Is<ContaInvestimento>(c => c.AgenciaId == 0001 && c.Numero.Equals(1040) && c.Banco == "Easyinvest" && c.Saldo == 600m)));
            Mock.Get(contaInvestimento.ExtratoInvestimentoRepository).Verify(x => x.Save(It.Is<ExtratoInvetimento>(e => e.IdAgencia == 0001 && e.IdConta == 1040 && e.Valor == 300m && e.Saldo == 600m && e.Descricao == "Depósito")));
        }

        [Fact(DisplayName = "Conta Investimento - Transferência agência origem não existe")]
        [Trait("Categoria", "Conta Investimento")]
        public void ContaInvestimento_Transferencia_ErroAgenciaOrigemNaoExiste()
        {
            // Arrange
            var contaInvestimento = GetContaInvestimento();

            // Act
            string msgErro;
            var result = contaInvestimento.Transferencia(0, 2, 500m, 0001, 0, out msgErro);

            // Assert
            Assert.False(result);
            Assert.Equal("Agência de origem não existe!", msgErro);
        }

        [Fact(DisplayName = "Conta Investimento - Transferência conta de origem não existe")]
        [Trait("Categoria", "Conta Investimento")]
        public void ContaInvestimento_Transferencia_ErroContaOrigemNaoExisteNaAgencia()
        {
            var agenciaDestino = 8792;

            // Arrange
            var contaInvestimento = GetContaInvestimento();

            // Act
            string msgErro;
            var result = contaInvestimento.Transferencia(0001, 0, 500m, agenciaDestino, 0, out msgErro);

            // Assert
            Assert.False(result);
            Assert.Equal("Conta de origem não existe!", msgErro);
        }

        [Fact(DisplayName = "Conta Investimento - Transferência agência destino não existe")]
        [Trait("Categoria", "Conta Investimento")]
        public void ContaInvestimento_Transferencia_ErroSeAgenciaDestinoNaoExiste()
        {
            // Arrange
            var contaInvestimento = GetContaInvestimento();

            // Act
            string msgErro;
            var result = contaInvestimento.Transferencia(0002, 1040, 1000m, 0, 1050, out msgErro);

            // Assert
            Assert.False(result);
            Assert.Equal("Agêncica de destino não existe!", msgErro);
        }

        
        [Fact(DisplayName = "Conta Investimento - Transferência conta destino não existe")]
        [Trait("Categoria", "Conta Investimento")]
        public void ContaInvestimento_Transferencia_ErroSeContaDestinoNaoExiste()
        {
            var agOrigem = 0001;
            var ccOrigem = 1040;
            var valor = 1000m;

            // Arrange
            var contaInvestimento = GetContaInvestimento();

            // Act
            string msgErro;
            var result = contaInvestimento.Transferencia(agOrigem, ccOrigem, valor, 0002, 0, out msgErro);

            // Assert
            Assert.False(result);
            Assert.Equal("Conta destino não existe!", msgErro);
        }

        [Fact(DisplayName = "Conta Investimento - Transferência maior ou igual a 0")]
        [Trait("Categoria", "Conta Investimento")]
        public void ContaInvestimento_Transferencia_ValorMaiorOuIgualZero()
        {
            throw new NotImplementedException();
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
    }
}
