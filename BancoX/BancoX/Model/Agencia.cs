namespace BancoX
{
    public class Agencia
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Banco { get; set; }
        public Tipo Tipo { get; set; }
    }

    public enum Tipo
    {
        Digital = 1,
        Físico = 2
    }
}
