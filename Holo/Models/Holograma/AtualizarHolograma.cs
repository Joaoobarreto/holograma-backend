namespace Holo.Models.Holograma
{
    public class AtualizarHolograma
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public double Valor { get; set; }
        public int CategoriaId { get; set; }
        public int Quantidade { get; set; }
        public bool Ativo { get; set; }
    }
}
