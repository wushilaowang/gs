using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GSwmgzCore.Models
{
    public partial class DistrictIndividualScores
    {
        public int Id { get; set; }
        public string districtCode { get; set; }
        public string districtName { get; set; }
        public int AppraisalCode { get; set; }
        public double IndividualScore { get; set; }
    }
}
