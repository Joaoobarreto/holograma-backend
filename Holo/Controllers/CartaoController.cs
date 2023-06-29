using Holo.Domain.config;
using Holo.Domain.Entities;
using Holo.Models.Cartao;
using Holo.Models.Orcamento;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Holo.Controllers
{
    [Route("cartao")]
    [ApiController]
    public class CartaoController : ControllerBase
    {
        private readonly HoloDbContext _context;

        public CartaoController(HoloDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("")]
        public ActionResult<Cartao> CriarCartao([FromBody] CriarCartao criarCartao)
        {
            if (criarCartao is null)
            {
                return BadRequest("Orcamento inválido");
            }

            Cartao novoCartao = new Cartao
            {
                Nome = criarCartao.Nome,
                Numero = criarCartao.Numero,
                CVV = criarCartao.Cvv,
                Mes = criarCartao.Mes,
                Ano = criarCartao.Ano,
                UsuarioId = criarCartao.UsuarioId
            };

            _context.Cartoes.Add(novoCartao);
            _context.SaveChanges();

            return Ok(new { Id = novoCartao.Id });
        }
    }
}