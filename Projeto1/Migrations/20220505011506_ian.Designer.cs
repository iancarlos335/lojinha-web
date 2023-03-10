// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Projeto1.Models;

namespace Projeto1.Migrations
{
    [DbContext(typeof(BdContext))]
    [Migration("20220505011506_ian")]
    partial class ian
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.14")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Projeto1.Models.Cidade", b =>
                {
                    b.Property<long>("CidaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sigla")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CidaID");

                    b.ToTable("Cidades");
                });

            modelBuilder.Entity("Projeto1.Models.Cliente", b =>
                {
                    b.Property<long>("ClienteID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("CidadeID")
                        .HasColumnType("bigint");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Endereco")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ClienteID");

                    b.HasIndex("CidadeID");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("Projeto1.Models.Cliente", b =>
                {
                    b.HasOne("Projeto1.Models.Cidade", "Cidades")
                        .WithMany("Clientes")
                        .HasForeignKey("CidadeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cidades");
                });

            modelBuilder.Entity("Projeto1.Models.Cidade", b =>
                {
                    b.Navigation("Clientes");
                });
#pragma warning restore 612, 618
        }
    }
}
