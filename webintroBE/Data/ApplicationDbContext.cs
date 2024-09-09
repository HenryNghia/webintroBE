using Microsoft.EntityFrameworkCore;
using webintroBE.Models;

namespace webintroBE.Data

{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        public DbSet<BaiViet> BaiViets { get; set; }
        public DbSet<Email> Emails { get; set; }
    }
}
