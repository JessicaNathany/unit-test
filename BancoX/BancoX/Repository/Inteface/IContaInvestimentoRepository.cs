using BancoX.Model;

namespace BancoX.Interface
{
    public interface IContaInvestimentoRepository
    {
        ContaInvestimento GetById(int idAgencia, int conta);

        void Save(ContaInvestimento carteira);
    }
}
