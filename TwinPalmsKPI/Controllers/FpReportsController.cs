using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TwinPalmsKPI.ActionFilters;

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
        public async Task<IActionResult> GetFbReport(int id)
        {
            var fbReport = await _repository.FbReport.GetFbReportAsync(id, trackChanges: false);

            if (fbReport == null)
            {
                _logger.LogInfo($"FbReport with id {id} doesn't exist in the database.");
                return NotFound();
            }

            var fbReportDto = _mapper.Map<FbReportDto>(fbReport);

            // Adding weathers
            fbReportDto.Weathers = fbReport.WeatherFbReports.Select(fbwr => fbwr.Weather).ToList();

            // Adding guestSourceOfBusinesses
            fbReportDto.GuestSourceOfBusinesses = fbReport.FbReportGuestSourceOfBusinesses.Select(fbwr => fbwr.GuestSourceOfBusiness).ToList();

            return Ok(fbReportDto);
        }

        // **************************************************** POST CreateFbReport *************************************************
        /// <summary>
        /// Creates a new fbReport
        /// </summary>
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateFbReport([FromBody] FbReportForCreationDto fbReport)
        {
            var fbReportEntity = _mapper.Map<FbReport>(fbReport);
            _repository.FbReport.CreateFbReport(fbReportEntity);

            await ValidateWeathers(fbReport, fbReportEntity);

            List<GuestSourceOfBusiness> GuestSourcesOfBusinessesFromDb =
                (List<GuestSourceOfBusiness>)await _repository.GuestSourceOfBusiness.GetAllGuestSourceOfBusinessesAsync(trackChanges: false);
            int nrOfGuestSourcesOfBusinessesFromDb = GuestSourcesOfBusinessesFromDb.Count;

            List<Outlet> OutletsFromDb = (List<Outlet>)await _repository.Outlet.GetAllOutletsAsync(trackChanges: false);
            int nrOfOutletsFromDb = OutletsFromDb.Count;

            List<LocalEvent> LocalEventFromDb = (List<LocalEvent>)await _repository.LocalEvent.GetAllLocalEventsAsync(trackChanges: false);
            int nrOfLocalEventsFromDb = LocalEventFromDb.Count;

            if (fbReport.GuestSourceOfBusinesses != null && fbReport.GuestSourceOfBusinesses.Count > 0)
            {
                ValidateGsobs(fbReport, fbReportEntity, nrOfGuestSourcesOfBusinessesFromDb);
            }
                
            await Validations(fbReport, nrOfOutletsFromDb, nrOfLocalEventsFromDb);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _repository.SaveAsync();
            return Ok();
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

        // ************************************************ ValidateLocalEvents *********************************************************
        private void ValidateGsobs(FbReportForCreationDto fbReport, FbReport fbReportEntity, int nrOfGuestSourcesOfBusinessesFromDb)
        {
            int gsobCounter = 0;

            foreach (var gsobId in fbReport.GuestSourceOfBusinesses)
            {
                gsobCounter++;

                // Validating if inputted guestSourceOFBusinessId exists in DB
                if (gsobId < 1 || gsobId > nrOfGuestSourcesOfBusinessesFromDb)
                {
                    ModelState.AddModelError("ArgumentOutOfRangeError",
                        $"GuestSourceOFBusiness[{gsobCounter}] must be an integer between 1 and {nrOfGuestSourcesOfBusinessesFromDb}. It's now {gsobId}");
                }

                var fbReportGuestSourceOfBusiness = new FbReportGuestSourceOfBusiness
                {
                    GuestSourceOfBusinessId = gsobId,
                    FbReportId = fbReportEntity.Id
                };

                fbReportEntity.FbReportGuestSourceOfBusinesses.Add(fbReportGuestSourceOfBusiness);
            }
        }

        // ************************************************* ValidateWeather **************************************************************
        private async Task ValidateWeathers(FbReportForCreationDto fbReport, FbReport fbReportEntity)
        {
            // Validating if there is any weather in FbReport
            if (fbReport.Weathers == null || fbReport.Weathers.Count == 0)
            {
                ModelState.AddModelError("ArgumentOutOfRangeError", "At least one WeatherId is required.");
            }
            else // If there is, validate weathers
            {
                List<Weather> weathersFromDb = (List<Weather>)await _repository.Weather.GetAllTypesOfWeatherAsync(trackChanges: false);
                int nrOfWeathersFromDb = weathersFromDb.Count;
                int weatherCounter = 0;

                foreach (var weatherId in fbReport.Weathers)
                {
                    weatherCounter++;

                    // Validating if inputted weatherId exists in DB
                    if (weatherId < 1 || weatherId > nrOfWeathersFromDb)
                    {
                        ModelState.AddModelError("ArgumentOutOfRangeError", $"WeatherId[{weatherCounter}] must be an integer between 1 and {nrOfWeathersFromDb}. It's now {weatherId}");
                    }

                    var fbReportWeather = new WeatherFbReport
                    {
                        WeatherId = weatherId,
                        FbReportId = fbReportEntity.Id
                    };

                    fbReportEntity.WeatherFbReports.Add(fbReportWeather);
                }
            }
        }

        // ******************************************************** Validations *************************************************
        private async Task Validations(FbReportForCreationDto fbReport, int nrOfOutletsFromDb, int nrOfLocalEventsFromDb)
        {
            // Validating if user exists in DB
            string userIdFromInput = fbReport.UserId;
            User user = await _repository.User.GetUserAsync(userIdFromInput, trackChanges: false);

            if (user == null)
            {
                ModelState.AddModelError("NotFoundError", $"User with id {userIdFromInput} doesn't exist in the database");
            }

            int outletId = fbReport.OutletId;

            // Validating if Outlet exists in DB
            if (outletId < 1 || outletId > nrOfOutletsFromDb)
            {
                ModelState.AddModelError("ArgumentOutOfRangeError", $"OutletId must be an integer between 1 and {nrOfOutletsFromDb}. It's now {outletId}");
            }

            int? localEventId = fbReport.LocalEventId;

            // Validating if LocalEvent exists in DB
            if (localEventId < 1 || localEventId > nrOfLocalEventsFromDb)
            {
                ModelState.AddModelError("ArgumentOutOfRangeError", $"LocalEventId must be an integer between 1 and {nrOfLocalEventsFromDb}. It's now {localEventId}");
            }

            // Validating isPublicHoliday
            if (fbReport.IsPublicHoliday.GetType() != typeof(bool))
            {
                ModelState.AddModelError("ArgumentTypeError", $"IsPublicHoliday must be a boolean");
            }
        }
    }
}
