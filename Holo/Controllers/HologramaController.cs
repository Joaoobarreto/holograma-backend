using Holo.Domain.Entities;
using Holo.Domain.config;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace Holo.Controllers
{
    [Route("holograma")]
    [ApiController]
    public class HologramaController : ControllerBase
    {
        private readonly HoloDbContext _context;

        public HologramaController(HoloDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("")]
        public ActionResult<List<Holograma>> ListarHologramas()
        {
            List<Holograma> hologramas = _context.Hologramas.ToList();
            return hologramas;
        }

        [HttpGet]
        [Route("atualizar")]
        public ActionResult<List<Holograma>> UpdateHologramas()
        {
            return Ok();
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<Holograma> GetHolograma(int id)
        {
            var holograma = _context.Hologramas.Find(id);

            if (holograma == null)
            {
                return NotFound();
            }

            return holograma;
        }

        [HttpGet]
        [Route("por-categoria/{categoriaId}")]
        public ActionResult<List<Holograma>> GetHologramaPorCategoria(int categoriaId)
        {
            var hologramas = _context.Hologramas.Where(x => x.CategoriaId == categoriaId).ToList();

            if (hologramas == null)
            {
                return NotFound();
            }

            return hologramas;
        }
    }
}
