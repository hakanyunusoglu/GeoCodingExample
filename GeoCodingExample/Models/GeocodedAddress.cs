using System.ComponentModel.DataAnnotations.Schema;

namespace GeoCodingExample.Models
{
    public class GeocodedAddress
    {
        public int ID { get; set; }
        public string Address()
        {
            string street = Street.Replace(" ", "+");
            return string.Concat(street,"+",StreetNumber,"+",City,"+",Country);
        }
        public string? Street { get; set; }
        public string? StreetNumber { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? Lat { get; set; }
        public string? Lng { get; set; }

    }
}
