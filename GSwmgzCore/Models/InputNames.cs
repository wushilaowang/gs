using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GSwmgzCore.Models
{
    public class InputNames
    {
        [Key]
        public int Id { get; set; }
        public string ProvinceName { get; set; }
        public string CityName { get; set; }
        public string DistrictName { get; set; }
        public string VillageName { get; set; }
        public int AppraisalCode { get; set; }
    }
}
