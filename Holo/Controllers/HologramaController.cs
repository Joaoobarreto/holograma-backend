using Holo.Domain.Entities;
using Holo.Domain.config;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using Holo.Models.Holograma;
using Holo.Models.Cartao;

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

        [HttpPost]
        [Route("criar")]
        public ActionResult<Holograma> CriarHologramas([FromBody] CriarHolograma criarHolograma)
        {
            if (criarHolograma is null)
            {
                return BadRequest("Holograma inválido");
            }

            var holograma = new Holograma()
            {
                Descricao = criarHolograma.Descricao,
                Valor = criarHolograma.valor,
                CategoriaId = criarHolograma.CategoriaId,
                Quantidade = criarHolograma.Quantidade,
                Ativo = true
            };

            _context.Hologramas.Add(holograma);
            _context.SaveChanges();

            return Ok(holograma);
        }

        [HttpPost]
        [Route("atualizar")]
        public ActionResult<Holograma> UpdateHologramas([FromBody] AtualizarHolograma atualizarHolograma)
        {
            var hologramaExistente = _context.Hologramas.Find(atualizarHolograma.Id);

            if (hologramaExistente is null)
            {
                return NotFound();
            }

            hologramaExistente.Descricao = atualizarHolograma.Descricao;
            hologramaExistente.Valor = atualizarHolograma.Valor;
            hologramaExistente.CategoriaId = atualizarHolograma.CategoriaId;
            hologramaExistente.Ativo = atualizarHolograma.Ativo;
            hologramaExistente.Quantidade = atualizarHolograma.Quantidade;

            _context.SaveChanges();

            return Ok(hologramaExistente);
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

        [HttpPost]
        [Route("por-descricao")]
        public ActionResult<List<Holograma>> GetHologramaPorDescricao([FromBody] GetHologramaPorDescricao getHologramaPorDescricao)
        {
            var hologramas =  _context.Hologramas
                                .Where(h => EF.Functions.Like(h.Descricao, "%" + getHologramaPorDescricao.Descricao + "%"))
                                .ToList();

            if (hologramas == null)
            {
                return NotFound();
            }

            return hologramas;
        }
    }
}
