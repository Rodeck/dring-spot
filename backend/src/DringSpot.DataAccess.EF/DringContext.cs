using DringSpot.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DringSpot.DataAccess.EF
{
    public class DringContext: DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<MeetingPlace> Places { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DringContext (DbContextOptions<DringContext> options)
            : base(options)
        {
        }
    }
}