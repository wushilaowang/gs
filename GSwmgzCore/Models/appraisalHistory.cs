using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GSwmgzCore.Models
{
    public partial class AppraisalHistory
    {
        public int Id { get; set; }
        public DateTime? Starttime { get; set; }
        public DateTime? Endtime { get; set; }
        public int AppraisalCode { get; set; }
        public string AppraisalName { get; set; }
        public string AppraisalType { get; set; }
        public double? CardScore { get; set; }
        public double? WJScore { get; set; }
    }
}
