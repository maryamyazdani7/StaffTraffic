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

            var users =  new List<ApplicationUser>();
            if (User.IsInRole("Admin"))
            {
                users = await _userService.Get();
            }
            else
            {
                var currentUser = await _userService.GetById(new Guid(userId));
                if (currentUser != null)
                {
                    users.Add(currentUser);
                }
            }
            var traffics = await _trafficService.Get(userId: (userId != null && User.IsInRole("User") ? new Guid(userId) : null));
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