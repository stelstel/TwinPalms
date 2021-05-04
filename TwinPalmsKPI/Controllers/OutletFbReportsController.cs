using AutoMapper;
using TwinPalmsKPI.ActionFilters;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace TwinPalmsKPI.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/")]
    [ApiController]
    public class OutletsFbReportsController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public OutletsFbReportsController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets a list of all Outlets
        /// </summary>
        // TODO Add Authorize
        //[HttpGet(Name = "GetOutlets")/*, Authorize(Roles = "Administrator, Manager")*/] 
        //public async Task<IActionResult> GetOutlets()
        //{
        //    var outlets = await _repository.Outlet.GetAllOutletsAsync(trackChanges: false);
        //    var outletsDto = _mapper.Map<IEnumerable<OutletDto>>(outlets);
        //    return Ok(outletsDto);
        //}

        // {outletId}/fbReport ////////////////////////////////////////
        // [HttpGet("{outletid}/fbReport", Name = "OutletById")] //////

        /// <summary>
        /// Gets a All FbReports by Outlet Id between two DateTimes
        /// </summary>
        [HttpGet("/outlet/{outletId}/fbReports", Name = "OutletFbReportsByIdAndDate")]
        public async Task<IActionResult> GetOutletFbReport(int outletId, DateTime fromDate, DateTime toDate)
        {
            var outlet = await _repository.Outlet.GetOutletAsync(outletId, trackChanges: false);
            
            if (outlet == null)
            {
                _logger.LogInfo($"Outlets with id {outletId} doesn't exist in the database.");
                return NotFound();
            }

            var outletFbReports = await _repository.FbReport.GetAllOutletFbReportsForOneOutlet(outletId, fromDate, toDate, trackChanges: false);
            
            if (outletFbReports == null)
            {
                _logger.LogInfo($"No reports for Outlet with id {outletId} found in the database between {fromDate} and {toDate}.");
                return NotFound();
            }

            //var outletDto = _mapper.Map<OutletDto>(outlet);
            //var outletFbReportsDto = _mapper.Map<FbReportDto>(outletFbReports);
            return Ok(outletFbReports);
        }

        /// <summary>
        /// Creates a new Outlet
        /// </summary>
        //[HttpPost]
        //[ServiceFilter(typeof(ValidationFilterAttribute))]
        //public async Task<IActionResult> CreateOutlet([FromBody] OutletForCreationDto outlet)
        //{
        //    var outletEntity = _mapper.Map<Outlet>(outlet);
        //    _repository.Outlet.CreateOutlet(outletEntity);
        //    await _repository.SaveAsync();
        //    var outletToReturn = _mapper.Map<OutletDto>(outletEntity);
        //    return CreatedAtRoute("OutletById", new { id = outletToReturn.Id }, outletToReturn);
        //}

        /// <summary>
        /// Deletes a Outlet by ID
        /// </summary>
        //[HttpDelete("{id}")]
        //[ServiceFilter(typeof(ValidateOutletExistsAttribute))]
        //public async Task<IActionResult> DeleteOutlet(int id)
        //{
        //    var outlet = HttpContext.Items["outlet"] as Outlet;
        //    _repository.Outlet.DeleteOutlet(outlet);
        //    await _repository.SaveAsync();
        //    return NoContent();
        //}

        /// <summary>
        /// Updates a Outlet by ID
        /// </summary>
        //[HttpPut("{id}")]
        //[ServiceFilter(typeof(ValidationFilterAttribute))]
        //[ServiceFilter(typeof(ValidateOutletExistsAttribute))]
        //public async Task<IActionResult> UpdateOutlet(int id, [FromBody] OutletForUpdateDto outlet)
        //{
        //    var outletEntity = HttpContext.Items["outlet"] as Outlet;
        //    _repository.Outlet.UpdateOutlet(outletEntity);
        //    _mapper.Map(outlet, outletEntity);
        //    await _repository.SaveAsync();
        //    var outletToReturn = _mapper.Map<OutletDto>(outletEntity);
        //    return CreatedAtRoute("OutletById", new { id = outletToReturn.Id }, outletToReturn);
        //}
    }
}
