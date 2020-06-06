using BancoX.Interface;
using System;
using System.Collections.Generic;
using System.Transactions;

namespace BancoX
{
   
    public class ContaCorrente : IContaCorrente
    {
        public ContaCorrente(IAgenciaRepository agenciaRepository, IContaRepository contaRepository, IExtratoRepository extratoRepository)
        {
            AgenciaRepository = agenciaRepository;
            ContaRepository = contaRepository;
            ExtratoRepository = extratoRepository;
        }

        public IAgenciaRepository AgenciaRepository { get; set; }

        public IContaRepository ContaRepository { get; set; }

        public IExtratoRepository ExtratoRepository { get; set; }

        public bool Deposito(int idAgencia, int conta, decimal valor, out string mensagemErro)
        {
            mensagemErro = "";

            var agencia = AgenciaRepository.GetById(idAgencia);

            if (agencia == null)
            {
                mensagemErro = "Agência inválida!";
                return false;
            }

            var contaCorrente = ContaRepository.GetById(idAgencia, conta);

            if (contaCorrente == null)
            {
                mensagemErro = "Conta inválida!";
                return false;
            }

            if (valor <= 0)
            {
                mensagemErro = "Valor do depósito deve ser maior do que 0!";
                return false;
            }

            contaCorrente.Saldo = contaCorrente.Saldo + valor;

            var extrato = new Extrato()
            {
                DataRegistro = DateTime.Today,
                AgenciaId = idAgencia,
                ContaId = conta,
                Valor = valor,
                Saldo = contaCorrente.Saldo,
                Descricao = "Depósito"
            };

            try
            {
                using (var transaction = new TransactionScope())
                {
                    ContaRepository.Save(contaCorrente);
                    ExtratoRepository.Save(extrato);
                    transaction.Complete();
                }
            }
            catch (Exception)
            {
                // incluir isso em um log...
                mensagemErro = "Ocorreu um erro ao fazer o depósito!";
                return false;
            }
            return true;
        }

        public bool Saque(int idAgencia, int conta, decimal valor, out string mensagemErro)
        {
            mensagemErro = "";

            var agencia = AgenciaRepository.GetById(idAgencia);

            if (agencia == null)
            {
                mensagemErro = "Agência inválida!";
                return false;
            }

            var contaCorrente = ContaRepository.GetById(idAgencia, conta);

            if (contaCorrente == null)
            {
                mensagemErro = "Conta inválida!";
                return false;
            }

            if (valor <= 0)
            {
                mensagemErro = "O valor do saque precisa ser maior que zero!";
                return false;
            }

            if (valor > contaCorrente.Saldo)
            {
                mensagemErro = "O valor do saque precisa ser menor ou igual ao saldo da conta!";
                return false;
            }

            contaCorrente.Saldo = contaCorrente.Saldo - valor;

            var extrato = new Extrato()
            {
                DataRegistro = DateTime.Now,
                AgenciaId = idAgencia,
                ContaId = conta,
                Valor = valor * - 1,
                Saldo = contaCorrente.Saldo,
                Descricao = "Saque"
            };

            try
            {
                using (var transaction = new TransactionScope())
                {
                    ContaRepository.Save(contaCorrente);
                    ExtratoRepository.Save(extrato);
                    transaction.Complete();
                }
            }
            catch (Exception)
            {
                // incluir isso em um log...
                mensagemErro = "Ocorreu um erro ao fazer o saque!";
                return false;
            }
            return true;
        }

        public decimal Saldo(int agencia, int conta, out string mensagemErro)
        {
            mensagemErro = "";

            var ag = AgenciaRepository.GetById(agencia);

            if(agencia == 0)
            {
                mensagemErro = "Agência inválida!";
                return 0;
            }

            var contaCorrente = ContaRepository.GetById(agencia, conta);

            if(contaCorrente == null)
            {
                mensagemErro = "Conta inválida!";
                return 0;
            }

            return contaCorrente.Saldo;
        }

