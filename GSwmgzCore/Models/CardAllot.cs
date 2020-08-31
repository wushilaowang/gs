using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GSwmgzCore.Models
{
    public partial class CardAllot
    {
        [Key]
        public int Id { get; set; }
        public string DistrictCode { get; set; }
        public string DistrictName { get; set; }
        public string CardCode { get; set; }
        public string CardName { get; set; }
        public int CardMaxCount { get; set; }
        public int CardCurCount { get; set; }
        public int AppraisalCode { get; set; }
        public double CardItemScore { get; set; }
        public int diORx { get; set; }
    }
}
