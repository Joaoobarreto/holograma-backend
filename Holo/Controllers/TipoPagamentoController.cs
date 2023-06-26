using Holo.Domain.config;
using Holo.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Holo.Controllers
{
    [Route("tipo-pagamento")]
    [ApiController]
    public class TipoPagamentoController : ControllerBase
    {
        private readonly HoloDbContext _context;

        public TipoPagamentoController(HoloDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("")]
        public ActionResult<List<TipoPagamento>> ListarHologramas()
        {
            List<TipoPagamento> tipoPagamento = _context.TiposPagamento.ToList();
            return tipoPagamento;
        }
    }
}