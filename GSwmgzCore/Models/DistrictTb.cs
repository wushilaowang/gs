using System;
using System.Collections.Generic;

namespace GSwmgzCore.Models
{
    public partial class DistrictTb
    {
        public string DistrictName { get; set; }
        public string DistrictCode { get; set; }
        public string OwenerCityName { get; set; }
        public string OwenerCityCode { get; set; }
        public int DistrictId { get; set; }

        public virtual CityTb OwenerCityCodeNavigation { get; set; }
    }
}
