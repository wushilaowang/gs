using System;
using System.Collections.Generic;

namespace GSwmgzCore.Models
{
    public partial class AppraisalResult
    {
        public int Id { get; set; }
        
        public string Beizhu { get; set; }
        public string PicPath { get; set; }
        public DateTime? WriteTime { get; set; }
        public string CardCode { get; set; }
        public string LocalName { get; set; }
        public string Uploader { get; set; }
        public string CardName { get; set; }
        public string DistrictName { get; set; }
        public string DistrictCode { get; set; }
        public int AppraisalCode { get; set; }
        public string Checks { get; set; }
        public string InputName { get; set; }
    }
}
