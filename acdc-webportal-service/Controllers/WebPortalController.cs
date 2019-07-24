using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using acdc_webportal_service.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace acdc_webportal_service.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class WebPortalController : ControllerBase
  {

    private readonly IDataAccessProvider _dataAccessProvider;
    public WebPortalController(IDataAccessProvider dataAccessProvider)
    {
      _dataAccessProvider = dataAccessProvider;
    }


    [HttpGet]
    [Route("GetAllRecords")]
    public async Task<IActionResult> GetAllRecords()
    {
      //return Ok(new string[] { "value1", "value2" }); //testing
      try
      {
        var records = await _dataAccessProvider.GetAllRecords();
        if (records == null)
        {
          return NotFound();
        }
        return Ok(records);
      }
      catch (Exception)
      {
        return BadRequest();
      }
    }


    [HttpPost]
    [Route("AddRecord")]
    public async Task<IActionResult> AddRecord([FromBody]WebPortalInfo webPortalInfo)
    {
      if (ModelState.IsValid)
      {
        try
        {
          Guid obj = Guid.NewGuid();
          webPortalInfo.AccessToken = obj.ToString();
          string accessToken = await _dataAccessProvider.AddRecord(webPortalInfo);
          if (accessToken != null)
          {
            string url = "http://cms-services-dev.dev.cf.private.springer.com/" + accessToken;
            return Ok(url);
          }
        }
        catch (Exception ex)
        {
          return BadRequest();
        }
      }
      return BadRequest();
    }


    [HttpGet]
    [Route("GetSingleRecord/{id}")]
    public async Task<IActionResult> GetSingleRecord(string accessToken)
    {
      if (accessToken == null)
      {
        return NotFound();
      }
      try
      {
        var record = await _dataAccessProvider.GetSingleRecord(accessToken);
        if (record == null)
        {
          return NotFound();
        }
        return Ok(record);
      }
      catch (Exception ex)
      {
        return BadRequest();
      }
    }


    [HttpPut]
    [Route("UpdateRecord")]
    public async Task<IActionResult> UpdateRecord([FromBody]WebPortalInfo webPortalInfo)
    {
      if (ModelState.IsValid)
      {
        try
        {
          await _dataAccessProvider.UpdateRecord(webPortalInfo);
          return Ok();
        }
        catch (Exception ex)
        {
          if (ex.GetType().FullName == "Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException")
          {
            return NotFound();
          }

          return BadRequest();
        }
      }
      return BadRequest();
    }



    [HttpDelete]
    [Route("DeleteRecord/{id}")]
    public async Task<IActionResult> DeleteRecord(string accessToken)
    {
      if (accessToken == null)
      {
        return BadRequest();
      }
      try
      {
        await _dataAccessProvider.DeleteRecord(accessToken);
        return Ok();
      }
      catch (Exception)
      {

        return BadRequest();
      }
    }
  }
}