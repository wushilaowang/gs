using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GSwmgzCore.Models
{
    public partial class HardwareAuthorization
    {
        [Key]
        public int Id { get; set; }
        public string HardwareCode { get; set; }
        public string CustomName { get; set; }
        public string CustomPhone { get; set; }
        public int LicenseState { get; set; }
    }
}
