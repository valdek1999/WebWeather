using Microsoft.AspNetCore.Http;
using NPOI.SS.UserModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebWeather.DataAccess;
using WebWeather.DataAccess.Models;
using WebWeather.Extensions;
namespace WebWeather.Services
{
    public class WeatherService
    {
        private readonly Repository<Weather, int> _dataWeatherRepository;
        public ExcelWeatherHandler ExcelWeatherHandler { get; }
        public WeatherService(Repository<Weather,int> dataWeatherRepository)
        {
            _dataWeatherRepository = dataWeatherRepository;
            ExcelWeatherHandler = new ExcelWeatherHandler();
        }

        public async Task<bool> LoadExcelWithWeatherToDb(IFormFileCollection files)
        {
            foreach (var excelBook in ExcelTransformer.TransformFilesToExcel(files))
            {
                foreach (var sheet in excelBook)
                {
                    foreach (var weatherData in ExcelWeatherHandler.GetWeatherDataEnumerator(sheet))
                    {
                        await _dataWeatherRepository.AddAsync(weatherData);
                    }
                    if (ExcelWeatherHandler.HasSomeError)
                    {
                        return false;
                    }
                    else
                    {
                        await _dataWeatherRepository.SaveAsync();
                    }
                }
            }
            return true;
        }
    }
}
