using Microsoft.AspNetCore.Http;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Collections.Generic;
using WebWeather.Services.Interfaces;

namespace WebWeather.Services
{
    public class ExcelTransformer : IExcelTransformer
    {
        IEnumerable<IWorkbook> IExcelTransformer.TransformFilesToExcelFormat(IFormFileCollection files)
        {
            foreach (var file in files)
            {
                using (var stream = file.OpenReadStream())
                {
                    var excelBook = new XSSFWorkbook(stream);
                    yield return excelBook;
                }
            }
        }
    }
}
