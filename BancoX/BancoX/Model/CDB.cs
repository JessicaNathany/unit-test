using System;
namespace BancoX.Model
{
    public class CDB
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public double ValorMinimo { get; set; }
        public double ValorLiquidoEstimado { get; set; }
        public DateTime DataVencimento { get; set; }
        public decimal IR { get; set; }
    }
}
