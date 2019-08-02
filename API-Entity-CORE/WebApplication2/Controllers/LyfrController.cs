using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApplication2.Models;
using WebApplication2.Utilities.Aplicacao;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LyfrController : ControllerBase
    {
        //variavel de contexto para acesso as utilidades do entity
        private readonly PessoaContext _context;

        //construtor
        public LyfrController(PessoaContext context)
        {
            _context = context;
        }

        //página inicial em branco
        [HttpGet]
        public ActionResult<string> Index()
        {
            return "";
        }

        // /api/Lyfr/Inserir/?json={"Id":3,"Nome":"Lucas","Senha":"123321","Idade":18,"Email":"a@eu.com"}
        [HttpPost]
        [Route("Inserir")]
        public string Inserir(string json = "")
        {
            string response;
            try
            {

                if (json == string.Empty)
                {
                    response = JsonConvert.SerializeObject("O usuário enviado é nulo!");
                    return response;
                }

                var pessoa = JsonConvert.DeserializeObject<Pessoa>(json);
                var pessoaExiste = new PessoaAplicacao(_context).GetUsuarioByEmail(pessoa.Email);

                if (pessoaExiste != null)
                {
                    response = JsonConvert.SerializeObject("O usuário enviado já existe na base de dados!");
                    return response;
                }
                
                var sucesso = new PessoaAplicacao(_context).Inserir(pessoa);

                if (sucesso)
                {
                    response = JsonConvert.SerializeObject("Inserido com sucesso");
                }
                else
                {
                    response = JsonConvert.SerializeObject("Erro! Preencha todos os campos e tente novamente.");
                }
        }
            catch (Exception)
            {
                response = JsonConvert.SerializeObject("Erro!");
            }

            return response;
        }

        [HttpPut]
        [Route("Alterar")]
        public string Alterar(string json = "")
        {
            string response;
            try
            {
                if (json == string.Empty)
                {
                    response = JsonConvert.SerializeObject("O usuário enviado é nulo!");
                    return response;
                }
            
                var pessoa = JsonConvert.DeserializeObject<Pessoa>(json);
                var sucesso = new PessoaAplicacao(_context).Alterar(pessoa);
                
                if (sucesso)
                {
                    response = JsonConvert.SerializeObject("Alterado com sucesso");
                }
                else
                {
                    response = JsonConvert.SerializeObject("Erro! Preencha todos os campos e tente novamente.");
                }
            }
            catch (Exception)
            {
                response = JsonConvert.SerializeObject("Ocorreu algum erro na hora de se conectar!");
            }

            return response;
        }

        [HttpDelete]
        [Route("Deletar")]
        public string Deletar(int id = -1)
        {
            string response;

            if (id <= -1)
            {
                response = JsonConvert.SerializeObject("O id enviado é inválido!");
                return response;
            }

            try
            {
                var sucesso = new PessoaAplicacao(_context).Deletar(id);

                if (sucesso)
                {
                    response = JsonConvert.SerializeObject("Deletado com sucesso");
                }
                else
                {
                    response = JsonConvert.SerializeObject("Erro! Tente novamente.");
                }
            }
            catch (Exception)
            {
                response = JsonConvert.SerializeObject("Erro ao tentar conectar com o servidor!");
            }

            return response;
        }


        [HttpGet]
        [Route("GetTodosUsuarios")]
        public string GetTodosUsuarios()
        {
            string response;

            try
            {
                var listaDePessoas = new PessoaAplicacao(_context).GetTodosUsuarios();

                if (listaDePessoas != null)
                {
                    response = JsonConvert.SerializeObject(listaDePessoas);
                }
                else
                {
                    response = JsonConvert.SerializeObject("Não existe nenhum usuário cadastrado!");
                }
            }
            catch (Exception)
            {
                response = JsonConvert.SerializeObject("Erro ao tentar conectar com o servidor!");
            }
            

            return response;
        }

        [HttpGet]
        [Route("GetUsuarioByEmail")]
        public string GetUsuarioByEmail(string email = "")
        {
            string response;

            if (email == string.Empty)
            {
                response = JsonConvert.SerializeObject("O email enviado é nulo!");
                return response;
            }

            try
            {
                var pessoa = new PessoaAplicacao(_context).GetUsuarioByEmail(email);

                if (pessoa == null||pessoa.Nome == string.Empty)
                {
                    response = JsonConvert.SerializeObject("Usuário não encontrado");
                }
                else
                {
                    response = JsonConvert.SerializeObject(pessoa);
                }
            }
            catch (Exception)
            {
                response = JsonConvert.SerializeObject("Erro ao tentar conectar com o servidor!");
            }

            return response;
        }

        [HttpGet]
        [Route("GetUsuarioById")]
        public string GetUsuarioById(int id = -1)
        {
            string response;

            if (id <= -1)
            {
                response = JsonConvert.SerializeObject("O id enviado é inválido!");
                return response;
            }

            try
            {
                var pessoa = new PessoaAplicacao(_context).GetUsuarioById(id);

                if (pessoa == null || pessoa.Nome == string.Empty)
                {
                    response = JsonConvert.SerializeObject("Usuário não encontrado");
                }
                else
                {
                    response = JsonConvert.SerializeObject(pessoa);
                }
            }
            catch (Exception)
            {
                response = JsonConvert.SerializeObject("Erro ao tentar conectar com o servidor!");
            }

            return response;
        }
    }
}
