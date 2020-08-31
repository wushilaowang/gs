using System;
using System.Collections.Generic;

namespace GSwmgzCore.Models
{
    public partial class ObWenJuan
    {
        public int Id { get; set; }
        
        public string Beizhu { get; set; }
        public string Fwdd { get; set; }
        public string DistrictName { get; set; }
        public string PlaceCode { get; set; }
        public int? CardNumber { get; set; }
        public string LocalName { get; set; }
        public string Uploader { get; set; }
        public string Time { get; set; }
        public DateTime? WriteTime { get; set; }
        public string Checks { get; set; }
    }
}
