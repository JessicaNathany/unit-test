using System;
using System.Collections.Generic;
namespace BancoX.Interface
{
    public interface ICarteira
    {
        void Resgate(int idTitulo, decimal valor,  DateTime dataAtual, out string mensagemErro);

        void Investit(int idTitulo, decimal valor, out string mensagemErro);

        List<ExtratoInvetimento>Extrato(int idTitulo, int idCarteira, out string mensagemErro);
    }
}
