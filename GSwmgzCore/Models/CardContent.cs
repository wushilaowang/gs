using System;
using System.Collections.Generic;

namespace GSwmgzCore.Models
{
    public partial class CardContent
    {
        public int Id { get; set; }
        public int? Cixu { get; set; }
        public string CardCode { get; set; }
        public string Item { get; set; }
        public double Score { get; set; }
        public double K50 { get; set; }
        public double K51 { get; set; }
        public double K52 { get; set; }
        public double K53 { get; set; }
        public double K54 { get; set; }
        public double K55 { get; set; }
        public string Beizhu { get; set; }
        public int? Multisign { get; set; }
        public string CardName { get; set; }
        public int AppraisalCode { get; set; }
        public int diORx { get; set; }
    }
}
