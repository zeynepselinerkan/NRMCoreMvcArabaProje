using Microsoft.AspNetCore.Mvc;
using NRMCoreMvcArabaProje.Models;
using System.Diagnostics;

namespace NRMCoreMvcArabaProje.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewData["ToplamTutar"] =cars.Sum(x => x.UnitPrice); // TEMPDATA : UYGULAMA BOYUNCA YAŞAYAN DATA, HER YERDE ETKİN (HER YERDE OLMAMASI İÇİN VIEWBAG YA DA VIEWDATA)
            ViewData["OrtalamaTutar"] =cars.Average(x => x.UnitPrice); // TEMPDATA : UYGULAMA BOYUNCA YAŞAYAN DATA
            return View(cars);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult AracGirisi()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AracGirisi(Car car)
        {
            cars.Add(car);
            return RedirectToAction("Index");
            //return View("Index",cars);
        }
        public IActionResult Detay(int id)
        {
            Car kayitSec = cars.Where(x => x.Id == id).FirstOrDefault();
            return View(kayitSec);
        }

        public IActionResult Delete(int id)
        {
            Car silinecekId = cars.Find(x => x.Id == id);
            cars.Remove(silinecekId);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            Car guncelleId = cars.Where(x => x.Id == id).FirstOrDefault();

            return View(guncelleId);
        }
        [HttpPost]
        public IActionResult Edit(Car car)
        {
            Car kayit = cars.Find(x => x.Id == car.Id);
            kayit.Marka = car.Marka;
            kayit.Model = car.Model;
            kayit.UnitPrice = car.UnitPrice;
            return RedirectToAction("index");
        }

        static List<Car> cars = new List<Car>()
        {
            new Car{Id=1,Marka="BMW",Model="X5",UnitPrice=2000000},
            new Car{Id=2,Marka="Mercedes",Model="A180",UnitPrice=1500000},
            new Car{Id=3,Marka="Volvo",Model="S90",UnitPrice=2500000},
        };

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}