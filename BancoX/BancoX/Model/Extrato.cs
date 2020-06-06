using System;

namespace BancoX
{
    public class Extrato
    {
        public int Id  { get; set; }

        public int AgenciaId { get; set; }

        public int ContaId { get; set; }

        public DateTime DataRegistro { get; set; }

        public string Descricao { get; set; }

        public decimal Valor { get; set; }

        public decimal Saldo { get; set; }
    }
}