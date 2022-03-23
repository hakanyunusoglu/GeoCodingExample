using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using static GeoCodingExample.Models.JsonToViewModel;

namespace GeoCodingExample.Models
{
    public class GeocodeHelper
    {
        //HttpClient kullanabilmek için ve Appsettings.json içerisinden veri çekebilmek için bu 2 sini yapıcı metod ile dependency işlemi yapılıyor. Tekrar tekrar new lememek için
        private IConfiguration configuration;
        HttpClient client;
        public string key;
        public GeocodeHelper(IConfiguration iconfig, HttpClient _client)
        {
            configuration = iconfig;
            client = _client;
            key = configuration.GetValue<string>("GoogeGeoCodingApi:ApiKey");  //appsettings.json içerisinde tanımlanan ApiKey verisi çekiliyor
        }
        
        //GeoCoding api ile gelen tüm verileri çekebilmek için RootObject class ı oluşturuldu. JsonToViewModel classının içerisinde hepsi tanımlandı. Verilen address e göre lokasyonu buluyoruz
        public RootObject FindGeoCode(string address)
        {
            var root = new RootObject();

            //Api nin adresi tanımlandı
            client.BaseAddress = new Uri("https://maps.googleapis.com/maps/api/geocode/");
            //Api ye vereceğimiz parametre tanımlandı
            var responseTalk = client.GetAsync($"json?address={address}&key={key}");
            //Api den aldığımız verileri result değişkenine atadık
            var result = responseTalk.Result;

            if (result.IsSuccessStatusCode)
            {
                //Api den aldığımız verileri satır satır sonuna kadar okuyup verileri readResult değişkenine atayacağız
                using (var streamReader = new StreamReader(result.Content.ReadAsStream()))
                {
                    var readResult = streamReader.ReadToEnd();
                    //Gelen değer boş değilse oluşturduğumuz rootObject classının içerisine atıyoruz
                    if (!string.IsNullOrWhiteSpace(readResult))
                    {
                        root = JsonConvert.DeserializeObject<RootObject>(readResult);
                    }
                }
                return root;
            }
            return root;
        }
        //Verilen lokasyonlara göre adresini buluyoruz
        public RootObject FindAddressToGeoCode(string? lat, string? lng)
        {
            var root = new RootObject();

            //Api nin adresi tanımlandı
            client.BaseAddress = new Uri("https://maps.googleapis.com/maps/api/geocode/");
            //Api ye vereceğimiz parametre tanımlandı. Lokasyon bilgilerini giriyoruz
            var responseTalk = client.GetAsync($"json?latlng={lat},{lng}&key={key}");
            //Api den aldığımız verileri result değişkenine atadık
            var result = responseTalk.Result;

            if (result.IsSuccessStatusCode)
            {
                //Api den aldığımız verileri satır satır sonuna kadar okuyup verileri readResult değişkenine atayacağız
                using (var streamReader = new StreamReader(result.Content.ReadAsStream()))
                {
                    var readResult = streamReader.ReadToEnd();
                    //Gelen değer boş değilse oluşturduğumuz rootObject classının içerisine atıyoruz
                    if (!string.IsNullOrWhiteSpace(readResult))
                    {
                        root = JsonConvert.DeserializeObject<RootObject>(readResult);
                    }
                }
                return root;
            }
            return root;
        }
    }
}
