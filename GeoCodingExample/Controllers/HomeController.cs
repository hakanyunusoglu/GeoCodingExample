using GeoCodingExample.Models;
using GeoCodingExample.Repository;
using Microsoft.AspNetCore.Mvc;

namespace GeoCodingExample.Controllers
{
    public class HomeController : Controller
    {
        private IAddressRepository rep;
        private GeocodeHelper geocodeHelper;
        public HomeController(IAddressRepository _rep, GeocodeHelper _geocodeHelper)
        {
            rep = _rep;
            geocodeHelper = _geocodeHelper;
        }
        
        public IActionResult Index()
        {
            return View(rep.GetAddressAll());
        }
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Create(GeocodedAddress model)
        {
            
            rep.Insert(model);
            
            return RedirectToAction("Index");
        }
        public IActionResult Details(int id)
        {
            GeocodedAddress detail = new GeocodedAddress();
           
            detail = rep.GetById(id);
            if (detail != null)
            {
                return View(detail);
            }
            return View(detail);
        }
        public IActionResult Distance(string address1,string address2)
        {
            if(address1 != null && address2 != null)
            { 
            ViewBag.distance = rep.FindDistance(address1,address2);
            }
            return View();
        }
    }
}