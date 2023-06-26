using Holo.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Holo.Domain.config
{
    public class HoloDbContext : DbContext
    {
        public HoloDbContext(DbContextOptions<HoloDbContext> options) : base(options)
        {
        }

        public DbSet<Cartao> Cartoes { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Holograma> Hologramas { get; set; }
        public DbSet<Orcamento> Orcamentos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<PedidoHolograma> PedidosHologramas { get; set; }
        public DbSet<TipoPagamento> TiposPagamento { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define as configurações das tabelas

            modelBuilder.Entity<Cartao>(entity =>
            {
                entity.ToTable("tb_cartao");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasColumnName("id").IsRequired();
                entity.Property(e => e.UsuarioId).HasColumnName("usuario_id").IsRequired();
                entity.Property(e => e.Numero).HasColumnName("numero").IsRequired();
                entity.Property(e => e.CVV).HasColumnName("cvv").IsRequired();
                entity.Property(e => e.Mes).HasColumnName("mes").IsRequired();
                entity.Property(e => e.Ano).HasColumnName("ano").IsRequired();
                entity.Property(e => e.Nome).HasColumnName("nome").IsRequired();

                entity.HasOne(e => e.Usuario)
                      .WithMany()
                      .HasForeignKey(e => e.UsuarioId);
            });

            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.ToTable("tb_categoria");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .IsRequired();

                entity.Property(e => e.Descricao)
                    .HasColumnName("descricao")
                    .IsRequired();
            });

            modelBuilder.Entity<Holograma>(entity =>
            {
                entity.ToTable("tb_holograma");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasColumnName("id").IsRequired();
                entity.Property(e => e.Descricao).HasColumnName("descricao").IsRequired();
                entity.Property(e => e.Valor).HasColumnName("valor").IsRequired();
                entity.Property(e => e.CategoriaId).HasColumnName("categoria_id").IsRequired();
                entity.Property(e => e.Ativo).HasColumnName("ativo").IsRequired();
                entity.Property(e => e.ArquivoId).HasColumnName("arquivo_id");
                entity.Property(e => e.Quantidade).HasColumnName("quantidade").IsRequired();

                entity.HasOne(e => e.Categoria)
                      .WithMany()
                      .HasForeignKey(e => e.CategoriaId);
            });

            modelBuilder.Entity<Orcamento>(entity =>
            {
                entity.ToTable("tb_orcamento");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .IsRequired();

                entity.Property(e => e.Nome)
                    .HasColumnName("nome")
                    .IsRequired();

                entity.Property(e => e.Telefone)
                    .HasColumnName("telefone")
                    .IsRequired();

                entity.Property(e => e.Observacoes)
                    .HasColumnName("observacoes")
                    .IsRequired();

                entity.Property(e => e.ArquivoId)
                    .HasColumnName("arquivo_id")
                    .IsRequired();
            });

            modelBuilder.Entity<Pedido>(entity =>
            {
                entity.ToTable("tb_pedido");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasColumnName("id").IsRequired();
                entity.Property(e => e.UsuarioId).HasColumnName("usuario_id").IsRequired();
                entity.Property(e => e.Data).HasColumnName("data").IsRequired();
                entity.Property(e => e.TipoPagamentoId).HasColumnName("tipoPagamento_id").IsRequired();
                entity.Property(e => e.CartaoId).HasColumnName("cartao_id").IsRequired();

                entity.HasOne(e => e.Usuario)
                      .WithMany()
                      .HasForeignKey(e => e.UsuarioId);

                entity.HasOne(e => e.TipoPagamento)
                      .WithMany()
                      .HasForeignKey(e => e.TipoPagamentoId);

                entity.HasOne(e => e.Cartao)
                      .WithMany()
                      .HasForeignKey(e => e.CartaoId);

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .IsRequired();

                entity.Property(e => e.EnderecoId)
                    .HasColumnName("endereco_id")
                    .IsRequired();

                entity.HasOne(e => e.Endereco)
                    .WithMany()
                    .HasForeignKey(e => e.EnderecoId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<PedidoHolograma>(entity =>
            {
                entity.ToTable("tb_pedido_holograma");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasColumnName("id").IsRequired();
                entity.Property(e => e.PedidoId).HasColumnName("pedido_id");
                entity.Property(e => e.HologramaId).HasColumnName("holograma_id");

                entity.HasOne(e => e.Pedido)
                      .WithMany()
                      .HasForeignKey(e => e.PedidoId);

                entity.HasOne(e => e.Holograma)
                      .WithMany()
                      .HasForeignKey(e => e.HologramaId);
            });

            modelBuilder.Entity<TipoPagamento>(entity =>
            {
                entity.ToTable("tb_tipoPagamento");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .IsRequired();

                entity.Property(e => e.Descricao)
                    .HasColumnName("descricao")
                    .IsRequired();
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("tb_usuario");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .IsRequired();

                entity.Property(e => e.Nome)
                    .HasColumnName("nome")
                    .IsRequired();

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .IsRequired();

                entity.Property(e => e.Cpf)
                    .HasColumnName("cpf");

                entity.Property(e => e.Telefone)
                    .HasColumnName("telefone")
                    .IsRequired();
            });

            modelBuilder.Entity<Endereco>(entity =>
            {
                entity.ToTable("tb_endereco");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .IsRequired();

                entity.Property(e => e.Cep)
                    .HasColumnName("cep")
                    .IsRequired();

                entity.Property(e => e.Logradouro)
                    .HasColumnName("logradouro")
                    .IsRequired();

                entity.Property(e => e.Numero)
                    .HasColumnName("numero")
                    .IsRequired();

                entity.Property(e => e.Cidade)
                    .HasColumnName("cidade")
                    .IsRequired();

                entity.Property(e => e.Estado)
                    .HasColumnName("estado")
                    .IsRequired();

                entity.Property(e => e.UsuarioId)
                    .HasColumnName("usuario_id")
                    .IsRequired();
            });
        }
    }

}
