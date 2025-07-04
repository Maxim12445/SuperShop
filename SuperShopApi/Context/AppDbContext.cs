using Microsoft.EntityFrameworkCore;
using SuperShopApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace SuperShopApi.Context
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Cliente>().HasData(
                new Cliente
                {
                    Id = 1,
                    Nome = "João",
                    Apelido = "Silva",
                    Nif = "123456789",
                    Morada = "Rua das Flores, 123",
                    Telefone = "912345678"
                },

                new Cliente
                {
                    Id = 2,
                    Nome = "Maria",
                    Apelido = "Santos",
                    Nif = "987654321",
                    Morada = "Avenida da Liberdade, 456",
                    Telefone = "919876543"
                }

                );
        }
    }
}
