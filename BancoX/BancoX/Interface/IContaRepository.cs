using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoX.Interface
{
    public interface IContaRepository
    {
        Conta GetById(int agenciaId, int contId);
    }
}
