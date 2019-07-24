using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace acdc_webportal_service.Models
{
  public class WebPortalInfo
  {
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public string AccessToken { get; set; }
    public string AcdcId { get; set; }
    public bool Flag { get; set; }
  }
}
