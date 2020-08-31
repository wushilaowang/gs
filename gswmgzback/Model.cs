using System;

namespace gswmgzback
{
    public class AlldistrictContent
    {

        public class AlldistrictRootobject
        {
            public string errorMsg { get; set; }
            public string msgCode { get; set; }
            public AlldistrictDatum[] data { get; set; }
        }

        public class AlldistrictDatum
        {
            public string districtName { get; set; }
            public string districtCode { get; set; }
            public string owenerCityName { get; set; }
            public string owenerCityCode { get; set; }
            public string districtId { get; set; }
            public string owenerCityCodeNavigation { get; set; }
            
        }
    }


    public class AllAppraisalCode
    {
        public string errorMsg { get; set; }
        public string msgCode { get; set; }
        public AppraisalCodesDatum[] data { get; set; }
    }

    public class AppraisalCodesDatum
    {
        public int Id { get; set; }
        public DateTime? Starttime { get; set; }
        public DateTime? Endtime { get; set; }
        public int AppraisalCode { get; set; }
        public string AppraisalName { get; set; }
        public string AppraisalType { get; set; }
    }
    //
    public class AlldistrictInfo
    {
        public string errorMsg { get; set; }
        public string msgCode { get; set; }
        public districtInfoDatum[] data { get; set; }
    }

    public class districtInfoDatum
    {
        public string districtCodet { get; set; }
        public string districtNamet { get; set; }
    }


    public class AppraisalResultInfo
    {
        public string errorMsg { get; set; }
        public string msgCode { get; set; }
        public AppraisalResultDatum[] data { get; set; }
    }

    public class AppraisalResultDatum
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
   

    //统计分数用
    public class summaryDatum
    {
        public double Id { get; set; }
        
        public double? Sum { get; set; }
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
    //区域内总分
    public class districtSumScoreDatum
    {
        public string districtCode { get; set; }
        public string districtName { get; set; }
        public double? SumScore { get; set; }
    }

    //卡片总分
    public class CardSumScoreDatum
    {
        public string cardCode { get; set; }
        public string cardName { get; set; }
        public double? SumScore { get; set; }
        public string checks { get; set; }
        public string InputName { get; set; }
        public string LocalName { get; set; }
        public int? AppraisalCode { get; set; }
    }
    public class CardOutlin
    {
        public int CardId { get; set; }
        public string CardName { get; set; }
        public string CardCode { get; set; }
        public int cardItemCount { get; set; }
        public int diORx { get; set; }
        public int AppraisalCode { get; set; }

    }

    public class AppraisalContentInfo
    {
        public string errorMsg { get; set; }
        public string msgCode { get; set; }
        public AppraisalContentDatum[] data { get; set; }
    }

    public class AppraisalContentDatum
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

    }


    public class CardAllotInfo
    {
        public string errorMsg { get; set; }
        public string msgCode { get; set; }
        public CardAllotDatum[] data { get; set; }
    }

    public class CardAllotDatum
    {
        public int Id { get; set; }
        public string DistrictCode { get; set; }
        public string DistrictName { get; set; }
        public string CardCode { get; set; }
        public string CardName { get; set; }
        public int CardMaxCount { get; set; }
        public int CardCurCount { get; set; }
        public int AppraisalCode { get; set; }
        public double CardItemScore { get; set; }
    }


    public class AllUserInfo
    {
        public string errorMsg { get; set; }
        public string msgCode { get; set; }
        public userInfoDatum[] data { get; set; }
    }

    public class userInfoDatum
    {
        public int Id { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string RealName { get; set; }
        public string DistrictCode { get; set; }
        public string DistrictName { get; set; }
        public int Type { get; set; }
        public int AppraisalCode { get; set; }
    }

    public class AllDistrictInfo
    {
        public string errorMsg { get; set; }
        public string msgCode { get; set; }
        public DistrictInfoDatum[] data { get; set; }
    }

    public class DistrictInfoDatum
    {
        public string DistrictName { get; set; }
        public string DistrictCode { get; set; }
        public string OwenerCityName { get; set; }
        public string OwenerCityCode { get; set; }
        public int DistrictId { get; set; }
    }

    public class HardwareAuthorizationInfo
    {
        public string errorMsg { get; set; }
        public string msgCode { get; set; }
        public HardwareAuthorizationDatum[] data { get; set; }
    }

    public class HardwareAuthorizationDatum
    {
        public int Id { get; set; }
        public string HardwareCode { get; set; }
        public string CustomName { get; set; }
        public string CustomPhone { get; set; }
        public int LicenseState { get; set; }
    }

}
