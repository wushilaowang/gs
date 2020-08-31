using System;
using System.Collections.Generic;

namespace GSwmgzCore.Models
{
    public partial class ObSysUser
    {
        public int Id { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string RealName { get; set; }
        public string DistrictCode { get; set; }
        public string DistrictName { get; set; }
        public int Type { get; set; }
        public int AppraisalCode { get; set; }
        public int diORx { get; set; }
    }
}
