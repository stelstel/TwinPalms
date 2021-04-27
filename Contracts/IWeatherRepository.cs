using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IWeatherRepository
    {
        Task<IEnumerable<Weather>> GetAllTypesOfWeatherAsync(bool trackChanges);
        Task<Weather> GetTypeOfWeatherAsync(int id, bool trackChanges);

        void CreateWeather(Weather weather);
        void DeleteWeather(Weather weather);
        void UpdateWeather(Weather weathery);
    }
}
