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
    public class CruiseCompaniesController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public CruiseCompaniesController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets a list of all companies
        /// </summary>
        // TODO Add Authorize
        [HttpGet(Name = "GetCruiseCompanies")/*, Authorize(Roles = "Administrator, Manager")*/] 
        public async Task<IActionResult> GetCruiseCompanies()
        {
            var cruiseCompanies = await _repository.CruiseCompany.GetAllCruiseCompaniesAsync(trackChanges: false);
            var CruiseCompaniesDto = _mapper.Map<IEnumerable<CruiseCompanyDto>>(cruiseCompanies);
            return Ok(CruiseCompaniesDto);
        }

        /// <summary>
        /// Gets a single cruiseCompany by ID
        /// </summary>
        [HttpGet("{id}", Name = "CruiseCompanyById")]
        public async Task<IActionResult> GetCruiseCompany(/*int cruiseCompanyId,*/ int id)
        {
            var cruiseCompany = await _repository.CruiseCompany.GetCruiseCompanyAsync(id, trackChanges: false);
            if (cruiseCompany == null)
            {
                _logger.LogInfo($"CruiseCompany with id {id} doesn't exist in the database.");
                return NotFound();
            }
            var cruiseCompanyDto = _mapper.Map<CruiseCompanyDto>(cruiseCompany);
            return Ok(cruiseCompanyDto);
        }

        /// <summary>
        /// Creates a new cruiseCompany
        /// </summary>
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateCruiseCompany([FromBody] CruiseCompanyForCreationDto cruiseCompany)
        {
            var cruiseCompanyEntity = _mapper.Map<CruiseCompany>(cruiseCompany);
            _repository.CruiseCompany.CreateCruiseCompany(cruiseCompanyEntity);
            await _repository.SaveAsync();
            var cruiseCompanyToReturn = _mapper.Map<CruiseCompanyDto>(cruiseCompanyEntity);
            return CreatedAtRoute("CruiseCompanyById", new { id = cruiseCompanyToReturn.Id }, cruiseCompanyToReturn);
        }

        /// <summary>
        /// Deletes a cruiseCompany by ID
        /// </summary>
        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidateCruiseCompanyExistsAttribute))]
        public async Task<IActionResult> DeleteCruiseCompany(int id)
        {
            var cruiseCompany = HttpContext.Items["cruiseCompany"] as CruiseCompany;
            _repository.CruiseCompany.DeleteCruiseCompany(cruiseCompany);
            await _repository.SaveAsync();
            return NoContent();
        }

        /// <summary>
        /// Updates a cruiseCompany by ID
        /// </summary>
        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateCruiseCompanyExistsAttribute))]
        public async Task<IActionResult> UpdateCruiseCompany(int id, [FromBody] CruiseCompanyForUpdateDto cruiseCompany)
        {
            var cruiseCompanyEntity = HttpContext.Items["cruiseCompany"] as CruiseCompany;
            _repository.CruiseCompany.UpdateCruiseCompany(cruiseCompanyEntity);
            _mapper.Map(cruiseCompany, cruiseCompanyEntity);
            await _repository.SaveAsync();
            var cruiseCompanyToReturn = _mapper.Map<CruiseCompanyDto>(cruiseCompanyEntity);
            return CreatedAtRoute("CruiseCompanyById", new { id = cruiseCompanyToReturn.Id }, cruiseCompanyToReturn);
        }
    }
}
