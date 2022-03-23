using GeoCodingExample.Models;
using System.Globalization;

namespace GeoCodingExample.Repository
{
    public class AddressRepository : IAddressRepository
    {
        DataContext db;
        private GeocodeHelper geocodeHelper;
        public AddressRepository(DataContext _db, GeocodeHelper _geocodehelper)
        {
            db = _db;
            geocodeHelper = _geocodehelper;
        }
        public void Delete(GeocodedAddress entity)
        {
            db.GeoCodedAddress.Remove(entity);
            db.SaveChanges();
        }

        public GeocodedAddress GetById(int id)
        {
            return db.GeoCodedAddress.FirstOrDefault(x=>x.ID == id);
        }

        public IQueryable<GeocodedAddress> GetAddressAll()
        {
            return db.GeoCodedAddress.AsQueryable();
        }

        public IEnumerable<GeocodedAddress> GetAddresses()
        {
            return db.GeoCodedAddress.ToList();
        }

        public void Insert(GeocodedAddress entity)
        {
            var result = geocodeHelper.FindGeoCode(entity.Address());
            //Burada api den gelen verilerin içerisinden lokasyon bilgilerini bulup değişkenlere atıyoruz
            string lat = Convert.ToString(result.results[0].geometry.location.lat, CultureInfo.InvariantCulture);
            string lng = Convert.ToString(result.results[0].geometry.location.lng, CultureInfo.InvariantCulture);
            //Bulduğumuz lokasyon bilgilerini modelimizin içerisindeki enlem boylam değişkenlerine atıp db ye kaydedilebilir hale getiriyoruz
            entity.Lat = lat;
            entity.Lng = lng;
            db.GeoCodedAddress.Add(entity);
            db.SaveChanges();
        }

        public void Update(GeocodedAddress entity)
        {
            db.GeoCodedAddress.Update(entity);
            db.SaveChanges();
        }

        //Girilen iki adres arasındaki mesafeyi ölçüyoruz
        public string FindDistance(string a, string b)
        {

            double lat1, lat2, lng1, lng2;
            double distance = 0;
            //İlk olarak girilen 1. adresin lokasyonlarını bulup ilgili değişkenlere atıyoruz
            var result1 = geocodeHelper.FindGeoCode(a);
            lat1 = Convert.ToDouble(result1.results[0].geometry.location.lat, CultureInfo.InvariantCulture);
            lng1 = Convert.ToDouble(result1.results[0].geometry.location.lng, CultureInfo.InvariantCulture);
            //Girilen 2. adresin lokasyonlarını bulup ilgili değişkenlere atıyoruz
            var result2 = geocodeHelper.FindGeoCode(b);
            lat2 = Convert.ToDouble(result2.results[0].geometry.location.lat, CultureInfo.InvariantCulture);
            lng2 = Convert.ToDouble(result2.results[0].geometry.location.lng, CultureInfo.InvariantCulture);

            //Girilen iki adres aynı mı değil mi diye kontrol ediyoruz. Lokasyon bilgileri eşitse mesafe olarak varsayılan 0 değerini döndürüyoruz
            if((lat1 == lat2) && (lng1==lng2))
            {
                
                return distance.ToString();
            }
            else
            {
                //Adresler aynı değilse arasındaki mesafeyi hesaplamak için gerekli matematik fonksiyonlarını uyguluyoruz
                double rlat1 = Math.PI * lat1 / 180;
                double rlat2 = Math.PI * lat2 / 180;
                double theta = lng1 - lng2;
                double rtheta = Math.PI * theta / 180;
                double dist =
                    Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) *
                    Math.Cos(rlat2) * Math.Cos(rtheta);
                dist = Math.Acos(dist);
                dist = dist * 180 / Math.PI;
                dist = dist * 60 * 1.1515;
                //Bulunan mesafenin KM türünden hesabı için aşağıdaki 1.609344 değeri ile çarpıp işlemi gerçekleştiriyoruz. Varsayılan olarak (mil) hesabı yapılıyor. Biz bu çarpım ile KM türüne çeviriyoruz
                distance = dist * 1.609344;
            }
            return Math.Round(distance,2).ToString();
        }
    }
}
