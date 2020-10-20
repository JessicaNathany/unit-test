using System;
namespace BancoX
{
    public class TesouroDireto
    {
        public int IdTesouro { get; set; }

        public string Nome { get; set; }

        public DateTime DataAplicacao { get; set; }

        public DateTime DataVencimento { get; set; }

        public decimal ValorMinimo { get; set; }

        public decimal IR { get; set; }
    }
}
