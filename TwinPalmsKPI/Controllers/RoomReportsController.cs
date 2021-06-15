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
    public class RoomReportsController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public RoomReportsController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets a list of all roomsReports
        /// </summary>
        // TODO Add Authorize
       /* [HttpGet(Name = "GetRoomsReport")*//*, Authorize(Roles = "Administrator, Manager")*//*]
        public async Task<IActionResult> GetRoomsReport()
        {
            var roomsReports = await _repository.RoomsReport.GetAllRoomsReportsAsync(trackChanges: false);
            var roomsReportsDto = _mapper.Map<IEnumerable<RoomsReportDto>>(roomsReports);
            return Ok(roomsReportsDto);
        }*/

        /// <summary>
        /// Gets a single roomsReport by ID
        /// </summary>
        [HttpGet("{id}", Name = "RoomsReporById")]
        public async Task<IActionResult> GetRoomsReport(/*int roomsReportId,*/ int id)
        {
            var roomsReport = await _repository.RoomsReport.GetRoomsReportAsync(id, trackChanges: false);
            if (roomsReport == null)
            {
                _logger.LogInfo($"RoomsReport with id {id} doesn't exist in the database.");
                return NotFound();
            }
            var roomsReportDto = _mapper.Map<RoomReportDto>(roomsReport);
            return Ok(roomsReportDto);
        }

        /// <summary>
        /// Creates a new roomsReport
        /// </summary>
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateRoomsReport([FromForm] RoomReportForCreationDto roomReport)
        {
            _logger.LogDebug("file " + roomReport.File);

            var roomReportEntity = _mapper.Map<RoomReport>(roomReport);
            if (roomReport.LocalEventId != null)
            {

                var localEvent = await _repository.LocalEvent.GetLocalEventAsync((int)roomReport.LocalEventId, false);
                
                if (localEvent == null)
                {
                    return NotFound("Local event does not exist");
                }
            }
            if (roomReport.RoomTypeId > 0)
            {

                var roomType = await _repository.RoomType.GetRoomTypeAsync(roomReport.RoomTypeId, false);

                if (roomType == null)
                {
                    return NotFound("Room type does not exist");
                }
            }

            _repository.RoomsReport.CreateRoomsReport(roomReportEntity);
            await _repository.SaveAsync();
            var roomReportToReturn = _mapper.Map<RoomReportDto>(roomReportEntity);
            return CreatedAtRoute("RoomsReportById", new { id = roomReportToReturn.Id }, roomReportToReturn);
        }

        /// <summary>
        /// Deletes a roomsReport by ID
        /// </summary>
        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidateRoomsReportExistsAttribute))]
        public async Task<IActionResult> DeleteRoomsReport(int id)
        {
            var roomsReport = HttpContext.Items["roomsReport"] as RoomReport;
            _repository.RoomsReport.DeleteRoomsReport(roomsReport);
            await _repository.SaveAsync();
            return NoContent();
        }

        /// <summary>
        /// Updates a roomsReport by ID
        /// </summary>
        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateRoomsReportExistsAttribute))]
        public async Task<IActionResult> UpdateRoomsReport(int id, [FromBody] RoomsReportForUpdateDto roomsReport)
        {
            var roomsReportEntity = HttpContext.Items["roomsReport"] as RoomReport;
            _repository.RoomsReport.UpdateRoomsReport(roomsReportEntity);
            _mapper.Map(roomsReport, roomsReportEntity);
            await _repository.SaveAsync();
            var roomsReportToReturn = _mapper.Map<RoomReportDto>(roomsReportEntity);
            return CreatedAtRoute("RoomsReportById", new { id = roomsReportToReturn.Id }, roomsReportToReturn);
        }
        /// <summary>
        /// Gets a list of all roomsReports
        /// </summary>
        // TODO Add Authorize
        [HttpGet()/*, Authorize(Roles = "Administrator, Manager")*/]
        public async Task<IActionResult> GetRoomsReport(int hotelId, [FromQuery] int[] roomTypes, DateTime fromDate, DateTime toDate)
        {
            var roomsReports = await _repository.RoomsReport.GetAllRoomsReportsDataAsync(hotelId, roomTypes, fromDate, toDate, false);
            var roomsReportsDto = _mapper.Map<IEnumerable<RoomReportDto>>(roomsReports);
            
            return Ok(roomsReportsDto);
        }
    }
}
