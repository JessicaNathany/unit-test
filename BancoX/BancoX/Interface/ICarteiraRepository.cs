namespace BancoX.Interface
{
    public interface ICarteiraRepository
    {
        Carteira GetById(int idCarteira);

        void Save(Carteira carteira);
    }
}
