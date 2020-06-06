using System;
using System.Collections.Generic;

namespace BancoX.Interface
{
    public interface IExtratoInvetimentoRepository
    {
        IList<ExtratoInvetimento> GetByPeriodo(int idCarteira, int idTitulo, DateTime dataInicio, DateTime dataFim);

        void Save(ExtratoInvetimento extrato);
    }
}
