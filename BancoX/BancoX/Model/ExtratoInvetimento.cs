using System;

namespace BancoX.Interface
{
    public class ExtratoInvetimento
    {
        public int Id { get; set; }

        public int IdTitulo { get; set; }

        public int IdAgencia { get; set; }

        public int IdConta { get; set; }

        public int IdCarteira { get; set; }

        public DateTime DataRegistro { get; set; }

        public string Descricao { get; set; }

        public decimal Valor { get; set; }

        public decimal Saldo { get; set; }
    }
}