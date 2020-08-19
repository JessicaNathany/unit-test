namespace BancoX.Model
{
    public class ContaInvestimento : Agencia
    {
        public int AgenciaId { get; set; }
        public decimal Saldo { get; set; }
        public int Numero { get; set; }
    }
}
