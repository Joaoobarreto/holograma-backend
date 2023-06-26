namespace Holo.Domain.Entities
{
    public class Cartao
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string Numero { get; set; }
        public int CVV { get; set; }
        public int Mes { get; set; }
        public int Ano { get; set; }
        public string Nome { get; set; }

        public Usuario Usuario { get; set; }
    }
}
