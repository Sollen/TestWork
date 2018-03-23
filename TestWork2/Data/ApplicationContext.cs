//Контекст данных EF

using Microsoft.EntityFrameworkCore;
using TestWork2.Models;

namespace TestWork2.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        { }

        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Category> Categories { get; set; }

    }
}
