using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text.Json;
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
        private readonly IWebHostEnvironment env;

        public FbReportsController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper, IWebHostEnvironment environment)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            env = environment;
        }

        // ************************************* GET GetFbReports *****************************************
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
            
            // This is already in fbReportDto
            //fbReportDto.ImageUrl = string.Format("data:image/jpg;base64,{0}", Convert.ToBase64String(fbReportDto.Image.Data));
            
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
        /// <remarks>
        /// For GuestSourceOfBusinesses:\
        /// Only use one object item and add as many objects you need to inside that one object.
        /// 
        /// Example:
        /// 
        /// [ \
        ///     { \
        ///         "GuestSourceOfBusinessId": 3, \
        ///         "GsobNrOfGuests": 33 \
        ///     }, \
        ///     { \
        ///         "GuestSourceOfBusinessId": 4, \
        ///          "GsobNrOfGuests": 44 \
        ///     } \
        /// ]
        /// </remarks>
        [HttpPost, DisableRequestSizeLimit]
        /*[Authorize(Roles = "Basic")]*/

        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateFbReport([FromForm]FbReportForCreationDto fbReport)
        {
            var formCollection = await Request.ReadFormAsync();

            if (env.IsDevelopment())    
            { 
                foreach (var item in formCollection.ToList())
                {
                    _logger.LogDebug($"Key: {item.Key}, Value: {item.Value}");
                }
            }

            // var serializeGsob = JsonSerializer.Serialize(formCollection["guestSourceOfBusinesses"]); /////////////////////////////
            _logger.LogDebug($"serialized: {formCollection["guestSourceOfBusinesses"]}");
            
            var gsobsFromRequest = JsonSerializer.Deserialize<IEnumerable<GsobDto>>(formCollection["guestSourceOfBusinesses"]).ToList();
            
            // Assign deserialized json gsobs to fbRerport
            fbReport.GuestSourceOfBusinesses = gsobsFromRequest;
            
            if (env.IsDevelopment())
            {
                foreach (var item in gsobsFromRequest)
                {
                    _logger.LogDebug($"nr of guests: {item.GsobNrOfGuests}, gsob Id: {item.GuestSourceOfBusinessId}");
                }
            }

            /*if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    ModelState.TryAddModelError(error.ErrorMessage, error.ErrorMessage);
                }

                return BadRequest(ModelState);
            }*/

            if (env.IsDevelopment())
            {
                if (fbReport.GuestSourceOfBusinesses.Count > 0)
                {
                    _logger.LogInfo("We have gsobs");

                    foreach (var item in fbReport.GuestSourceOfBusinesses)
                    {
                        _logger.LogInfo("gsobId: " + item.GuestSourceOfBusinessId);
                        _logger.LogInfo("gsobNrOfGuests: " + item.GsobNrOfGuests);
                    }
                }
                else
                {
                    _logger.LogInfo("There are no gsobs");
                }
            }
            
            var file = fbReport.File;
            var fbReportEntity = _mapper.Map<FbReport>(fbReport);

            /*if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                return BadRequest(errors);
            }*/
            if (file != null)
            {
                
            try
            {
                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {
                    var splitFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"').Split('.');
                    var fileName = Guid.NewGuid() + "." + splitFileName[1];
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                        fbReportEntity.ImagePath = dbPath;
                        _logger.LogInfo("entity to be created: " + fbReportEntity.ImagePath);
                        _repository.FbReport.CreateFbReport(fbReportEntity);
                        //await _repository.SaveAsync(); TODO: May we delete? This caused fbreports to be saved before validation- wrongful reports gets saved.
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
            }

            /*********************************** Validate related models **************************************/
            await ValidateWeathers(fbReport, fbReportEntity);
           
            var gsobsFromDb = 
                await _repository.GuestSourceOfBusiness.GetAllGuestSourceOfBusinessesAsync(trackChanges: false);
            int nrOfGsobsFromDb = gsobsFromDb.Count();

            var outletsFromDb = await _repository.Outlet.GetAllOutletsAsync(trackChanges: false);
            int nrOfOutletsFromDb = outletsFromDb.Count();

            var localEventsFromDb = await _repository.LocalEvent.GetAllLocalEventsAsync(trackChanges: false);
            int nrOfLocalEventsFromDb = localEventsFromDb.Count();

            if (fbReport.GuestSourceOfBusinesses != null && fbReport.GuestSourceOfBusinesses.Count > 0)
            {
                ValidateAndAddGsobs(fbReport.GuestSourceOfBusinesses, fbReportEntity, nrOfGsobsFromDb);
            }

            await Validations(fbReport, nrOfOutletsFromDb, nrOfLocalEventsFromDb);
            /*********************************************************************************************************/

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                return BadRequest(errors);
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

        // ************************************************ ValidateAndAddGsobs *********************************************************
        // Validating GuestSourceOfBusinesses and adds them to the fbReportEntity if the validation succeeds.
        // If failure adds an error to ModelState
        private void ValidateAndAddGsobs(IEnumerable<GsobDto> guestSourceOfBusinesses, FbReport fbReportEntity, int nrOfGuestSourcesOfBusinessesFromDb)
        {
            int gsobCounter = 0;

            if (env.IsDevelopment())
            {
                _logger.LogDebug("reportId " + fbReportEntity.Id);
            }

            foreach (var gsob in guestSourceOfBusinesses)
            {
                if (env.IsDevelopment())
                {
                    _logger.LogDebug("gsobId " + gsob.GuestSourceOfBusinessId);
                    _logger.LogDebug("gsobNrofguesst " + gsob.GsobNrOfGuests);
                }

                gsobCounter++;

                // Validating if inputted guestSourceOFBusinessId exists in DB
                if (gsob.GuestSourceOfBusinessId < 1 || gsob.GuestSourceOfBusinessId > nrOfGuestSourcesOfBusinessesFromDb)
                {
                    ModelState.AddModelError("ArgumentOutOfRangeError",
                        $"GuestSourceOFBusiness[{gsobCounter}] must be an integer between 1 and {nrOfGuestSourcesOfBusinessesFromDb}. It's now {gsob.GuestSourceOfBusinessId}");
                }

                if (ModelState.IsValid)
                {
                    var fbReportGuestSourceOfBusiness = new FbReportGuestSourceOfBusiness
                    {
                        FbReportId = fbReportEntity.Id,
                        GuestSourceOfBusinessId = gsob.GuestSourceOfBusinessId,
                        GsobNrOfGuests = gsob.GsobNrOfGuests,
                    };

                    fbReportEntity.FbReportGuestSourceOfBusinesses.Add(fbReportGuestSourceOfBusiness);
                }
            }
        }

        // ************************************************* ValidateWeather **************************************************************
        private async Task ValidateWeathers(FbReportForCreationDto fbReport, FbReport fbReportEntity)
        {
            // Validating if there are any weathers in FbReport
            if (fbReport.Weathers == null || fbReport.Weathers.Count == 0)
            {
                ModelState.AddModelError("ArgumentError", "At least one WeatherId is required.");
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
                        ModelState.AddModelError("ArgumentOutOfRangeError", 
                            $"WeatherId[{weatherCounter}] must be an integer between 1 and {nrOfWeathersFromDb}. It's now {weatherId}");
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

        // ********************************************************** Validations ***************************************************
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
