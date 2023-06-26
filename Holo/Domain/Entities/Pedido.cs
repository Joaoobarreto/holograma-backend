using System;

namespace Holo.Domain.Entities
{
    public class Pedido
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public DateTime Data { get; set; }
        public int TipoPagamentoId { get; set; }
        public int CartaoId { get; set; }
        public string Status { get; set; }
        public int EnderecoId { get; set; }

        public Usuario Usuario { get; set; }
        public TipoPagamento TipoPagamento { get; set; }
        public Cartao Cartao { get; set; }
        public Endereco Endereco { get; set; }

        public Pedido() { }
        public Pedido(int usuarioId, DateTime data, int tipoPagamentoId, int cartoId, string status)
        {
            UsuarioId = usuarioId;
            Data = data;
            TipoPagamentoId = tipoPagamentoId;
            CartaoId = cartoId;
            Status = status;
        }
    }
}
