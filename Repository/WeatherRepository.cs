using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class WeatherRepository : RepositoryBase<Weather>, IWeatherRepository
    {
        public WeatherRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateWeather(Weather weather) => Create(weather);

        public void DeleteWeather(Weather weather) => Delete(weather);

        public void UpdateWeather(Weather weather) => Update(weather);

        public async Task<IEnumerable<Weather>> GetAllTypesOfWeatherAsync(bool trackChanges) =>
            await FindAll(trackChanges)
            .OrderBy(c => c.Id)
            .ToListAsync();

        public async Task<Weather> GetTypeOfWeatherAsync(int id, bool trackChanges) =>
            await FindByCondition(c => c.Id.Equals(id), trackChanges)
            .SingleOrDefaultAsync();                      
    }
}
