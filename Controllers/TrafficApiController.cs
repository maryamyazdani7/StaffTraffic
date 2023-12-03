using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StaffTraffic.Areas.Identity.Data;
using StaffTraffic.DataAccess;
using StaffTraffic.Models.Entities;

namespace StaffTraffic.Controllers
{
    [Route("api/traffic")]
    [ApiController]
    [Authorize]
    public class TrafficApiController : ControllerBase
    {
        private readonly TrafficService _trafficService;
        private readonly UserManager<ApplicationUser> _userManager;

        public TrafficApiController(UserManager<ApplicationUser> userManager,
            TrafficService trafficService)
        {
            _trafficService = trafficService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Traffic traffic)
        {
            var t = await _trafficService.Create(traffic);
            return t != default ? Ok(t) : BadRequest();

        }

        [HttpGet]
        public async Task<List<Traffic>> Get(Guid? userId = null,
            DateTime? createdOn = null,
            DateTime? regDate = null)
        {
            var traffics = await _trafficService.Get(userId, createdOn, regDate);
            return traffics;

        }
        [HttpGet, Route("{id}")]
        public async Task<Traffic?> GetById(Guid id)
        {
            var traffic = await _trafficService.GetById(id);
            return traffic;

        }
    }
}
