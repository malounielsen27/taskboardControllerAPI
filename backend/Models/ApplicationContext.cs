using Microsoft.EntityFrameworkCore;

namespace backend.Models
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
           
        }
        public DbSet<Board> Boards { get; set; }
        public DbSet<Column> Columns { get; set; }

        public DbSet<Card> Cards { get; set; }

    }
}
