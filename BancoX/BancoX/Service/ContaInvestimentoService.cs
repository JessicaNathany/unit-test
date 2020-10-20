using System;
using System.Collections.Generic;
using System.Transactions;
using BancoX.Interface;
using FluentValidation.Results;

namespace BancoX
{
    public class ContaInvestimentoService : IContaInvestimentoService
    {
        public IAgenciaRepository AgenciaRepository { get; private set; }

        public IContaRepository ContaRepository { get; private set; }

        public IExtratoInvetimentoRepository ExtratoInvestimentoRepository { get; private set; }

        public IContaInvestimentoRepository ContaInvestimentoRepository { get; private set; }

        public ContaInvestimentoService(IExtratoInvetimentoRepository extratoInvestimentoRepository, IContaInvestimentoRepository contaInvestimentoRepository, IAgenciaRepository agenciaRepository, IContaRepository contaRepository)
        {
            ExtratoInvestimentoRepository = extratoInvestimentoRepository;
            ContaInvestimentoRepository = contaInvestimentoRepository;
            AgenciaRepository = agenciaRepository;
            ContaRepository = contaRepository;
        }

        public bool Deposito(int idAgencia, int numero, string banco, decimal valor, out string mensagemErro)
        {
            mensagemErro = "";

            var agencia = AgenciaRepository.GetById(idAgencia);

            var contaInvestimento = ContaInvestimentoRepository.GetById(idAgencia, numero);

            if (agencia == null)
            {
                mensagemErro = "Agência inválida!";
                return false;
            }

            if (contaInvestimento == null)
            {
                mensagemErro = "Conta inválida!";
                return false;
            }

            if (valor < 50)
            {
                mensagemErro = "O valor do depósito precisa ser maior do que 0!";
                return false;
            }

            contaInvestimento.Saldo = contaInvestimento.Saldo + valor;

            var extrato = new ExtratoInvetimento
            {
                DataRegistro = DateTime.Now,
                Saldo = contaInvestimento.Saldo,
                IdAgencia = 0001,
                IdConta = 1040,
                Valor = 300m,
                Descricao = "Depósito"
            };

            try
            {
                using (var transaction = new TransactionScope())
                {
                    ContaInvestimentoRepository.Save(contaInvestimento);
                    ExtratoInvestimentoRepository.Save(extrato);
                    transaction.Complete();
                }
            }
            catch (Exception)
            {
                mensagemErro = "Ocorreu um erro ao fazer o depósito!";
                return false;
            }

            return true;
        }

        public IList<ExtratoInvetimento> Extrato(int idAgencia, int conta, DateTime dataInicio, DateTime dataFim, out string mensagemErro)
        {
            mensagemErro = "";

            if (dataInicio > dataFim)
            {
                mensagemErro = "A data de inicio deve ser menor do que a data fim!";
                return null;
            }

            try
            {
                var extradoInvestimentoLista = ExtratoInvestimentoRepository.GetByPeriodo(idAgencia, conta, dataInicio, dataFim);
                return extradoInvestimentoLista;
            }
            catch (Exception)
            {
                mensagemErro = "Ocorreu um erro ao fazer obter o extrato!";
                return null;
            }
        }

        public bool ResgateInvestimento(double valorRetirada, int idAgencia, int contaCorrente, string nomeBanco, DateTime dataResgate, DateTime dataVencimentoTitulo, out string mensagemErro)
        {
            mensagemErro = "";
           
            if (valorRetirada <= 0)
            {
                mensagemErro = "Valor da retirada do título deverá ser maior do que zero!";
                return false;
            }

            if (dataResgate < dataVencimentoTitulo)
            {
                mensagemErro = "A data de resgate deve ser maior do que a data de vencimento do título!";
                return false;
            }
           
            return true;
        }

        public bool Transferencia(int agenciaOrigem, int contaOrigem, decimal valor, int agenciaDestino, int contaDestino, out string mensagemErro)
        {
            mensagemErro = "";

            var agencia = AgenciaRepository.GetById(agenciaOrigem);

            var agDestino = AgenciaRepository.GetById(agenciaDestino);

            var contaInvestimento = ContaInvestimentoRepository.GetById(agenciaOrigem, contaOrigem);

            var ccDestino = ContaInvestimentoRepository.GetById(agenciaDestino, contaDestino);

            if (agencia == null)
            {
                mensagemErro = "Agência de origem não existe!";
                return false;
            }

            if (agDestino == null)
            {
                mensagemErro = "Agêncica de destino não existe!";
                return false;
            }

            if (contaInvestimento == null)
            {
                mensagemErro = "Conta de origem não existe!";
                return false;
            }

            if (ccDestino == null)
            {
                mensagemErro = "Conta destino não existe!";
                return false;
            }

            if (valor <= 0)
            {
                mensagemErro = "O valor da transferência precisa deve ser maior do que 0!";
                return false;
            }

            contaInvestimento.Saldo = contaInvestimento.Saldo - valor;

            var extratoContaOrigem = new ExtratoInvetimento()
            {
                DataRegistro = DateTime.Now,
                IdAgencia = contaInvestimento.Id,
                IdConta = contaInvestimento.Id,
                Valor = valor * -1,
                Saldo = contaInvestimento.Saldo,
                Descricao = $"Transferência para Ag {agenciaDestino} Cc {contaDestino}",
            };

            contaInvestimento.Saldo = ccDestino.Saldo + valor;

            ccDestino.Saldo = ccDestino.Saldo + valor;

            var extratoContaDestino = new ExtratoInvetimento()
            {
                DataRegistro = DateTime.Now,
                IdAgencia = ccDestino.AgenciaId,
                IdConta = ccDestino.Id,
                Valor = valor,
                Saldo = ccDestino.Saldo,
                Descricao = $"Transferência para Ag {agenciaOrigem} Cc {contaInvestimento}",
            };

            try
            {
                using (var transaction = new TransactionScope())
                {
                    ContaInvestimentoRepository.Save(contaInvestimento);
                    ExtratoInvestimentoRepository.Save(extratoContaOrigem);
                    ExtratoInvestimentoRepository.Save(extratoContaDestino);
                    transaction.Complete();
                }
            }
            catch (Exception)
            {
                mensagemErro = "Ocorreu um problema ao fazer a tranferência!";
                return false;
            }

            return true;
        }
    }
}
