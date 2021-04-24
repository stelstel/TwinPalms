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
    public class HotelsController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public HotelsController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets a list of all Hotels
        /// </summary>
        // TODO Add Authorize
        [HttpGet(Name = "GetHotels")/*, Authorize(Roles = "Administrator, Manager")*/] 
        public async Task<IActionResult> GetHotels()
        {
            var hotels = await _repository.Hotel.GetAllHotelsAsync(trackChanges: false);
            var hotelsDto = _mapper.Map<IEnumerable<HotelDto>>(hotels);
            return Ok(hotelsDto);
        }

        /// <summary>
        /// Gets a single Hotel by ID
        /// </summary>
        [HttpGet("{id}", Name = "HotelById")]
        public async Task<IActionResult> GetHotel(int hotelId, int id)
        {
            var hotel = await _repository.Hotel.GetHotelAsync(id, trackChanges: false);
            if (hotel == null)
            {
                _logger.LogInfo($"Hotels with id {id} doesn't exist in the database.");
                return NotFound();
            }
            var hotelDto = _mapper.Map<HotelDto>(hotel);
            return Ok(hotelDto);
        }

        /// <summary>
        /// Creates a new Hotel
        /// </summary>
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateHotel([FromBody] HotelForCreationDto hotel)
        {
            var hotelEntity = _mapper.Map<Hotel>(hotel);
            _repository.Hotel.CreateHotel(hotelEntity);
            await _repository.SaveAsync();
            var hotelToReturn = _mapper.Map<HotelDto>(hotelEntity);
            return CreatedAtRoute("HotelById", new { id = hotelToReturn.Id }, hotelToReturn);
        }

        /// <summary>
        /// Deletes a Hotel by ID
        /// </summary>
        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidateHotelExistsAttribute))]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            var hotel = HttpContext.Items["hotel"] as Hotel;
            _repository.Hotel.DeleteHotel(hotel);
            await _repository.SaveAsync();
            return NoContent();
        }

        /// <summary>
        /// Updates a Hotel by ID
        /// </summary>
        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateHotelExistsAttribute))]
        public async Task<IActionResult> UpdateHotel(int id, [FromBody] HotelForUpdateDto hotel)
        {
            var hotelEntity = HttpContext.Items["hotel"] as Hotel;
            _repository.Hotel.UpdateHotel(hotelEntity);
            _mapper.Map(hotel, hotelEntity);
            await _repository.SaveAsync();
            var hotelToReturn = _mapper.Map<HotelDto>(hotelEntity);
            return CreatedAtRoute("HotelById", new { id = hotelToReturn.Id }, hotelToReturn);
        }
    }
}
