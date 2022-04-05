using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace WebWeather.Services.Interfaces
{
    public interface IExcelTransformer
    {
        public IEnumerable<IWorkbook> TransformFilesToExcelFormat(IFormFileCollection files);
    }
}
