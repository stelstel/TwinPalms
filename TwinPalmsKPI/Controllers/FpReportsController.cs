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
    [Route("api/[controller]")]
    [ApiController]
    public class FbReportsController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public FbReportsController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets a list of all fbReports
        /// </summary>
        // TODO Add Authorize
        [HttpGet(Name = "GetFbReports")/*, Authorize(Roles = "Administrator, Manager")*/] 
        public async Task<IActionResult> GetFbReports()
        {
            var fbReports = await _repository.FbReport.GetAllFbReportsAsync(trackChanges: false);
            var fbReportsDto = _mapper.Map<IEnumerable<FbReportDto>>(fbReports);
            return Ok(fbReportsDto);
        }

        /// <summary>
        /// Gets a single fbReport by ID
        /// </summary>
        [HttpGet("{id}", Name = "FbReportById")]
        public async Task<IActionResult> GetFbReport(int fbReportId, int id)
        {
            var fbReport = await _repository.FbReport.GetFbReportAsync(id, trackChanges: false);
            if (fbReport == null)
            {
                _logger.LogInfo($"FbReport with id {id} doesn't exist in the database.");
                return NotFound();
            }
            var fbReportDto = _mapper.Map<FbReportDto>(fbReport);
            return Ok(fbReportDto);
        }

        /// <summary>
        /// Creates a new fbReport
        /// </summary>
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateFbReport([FromBody] FbReportForCreationDto fbReport)
        {
            var fbReportEntity = _mapper.Map<FbReport>(fbReport);
            _repository.FbReport.CreateFbReport(fbReportEntity);
            await _repository.SaveAsync();
            var fbReportToReturn = _mapper.Map<FbReportDto>(fbReportEntity);
            return CreatedAtRoute("FbReportById", new { id = fbReportToReturn.Id }, fbReportToReturn);
        }

        /// <summary>
        /// Deletes a fbReport by ID
        /// </summary>
        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidateFbReportExistsAttribute))]
        public async Task<IActionResult> DeleteFbReport(int id)
        {
            var fbReport = HttpContext.Items["fbReport"] as FbReport;
            _repository.FbReport.DeleteFbReport(fbReport);
            await _repository.SaveAsync();
            return NoContent();
        }

        /// <summary>
        /// Updates a fbReport by ID
        /// </summary>
        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateFbReportExistsAttribute))]
        public async Task<IActionResult> UpdateFbReport(int id, [FromBody] FbReportForUpdateDto fbReport)
        {
            var fbReportEntity = HttpContext.Items["fbReport"] as FbReport;
            _repository.FbReport.UpdateFbReport(fbReportEntity);
            _mapper.Map(fbReport, fbReportEntity);
            await _repository.SaveAsync();
            var fbReportToReturn = _mapper.Map<FbReportDto>(fbReportEntity);
            return CreatedAtRoute("FbReportById", new { id = fbReportToReturn.Id }, fbReportToReturn);
        }
    }
}
