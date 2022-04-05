﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebWeather.Data.WeatherProvider;
using WebWeather.Models;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using WebWeather.Extensions;
using Microsoft.EntityFrameworkCore;

namespace WebWeather.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly DataWeatherContext _dataWeatherContext;

        private readonly IWebHostEnvironment _appEnvironment;
        public HomeController(ILogger<HomeController> logger, DataWeatherContext dataWeatherContext, IWebHostEnvironment appEnvironment)
        {
            _logger = logger;
            _dataWeatherContext = dataWeatherContext;
            _appEnvironment = appEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Weathers(int? year, int? month, int page = 1,
            SortState sortOrder = SortState.DateAsc)
        {
            int pageSize = 10;

            //фильтрация
            IQueryable<Weather> weather = _dataWeatherContext.Weather;

            if (year != null)
            {
                weather = weather.Where(w => w.Date.Year == year);
            }
            if (month != null)
            {
                weather = weather.Where(w => w.Date.Month == month);
            }

            //сортировка
            switch (sortOrder)
            {
                case SortState.DateAsc:
                    weather = weather.OrderBy(w => w.Date).ThenBy(w => w.Time);
                    break;
                case SortState.DateDesc:
                    weather = weather.OrderByDescending(w => w.Date).ThenByDescending(w => w.Time);
                    break;
            }

            // пагинация
            var count = await weather.CountAsync();
            var items = await weather.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            // формируем модель представления
            IndexViewModel viewModel = new IndexViewModel
            {
                PageViewModel = new PageViewModel(count, page, pageSize),
                SortViewModel = new SortViewModel(sortOrder),
                FilterViewModel = new FilterViewModel(_dataWeatherContext.Weather.ToList(), month, year),
                Weathers = items
            };
            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<IActionResult> AddFile(IFormFileCollection uploads)
        {
            foreach (var uploadedFile in uploads)
            {
                using (var stream = uploadedFile.OpenReadStream())
                {
                    var excelBook = new XSSFWorkbook(stream);

                    foreach(var sheet in excelBook)
                    {
                        var sheetValided = sheet.CheckValidOfSheet();
                        if (sheetValided)
                        {
                            foreach(var weatherData in sheet.GetWeatherDataEnumerator())
                            {
                                await _dataWeatherContext.AddAsync(weatherData);
                            }
                        }
                        await _dataWeatherContext.SaveChangesAsync();
                    }
                }
            }

            return RedirectToAction("Index");
        }
    }
}
