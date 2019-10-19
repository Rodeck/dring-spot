using DringSpot.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DringSpot.DataAccess.EF
{
    public class DringContext: DbContext
    {
        public DbSet<User> Users { get; set; }

        public DringContext (DbContextOptions<DringContext> options)
            : base(options)
        {
        }
    }
}