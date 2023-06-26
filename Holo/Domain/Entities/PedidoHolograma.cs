namespace Holo.Domain.Entities
{
    public class PedidoHolograma
    {
        public int Id { get; set; }
        public int? PedidoId { get; set; }
        public int? HologramaId { get; set; }

        public Pedido Pedido { get; set; }
        public Holograma Holograma { get; set; }
    }
}
