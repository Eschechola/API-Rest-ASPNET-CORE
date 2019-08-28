using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Models
{
    public partial class PessoaContext : DbContext
    {
        public PessoaContext()
        {
        }

        public PessoaContext(DbContextOptions<PessoaContext> options)
            : base(options)
        {
        }

        //instancia uma tabela de Pessoas no entity
        public virtual DbSet<Pessoa> Pessoa { get; set; }

        //cria um model com os mesmos campos do banco de dados
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pessoa>(entity =>
            {
                entity.ToTable("pessoa");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Email).HasColumnType("varchar(180)");

                entity.Property(e => e.Idade).HasColumnType("int(11)");

                entity.Property(e => e.Nome).HasColumnType("varchar(60)");

                entity.Property(e => e.Senha).HasColumnType("varchar(60)");
            });
        }
    }
}
