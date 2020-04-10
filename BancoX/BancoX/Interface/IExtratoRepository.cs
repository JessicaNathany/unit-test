using System;
using System.Collections.Generic;

namespace BancoX.Interface
{
    public interface IExtratoRepository
    {
        IList<Extrato> GetByPeriodo(int agenciaId, int contaId, DateTime dataInicio, DateTime dataFim);

        void Save(Extrato extrato);

        decimal GetSaldoAnterior(int agenciaId, int contaId, DateTime dataInicio, DateTime dataFim);
    }
}
