namespace BancoX.Model
{
    public class ContaInvest : Agencia
    {
        public int AgenciaId { get; set; }

        public decimal Saldo { get; set; }

        public int Numero { get; set; }
    }
}
