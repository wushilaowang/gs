using System;
using System.Collections.Generic;

namespace GSwmgzCore.Models
{
    public partial class ProvinceTb
    {
        public ProvinceTb()
        {
            CityTb = new HashSet<CityTb>();
        }

        public string ProvinceName { get; set; }
        public string ProvinceCode { get; set; }

        public virtual ICollection<CityTb> CityTb { get; set; }
    }
}
