using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto1.Models
{
    public class BdContext : DbContext
    {
        public BdContext(DbContextOptions<BdContext> options) : base(options)
        {

        }
        public DbSet<Cliente> Clientes { get; set; }

        public DbSet<Cidade> Cidades { get; set; }
    }
}
