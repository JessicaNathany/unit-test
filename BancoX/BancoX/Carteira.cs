using System;
using System.Collections.Generic;
using BancoX.Interface;
namespace BancoX
{
    public class Carteira : ICarteira
    {
        public Carteira(IExtratoInvetimentoRepository extratoInvestimentoRepository, ICarteiraRepository carteiraRepository)
        {
            _IExtratoInvestimentoRepository = extratoInvestimentoRepository;
            _ICarteiraRepository = carteiraRepository;
        }

        public IExtratoInvetimentoRepository _IExtratoInvestimentoRepository { get; set; }
        public ICarteiraRepository _ICarteiraRepository { get; set; }

        public List<ExtratoInvetimento> Extrato(int idTitulo, int idCarteira, out string mensagemErro)
        {
            throw new NotImplementedException();
        }

        public void Investit(int idTitulo, decimal valor, out string mensagemErro)
        {
            throw new NotImplementedException();
        }

        public void Resgate(int idTitulo, decimal valor, DateTime dataAtual, out string mensagemErro)
        {
            throw new NotImplementedException();
        }
    }
}
