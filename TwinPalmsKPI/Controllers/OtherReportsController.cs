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
    public class OtherReportsController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public OtherReportsController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets a list of all OtherReports
        /// </summary>
        // TODO Add Authorize
        [HttpGet(Name = "GetOtherReports")/*, Authorize(Roles = "Administrator, Manager")*/] 
        public async Task<IActionResult> GetOtherReports()
        {
            var otherReports = await _repository.OtherReport.GetAllOtherReportsAsync(trackChanges: false);
            var otherReportsDto = _mapper.Map<IEnumerable<OtherReportDto>>(otherReports);
            return Ok(otherReportsDto);
        }

        /// <summary>
        /// Gets a single OtherReport by ID
        /// </summary>
        [HttpGet("{id}", Name = "OtherReportById")]
        public async Task<IActionResult> GetOtherReport(int id)
        {
            var otherReport = await _repository.OtherReport.GetOtherReportAsync(id, trackChanges: false);
            if (otherReport == null)
            {
                _logger.LogInfo($"OtherReport with id {id} doesn't exist in the database.");
                return NotFound();
            }
            var otherReportDto = _mapper.Map<OtherReportDto>(otherReport);
            return Ok(otherReportDto);
        }

        /// <summary>
        /// Creates a new OtherReport
        /// </summary>
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateOtherReport([FromBody] OtherReportForCreationDto otherReport)
        {
            var otherReportEntity = _mapper.Map<OtherReport>(otherReport);
            _repository.OtherReport.CreateOtherReport(otherReportEntity);
            await _repository.SaveAsync();
            var otherReportToReturn = _mapper.Map<OtherReportDto>(otherReportEntity);
            return CreatedAtRoute("OtherReportById", new { id = otherReportToReturn.Id }, otherReportToReturn);
        }

        /// <summary>
        /// Deletes a OtherReport by ID
        /// </summary>
        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidateOtherReportExistsAttribute))]
        public async Task<IActionResult> DeleteOtherReport(int id)
        {
            var otherReport = HttpContext.Items["otherReport"] as OtherReport;
            _repository.OtherReport.DeleteOtherReport(otherReport);
            await _repository.SaveAsync();
            return NoContent();
        }

        /// <summary>
        /// Updates a OtherReport by ID
        /// </summary>
        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateOtherReportExistsAttribute))]
        public async Task<IActionResult> UpdateOtherReport(int id, [FromBody] OtherReportForUpdateDto otherReport)
        {
            var otherReportEntity = HttpContext.Items["otherReport"] as OtherReport;
            _repository.OtherReport.UpdateOtherReport(otherReportEntity);
            _mapper.Map(otherReport, otherReportEntity);
            await _repository.SaveAsync();
            var otherReportToReturn = _mapper.Map<OtherReportDto>(otherReportEntity);
            return CreatedAtRoute("OtherReportById", new { id = otherReportToReturn.Id }, otherReportToReturn);
        }
    }
}
