using acdc_webportal_service.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace acdc_webportal_service.Dal
{
  public class WebPortalContext:IdentityDbContext
  {
    public WebPortalContext(DbContextOptions<WebPortalContext> options)
      : base(options)
    { }

    public WebPortalContext()
    { }

    public virtual DbSet<WebPortalInfo> WebPortalInfos { get; set; }

    public override int SaveChanges()
    {
      ChangeTracker.DetectChanges();
      return base.SaveChanges();
    }

  }
}
