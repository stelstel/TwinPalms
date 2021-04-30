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
using System.Text;
using System.Text.Json;

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
        public async Task<IActionResult> GetFbReport(/*int fbReportId,*/ int id)
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

        //*********************************************************** POST **************************************
        /// <summary>
        /// Creates a new fbReport
        /// </summary>
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateFbReport([FromBody] FbReportForCreationDto fbReport)
        {
            var fbReportEntity = _mapper.Map<FbReport>(fbReport);
            _repository.FbReport.CreateFbReport(fbReportEntity);

            StringBuilder sbWeatherIds = new StringBuilder();
            int weatherCounter = 0;

            // Adding to Weather*
            foreach (var weatherId in fbReport.Weathers)
            {
                var fbReportWeather = new WeatherFbReport
                {
                    WeatherId = weatherId,
                    FbReportId = fbReportEntity.Id
                };

                sbWeatherIds.Append($"{ fbReportWeather.WeatherId}");

                if (weatherCounter < fbReport.Weathers.Count - 1)
                {
                    sbWeatherIds.Append(", ");
                }

                fbReportEntity.WeatherFbReports.Add(fbReportWeather);
                weatherCounter++;
            }

            StringBuilder sbGuestSourceOfBusinessIds = new StringBuilder();
            int guestSourceOfBusinessCounter = 0;

            // Adding *GuestSourceOfBusiness
            foreach (var guestSourceOfBusinessId in fbReport.GuestSourceOfBusinesses)
            {
                var fbReportGuestSourceOfBusiness = new FbReportGuestSourceOfBusiness
                {
                    GuestSourceOfBusinessId = guestSourceOfBusinessId,
                    FbReportId = fbReportEntity.Id
                };

                sbGuestSourceOfBusinessIds.Append($"{ fbReportGuestSourceOfBusiness.GuestSourceOfBusinessId}");

                if (guestSourceOfBusinessCounter < fbReport.GuestSourceOfBusinesses.Count - 1)
                {
                    sbGuestSourceOfBusinessIds.Append(", ");
                }

                fbReportEntity.FbReportGuestSourceOfBusinesses.Add(fbReportGuestSourceOfBusiness);
                guestSourceOfBusinessCounter++;
            }

            await _repository.SaveAsync();
            var fbReportToReturn = _mapper.Map<FbReportDto>(fbReportEntity);

            //return CreatedAtRoute("FbReportById", new { id = fbReportToReturn.Id }, fbReportToReturn);

            StringBuilder sbToReturn = BuildResponse(fbReportEntity, sbWeatherIds, sbGuestSourceOfBusinessIds);

            return Ok(sbToReturn.ToString());

        }

        private static StringBuilder BuildResponse(FbReport fbReportEntity, StringBuilder sbWeatherIds, StringBuilder sbGuestSourceOfBusinessIds)
        {
            StringBuilder sbToReturn = new StringBuilder();

            sbToReturn.Append($"\"tables\": {fbReportEntity.Tables},{Environment.NewLine}");
            sbToReturn.Append($"\"food\": {fbReportEntity.Food},{Environment.NewLine}");
            sbToReturn.Append($"\"beverage\": {fbReportEntity.Beverage},{Environment.NewLine}");
            sbToReturn.Append($"\"date\": {fbReportEntity.Date},{Environment.NewLine}");
            sbToReturn.Append($"\"otherincome\": {fbReportEntity.OtherIncome},{Environment.NewLine}");
            sbToReturn.Append($"\"guestsfromhotel\": {fbReportEntity.GuestsFromHotel},{Environment.NewLine}");
            sbToReturn.Append($"\"ispublicholiday\": {fbReportEntity.IsPublicHoliday},{Environment.NewLine}");
            sbToReturn.Append($"\"notes\": \"{fbReportEntity.Notes}\",{Environment.NewLine}");
            sbToReturn.Append($"\"guestsfromoutsidehotel\": {fbReportEntity.GuestsFromOutsideHotel},{Environment.NewLine}");
            sbToReturn.Append($"\"outletid\": {fbReportEntity.OutletId},{Environment.NewLine}");
            sbToReturn.Append($"\"userid\": \"{fbReportEntity.UserId}\",{Environment.NewLine}");
            sbToReturn.Append($"\"localeventid\": {fbReportEntity.LocalEventId},{Environment.NewLine}");

            sbToReturn.Append($"\"weathers\": [{Environment.NewLine}");
            sbToReturn.Append($"{sbWeatherIds.ToString()}{Environment.NewLine}],{Environment.NewLine}");
            sbToReturn.Append($"\"guestsourceofbusinesses\": [{Environment.NewLine}");
            sbToReturn.Append($" {sbGuestSourceOfBusinessIds.ToString()}{Environment.NewLine}]");

            return sbToReturn;
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
