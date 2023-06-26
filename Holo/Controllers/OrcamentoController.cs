using Holo.Domain.config;
using Holo.Domain.Entities;
using Holo.Models.Orcamento;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Holo.Controllers
{
    [Route("orcamento")]
    [ApiController]
    public class OrcamentoController : ControllerBase
    {
        private readonly HoloDbContext _context;

        public OrcamentoController(HoloDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("")]
        public ActionResult<List<Orcamento>> ListarOrcamentos()
        {
            List<Orcamento> orcamentos = _context.Orcamentos.ToList();
            return Ok(orcamentos);
        }

        [HttpPost]
        [Route("criar")]
        public ActionResult<Orcamento> CriarOrcamento([FromBody] CriarOrcamento criarOrcamento)
        {
            if (criarOrcamento is null)
            {
                return BadRequest("Orcamento inválido");
            }

            Orcamento novoOrcamento = new Orcamento
            {
                Nome = criarOrcamento.Nome,
                Telefone = criarOrcamento.Telefone,
                Observacoes = criarOrcamento.Observacoes,
                ArquivoId = criarOrcamento.ArquivoId
            };

            _context.Orcamentos.Add(novoOrcamento);
            _context.SaveChanges();

            return Ok(novoOrcamento);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<Orcamento> GetOrcamento(int id)
        {
            Orcamento orcamento = _context.Orcamentos.FirstOrDefault(o => o.Id == id);

            if (orcamento is null)
            {
                return NotFound();
            }

            return Ok(orcamento);
        }
    }
}
