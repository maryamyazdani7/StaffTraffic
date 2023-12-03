using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StaffTraffic.Areas.Identity.Data;
using StaffTraffic.Models.Entities;
using System.Data;

namespace StaffTraffic.Data;

public class ApplicationContext : IdentityDbContext<ApplicationUser, Role, Guid>
{
    public ApplicationContext()
    {
        
    }
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
    }

    public DbSet<Traffic> Traffic { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ApplicationUser>(b =>
        {
            b.Property(u => u.Id).HasDefaultValueSql("newsequentialid()");
        });

        builder.Entity<Role>(b =>
        {
            b.Property(u => u.Id).HasDefaultValueSql("newsequentialid()");
        });

        builder.Entity<Traffic>(b =>
        {
            b.Property(u => u.Id).HasDefaultValueSql("newsequentialid()");
        });

    }
}
