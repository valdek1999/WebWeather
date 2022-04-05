using System.Collections.Generic;
using WebWeather.DataAccess.Models;

namespace WebWeather.Models
{
    public class IndexViewModel
    {
        public IEnumerable<Weather> Weathers { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public FilterViewModel FilterViewModel { get; set; }
        public SortViewModel SortViewModel { get; set; }
    }
}
