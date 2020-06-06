namespace BancoX.Model
{
    public class ContaInvest : Agencia
    {
        public int Id { get; set; }

        public int AgenciaId { get; set; }

        public decimal Saldo { get; set; }
    }
}
