using BancoX.Model;

namespace BancoX.Interface
{
    public interface IContaInvestimentoRepository
    {
        ContaInvestimento GetById(int idCarteira);

        void Save(ContaInvestimento carteira);
    }
}
