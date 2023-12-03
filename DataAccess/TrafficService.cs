using Microsoft.EntityFrameworkCore;
using StaffTraffic.Areas.Identity.Data;
using StaffTraffic.Data;
using StaffTraffic.Models.Entities;

namespace StaffTraffic.DataAccess
{
    public class TrafficService
    {
        readonly ApplicationContext _context;
        public TrafficService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Guid> Create(Traffic traffic)
        {
            await _context.Traffic.AddAsync(traffic);
            _context.SaveChanges();
            return traffic.Id;
        }
        public async Task<bool> Update(Traffic traffic)
        {
            var trafficTarget = await _context.Traffic.AsNoTracking().FirstOrDefaultAsync(x => x.Id == traffic.Id);
            if (traffic is null)
            {
                return false;
            }
            _context.Traffic.Update(traffic);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(Traffic traffic)
        {
            var trafficTarget = await _context.Traffic.AsNoTracking().FirstOrDefaultAsync(x => x.Id == traffic.Id);
            if (traffic is null)
            {
                return false;
            }
            _context.Traffic.Remove(traffic);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<Traffic?> GetById(Guid id)
        {
            var traffic = await _context.Traffic.FirstOrDefaultAsync(x => x.Id == id);
            return traffic;
        }
        public async Task<List<Traffic>> Get(Guid? userId = null,
            DateTime? createdOn = null,
            DateTime? regDate = null)
        {
            try
            {
                var query = _context.Traffic.Include(x => x.User)
                    .OrderByDescending(x => x.RegDate);
                if (userId is not null)
                {
                    query.Where(x => x.UserId == userId);
                }
                if (createdOn is not null)
                {
                    query.Where(x => x.CreatedOn >= createdOn);
                }
                if (regDate is not null)
                {
                    query.Where(x => x.RegDate >= regDate);
                }

                var traffic = await query.ToListAsync();
                return traffic;

            }
            catch
            {
                return new List<Traffic>();
            }
        }

    }
}
