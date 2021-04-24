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
    public class CruiseShipsController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public CruiseShipsController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets a list of all companies
        /// </summary>
        // TODO Add Authorize
        [HttpGet(Name = "GetCruiseShips")/*, Authorize(Roles = "Administrator, Manager")*/] 
        public async Task<IActionResult> GetCruiseShips()
        {
            var companies = await _repository.CruiseShip.GetAllCruiseShipsAsync(trackChanges: false);
            var companiesDto = _mapper.Map<IEnumerable<CruiseShipDto>>(companies);
            return Ok(companiesDto);
        }

        /// <summary>
        /// Gets a single cruiseShip by ID
        /// </summary>
        [HttpGet("{id}", Name = "CruiseShipById")]
        public async Task<IActionResult> GetCruiseShip(int cruiseShipId, int id)
        {
            var cruiseShip = await _repository.CruiseShip.GetCruiseShipAsync(id, trackChanges: false);
            if (cruiseShip == null)
            {
                _logger.LogInfo($"CruiseShip with id {id} doesn't exist in the database.");
                return NotFound();
            }
            var cruiseShipDto = _mapper.Map<CruiseShipDto>(cruiseShip);
            return Ok(cruiseShipDto);
        }

        /// <summary>
        /// Creates a new cruiseShip
        /// </summary>
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateCruiseShip([FromBody] CruiseShipForCreationDto cruiseShip)
        {
            var cruiseShipEntity = _mapper.Map<CruiseShip>(cruiseShip);
            _repository.CruiseShip.CreateCruiseShip(cruiseShipEntity);
            await _repository.SaveAsync();
            var cruiseShipToReturn = _mapper.Map<CruiseShipDto>(cruiseShipEntity);
            return CreatedAtRoute("CruiseShipById", new { id = cruiseShipToReturn.Id }, cruiseShipToReturn);
        }

        /// <summary>
        /// Deletes a cruiseShip by ID
        /// </summary>
        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidateCruiseShipExistsAttribute))]
        public async Task<IActionResult> DeleteCruiseShip(int id)
        {
            var cruiseShip = HttpContext.Items["cruiseShip"] as CruiseShip;
            _repository.CruiseShip.DeleteCruiseShip(cruiseShip);
            await _repository.SaveAsync();
            return NoContent();
        }

        /// <summary>
        /// Updates a cruiseShip by ID
        /// </summary>
        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateCruiseShipExistsAttribute))]
        public async Task<IActionResult> UpdateCruiseShip(int id, [FromBody] CruiseShipForUpdateDto cruiseShip)
        {
            var cruiseShipEntity = HttpContext.Items["cruiseShip"] as CruiseShip;
            _repository.CruiseShip.UpdateCruiseShip(cruiseShipEntity);
            _mapper.Map(cruiseShip, cruiseShipEntity);
            await _repository.SaveAsync();
            var cruiseShipToReturn = _mapper.Map<CruiseShipDto>(cruiseShipEntity);
            return CreatedAtRoute("CruiseShipById", new { id = cruiseShipToReturn.Id }, cruiseShipToReturn);
        }
    }
}
