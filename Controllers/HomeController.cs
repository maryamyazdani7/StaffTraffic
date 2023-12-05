using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StaffTraffic.Areas.Identity.Data;
using StaffTraffic.DataAccess;
using StaffTraffic.Models;
using StaffTraffic.Models.Entities;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Security.Claims;

namespace StaffTraffic.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TrafficService _trafficService;
        private readonly UserService _userService;

        public HomeController(UserManager<ApplicationUser> userManager,
            ILogger<HomeController> logger,
            TrafficService trafficService,
            UserService usreService)
        {
            _logger = logger;
            _trafficService = trafficService;
            _userService = usreService;
        }


        
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            //var t = await _trafficService.Create(new Traffic
            //{
            //    UserId = new Guid(userId),
            //    CreatedOn = DateTime.Now,
            //    RegDate = DateTime.Now.AddDays(-1),
            //    IsInDate = true,
            //    Description = "test"
            //});
            //var t2 = await _trafficService.Create(new Traffic
            //{
            //    UserId = new Guid(userId),
            //    CreatedOn = DateTime.Now,
            //    RegDate = DateTime.Now.AddDays(-1),
            //    IsInDate = false,
            //    Description = "test2"
            //}
            //);
            var users = _userService.Get().GetAwaiter().GetResult();
            var traffics = _trafficService.Get(userId: (userId != null ? new Guid(userId) : null)).GetAwaiter().GetResult();
            var pageModel = new TrafficPageViewModel
            {
                Traffics = traffics,
                Users = users
            };
            return View(pageModel);
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