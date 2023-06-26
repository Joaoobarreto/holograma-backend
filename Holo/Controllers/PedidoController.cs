using Holo.Domain.config;
using Holo.Domain.Entities;
using Holo.Models.Pedidos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Holo.Controllers
{
    [Route("pedido")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly HoloDbContext _context;

        public PedidoController(HoloDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("")]
        public ActionResult<List<Pedido>> ListarPedidos()
        {
            List<Pedido> pedidos = _context.Pedidos.ToList();
            return Ok(pedidos);
        }

        [HttpPost]
        [Route("novo")]
        public ActionResult<Pedido> CriarPedido([FromBody] CriarPedido criarPedido)
        {
            if (criarPedido is null)
            {
                return BadRequest("Pedido inválido");
            }

            Pedido novoPedido = new Pedido(criarPedido.UsuarioId, DateTime.Now, criarPedido.TipoPagamentoId, criarPedido.CartaoId, "aguardando confirmação");

            foreach (int hologramaId in criarPedido.Hologramas)
            {
                PedidoHolograma pedidoHolograma = new PedidoHolograma
                {
                    PedidoId = novoPedido.Id,
                    HologramaId = hologramaId
                };

                _context.PedidosHologramas.Add(pedidoHolograma);
            }

            _context.Pedidos.Add(novoPedido);
            _context.SaveChanges();

            return Ok(novoPedido);
        }

        [HttpGet]
        [Route("listar-por-usuario/{usuarioId}")]
        public ActionResult<List<Pedido>> ListarPedidosPorUsuario(int usuarioId)
        {
            List<Pedido> pedidos = _context.Pedidos.Where(p => p.UsuarioId == usuarioId).ToList();
            return Ok(pedidos);
        }

        [HttpPost]
        [Route("atualizar")]
        public ActionResult<Pedido> UpdatePedido(AtualizarPedido atualizarPedido)
        {
            Pedido pedido = _context.Pedidos.FirstOrDefault(p => p.Id == atualizarPedido.Id);

            if (pedido is null)
            {
                return NotFound();
            }

            pedido.Status = atualizarPedido.Status;

            _context.SaveChanges();

            return Ok(pedido);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<Pedido> GetPedido(int id)
        {
            var pedido = _context.Pedidos.Find(id);

            if (pedido == null)
            {
                return NotFound();
            }

            return pedido;
        }
    }
}
