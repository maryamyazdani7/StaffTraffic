using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StaffTraffic.Areas.Identity.Data;
using StaffTraffic.DataAccess;
using StaffTraffic.Models;
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
        public async Task<IActionResult> Create(TrafficParams traffic)
        {
            var inTraffic = await _trafficService.Create(new Traffic
            {
            
                UserId = traffic.UserId,
                CreatedOn = DateTime.Now,
                RegDate = traffic.InDate,
                IsInDate = true,
                Description = traffic.Description
            
            });
            var outTraffic = await _trafficService.Create(new Traffic
            {
            
                UserId = traffic.UserId,
                CreatedOn = DateTime.Now,
                RegDate = traffic.OutDate,
                IsInDate = false,
                Description = traffic.Description
            
            });

            return outTraffic != default && inTraffic != default ? Ok() : BadRequest();

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
