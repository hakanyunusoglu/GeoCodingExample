using GeoCodingExample.Models;

namespace GeoCodingExample.Repository
{
    public interface IAddressRepository
    {
        IEnumerable<GeocodedAddress> GetAddresses();
        IQueryable<GeocodedAddress> GetAddressAll();
        GeocodedAddress GetById(int id);
        string FindDistance(string a,string b);
        void Insert(GeocodedAddress entity);
        void Update(GeocodedAddress entity);
        void Delete(GeocodedAddress entity);
    }
}
