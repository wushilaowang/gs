using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GSwmgzCore.Models
{
    public partial class wmcscpContext : DbContext
    {

        public wmcscpContext(DbContextOptions<wmcscpContext> options)
            : base(options)
        {

        }

        public virtual DbSet<InputNames> InputNames { get; set; }
        public virtual DbSet<CardContent> CardContent { get; set; }
        public virtual DbSet<CardOutlin> CardOutlin { get; set; }
        public virtual DbSet<CityTb> CityTb { get; set; }
        public virtual DbSet<CphzContent> CphzContent { get; set; }
        public virtual DbSet<DistrictStatus> DistrictStatus { get; set; }
        public virtual DbSet<DistrictTb> DistrictTb { get; set; }
        public virtual DbSet<ObSysUser> ObSysUser { get; set; }
        public virtual DbSet<ObWenJuan> ObWenJuan { get; set; }
        public virtual DbSet<AppraisalResult> AppraisalResult { get; set; }
        public virtual DbSet<ProvinceTb> ProvinceTb { get; set; }
        public virtual DbSet<Summarycount> Summarycount { get; set; }
        public virtual DbSet<Wmcphz> Wmcphz { get; set; }
        public virtual DbSet<CardAllot> CardAllot { get; set; }
        public virtual DbSet<AppraisalHistory> AppraisalHistory { get; set; }
        public virtual DbSet<UserType> UserType { get; set; }
        public virtual DbSet<SumScore> SumScore { get; set; }
        public virtual DbSet<DistrictIndividualScores> DistrictIndividualScores { get; set; }
        public virtual DbSet<HardwareAuthorization> HardwareAuthorization { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InputNames>(entity =>
            {
                entity.ToTable("InputNames");
            });
            modelBuilder.Entity<HardwareAuthorization>(entity =>
            {
                entity.ToTable("HardwareAuthorization");
            });
            modelBuilder.Entity<DistrictIndividualScores>(entity =>
            {
                entity.ToTable("DistrictIndividualScores");
            });

            modelBuilder.Entity<SumScore>(entity =>
            {
                entity.ToTable("SumScore");
            });

            modelBuilder.Entity<UserType>(entity =>
            {
                entity.ToTable("UserType");
            });

            modelBuilder.Entity<AppraisalHistory>(entity =>
            {
                entity.ToTable("AppraisalHistory");
            });

            modelBuilder.Entity<CardAllot>(entity =>
            {
                entity.ToTable("CardAllot");
            });

            modelBuilder.Entity<CardContent>(entity =>
            {
                entity.ToTable("cardContent");

                entity.Property(e => e.Beizhu)
                    .HasColumnName("beizhu")
                    .HasMaxLength(400);

                entity.Property(e => e.CardCode)
                    .HasColumnName("cardCode")
                    .HasMaxLength(50);

                entity.Property(e => e.Cixu).HasColumnName("cixu");

                entity.Property(e => e.Item)
                    .HasColumnName("item")
                    .HasMaxLength(100);

                entity.Property(e => e.Multisign).HasColumnName("multisign");

                entity.Property(e => e.Score).HasColumnName("score");
            });

            modelBuilder.Entity<CardOutlin>(entity =>
            {
                entity.HasKey(e => e.CardId);

                entity.ToTable("cardOutlin");

                entity.Property(e => e.CardId).HasColumnName("cardId");

                entity.Property(e => e.CardCode)
                    .HasColumnName("cardCode")
                    .HasMaxLength(10);

                entity.Property(e => e.CardName)
                    .HasColumnName("cardName")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<CityTb>(entity =>
            {
                entity.HasKey(e => e.CityCode);

                entity.ToTable("cityTB");

                entity.Property(e => e.CityCode)
                    .HasColumnName("cityCode")
                    .HasMaxLength(20)
                    .ValueGeneratedNever();

                entity.Property(e => e.CityId)
                    .HasColumnName("cityId")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.CityName)
                    .HasColumnName("cityName")
                    .HasMaxLength(10);

                entity.Property(e => e.OwenerProvinceCode)
                    .HasColumnName("owenerProvinceCode")
                    .HasMaxLength(20);

                entity.Property(e => e.OwenerProvinceName)
                    .HasColumnName("owenerProvinceName")
                    .HasMaxLength(10);

                entity.HasOne(d => d.OwenerProvinceCodeNavigation)
                    .WithMany(p => p.CityTb)
                    .HasForeignKey(d => d.OwenerProvinceCode)
                    .HasConstraintName("FK_cityTB_provinceTB");
            });

            modelBuilder.Entity<CphzContent>(entity =>
            {
                entity.ToTable("cphz_content");

                entity.Property(e => e.Cixu).HasColumnName("cixu");

                entity.Property(e => e.Item)
                    .HasColumnName("item")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<DistrictStatus>(entity =>
            {
                entity.ToTable("districtStatus");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DistrictCode)
                    .HasColumnName("districtCode")
                    .HasMaxLength(20);

                entity.Property(e => e.DistrictCode).HasColumnName("DistrictCode");

                entity.Property(e => e.DistrictName)
                    .HasColumnName("districtName")
                    .HasMaxLength(50);

                entity.Property(e => e.P01).HasColumnName("p01");

                entity.Property(e => e.P02).HasColumnName("p02");

                entity.Property(e => e.P03).HasColumnName("p03");

                entity.Property(e => e.P04).HasColumnName("p04");

                entity.Property(e => e.P05).HasColumnName("p05");

                entity.Property(e => e.P06).HasColumnName("p06");

                entity.Property(e => e.P07).HasColumnName("p07");

                entity.Property(e => e.P08).HasColumnName("p08");

                entity.Property(e => e.P09).HasColumnName("p09");

                entity.Property(e => e.P10).HasColumnName("p10");

                entity.Property(e => e.P11).HasColumnName("p11");

                entity.Property(e => e.P12).HasColumnName("p12");

                entity.Property(e => e.P13).HasColumnName("p13");

                entity.Property(e => e.P14).HasColumnName("p14");

                entity.Property(e => e.P15).HasColumnName("p15");

                entity.Property(e => e.P16).HasColumnName("p16");

                entity.Property(e => e.P17).HasColumnName("p17");

                entity.Property(e => e.P18).HasColumnName("p18");

                entity.Property(e => e.P19).HasColumnName("p19");

                entity.Property(e => e.P20).HasColumnName("p20");

                entity.Property(e => e.P21).HasColumnName("p21");

                entity.Property(e => e.P22).HasColumnName("p22");

                entity.Property(e => e.P23).HasColumnName("p23");

                entity.Property(e => e.P24).HasColumnName("p24");

                entity.Property(e => e.P25).HasColumnName("p25");

                entity.Property(e => e.P26).HasColumnName("p26");

                entity.Property(e => e.P27).HasColumnName("p27");

                entity.Property(e => e.P28).HasColumnName("p28");

                entity.Property(e => e.P29).HasColumnName("p29");

                entity.Property(e => e.P30).HasColumnName("p30");

                entity.Property(e => e.P31).HasColumnName("p31");

                entity.Property(e => e.P32).HasColumnName("p32");

                entity.Property(e => e.P33).HasColumnName("p33");

                entity.Property(e => e.P34).HasColumnName("p34");

                entity.Property(e => e.P35).HasColumnName("p35");

                entity.Property(e => e.P36).HasColumnName("p36");

                entity.Property(e => e.P37).HasColumnName("p37");

                entity.Property(e => e.P38).HasColumnName("p38");

                entity.Property(e => e.P39).HasColumnName("p39");

                entity.Property(e => e.P40).HasColumnName("p40");

                entity.Property(e => e.P41).HasColumnName("p41");

                entity.Property(e => e.P42).HasColumnName("p42");

                entity.Property(e => e.P43).HasColumnName("p43");

                entity.Property(e => e.P44).HasColumnName("p44");

                entity.Property(e => e.P45).HasColumnName("p45");

                entity.Property(e => e.P46).HasColumnName("p46");

                entity.Property(e => e.P47).HasColumnName("p47");

                entity.Property(e => e.P48).HasColumnName("p48");

                entity.Property(e => e.P49).HasColumnName("p49");

                entity.Property(e => e.P99).HasColumnName("p99");
            });

            modelBuilder.Entity<DistrictTb>(entity =>
            {
                entity.HasKey(e => e.DistrictCode);

                entity.ToTable("districtTB");

                entity.Property(e => e.DistrictCode)
                    .HasColumnName("districtCode")
                    .HasMaxLength(20)
                    .ValueGeneratedNever();

                entity.Property(e => e.DistrictId)
                    .HasColumnName("districtId")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.DistrictName)
                    .HasColumnName("districtName")
                    .HasMaxLength(10);

                entity.Property(e => e.OwenerCityCode)
                    .HasColumnName("owenerCityCode")
                    .HasMaxLength(20);

                entity.Property(e => e.OwenerCityName)
                    .HasColumnName("owenerCityName")
                    .HasMaxLength(10);

                entity.HasOne(d => d.OwenerCityCodeNavigation)
                    .WithMany(p => p.DistrictTb)
                    .HasForeignKey(d => d.OwenerCityCode)
                    .HasConstraintName("FK_districtTB_cityTB");
            });

            modelBuilder.Entity<ObSysUser>(entity =>
            {
                entity.ToTable("obSysUser");

                entity.Property(e => e.Account).HasMaxLength(50);

                entity.Property(e => e.DistrictCode).HasColumnName("districtCode");

                entity.Property(e => e.DistrictName).HasColumnName("districtName");

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.RealName).HasMaxLength(50);

               
            });

            modelBuilder.Entity<ObWenJuan>(entity =>
            {
                entity.Property(e => e.Beizhu).HasColumnName("beizhu");

                entity.Property(e => e.CardNumber).HasColumnName("cardNumber");

                entity.Property(e => e.DistrictName)
                    .HasColumnName("districtName")
                    .HasMaxLength(50);

                entity.Property(e => e.Fwdd)
                    .HasColumnName("fwdd")
                    .HasMaxLength(50);

                entity.Property(e => e.LocalName)
                    .HasColumnName("localName")
                    .HasMaxLength(50);

                entity.Property(e => e.PlaceCode)
                    .HasColumnName("placeCode")
                    .HasMaxLength(10);

                entity.Property(e => e.Time)
                    .HasColumnName("time")
                    .HasMaxLength(50);

                entity.Property(e => e.Uploader)
                    .HasColumnName("uploader")
                    .HasMaxLength(50);

                entity.Property(e => e.WriteTime).HasColumnName("writeTime");
            });

            modelBuilder.Entity<AppraisalResult>(entity =>
            {
                entity.ToTable("AppraisalResult");

                entity.Property(e => e.Beizhu).HasColumnName("beizhu");
                
                entity.Property(e => e.CardName)
                    .HasColumnName("cardName")
                    .HasMaxLength(50);

                entity.Property(e => e.CardCode)
                    .HasColumnName("cardCode")
                    .HasMaxLength(50);
                

                entity.Property(e => e.DistrictCode)
                    .HasColumnName("districtCode")
                    .HasMaxLength(50);

                entity.Property(e => e.LocalName)
                    .HasColumnName("localName")
                    .HasMaxLength(50);

                entity.Property(e => e.PicPath).HasColumnName("picPath");

                entity.Property(e => e.Uploader)
                    .HasColumnName("uploader")
                    .HasMaxLength(50);

                entity.Property(e => e.WriteTime)
                    .HasColumnName("writeTime")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<ProvinceTb>(entity =>
            {
                entity.HasKey(e => e.ProvinceCode);

                entity.ToTable("provinceTB");

                entity.Property(e => e.ProvinceCode)
                    .HasColumnName("provinceCode")
                    .HasMaxLength(20)
                    .ValueGeneratedNever();

                entity.Property(e => e.ProvinceName)
                    .IsRequired()
                    .HasColumnName("provinceName")
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<Summarycount>(entity =>
            {
                entity.Property(e => e.SummaryItems).HasColumnName("summaryItems");

                entity.Property(e => e.SummaryName)
                    .HasColumnName("summaryName")
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<Wmcphz>(entity =>
            {
                entity.ToTable("wmcphz");

                entity.Property(e => e.DistrictId)
                    .HasColumnName("districtId")
                    .HasMaxLength(50);

                entity.Property(e => e.DistrictName)
                    .HasColumnName("districtName")
                    .HasMaxLength(50);

                entity.Property(e => e.FilePath).HasColumnName("filePath");

                entity.Property(e => e.Hz1).HasColumnName("hz1");

                entity.Property(e => e.Hz10).HasColumnName("hz10");

                entity.Property(e => e.Hz11).HasColumnName("hz11");

                entity.Property(e => e.Hz12).HasColumnName("hz12");

                entity.Property(e => e.Hz13).HasColumnName("hz13");

                entity.Property(e => e.Hz14).HasColumnName("hz14");

                entity.Property(e => e.Hz15).HasColumnName("hz15");

                entity.Property(e => e.Hz16).HasColumnName("hz16");

                entity.Property(e => e.Hz17).HasColumnName("hz17");

                entity.Property(e => e.Hz18).HasColumnName("hz18");

                entity.Property(e => e.Hz19).HasColumnName("hz19");

                entity.Property(e => e.Hz2).HasColumnName("hz2");

                entity.Property(e => e.Hz20).HasColumnName("hz20");

                entity.Property(e => e.Hz21).HasColumnName("hz21");

                entity.Property(e => e.Hz22).HasColumnName("hz22");

                entity.Property(e => e.Hz23).HasColumnName("hz23");

                entity.Property(e => e.Hz24).HasColumnName("hz24");

                entity.Property(e => e.Hz25).HasColumnName("hz25");

                entity.Property(e => e.Hz26).HasColumnName("hz26");

                entity.Property(e => e.Hz27).HasColumnName("hz27");

                entity.Property(e => e.Hz28).HasColumnName("hz28");

                entity.Property(e => e.Hz29).HasColumnName("hz29");

                entity.Property(e => e.Hz3).HasColumnName("hz3");

                entity.Property(e => e.Hz30).HasColumnName("hz30");

                entity.Property(e => e.Hz31).HasColumnName("hz31");

                entity.Property(e => e.Hz32).HasColumnName("hz32");

                entity.Property(e => e.Hz33).HasColumnName("hz33");

                entity.Property(e => e.Hz34).HasColumnName("hz34");

                entity.Property(e => e.Hz35).HasColumnName("hz35");

                entity.Property(e => e.Hz36).HasColumnName("hz36");

                entity.Property(e => e.Hz37).HasColumnName("hz37");

                entity.Property(e => e.Hz38).HasColumnName("hz38");

                entity.Property(e => e.Hz39).HasColumnName("hz39");

                entity.Property(e => e.Hz4).HasColumnName("hz4");

                entity.Property(e => e.Hz40).HasColumnName("hz40");

                entity.Property(e => e.Hz41).HasColumnName("hz41");

                entity.Property(e => e.Hz42).HasColumnName("hz42");

                entity.Property(e => e.Hz43).HasColumnName("hz43");

                entity.Property(e => e.Hz44).HasColumnName("hz44");

                entity.Property(e => e.Hz45).HasColumnName("hz45");

                entity.Property(e => e.Hz46).HasColumnName("hz46");

                entity.Property(e => e.Hz47).HasColumnName("hz47");

                entity.Property(e => e.Hz48).HasColumnName("hz48");

                entity.Property(e => e.Hz49).HasColumnName("hz49");

                entity.Property(e => e.Hz5).HasColumnName("hz5");

                entity.Property(e => e.Hz50).HasColumnName("hz50");

                entity.Property(e => e.Hz51).HasColumnName("hz51");

                entity.Property(e => e.Hz52).HasColumnName("hz52");

                entity.Property(e => e.Hz53).HasColumnName("hz53");

                entity.Property(e => e.Hz6).HasColumnName("hz6");

                entity.Property(e => e.Hz7).HasColumnName("hz7");

                entity.Property(e => e.Hz8).HasColumnName("hz8");

                entity.Property(e => e.Hz9).HasColumnName("hz9");

                entity.Property(e => e.WriteTime)
                    .HasColumnName("writeTime")
                    .HasColumnType("datetime");
            });
        }
    }
}
