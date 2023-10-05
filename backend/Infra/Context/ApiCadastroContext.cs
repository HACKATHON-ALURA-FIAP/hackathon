using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Context
{
    public class ApiCadastroContext : DbContext
    {
        public ApiCadastroContext(DbContextOptions options) : base(options) { }

        public ApiCadastroContext() { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApiCadastroContext).Assembly);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = Environment.GetEnvironmentVariable("ConnectionString");
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder
                //    .UseLazyLoadingProxies()
                //    .UseNpgsql(connectionString);
            }
        }
    }
}
