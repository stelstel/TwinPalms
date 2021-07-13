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
using System.Security.Claims;

namespace TwinPalmsKPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuestSourceOfBusinessController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public GuestSourceOfBusinessController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets a list of all GuestSourceOfBusinesses
        /// </summary>
        [HttpGet(Name = "GetGuestSourceOfBusinesses"), Authorize] 
        public async Task<IActionResult> GetGuestSourceOfBusinesses()
        {
            var guestSourceOfBusinesses = await _repository.GuestSourceOfBusiness.GetAllGuestSourceOfBusinessesAsync(trackChanges: false);
            var guestSourceOfBusinesssDto = _mapper.Map<IEnumerable<GuestSourceOfBusinessDto>>(guestSourceOfBusinesses);
            return Ok(guestSourceOfBusinesssDto);
        }

        /// <summary>
        /// Gets a single GuestSourceOfBusiness by ID
        /// </summary>
        [HttpGet("{id}", Name = "GuestSourceOfBusinessById"), Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> GetGuestSourceOfBusiness(int id)
        {
            var guestSourceOfBusiness = await _repository.GuestSourceOfBusiness.GetGuestSourceOfBusinessAsync(id, trackChanges: false);
            if (guestSourceOfBusiness == null)
            {
                _logger.LogInfo($"GuestSourceOfBusiness with id {id} doesn't exist in the database.");
                return NotFound();
            }
            var guestSourceOfBusinessDto = _mapper.Map<GuestSourceOfBusinessDto>(guestSourceOfBusiness);
            return Ok(guestSourceOfBusinessDto);
        }

        /// <summary>
        /// Creates a new GuestSourceOfBusiness
        /// </summary>
        [HttpPost, Authorize(Roles = "SuperAdmin")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateGuestSourceOfBusiness([FromBody] GuestSourceOfBusinessForCreationDto guestSourceOfBusiness)
        {
            var guestSourceOfBusinessEntity = _mapper.Map<GuestSourceOfBusiness>(guestSourceOfBusiness);
            _repository.GuestSourceOfBusiness.CreateGuestSourceOfBusiness(guestSourceOfBusinessEntity);
            await _repository.SaveAsync();
            var guestSourceOfBusinessToReturn = _mapper.Map<GuestSourceOfBusinessDto>(guestSourceOfBusinessEntity);
            return CreatedAtRoute("GuestSourceOfBusinessById", new { id = guestSourceOfBusinessToReturn.Id }, guestSourceOfBusinessToReturn);
        }

        /// <summary>
        /// Deletes a GuestSourceOfBusiness by ID
        /// </summary>
        [HttpDelete("{id}"), Authorize(Roles = "SuperAdmin")]
        [ServiceFilter(typeof(ValidateGuestSourceOfBusinessExistsAttribute))]
        public async Task<IActionResult> DeleteGuestSourceOfBusiness(int id)
        {
            var guestSourceOfBusiness = HttpContext.Items["guestSourceOfBusiness"] as GuestSourceOfBusiness;
            _repository.GuestSourceOfBusiness.DeleteGuestSourceOfBusiness(guestSourceOfBusiness);
            await _repository.SaveAsync();
            return NoContent();
        }

        /// <summary>
        /// Updates a GuestSourceOfBusiness by ID
        /// </summary>
        [HttpPut("{id}"), Authorize(Roles = "SuperAdmin")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateGuestSourceOfBusinessExistsAttribute))]
        public async Task<IActionResult> UpdateGuestSourceOfBusiness(int id, [FromBody] GuestSourceOfBusinessForUpdateDto guestSourceOfBusiness)
        {
            var guestSourceOfBusinessEntity = HttpContext.Items["guestSourceOfBusiness"] as GuestSourceOfBusiness;
            _repository.GuestSourceOfBusiness.UpdateGuestSourceOfBusiness(guestSourceOfBusinessEntity);
            _mapper.Map(guestSourceOfBusiness, guestSourceOfBusinessEntity);
            await _repository.SaveAsync();
            var guestSourceOfBusinessToReturn = _mapper.Map<GuestSourceOfBusinessDto>(guestSourceOfBusinessEntity);
            return CreatedAtRoute("GuestSourceOfBusinessById", new { id = guestSourceOfBusinessToReturn.Id }, guestSourceOfBusinessToReturn);
        }
    }
}
