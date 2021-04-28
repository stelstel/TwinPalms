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
    public class WeatherController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public WeatherController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        /// <summary>
        /// Gets a list of all weathertypes
        /// </summary>
        // TODO Add Authorize
        [HttpGet(Name = "GetWeatherTypes")/*, Authorize(Roles = "Administrator, Manager")*/]
        public async Task<IActionResult> GetAllTypesOfWeather()
        {
            var weathertypes = await _repository.Weather.GetAllTypesOfWeatherAsync(trackChanges: false);
            var weatherDto = _mapper.Map<IEnumerable<WeatherDto>>(weathertypes);
            return Ok(weatherDto);
        }
        /// <summary>
        /// Gets a single weathertype by ID
        /// </summary>
        [HttpGet("{id}", Name = "WeathertypeById")]
        public async Task<IActionResult> GetWeatherType(int id)
        {
            var weather = await _repository.Weather.GetTypeOfWeatherAsync(id, trackChanges: false);
            if (weather == null)
            {
                _logger.LogInfo($"Weathertype with id {id} doesn't exist in the database.");
                return NotFound();
            }
            var weatherDto = _mapper.Map<WeatherDto>(weather);
            return Ok(weatherDto);
        }

        /// <summary>
        /// Creates a new type of weather
        /// </summary>
        [HttpPost, Authorize(Roles = "Administrator, Manager")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateWeather([FromBody] WeatherForCreationDto weather)
        {
            _logger.LogInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var weatherEntity = _mapper.Map<Weather>(weather);
            _repository.Weather.CreateWeather(weatherEntity);
            await _repository.SaveAsync();
            var weatherToReturn = _mapper.Map<WeatherDto>(weatherEntity);
            return CreatedAtRoute("WeathertypeById", new { id = weatherToReturn.Id }, weatherToReturn);
        }

        /// <summary>
        /// Deletes a Weathertype by ID
        /// </summary>
        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidateWeatherExistsAttribute))]
        public async Task<IActionResult> DeleteWeather(int id)
        {
            var weather = HttpContext.Items["weather"] as Weather;
            _repository.Weather.DeleteWeather(weather);
            await _repository.SaveAsync();
            return NoContent();
        }

        /// <summary>
        /// Updates a weathertype by ID
        /// </summary>
        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateWeatherExistsAttribute))]
        public async Task<IActionResult> UpdateWeather(int id, [FromBody] WeatherForUpdateDto weather)
        {
            var weatherEntity = HttpContext.Items["weather"] as Weather;
            _repository.Weather.UpdateWeather(weatherEntity);
            _mapper.Map(weather, weatherEntity);
            await _repository.SaveAsync();
            var weatherToReturn = _mapper.Map<WeatherDto>(weatherEntity);
            return CreatedAtRoute("WeathertypeById", new { id = weatherToReturn.Id }, weatherToReturn);
        }
    }
}
