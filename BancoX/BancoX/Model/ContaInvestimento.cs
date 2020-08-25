namespace BancoX.Model
{
    public class ContaInvestimento 
    {
        public int AgenciaId { get; set; }
        public string Banco { get; set; }
        public int Id { get; set; }
        public decimal Saldo { get; set; }
        public decimal Valor { get; set; }
        public int Numero { get; set; }
    }
}
