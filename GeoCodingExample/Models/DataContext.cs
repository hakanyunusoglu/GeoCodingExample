using Microsoft.EntityFrameworkCore;

namespace GeoCodingExample.Models
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<GeocodedAddress> GeoCodedAddress { get; set; }
    }
}
