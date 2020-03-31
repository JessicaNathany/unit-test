using System;
using System.Collections.Generic;
using System.Linq;
namespace BancoX
{
    public class TesouroDireto
    {
        public int IdTesouro { get; set; }
        public DateTime DataAplicacao { get; set; }
        public decimal Valor { get; set; }
        public decimal IR { get; set; }
    }
}
