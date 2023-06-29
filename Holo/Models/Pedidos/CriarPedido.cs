using System.Collections.Generic;

namespace Holo.Models.Pedidos
{
    public class CriarPedido
    {
        public int UsuarioId { get; set; }
        public int TipoPagamentoId { get; set; }
        public int CartaoId { get; set; }
        public List<int> Hologramas { get; set; }
        public int EnderecoID { get; set; }
    }
}
