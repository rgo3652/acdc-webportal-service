using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace acdc_webportal_service.Models
{
  public interface IDataAccessProvider
  {
    Task<string> AddRecord(WebPortalInfo webPortalInfo);
    Task UpdateRecord(WebPortalInfo webPortalInfo);
    Task DeleteRecord(string accessToken);
    Task<WebPortalInfo> GetSingleRecord(string accessToken);
    Task<List<WebPortalInfo>> GetAllRecords();
  }
}
