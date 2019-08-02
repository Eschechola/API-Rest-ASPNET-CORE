using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.Utilities.Aplicacao
{
    public class PessoaAplicacao
    {
        //variavel de contexto para acesso as utilidades do entity
        private readonly PessoaContext _context;

        //construtor
        public PessoaAplicacao(PessoaContext context)
        {
            _context = context;
        }

        public bool Inserir(Pessoa pessoa)
        {
            //verifica se a pessoa não é nula e tem todos os campos preenchidos
            //se tiver tudo certo ele tenta adicionar ao banco, caso ocorrra algum erro retorna falso
            if (pessoa != null)
            {
                if(!(pessoa.Nome==string.Empty||pessoa.Senha==string.Empty||pessoa.Idade < 0))
                {
                    _context.Add(pessoa);
                    _context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }


        public bool Alterar(Pessoa pessoa)
        {
            //recebe um json com o ID da pessoa (campo nao alteravel)
            //e se os dados tiverem corretos altera todos os dados da pessoa que tiver esse id
            // *O ID ESTÁ COMO PRIMARY KEY, O ENTITY ALTERA POR ELA*
            try
            {
                _context.Pessoa.Update(pessoa);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool Deletar(int id)
        {
            //cria um objeto que vai receber o id passado
            Pessoa pessoa = new Pessoa
            {
                Id = id
            };

            //se o id existir no banco ele deleta os dados
            //caso contrario ele nao altera o banco e retorna falso
            try
            {
                _context.Pessoa.Remove(pessoa);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;

        }

        public List<Pessoa> GetTodosUsuarios()
        {
            //lista com todas as pessoas que sera retornada
            List<Pessoa> listaDePessoas = new List<Pessoa>();

            try
            {
                //da um select em todos os dados do banco de dados
                var pessoasDoBanco = _context.Pessoa.Select(x => x);
                
                //adiciona todos os dados retornados da query na lista de pessoas e retorna
                //caso ocorra algum erro retorna nulo
                foreach(var p in pessoasDoBanco)
                {
                    listaDePessoas.Add(p);
                }
            }
            catch (Exception)
            {
                return null;
            }
            
            return listaDePessoas;
        }

        public Pessoa GetUsuarioByEmail(string email)
        {
            Pessoa pessoa = new Pessoa();
            try
            {
                //retorna o primeiro usuario com o email informado
                //caso ocorra algum erro retorna nulo
                var pessoaDoBanco = _context.Pessoa.Where(x => x.Email == email).ToList();
                pessoa = pessoaDoBanco.FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }

            return pessoa;
        }

        public Pessoa GetUsuarioById(int id)
        {
            Pessoa pessoa = new Pessoa();

            try
            {
                //retorna o primeiro usuario com o id informado
                //caso ocorra algum erro retorna nulo
                var pessoaDoBanco = _context.Pessoa.Where(x => x.Id == id).ToList();
                pessoa = pessoaDoBanco.FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }

            return pessoa;
        }
    }
}
