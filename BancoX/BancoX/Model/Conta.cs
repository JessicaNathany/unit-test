using BancoX.Model;

namespace BancoX
{
    // conta corrente 
    public class Conta
    {
        public int Id { get; set; }

        public int AgenciaId { get; set; }

       public decimal Saldo { get; set; }

       public Cliente Cliente { get; set; }
    }
}
