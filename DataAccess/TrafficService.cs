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
        public async Task<bool> Delete(Guid id)
        {
            var trafficTarget = await _context.Traffic.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (trafficTarget is null)
            {
                return false;
            }
            _context.Traffic.Remove(trafficTarget);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<Traffic?> GetById(Guid id)
        {
            var traffic = await _context.Traffic.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == id);
            return traffic;
        }
        public async Task<List<Traffic>> Get(Guid? userId = null,
            DateTime? createdOn = null,
            DateTime? outDate = null,
            DateTime? inDAte = null)
        {
            try
            {
                var query = _context.Traffic.Include(x => x.User)
                    .OrderByDescending(x => x.CreatedOn).AsQueryable();
                if (userId is not null)
                {
                   query = query.Where(x => x.User.Id.Equals(userId));
                }
                if (createdOn is not null)
                {
                    query = query.Where(x => x.CreatedOn >= createdOn);
                }
                if (outDate is not null)
                {
                    query = query.Where(x => x.OutDate >= outDate);
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
