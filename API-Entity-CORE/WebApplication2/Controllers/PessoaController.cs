using System;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApplication2.Models;
using WebApplication2.Utilities.Aplicacao;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoaController : ControllerBase
    {
        //variavel de contexto para acesso as utilidades do entity
        private readonly PessoaContext _context;

        //construtor
        public PessoaController(PessoaContext context)
        {
            _context = context;
        }

        //página inicial em branco
        [HttpGet]
        public ActionResult<string> Index()
        {
            return "";
        }


        [HttpPost]
        [Route("Inserir")]
        public IActionResult Inserir([FromBody]Pessoa pessoa)
        {
            try
            {

                if (pessoa == null)
                {
                    return BadRequest("A pessoa enviada é nula!");
                }

                var pessoaExiste = new PessoaAplicacao(_context).GetUsuarioByEmail(pessoa.Email);

                if (pessoaExiste != null)
                {
                    return BadRequest("O usuário enviado já existe na base de dados!");
                }
                
                var sucesso = new PessoaAplicacao(_context).Inserir(pessoa);

                if (sucesso)
                {
                    return Ok("Inserido com sucesso");
                }
                else
                {
                    return BadRequest("Erro! Preencha todos os campos e tente novamente.");
                }
            }
            catch (Exception)
            {
                return BadRequest("Erro!");
            }
        }

        [HttpPut]
        [Route("Alterar")]
        public IActionResult Alterar([FromBody]Pessoa pessoa)
        {
            try
            {
                if (pessoa==null)
                {
                    return BadRequest("A pessoa enviada é nula!");
                }
            
                var sucesso = new PessoaAplicacao(_context).Alterar(pessoa);
                
                if (sucesso)
                {
                    return Ok("Alterado com sucesso");
                }
                else
                {
                    return BadRequest("Erro! Preencha todos os campos e tente novamente.");
                }
            }
            catch (Exception)
            {
                return BadRequest("Ocorreu algum erro na hora de se conectar!");
            }
        }

        [HttpDelete]
        [Route("Deletar")]
        public IActionResult Deletar([FromBody]int id)
        {
            if (id < 0)
            {
                return BadRequest("O id enviado é inválido!");
            }

            try
            {
                var sucesso = new PessoaAplicacao(_context).Deletar(id);

                if (sucesso)
                {
                    return Ok("Deletado com sucesso");
                }
                else
                {
                    return BadRequest("Erro! Tente novamente.");
                }
            }
            catch (Exception)
            {
                return BadRequest("Erro ao tentar conectar com o servidor!");
            }
        }


        [HttpGet]
        [Route("GetTodosUsuarios")]
        public IActionResult GetTodosUsuarios()
        {
            try
            {
                var listaDePessoas = new PessoaAplicacao(_context).GetTodosUsuarios();

                if (listaDePessoas != null)
                {
                    var listaSerializada = JsonConvert.SerializeObject(listaDePessoas);
                    return Ok(listaSerializada);
                }
                else
                {
                    return BadRequest("Não existe nenhum usuário cadastrado!");
                }
            }
            catch (Exception)
            {
                return BadRequest("Erro ao tentar conectar com o servidor!");
            }
        }

        [HttpPost]
        [Route("GetUsuarioByEmail")]
        public IActionResult GetUsuarioByEmail([FromBody]string email)
        {
            if (email == string.Empty)
            {
                return BadRequest("O email enviado é nulo!");
            }

            try
            {
                var pessoa = new PessoaAplicacao(_context).GetUsuarioByEmail(email);

                if (pessoa == null || pessoa.Nome == string.Empty)
                {
                    return BadRequest("Usuário não encontrado");
                }
                else
                {
                    var usuarioSerialziado = JsonConvert.SerializeObject(pessoa);
                    return BadRequest(usuarioSerialziado);
                }
            }
            catch (Exception)
            {
                return BadRequest("Erro ao tentar conectar com o servidor!");
            }
        }

        [HttpPost]
        [Route("GetUsuarioById")]
        public IActionResult GetUsuarioById([FromBody]int id)
        {
            if (id < 0)
            {
                return BadRequest("O id enviado é inválido!");
            }

            try
            {
                var pessoa = new PessoaAplicacao(_context).GetUsuarioById(id);

                if (pessoa == null || pessoa.Nome == string.Empty)
                {
                    return BadRequest("Usuário não encontrado");
                }
                else
                {
                    var pessoaSerializada = JsonConvert.SerializeObject(pessoa);
                    return Ok(pessoaSerializada);
                }
            }
            catch (Exception)
            {
                return BadRequest("Erro ao tentar conectar com o servidor!");
            }
        }
    }
}
