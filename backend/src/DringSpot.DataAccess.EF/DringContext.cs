using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DringSpot.DataAccess.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DringSpot.DataAccess.EF
{
    public class DringContext: DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<MeetingPlace> Places { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<PlaceWithinFindModel> PlacesWithin { get; set; }

        public DringContext(DbContextOptions<DringContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoryPlace>()
                .HasKey(t => new { t.CategoryId, t.MeetingPlaceId });
        }

        public IQueryable<PlaceWithinFindModel> GetPlacesWithin(double lat, double lon, double distance)
        {
            var lonParam = new SqlParameter("@long", lon);
            var longParam = new SqlParameter("@lat", lat);
            var distanceParam = new SqlParameter("@range", distance);
            
            return PlacesWithin.FromSqlRaw("exec getPlacesWithinRange @long, @lat, @range", lonParam, longParam, distanceParam);
        }
    }
}