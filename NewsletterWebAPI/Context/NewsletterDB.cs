using Microsoft.EntityFrameworkCore;
using NewsletterWebAPI.Models;

namespace NewsletterWebAPI.Context
{
    public class NewsletterDB : DbContext
    {
        public NewsletterDB(DbContextOptions options) : base(options)
        {
        }

      public  DbSet<User> Users { get; set; }
      public  DbSet<Newsletter> Newsletters { get; set; }
    }
}