        public bool Transferencia(int agenciaOrigem, int contaOrigem, decimal valor, int agenciaDestino, int contaDestino, out string mensagemErro)
        {
            mensagemErro = "";

            var agOrigem = AgenciaRepository.GetById(agenciaOrigem);

            if (agOrigem == null)
            {
                mensagemErro = "Agência de origem inválida!";
                return false;
            }

            var contaCorrenteOrigem = ContaRepository.GetById(agenciaOrigem, contaOrigem);

            if (contaCorrenteOrigem == null)
            {
                mensagemErro = "Conta de origem inválida!";
                return false;
            }

            if(valor <= 0)
            {
                mensagemErro = "O valo deve ser maior do que zero!";
                return false;
            }

            if(valor > contaCorrenteOrigem.Saldo)
            {
                mensagemErro = "O valor do saque precisa ser menor ou igual ao saldo da conta!";
                return false;
            }

            var agDestino = AgenciaRepository.GetById(agenciaDestino);

            if (agDestino == null)
            {
                mensagemErro = "Agência de destino inválida!";
                return false;
            }

            var contaCorrenteDestino = ContaRepository.GetById(agenciaDestino, contaDestino);

            if (contaCorrenteDestino == null)
            {
                mensagemErro = "Conta de destino inválida!";
                return false;
            }

            contaCorrenteOrigem.Saldo = contaCorrenteOrigem.Saldo - valor;

            var extratoOrigem = new Extrato()
            {
                DataRegistro = DateTime.Now,
                AgenciaId = agenciaOrigem,
                ContaId = contaCorrenteOrigem.Id,
                Valor = valor * -1,
                Saldo = contaCorrenteDestino.Saldo,
                Descricao = $"Transferência para Ag {agenciaDestino} Cc {contaCorrenteDestino} ",
            };

            contaCorrenteDestino.Saldo = contaCorrenteDestino.Saldo + valor;

            var extratoDestino = new Extrato()
            {
                DataRegistro = DateTime.Now,
                AgenciaId = agenciaDestino,
                ContaId = contaCorrenteDestino.Id,
                Valor = valor,
                Saldo = contaCorrenteDestino.Saldo,
                Descricao = $"Transferência de Ag {agenciaOrigem} Cc {contaCorrenteOrigem} ",
            };

            try
            {
                using (var transaction = new TransactionScope())
                {
                    ContaRepository.Save(contaCorrenteOrigem);
                    ExtratoRepository.Save(extratoOrigem);
                    ExtratoRepository.Save(extratoDestino);
                    transaction.Complete();
                }
            }
            catch (Exception ex)
            {
                mensagemErro = "Ocorreu um problema ao fazer a tranferência!";
                return false;
            }

            return true;
        }

        public IList<Extrato> Extrato(int idAgencia, int conta, DateTime dataInicio, DateTime dataFim, out string mensagemErro)
        {
            mensagemErro = "";

            var agencia = AgenciaRepository.GetById(idAgencia);

            if (agencia == null)
            {
                mensagemErro = "Agência inválida!";
                return null;
            }

            var contaCorrente = ContaRepository.GetById(idAgencia, conta);

            if(contaCorrente == null)
            {
                mensagemErro = "Conta de origem é invalida!";
                return null;
            }

            if(dataInicio > dataFim)
            {
                mensagemErro = "A data de inicio deve ser menor do que a data fim!";
                return null;
            }

            if ((dataFim - dataInicio).Days > 120)
            {
                mensagemErro = "O período não deve ser superior a 120 dias!";
                return null;
            }

            try
            {
                var extratoLista = ExtratoRepository.GetByPeriodo(agencia.Id, conta, dataInicio, dataFim);
                return extratoLista;
            }
            catch (Exception)
            {
                // incluir isso em um log...
                mensagemErro = "Ocorreu um erro ao fazer obter o extrato!";
                return null;
            }
        }
    }
}
