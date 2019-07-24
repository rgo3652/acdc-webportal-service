using acdc_webportal_service.Dal;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace acdc_webportal_service.Models
{
  public class DataAccessProvider : IDataAccessProvider
  {
    private readonly WebPortalContext _context;
    public DataAccessProvider(WebPortalContext context)
    {
      _context = context;
    }

    public async Task<List<WebPortalInfo>> GetAllRecords()
    {
      if (_context != null)
      {
        return await _context.WebPortalInfos.ToListAsync();
      }

      return null;
    }

    public async Task<string> AddRecord(WebPortalInfo webPortalInfo)
    {
      if (_context != null)
      {
        await _context.WebPortalInfos.AddAsync(webPortalInfo);
        await _context.SaveChangesAsync();
        return webPortalInfo.AccessToken;
      }
      return null;
    }

    public async Task UpdateRecord(WebPortalInfo webPortalInfo)
    {
      if (_context != null)
      {
        _context.WebPortalInfos.Update(webPortalInfo);
        await _context.SaveChangesAsync();
      }
    }

    public async Task DeleteRecord(string accessToken)
    {
      if (_context != null)
      {
        var entity = await _context.WebPortalInfos.FirstOrDefaultAsync(t => t.AccessToken == accessToken);
        if (entity != null)
        {
          _context.WebPortalInfos.Remove(entity);
          await _context.SaveChangesAsync();
        }
      }
    }

    public async Task<WebPortalInfo> GetSingleRecord(string accessToken)
    {
      if (_context != null)
      {
        return await _context.WebPortalInfos.FirstOrDefaultAsync(x => x.AccessToken == accessToken);
      }
      return null;
    }
  }
}
