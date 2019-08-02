using System;
using System.Collections.Generic;

namespace WebApplication2.Models
{
    public partial class Pessoa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
        public int Idade { get; set; }
        public string Email { get; set; }
    }
}
