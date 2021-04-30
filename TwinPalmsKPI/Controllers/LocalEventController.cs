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
    public class LocalEventController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public LocalEventController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets a list of all LocalEvents
        /// </summary>
        // TODO Add Authorize
        [HttpGet(Name = "GetLocalEvents")/*, Authorize(Roles = "Administrator, Manager")*/] 
        public async Task<IActionResult> GetLocalEvents()
        {
            var localEvents = await _repository.LocalEvent.GetAllLocalEventsAsync(trackChanges: false);
            var localEventsDto = _mapper.Map<IEnumerable<LocalEventDto>>(localEvents);
            return Ok(localEventsDto);
        }

        /// <summary>
        /// Gets a single LocalEvent by ID
        /// </summary>
        [HttpGet("{id}", Name = "LocalEventById")]

        [Authorize(Roles = "Admin, SuperAdmin")]
        public async Task<IActionResult> GetLocalEvent(/*int companyId,*/ int id) // TODO Check if parameter has any use, otherwise delete
        {
            var localEvent = await _repository.LocalEvent.GetLocalEventAsync(id, trackChanges: false);
            if (localEvent == null)
            {
                _logger.LogInfo($"LocalEvent with id {id} doesn't exist in the database.");
                return NotFound();
            }
            var localEventDto = _mapper.Map<LocalEventDto>(localEvent);
            return Ok(localEventDto);
        }

        /// <summary>
        /// Creates a new LocalEvent
        /// </summary>
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateLocalEvent([FromBody] LocalEventForCreationDto localEvent)
        {
            var localEventEntity = _mapper.Map<LocalEvent>(localEvent);
            _repository.LocalEvent.CreateLocalEvent(localEventEntity);
            await _repository.SaveAsync();
            var localEventToReturn = _mapper.Map<LocalEventDto>(localEventEntity);
            return CreatedAtRoute("LocalEventById", new { id = localEventToReturn.Id }, localEventToReturn);
        }

        /// <summary>
        /// Deletes a LocalEvent by ID
        /// </summary>
        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidateLocalEventExistsAttribute))]
        public async Task<IActionResult> DeleteLocalEvent(int id)
        {
            var localEvent = HttpContext.Items["localEvent"] as LocalEvent;
            _repository.LocalEvent.DeleteLocalEvent(localEvent);
            await _repository.SaveAsync();
            return NoContent();
        }

        /// <summary>
        /// Updates a LocalEvent by ID
        /// </summary>
        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateLocalEventExistsAttribute))]
        public async Task<IActionResult> UpdateLocalEvent(int id, [FromBody] LocalEventForUpdateDto localEvent)
        {
            var localEventEntity = HttpContext.Items["localEvent"] as LocalEvent;
            _repository.LocalEvent.UpdateLocalEvent(localEventEntity);
            _mapper.Map(localEvent, localEventEntity);
            await _repository.SaveAsync();
            var localEventToReturn = _mapper.Map<LocalEventDto>(localEventEntity);
            return CreatedAtRoute("LocalEventById", new { id = localEventToReturn.Id }, localEventToReturn);
        }
    }
}
