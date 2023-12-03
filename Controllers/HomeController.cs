using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StaffTraffic.Areas.Identity.Data;
using StaffTraffic.DataAccess;
using StaffTraffic.Models;
using StaffTraffic.Models.Entities;
using System.Diagnostics;
using System.Security.Claims;

namespace StaffTraffic.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TrafficService _trafficService;

        public HomeController(UserManager<ApplicationUser> userManager,
            ILogger<HomeController> logger,
            TrafficService trafficService)
        {
            _logger = logger;
            _trafficService = trafficService;
        }


        
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var traffics = _trafficService.Get(userId: (userId != null ? new Guid(userId) : null)).GetAwaiter().GetResult();
            return View(traffics);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}