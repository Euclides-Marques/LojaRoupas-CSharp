using LojaRoupas.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LojaRoupas.Context
{
    //Aqui é onde eu faço a minha configuração dos models que eu criei para eu começar a usá-los
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Roupa> Roupas { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Marca> Marcas { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Contratos> Contratos { get; set; }
    }
}
