using System.Collections.Generic;

namespace Holo.Domain.Entities
{
    public class Holograma
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public double Valor { get; set; }
        public int CategoriaId { get; set; }
        public bool Ativo { get; set; }
        public string ArquivoId { get; set; }
        public int Quantidade { get; set; }

        public Categoria Categoria { get; set; }
    }
}
