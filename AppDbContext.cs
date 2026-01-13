using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace WebApp
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Fatura> Faturas { get; set; }
    }
}