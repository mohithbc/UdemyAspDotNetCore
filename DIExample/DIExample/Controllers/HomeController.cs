using Microsoft.AspNetCore.Mvc;
using Services;

namespace DIExample.Controllers
{
    public class HomeController : Controller
    {
        //private readonly CitiesService _citiesService;
        //// Constructor
        //public HomeController(CitiesService citiesService)
        //{
        //    _citiesService = citiesService;
        //}
        [Route("/")]     
        public IActionResult Index([FromServices]CitiesService _citiesService)
        {     
           List<string> cities = _citiesService.GetCities();
            return View();
        }
    }
}  
