using Holo.Domain.config;
using Holo.Domain.Entities;
using Holo.Models.Usuario;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Holo.Controllers
{
    [Route("usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly HoloDbContext _context;

        public UsuarioController(HoloDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("")]
        public ActionResult<List<Usuario>> ListarUsuarios()
        {
            List<Usuario> usuarios = _context.Usuarios.ToList();
            return Ok(usuarios);
        }

        [HttpPost]
        [Route("criar")]
        public ActionResult<Usuario> CriarUsuario([FromBody] CriarUsuario criarUsuario)
        {
            if (criarUsuario is null)
            {
                return BadRequest("Usuário inválido");
            }

            Usuario novoUsuario = new Usuario
            {
                Nome = criarUsuario.Nome,
                Email = criarUsuario.Email,
                Cpf = criarUsuario.Cpf,
                Telefone = criarUsuario.Telefone,
            };

            _context.Usuarios.Add(novoUsuario);
            _context.SaveChanges();

            return Ok(novoUsuario);
        }

        [HttpPost]
        [Route("atualizar")]
        public ActionResult<Usuario> AtualizarUsuario([FromBody] AtualizarUsuario atualizarUsuario)
        {
            if (atualizarUsuario is null)
            {
                return BadRequest("Usuário inválido");
            }

            Usuario usuarioExistente = _context.Usuarios.FirstOrDefault(u => u.Id == atualizarUsuario.Id);

            if (usuarioExistente is null)
            {
                return NotFound();
            }

            usuarioExistente.Nome = atualizarUsuario.Nome;
            usuarioExistente.Email = atualizarUsuario.Email;
            usuarioExistente.Cpf = atualizarUsuario.Cpf;
            usuarioExistente.Telefone = atualizarUsuario.Telefone;

            if (atualizarUsuario.Endereco is not null)
            {
                Endereco endereco = _context.Enderecos.FirstOrDefault(e => e.UsuarioId == atualizarUsuario.Id);
                if (endereco is null)
                {
                    _context.Enderecos.Add(atualizarUsuario.Endereco);
                }
                else
                {
                    endereco.Cep = atualizarUsuario.Endereco.Cep;
                    endereco.Logradouro = atualizarUsuario.Endereco.Logradouro;
                    endereco.Complemento = atualizarUsuario.Endereco.Complemento;
                    endereco.Numero = atualizarUsuario.Endereco.Numero;
                    endereco.Cidade = atualizarUsuario.Endereco.Cidade;
                    endereco.Estado = atualizarUsuario.Endereco.Estado;

                }
            }

            _context.SaveChanges();

            return Ok();
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<UsuarioResponse> GetUsuario(int id)
        {
            Usuario usuario = _context.Usuarios.FirstOrDefault(u => u.Id == id);
            Endereco endereco = _context.Enderecos.FirstOrDefault(e => e.UsuarioId == id);

            if (usuario is null)
            {
                return NotFound();
            }

            UsuarioResponse response = new UsuarioResponse()
            {
                Cpf = usuario.Cpf,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Telefone = usuario.Telefone,
                Endereco = endereco,
            };

            return Ok(response);
        }
    }
}
