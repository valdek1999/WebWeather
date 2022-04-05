using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using WebWeather.Data.WeatherProvider;

namespace WebWeather.Extensions
{
    /// <summary>
    /// Типы ячеек в Excel таблице.
    /// </summary>
    public enum WeatherCell
    {
        Date,
        Time,
        AirTemperature,
        AirHumidity,
        DewPointTemperature,
        AtmosphericPressure,
        WindDirection,
        WindSpeed,
        Cloudiness,
        LowerCloudinessLimit,
        HorizontalVisibility,
        WeatherEvent
    }
    public static class WeatherServiceHelper
    {
        /// <summary>
        /// Получить иттератор данных о погоде по листу
        /// </summary>
        /// <param name="sheet"></param>
        /// <returns></returns>
        public static IEnumerable<Weather> GetWeatherDataEnumerator(this ISheet sheet)
        {
            var startRow = sheet.SearchOfStartingRowWithData(out var sheetIsValid);
            for (int i = startRow; i < sheet.LastRowNum; i++)
            {
                var row = sheet.GetRow(i);
                var weather = GetWeatherData(row);
                yield return weather;
            }
        }
        /// <summary>
        /// Получить модель данных о погоде по строке листа
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public static Weather GetWeatherData(IRow row)
        {
            var date = row.GetCellFromRowByType(WeatherCell.Date).GetDateFromCell().Value;
            var time = row.GetCellFromRowByType(WeatherCell.Time).GetTimeFromCell().Value;
            return new Weather
            {
                Date = date,
                Time = time,
                AirTemperature = row.GetCellFromRowByType(WeatherCell.AirTemperature).GetFloatFromCell().Value.GetValueOrDefault(),
                AirHumidity = row.GetCellFromRowByType(WeatherCell.AirHumidity).GetIntFromCell().Value.GetValueOrDefault(),
                DewPointTemperature = row.GetCellFromRowByType(WeatherCell.DewPointTemperature).GetFloatFromCell().Value.GetValueOrDefault(),
                AtmosphericPressure = row.GetCellFromRowByType(WeatherCell.AtmosphericPressure).GetIntFromCell().Value.GetValueOrDefault(),
                WindDirection = row.GetCellFromRowByType(WeatherCell.WindDirection)?.GetStringFromCell().Value,
                WindSpeed = row.GetCellFromRowByType(WeatherCell.WindSpeed)?.GetIntFromCell().Value,
                Cloudiness = row.GetCellFromRowByType(WeatherCell.Cloudiness)?.GetIntFromCell().Value,
                LowerCloudinessLimit = row.GetCellFromRowByType(WeatherCell.LowerCloudinessLimit)?.GetFloatFromCell().Value,
                HorizontalVisibility = row.GetCellFromRowByType(WeatherCell.HorizontalVisibility)?.GetStringFromCell().Value,
                WeatherEvent = row.GetCellFromRowByType(WeatherCell.WeatherEvent)?.GetStringFromCell().Value,
            };
        }
        #region Валидация объектов Sheet, row, cell
        /// <summary>
        /// Проверка на валидность всего листа
        /// </summary>
        /// <param name="sheet"></param>
        /// <returns></returns>
        public static bool CheckValidOfSheet(this ISheet sheet)
        {
            var startRow = sheet.SearchOfStartingRowWithData(out var sheetIsValid);

            for (int i = startRow; i < sheet.LastRowNum; i++)
            {
                var row = sheet.GetRow(i);

                if (CheckValidOfRow(row) is false)
                {
                    CheckValidOfRow(row);
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Поиск строки от которой надо начать парсинг данных
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="sheetIsValid"></param>
        /// <returns></returns>
        public static int SearchOfStartingRowWithData(this ISheet sheet, out bool sheetIsValid)
        {
            sheetIsValid = false;
            for (int i = 0; i < sheet.LastRowNum; i++)
            {
                var row = sheet.GetRow(i);

                if (CheckValidOfRow(row))
                {
                    sheetIsValid = true;
                    return i;
                }
            }
            return 0;
        }
        /// <summary>
        /// Проверка на валидность строки
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public static bool CheckValidOfRow(IRow row)
        {
            var cellTypes = Enum.GetValues<WeatherCell>();
            foreach (var cellType in cellTypes)
            {
                if (row.Cells.Count <= (int)cellType)
                {
                    return true;
                }
                var cell = row.GetCellFromRowByType(cellType);
                if (CheckCell(cell, cellType) is false)
                {
                    return false;
                }
            }
            return true;
        }


        /// <summary>
        /// Проверка на валидность ячейки
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="cellType"></param>
        /// <returns></returns>
        public static bool CheckCell(ICell cell, WeatherCell cellType)
        {
            switch (cellType)
            {
                case WeatherCell.Date:
                    return DateTime.TryParseExact(cell.ToString().Trim(), "dd.MM.yyyy", new CultureInfo("ru-Ru"), DateTimeStyles.AssumeLocal, out _);
                case WeatherCell.Time:
                    return DateTime.TryParseExact(cell.ToString().Trim(), "H:m", new CultureInfo("ru-Ru"), DateTimeStyles.AssumeLocal, out var time);
                case WeatherCell.AirTemperature:
                case WeatherCell.DewPointTemperature:
                case WeatherCell.AirHumidity:
                    return float.TryParse(cell.ToString(), out _);
                case WeatherCell.AtmosphericPressure:
                    return int.TryParse(cell.ToString(), out _);
                case WeatherCell.WindSpeed:
                case WeatherCell.Cloudiness:
                    if (string.IsNullOrEmpty(cell.ToString().Replace(" ", "")) || cell.CellType == CellType.Blank)
                    {
                        return true;
                    }
                    else
                    {
                        return int.TryParse(cell.ToString(), out _);
                    }
                case WeatherCell.LowerCloudinessLimit:
                    if (string.IsNullOrEmpty(cell.ToString().Replace(" ", "")) || cell.CellType == CellType.Blank)
                    {
                        return true;
                    }
                    else
                    {
                        return float.TryParse(cell.ToString(), out _);
                    }
                case WeatherCell.WindDirection:
                case WeatherCell.WeatherEvent:
                case WeatherCell.HorizontalVisibility:
                    return true;
                default:
                    return true;
            }
        }

        #endregion

        #region Парсеры ячеек в нужный тип
        public static (bool IsParsed,DateTime Value) GetDateFromCell(this ICell cell)
        {            
            return (DateTime.TryParseExact(cell.ToString().Trim(), "dd.MM.yyyy", new CultureInfo("ru-Ru"), DateTimeStyles.AssumeLocal, out var value), value);
        }
        public static (bool IsParsed, DateTime Value) GetTimeFromCell(this ICell cell)
        {
            var isParsed = DateTime.TryParseExact(cell.ToString().Trim(), "HH:mm", new CultureInfo("ru-Ru"), DateTimeStyles.AssumeLocal, out var value);
            return (isParsed, value);
        }
        public static (bool isParsed, float? Value) GetFloatFromCell(this ICell cell)
        {
            var isParsed = float.TryParse(cell.ToString().Trim(), out float value);
            if (isParsed)
            {
                return (isParsed, value);
            }
            else
            {
                return (isParsed, null);
            }
        }
        public static (bool isParsed, int? Value) GetIntFromCell(this ICell cell)
        {
            var isParsed = int.TryParse(cell.ToString().Trim(), out int value);
            if (isParsed)
            {
                return (isParsed, value);
            }
            else
            {
                return (isParsed, null);
            }
        }
        public static (bool isParsed, string Value) GetStringFromCell(this ICell cell)
        {
            return (true,cell.ToString().Trim());
        }

        #endregion

        /// <summary>
        /// Получение ячейки из строки по типу ячейки
        /// </summary>
        /// <param name="row"></param>
        /// <param name="weatherCellType"></param>
        /// <returns></returns>
        public static ICell GetCellFromRowByType(this IRow row, WeatherCell weatherCellType)
        {
            return row.GetCell((int)weatherCellType);
        }
    }
}
