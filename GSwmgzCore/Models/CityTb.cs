using System;
using System.Collections.Generic;

namespace GSwmgzCore.Models
{
    public partial class CityTb
    {
        public CityTb()
        {
            DistrictTb = new HashSet<DistrictTb>();
        }

        public string CityName { get; set; }
        public string CityCode { get; set; }
        public string OwenerProvinceName { get; set; }
        public string OwenerProvinceCode { get; set; }
        public int CityId { get; set; }

        public virtual ProvinceTb OwenerProvinceCodeNavigation { get; set; }
        public virtual ICollection<DistrictTb> DistrictTb { get; set; }
    }
}
