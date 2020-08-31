
using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using Series = System.Windows.Forms.DataVisualization.Charting.Series;
using MSWord = Microsoft.Office.Interop.Word;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms.DataVisualization.Charting;

namespace gswmgzback
{
    public partial class ResultShow : Form
    {

        public string yumin = "https://wmcscp.gsinfo.cn/gswmgz/cepingresult/";// "http://211.158.66.55/wmgz/api/";//  
        //////公共成员
        ///
        public static List<CardOutlin> PUBLCardOutlins = new List<CardOutlin>();
        //保存卡片分值
        public static List<summaryDatum> PUBLCardScore = new List<summaryDatum>();
        public static string PUBdistrictName = "";


        //保存测评次数信息
        public List<AppraisalCodesDatum> LAppraisalCodes = new List<AppraisalCodesDatum>();
        //保存县/区信息
        public List<districtInfoDatum> LdistrictInfos = new List<districtInfoDatum>();
        //保存测评结果
        public List<AppraisalResultDatum> LCPresults = new List<AppraisalResultDatum>();


        public  List<CardOutlin> LCardOutlins = new List<CardOutlin>();
        public List<DistrictTb> AllDistrict = new List<DistrictTb>();
        //保存卡片分值
        public  List<summaryDatum> LCardScore = new List<summaryDatum>();
        //保存问卷的比例与总分
        public List<string> Cur_WenJuanScore;
        //保存测评卡片内容
        public List<AppraisalContentDatum> LCPcontents = new List<AppraisalContentDatum>();

        //保存卡片基础分(cardallot)
        public List<CardAllotDatum> LCardallot = new List<CardAllotDatum>();

        //全局变量,方便查数据
        int Select_AppraisalCode = 0;
        string Select_DistrictCode = "";
        string Select_CardCode = "";
        List<int> Select_CardIds = new List<int>();
        public ResultShow()
        {

            InitializeComponent();
        }

        private void ResultShow_Load(object sender, EventArgs e)
        {
            iniResultShow();
            //隔行变色
            DGV_result.RowsDefaultCellStyle.BackColor = Color.White;
            DGV_result.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
            //不显示空行
            DGV_result.AllowUserToAddRows = false;

            //设置chart的格式
            chartSetStyle(chart_district);
            chartSetStyle(chart_province);
            chartSetStyle(chart_cardInDistrict);
        }
        //设置chart的格式
        private void chartSetStyle(Chart chart)
        {
            chart.ChartAreas["ChartArea1"].AxisX.ScrollBar.IsPositionedInside = false;//设置滚动条是在外部显示
            chart.ChartAreas["ChartArea1"].AxisX.ScrollBar.Size = 20;//设置滚动条的宽度

            chart.ChartAreas["ChartArea1"].AxisX.ScrollBar.ButtonStyle = ScrollBarButtonStyles.SmallScroll;//滚动条只显示向前的按钮，主要是为了不显示取消显示的按钮
            chart.ChartAreas["ChartArea1"].AxisX.ScaleView.Size = 10;//设置图表可视区域数据点数，说白了一次可以看到多少个X轴区域
            chart.ChartAreas["ChartArea1"].AxisX.ScaleView.MinSize = 1;//设置滚动一次，移动几格区域
            chart.ChartAreas["ChartArea1"].AxisX.Interval = 1;//设置X轴的间隔，设置它是为了看起来方便点，也就是要每个X轴的记录都显示出来
            chart.ChartAreas["ChartArea1"].AxisX.Minimum = 0;//X轴起始点
            //取消显示网格
            chart.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.Transparent;
            chart.ChartAreas[0].AxisY.MajorGrid.LineColor = System.Drawing.Color.Transparent;
        }

        private void iniResultShow()
        {

            //获取所有测评code
            CB_AppraisalCode.Items.Clear();
            LAppraisalCodes.Clear();


            string url = Form1.UrlPre + "GetAllAppraisalCode" + "?entrance=" + LogIn.entrance;
            string Httpres = HttpGet.HttpGetFunc(url);
            if (Httpres == null)
            {
                MessageBox.Show("网络未连接");
                return;
            }
            //将字符串转换成json
            var Httpjsonresult = JObject.Parse(Httpres);
            //获取json中的data部分
            JToken HttpJsonvalue = Httpjsonresult.GetValue("data");
            int resultrow = HttpJsonvalue.Count();

            if (LogIn.userinfosDatum.Type == 1)
            {
                //如果是测评带队则可以看所有的，但是不能创建测评和用户
                for (int i = 0; i < resultrow; i++)
                {
                    int column = HttpJsonvalue[i].Count();
                    var s = HttpJsonvalue[i].ToString();
                    AppraisalCodesDatum allAppraisalCodesDatum = JsonConvert.DeserializeObject<AppraisalCodesDatum>(s);
                    //这是全都能看的代码
                    CB_AppraisalCode.Items.Add(allAppraisalCodesDatum.AppraisalName);
                    LAppraisalCodes.Add(allAppraisalCodesDatum);
                    ////这是只能看自己的的代码
                    //if (LogIn.userinfosDatum.AppraisalCode == allAppraisalCodesDatum.AppraisalCode)
                    //{
                    //    CB_AppraisalCode.Items.Add(allAppraisalCodesDatum.AppraisalName);
                    //    LAppraisalCodes.Add(allAppraisalCodesDatum);
                    //    break;
                    //}
                }
            }
            else//管理员可以看到所有测评记录
            {
                for (int i = 0; i < resultrow; i++)
                {
                    int column = HttpJsonvalue[i].Count();
                    var s = HttpJsonvalue[i].ToString();
                    AppraisalCodesDatum allAppraisalCodesDatum = JsonConvert.DeserializeObject<AppraisalCodesDatum>(s);
                    CB_AppraisalCode.Items.Add(allAppraisalCodesDatum.AppraisalName);
                    LAppraisalCodes.Add(allAppraisalCodesDatum);
                }
            }
            //获取所有districtcode
            
            url = Form1.UrlPre + "ShowAlldistrict" ;
            Httpres = HttpGet.HttpGetFunc(url);
            if (Httpres == null)
            {
                MessageBox.Show("网络未连接");
                return;
            }
            //将字符串转换成json
            Httpjsonresult = JObject.Parse(Httpres);
            //获取json中的data部分
            HttpJsonvalue = Httpjsonresult.GetValue("data");
            resultrow = HttpJsonvalue.Count();
            for (int i = 0; i < resultrow; i++)
            {
                int column = HttpJsonvalue[i].Count();
                var s = HttpJsonvalue[i].ToString();
                DistrictTb allAppraisalCodesDatum = JsonConvert.DeserializeObject<DistrictTb>(s);
                AllDistrict.Add(allAppraisalCodesDatum);
            }
        }

        private void CB_AppraisalCode_MouseClick(object sender, MouseEventArgs e)
        {

        }
        //获取测评内容
        private void getAppraisalContent()
        {
            LCPcontents.Clear();
            //AppraisalContentDatum
            string url = Form1.UrlPre + "ShowCardContent?AppraisalCode=" + Select_AppraisalCode;
            string Httpres = HttpGet.HttpGetFunc(url);
            if (Httpres == null)
            {
                MessageBox.Show("网络未连接");
                return;
            }

            //将字符串转换成json
            var Httpjsonresult = JObject.Parse(Httpres);
            //获取json中的data部分
            JToken HttpJsonvalue = Httpjsonresult.GetValue("data");
            int resultrow = HttpJsonvalue.Count();

            for (int i = 0; i < resultrow; i++)
            {
                int column = HttpJsonvalue[i].Count();
                var s = HttpJsonvalue[i].ToString();
                AppraisalContentDatum appraisalContentDatum = JsonConvert.DeserializeObject<AppraisalContentDatum>(s);
                LCPcontents.Add(appraisalContentDatum);
            }
        }



        //获取测评结果
        private void getCPresult()
        {
            LCPresults.Clear();
            if (LogIn.userinfosDatum.Type == 1)
            {
                //如果是测评带队则只能看当前自己负责的区域

                string[] CurrentUserDistrict = LogIn.userinfosDatum.DistrictCode.Split(',');
                foreach (var dis in CurrentUserDistrict)
                {
                    string url = Form1.UrlPre + "GetCpResults?AppraisalCode=" + Select_AppraisalCode + "&districtCode=" + dis;
                    string Httpres = HttpGet.HttpGetFunc(url);
                    if (Httpres == null)
                    {
                        MessageBox.Show("网络未连接");
                        return;
                    }

                    //将字符串转换成json
                    var Httpjsonresult = JObject.Parse(Httpres);
                    //获取json中的data部分
                    JToken HttpJsonvalue = Httpjsonresult.GetValue("data");
                    int resultrow = HttpJsonvalue.Count();

                    for (int i = 0; i < resultrow; i++)
                    {
                        int column = HttpJsonvalue[i].Count();
                        var s = HttpJsonvalue[i].ToString();
                        AppraisalResultDatum appraisalResultDatum = JsonConvert.DeserializeObject<AppraisalResultDatum>(s);

                        LCPresults.Add(appraisalResultDatum);
                    }

                }

            }
            else//管理员可以看到所有测评区域
            {

                string url = Form1.UrlPre + "GetCpResults?AppraisalCode=" + Select_AppraisalCode;
                string Httpres = HttpGet.HttpGetFunc(url);
                if (Httpres == null)
                {
                    MessageBox.Show("网络未连接");
                    return;
                }

                //将字符串转换成json
                var Httpjsonresult = JObject.Parse(Httpres);
                //获取json中的data部分
                JToken HttpJsonvalue = Httpjsonresult.GetValue("data");
                int resultrow = HttpJsonvalue.Count();

                for (int i = 0; i < resultrow; i++)
                {
                    int column = HttpJsonvalue[i].Count();
                    var s = HttpJsonvalue[i].ToString();
                    AppraisalResultDatum appraisalResultDatum = JsonConvert.DeserializeObject<AppraisalResultDatum>(s);

                    LCPresults.Add(appraisalResultDatum);
                }
            }
        }

        //获取当前测评编号
        private void getCurAppraisalCode()
        {
            string AppraisalName = CB_AppraisalCode.Text;
            Select_AppraisalCode = LAppraisalCodes.Where(x => x.AppraisalName == AppraisalName).Select(x => x.AppraisalCode).FirstOrDefault();
        }
        //获取当前县/区code
        private void getCurDistrictCode()
        {
            string DistrictName = CB_Districts.Text;

            Select_DistrictCode = LdistrictInfos.Where(x => x.districtNamet == DistrictName).Select(x => x.districtCodet).FirstOrDefault();

        }

        //获取当前cardcode
        private void getCurCardCode(string area = "")
        {
            string CardName = LB_CardName.Text;
            if (area != "" && !CardName.Contains("问卷"))
            {
                if (!CardName.Contains("未成"))
                {

                    Select_CardCode = LCPresults.Where(x => x.CardName+x.InputName  == CardName && x.AppraisalCode == Select_AppraisalCode && x.DistrictCode == Select_DistrictCode).Select(x => x.CardCode).FirstOrDefault();
                }
                else
                {
                    Select_CardCode = LCPresults.Where(x => x.CardName == CardName && x.AppraisalCode == Select_AppraisalCode && x.DistrictCode == Select_DistrictCode).Select(x => x.CardCode).FirstOrDefault();
                }
            }
            else
            {
                Select_CardCode = LCPresults.Where(x => x.InputName == CardName && x.AppraisalCode == Select_AppraisalCode).Select(x => x.CardCode).FirstOrDefault();
            }
        }

        //获取当前选中的结果的id
        private void getCurCardId(string area = "")
        {
            string CardName = LB_CardName.Text;
            if (area != "" && !CardName.Contains("问卷"))
            {
                if (!CardName.Contains("未成"))
                {

                    Select_CardIds = LCPresults.Where(x => x.CardName + x.InputName == CardName && x.AppraisalCode == Select_AppraisalCode && x.DistrictCode == area).OrderBy(x => x.Id).OrderBy(x => x.CardCode).Select(x => x.Id).ToList();
                }
                else
                {
                    Select_CardIds = LCPresults.Where(x => x.CardName == CardName && x.AppraisalCode == Select_AppraisalCode && x.DistrictCode == area).OrderBy(x => x.Id).OrderBy(x => x.CardCode).Select(x => x.Id).ToList();
                }
            }
            else
            {
                Select_CardIds = LCPresults.Where(x => x.InputName == CardName && x.AppraisalCode == Select_AppraisalCode).OrderBy(x => x.Id).OrderBy(x => x.CardCode).Select(x => x.Id).ToList();
            }
        }

        private void CB_Districts_MouseClick(object sender, MouseEventArgs e)
        {


        }

        List<string> curContent = new List<string>();
        List<string> curOptions = new List<string>();
        List<string> curBeiZhu = new List<string>();
        List<double> curscores = new List<double>();
        double CurCardSumScore = 0;
        List<districtSumScoreDatum> cardInDistrict;
        public void LB_CardName_MouseClick(object sender, MouseEventArgs e)
        {
            setdata();
        }
        //设置数据
        public void setdata()
        {
            DGV_result.Rows.Clear();
            LB_CardTitle.Text = LB_CardName.Text + "详情";

            getCurCardCode(Select_DistrictCode);
            getCurCardId(Select_DistrictCode);
            //if (CB_AppraisalCode.Text.Contains("未成年"))
            //{

            //    getCurCardCode(Select_DistrictCode);
            //    getCurCardId(Select_DistrictCode);
            //}
            //else
            //{
            //    getCurCardCode();
            //    getCurCardId();
            //}
            LB_PicList.Items.Clear();
            chart_cardInDistrict.Series.Clear();
            curContent.Clear();
            curContent = LCPcontents.Where(x => x.CardCode == Select_CardCode && x.AppraisalCode == Select_AppraisalCode).Select(x => x.Item).ToList();
            curBeiZhu = LCPcontents.Where(x => x.CardCode == Select_CardCode && x.AppraisalCode == Select_AppraisalCode).Select(x => x.Beizhu).ToList();

            int count = 0;
            //知道当前点的是哪一个卡片(同名特化)
            for (int j = 0; j < LB_CardName.Items.Count; j++)
            {

                if (LB_CardName.Items[j].ToString() == LB_CardName.Text)
                {
                    count++;
                    if (j == LB_CardName.SelectedIndex)
                    {
                        break;
                    }
                }
            }
            //CB_Districts.Text = LCPresults.Where(x => x.Id == Select_CardIds[count - 1] && x.AppraisalCode == Select_AppraisalCode).Select(x => x.DistrictName).FirstOrDefault();
            //获取该卡片在整个测评里的排名
            cardInDistrict = LCardScore.Where(x => x.AppraisalCode == Select_AppraisalCode && x.CardCode == Select_CardCode)
                .GroupBy(x => new { x.DistrictName, x.Sum })
                .Select(x => new districtSumScoreDatum
                {
                    districtName = x.Key.DistrictName,
                    SumScore = x.Key.Sum
                })
                .ToList();
            //结果显示到曲线

            //设置曲线高度
            chart_cardInDistrict.ChartAreas["ChartArea1"].AxisY.Maximum = double.Parse((cardInDistrict.Select(X => X.SumScore).Max()).ToString()) + 0.2;
            //绘制曲线
            //曲线设置

            Series cardSeries = new Series(LB_CardName.Text + "得分");

            cardSeries.XValueType = ChartValueType.String;  //设置X轴上的值类型

            cardSeries.IsValueShownAsLabel = true;
            //设置曲线类型,这里是柱状图
            cardSeries.ChartType = SeriesChartType.Column;
            //给系列上的点进行赋值，分别对应横坐标和纵坐标的值
            for (int i = 0; i < cardInDistrict.Count; i++)
            {
                cardSeries.Points.AddXY(cardInDistrict[i].districtName, cardInDistrict[i].SumScore);

            }
            //把series添加到chart上
            chart_cardInDistrict.Series.Add(cardSeries);

            var curRes = LCPresults.Where(x => x.Id == Select_CardIds[count - 1] && x.AppraisalCode == Select_AppraisalCode).FirstOrDefault();
            var curScores = LCardScore.Where(x => x.Id == Select_CardIds[count - 1] && x.AppraisalCode == Select_AppraisalCode).FirstOrDefault();
            //存储测评结果
            curOptions.Clear();
            //力不能及,先想个办法解决
            for (int i = 0; i < curRes.Checks.Length; i++)
            {
                curOptions.Add(curRes.Checks.Substring(i, 1));
            }

            //存储测评得分
            curscores.Clear();
            var temp = curScores.Checks.Split(',');

            for (int i = 0; i < temp.Count(); i++)
            {
                curscores.Add(double.Parse(temp[i]));
            }


            CurCardSumScore = 0;
            //显示卡片详情

            for (int i = 0; i < curContent.Count; i++)
            {
                string resultCH = "不符合";

                {
                    switch (curOptions[i])
                    {
                        case "A":
                            resultCH = "A";
                            break;
                        case "B":
                            resultCH = "B";
                            break;
                        case "C":
                            resultCH = "C";
                            break;
                        case "D":
                            resultCH = "D";
                            break;
                    }
                    DGV_result.Rows.Add(curContent[i], resultCH, curscores[i]);

                }
                CurCardSumScore += curscores[i];
            }

            LB_CardScore.Text = "卡片得分: " + Math.Round(CurCardSumScore, 2);
            
            if (!CB_AppraisalCode.Text.Contains("未成年"))
            {
                CurCardSumScore = Convert.ToDouble(LCardScore.Where(x => x.Id == Select_CardIds[count - 1] && x.AppraisalCode == Select_AppraisalCode).FirstOrDefault().Sum);
                LB_CardScore.Text = "得分: " + Math.Round(CurCardSumScore, 2);

            }
            //显示图片列表
            var pics = curScores.PicPath.Split(',');
            foreach (var item in pics)
            {
                LB_PicList.Items.Add(item);
            }
            //显示备注
            if (curScores.Beizhu == "")
            {
                TB_beizhu.Text = "用户没有备注";
            }
            else if (curScores.LocalName != "")
            {
                TB_beizhu.Text = curScores.Beizhu + "\r\n ----------由 " + curScores.Uploader + " 上传于 " + curScores.LocalName + "  上传时间: " + curScores.WriteTime;
            }
            else
            {
                TB_beizhu.Text = curScores.Beizhu;
            }
        }

        List<CardSumScoreDatum> cardSumScores;
        double districtSumScore = 0;
        private void CB_Districts_SelectedIndexChanged(object sender, EventArgs e)
        {
            LB_CardName.Items.Clear();
            getCurDistrictCode();
            chart_district.Series.Clear();
            //ShowWJscore();
            juvenilesSeleceArea();
            //if (LogIn.entrance.Contains("未成年"))
            //{
            //    juvenilesSeleceArea();
            //}
            ////else
            ////{
            ////    selectArea();
            ////}
        }
        
        //未成年人算分
        private void juvenilesSeleceArea()
        {
           
            
            //按照卡片名排序
            var cardnames = LCPresults.Where(x => x.DistrictCode.Substring(0, 6) == Select_DistrictCode.Substring(0, 6)).Select(x => new { x.CardName, x.Id, x.InputName, x.CardCode }).OrderBy(x => x.Id).OrderBy(x => x.CardCode).ToList();

            foreach (var item in cardnames)
            {
                if (!CB_AppraisalCode.Text.Contains("未成"))
                {
                    if (item.CardName.Contains("问卷"))
                    {

                        LB_CardName.Items.Add(item.InputName);
                    }
                    else
                    {
                        LB_CardName.Items.Add(item.CardName+item.InputName);
                    }
                }
                else
                {
                    LB_CardName.Items.Add(item.CardName);
                }
            }
            List<CardSumScoreDatum> temp = LCardScore.Where(x => x.AppraisalCode == Select_AppraisalCode)
                 .GroupBy(x => new { x.CardName, x.Sum, x.DistrictName }).Select(x => (new CardSumScoreDatum
                 {
                     cardName = x.Key.DistrictName,
                     SumScore = x.Key.Sum
                 })).ToList();
            temp = temp.GroupBy(x => new { x.cardName }).Select(x => (new CardSumScoreDatum
            {
                cardName = x.Key.cardName,
                SumScore = x.Sum(s => s.SumScore)
            })).ToList();
            double SumScore = 0.0;
            double.TryParse(temp.Max(x => x.SumScore).ToString(), out SumScore);
            //绘制曲线
            //设置曲线高度
            chart_district.ChartAreas["ChartArea1"].AxisY.Maximum = SumScore + 0.2;
            //曲线设置
            Series cardSeries = new Series("得分");

            cardSeries.XValueType = ChartValueType.String;  //设置X轴上的值类型
            cardSeries.IsValueShownAsLabel = true;
            cardSumScores = LCardScore.Where(x => x.AppraisalCode == Select_AppraisalCode && x.DistrictCode.Substring(0, 6) == Select_DistrictCode.Substring(0, 6))
                 .GroupBy(x => new { x.CardName, x.Sum }).Select(x => (new CardSumScoreDatum
                 {
                     cardName = x.Key.CardName,
                     SumScore = x.Key.Sum
                 })).ToList();
            //设置曲线类型,这里是柱状图
            cardSeries.ChartType = SeriesChartType.Column;
            //给系列上的点进行赋值，分别对应横坐标和纵坐标的值
            for (int i = 0; i < cardSumScores.Count; i++)
            {
                cardSeries.Points.AddXY(cardSumScores[i].cardName, cardSumScores[i].SumScore);
                districtSumScore += double.Parse(cardSumScores[i].SumScore.ToString());
            }

            //把series添加到chart上
            chart_district.Series.Add(cardSeries);

            LB_DistrictScore.Text = "总分:" + Math.Round(districtSumScore, 2, MidpointRounding.AwayFromZero);
        }
       
        //生成每张卡片的分数
        private double CreateJuvenilesSumScore()
        {
            //计总分
            double SumScore = 0.0;
            //用于记录每个选项的选择情况的双链表
            List<List<string>> allChecksByItem = new List<List<string>>();
            for (int j = 0; j < LCardOutlins.Count; j++)
            {
                allChecksByItem.Clear();
                if (cardSumScores != null) cardSumScores.Clear();
                 cardSumScores = LCardScore.Where(x => x.CardCode == LCardOutlins[j].CardCode &&x.DistrictCode== Select_DistrictCode).Select(d => new CardSumScoreDatum { cardName = d.CardName, cardCode = d.CardCode, checks = d.Checks, InputName = d.InputName, LocalName = d.LocalName }).ToList();
                if (cardSumScores.Count == 0)
                {
                    continue;
                }
                for (int k = 0; k < cardSumScores.Count; k++)
                {
                    allChecksByItem.Add(cardSumScores[k].checks.Split(',').ToList());
                }

                string TempScore = "";

                double cardSumScore = 0.0;
                //已经获取了当前卡片的所有结果,开始进行汇总

                for (int l = 0; l < allChecksByItem[0].Count; l++)
                {
                    double carditemScore = LCPcontents.Where(x => x.CardCode == LCardOutlins[j].CardCode && x.Cixu == l + 1 ).FirstOrDefault().Score;

                    string temp = "";
                    for (int m = 0; m < allChecksByItem.Count; m++)
                    {
                        temp += (allChecksByItem[m][l]);
                    }
                    //选A的个数
                    int ACount = temp.Split('1').Length - 1;
                    if (cardSumScores[0].cardName.Contains("辅导"))//1所
                    {
                        if (ACount >= 1)
                        {
                            TempScore += carditemScore + ",";
                            cardSumScore += carditemScore;
                        }
                        else
                        {
                            TempScore += "0,";
                        }

                    }
                    else if (cardSumScores[0].cardName.Contains("机构"))//两所
                    {
                        if (ACount >= 2)
                        {
                            TempScore += carditemScore + ",";
                            cardSumScore += carditemScore;
                        }
                        else if (ACount >= 1)
                        {
                            TempScore += carditemScore * 0.66 + ",";
                            cardSumScore += carditemScore * 0.66;
                        }
                        else
                        {
                            TempScore += "0,";
                        }

                    }
                    else if (cardSumScores[0].cardName.Contains("网吧") || cardSumScores[0].cardName.Contains("大街") || cardSumScores[0].cardName.Contains("公益"))//三所
                    {
                        if (ACount >= 3)
                        {
                            TempScore += carditemScore + ",";
                            cardSumScore += carditemScore;
                        }
                        else if (ACount >= 2)
                        {
                            TempScore += carditemScore * 0.66 + ",";
                            cardSumScore += carditemScore * 0.66;
                        }
                        else if (ACount >= 1)
                        {
                            TempScore += carditemScore * 0.33 + ",";
                            cardSumScore += carditemScore * 0.33;
                        }
                        else
                        {
                            TempScore += "0,";
                        }

                    }
                    else//四所
                    {
                        if (ACount >= 4)
                        {
                            TempScore += carditemScore + ",";
                            cardSumScore += carditemScore;
                        }
                        else if (ACount >= 3)
                        {
                            TempScore += carditemScore * 0.66 + ",";
                            cardSumScore += carditemScore * 0.66;
                        }
                        else if (ACount >= 2)
                        {
                            TempScore += carditemScore * 0.33 + ",";
                            cardSumScore += carditemScore * 0.33;
                        }
                        else
                        {
                            TempScore += "0,";
                        }
                    }
                }
                var q = from p in LCardScore
                        where p.CardCode == LCardOutlins[j].CardCode && p.DistrictCode == Select_DistrictCode
                        select p;
                foreach (var p in q)
                {
                    p.Checks = TempScore.Substring(0, TempScore.Length - 1);
                    p.Sum = cardSumScore;
                }
                SumScore += cardSumScore;
            }
            return SumScore;
        }
        
        List<districtSumScoreDatum> districtSumScores;
        private void CB_AppraisalCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            CB_Districts.Items.Clear();
            LB_CardName.Items.Clear();
            chart_province.Series.Clear();
            LdistrictInfos.Clear();
            getCurAppraisalCode();
            LCardallot.Clear();
            LCardScore.Clear();

            LCardOutlins.Clear();
            districtSumScore = 0;
            //获取cardOutlin(该次测评有几类卡)
            string url = Form1.UrlPre + "ShowCardList?entrance=" + LogIn.entrance;
            string Httpres = HttpGet.HttpGetFunc(url);
            if (Httpres == null)
            {
                MessageBox.Show("网络未连接");
            }
            List<string> s1 = new List<string>();
            //将字符串转换成json
            var Httpjsonresult = JObject.Parse(Httpres);
            //获取json中的data部分
            JToken HttpJsonvalue = Httpjsonresult.GetValue("data");
            int resultrow = HttpJsonvalue.Count();

            for (int i = 0; i < resultrow; i++)
            {
                int column = HttpJsonvalue[i].Count();
                var s = HttpJsonvalue[i].ToString();
                CardOutlin cardOutlin = JsonConvert.DeserializeObject<CardOutlin>(s);

                LCardOutlins.Add(cardOutlin);
                PUBLCardOutlins.Add(cardOutlin);
            }


            //显示开始与结束时间
            var times = LAppraisalCodes.Where(x => x.AppraisalCode == Select_AppraisalCode).Select(x => new { x.Starttime, x.Endtime }).FirstOrDefault();
            DTP_startTime.Text = times.Starttime.ToString();
            DTP_endTime.Text = times.Endtime.ToString();
            //获取该次测评所有结果
            getCPresult();
            //查看代码是否可以提取为方法,
            ////////////////////////////////////////////
            if (CB_AppraisalCode.Text.Contains("未成年"))
            {
                showDataByArea();
            }
            else
            {
                //label4.Visible = false;
                //CB_Districts.Visible = false;
                showDataWithoutArea();
                ////现在村镇和普通的一样了
                //if (CB_AppraisalCode.Text.Contains("村镇"))//村镇独特算分
                //{
                //    ShowVillageScore();
                //}
                //else
                //{
                //    showDataWithoutArea();
                //}
            }
        }
        

        
        /// <summary>
        /// 除了未成年人都在这
        /// 点击测评类型后直接生成所有结果,不需要额外点击测评区域
        /// </summary>
        private void showDataWithoutArea()
        {

            LCardScore.Clear();
            
            //获取所有卡片内容
            getAppraisalContent();

            chart_district.Series.Clear();
            //计算卡片的分数
            for (int s = 0; s < LCPresults.Count; s++)
            {
                //获取当前记录的基础分值
                double CurCardScore = 0.0;
                double tempscore = 0.00;//记录总分
                string scores = "";//用逗号隔开每个选项的分值
                for (int i = 0; i < LCPresults[s].Checks.Length; i++)
                {
                    var tmp = LCPcontents.Where(x => x.AppraisalCode == LCPresults[s].AppraisalCode && x.CardCode == LCPresults[s].CardCode && x.Cixu == i + 1).First();
                    CurCardScore = LCPcontents.Where(x => x.AppraisalCode == LCPresults[s].AppraisalCode && x.CardCode == LCPresults[s].CardCode && x.Cixu == i + 1)
                    .Select(x => x.Score).FirstOrDefault();
                    if (LCPresults[s].CardName.Contains("问卷"))
                    {
                        CurCardScore = 1.00;
                    }
                    switch (LCPresults[s].Checks.Substring(i, 1))
                    {
                        case "A":
                            tempscore += Math.Round(CurCardScore, 2, MidpointRounding.AwayFromZero);
                            scores += Math.Round(CurCardScore, 2, MidpointRounding.AwayFromZero) + ",";
                            break;
                        case "B":
                            if (LCPresults[s].CardCode.Contains("K9")&& tmp.Beizhu.Split(',').Count()==2)
                            {
                                tempscore += 0;
                                scores += "0,";
                            }
                            else
                            {
                                tempscore += Math.Round(CurCardScore * 0.66, 2, MidpointRounding.AwayFromZero);
                                scores += Math.Round(CurCardScore * 0.66, 2, MidpointRounding.AwayFromZero) + ",";
                            }
                            break;
                        case "C":
                            if (LCPresults[s].CardCode.Contains("K9") && tmp.Beizhu.Split(',').Count() == 3)
                            {
                                tempscore += 0;
                                scores += "0,";
                            }
                            else
                            {tempscore += Math.Round(CurCardScore * 0.33, 2, MidpointRounding.AwayFromZero);
                            scores += Math.Round(CurCardScore * 0.33, 2, MidpointRounding.AwayFromZero) + ","; }
                            
                            break;
                        case "D":
                            if (LCPresults[s].CardCode.Contains("K9") && tmp.Beizhu.Split(',').Count() ==4)
                            {
                                tempscore += 0;
                                scores += "0,";
                            }
                            else
                            {tempscore += 0;
                            scores += "0,"; }
                            break;
                    }
                }
                string citycode = LCPresults[s].DistrictCode.Substring(0,4)+"00";
                string cityname = AllDistrict.Where(x => x.OwenerCityCode.Substring(0,6) == citycode).First().OwenerCityName;

                summaryDatum summary = new summaryDatum
                {
                    Id = (LCPresults[s].Id),
                    DistrictCode = citycode,
                    CardCode = LCPresults[s].CardCode,
                    DistrictName = cityname,
                    CardName = LCPresults[s].CardName,
                    Beizhu = LCPresults[s].Beizhu,
                    PicPath = LCPresults[s].PicPath,
                    WriteTime = LCPresults[s].WriteTime,
                    LocalName = LCPresults[s].LocalName,
                    Uploader = LCPresults[s].Uploader,
                    AppraisalCode = LCPresults[s].AppraisalCode,
                    Checks = scores.Substring(0, scores.Length - 1),
                    Sum = Math.Round(tempscore, 2, MidpointRounding.AwayFromZero),
                    InputName = LCPresults[s].CardName.Contains("问卷") ? LCPresults[s].InputName.Substring(0, LCPresults[s].InputName.Length-4)+ LCPresults[s].CardName : LCPresults[s].CardName+LCPresults[s].InputName 
                    //InputName = LCPresults[s].InputName

                };
                LCardScore.Add(summary);
                summary.InputName = LCPresults[s].InputName;
                PUBLCardScore.Add(summary);
            }
            //获取该次测评所有已测过的县,把县的名字放进combox里
            LdistrictInfos = LCardScore.GroupBy(x => new { x.DistrictCode, x.DistrictName }).Select(x => new districtInfoDatum { districtCodet = x.Key.DistrictCode, districtNamet = x.Key.DistrictName }).ToList();

            foreach (var item in LdistrictInfos)
            {
                CB_Districts.Items.Add(item.districtNamet);
            }

            if (CB_AppraisalCode.Text.Contains("校"))
            {
                string curInputName = LCardScore[0].InputName;
                int curInputStartCount = 0;//用于记录当前InputName的第一个下标
                int curInputcount = 0;//用来记录当前Inputname数量
                double? curInputSum = 0;//记录当前总分
                for(int i = 0; i < LCardScore.Count; i++)
                {
                    if(curInputName!= LCardScore[i].InputName && !LCardScore[i].CardName.Contains("问卷"))
                    {
                        double? curSum = curInputcount==0?0: Math.Round(double.Parse((curInputSum / curInputcount).ToString()),2);
                        for (int j=0;j< curInputcount; j++)
                        {
                            LCardScore[curInputStartCount+j].Sum = curSum;
                        }
                        curInputcount = 0;
                        curInputSum = 0;
                        curInputName = LCardScore[i].InputName;
                        curInputStartCount = i;


                        curInputcount += 1;
                        curInputSum += LCardScore[i].Sum;
                    }
                    else if (!LCardScore[i].CardName.Contains("问卷"))
                    {
                        curInputcount += 1;
                        curInputSum += LCardScore[i].Sum;
                    }
                    else
                    {
                        double? curSum = curInputcount == 0 ? 0 : Math.Round(double.Parse((curInputSum / curInputcount).ToString()), 2);
                        for (int j = 0; j < curInputcount; j++)
                        {
                            LCardScore[curInputStartCount + j].Sum = curSum;
                        }
                    }

                }
            }
            /*
             *统一计算完毕,开始单独计算问卷
             *由于文明测评实地考察包含多种情况无法按地域直接划分,故所有问卷集中在一起,无法分辨,
             *对测评结果按照inputname进行分组,在新的list中保存每个单位的问卷分值
             * 
             */
            List<List<string>> allChecksByItem = new List<List<string>>();

            //记录问卷要分成多少类
            int siteNum = 0;
            for (int j = 0; j < LCardOutlins.Count; j++)
            {
                if (!LCardOutlins[j].CardName.Contains("问卷"))
                {
                    continue;
                }
                cardSumScores = LCardScore.Where(x => x.CardCode == LCardOutlins[j].CardCode).Select(d => new CardSumScoreDatum { cardName = d.CardName, cardCode = d.CardCode, checks = d.Checks, InputName = d.InputName, AppraisalCode = d.AppraisalCode, LocalName = d.LocalName }).OrderBy(x => x.InputName).ToList();
                //问卷分组数量
                int s = 0;
                siteNum = cardSumScores.OrderBy(x => x.InputName).GroupBy(x => x.InputName).Count();
                var siteWJNum = cardSumScores.OrderBy(x => x.InputName).GroupBy(x => x.InputName).Select(x => x.Count()).ToList();
                for (int l = 0; l < siteNum; l++)
                {
                    allChecksByItem.Clear();
                    for (int k = 0; k < siteWJNum[l]; k++)
                    {
                        allChecksByItem.Add(cardSumScores[s].checks.Split(',').ToList());
                        s++;
                    }
                    //已经获取了当前卡片的所有问卷,开始进行汇总
                    createWJScoreByVillage(allChecksByItem, j, l, siteWJNum);
                   
                }
            }
            ////按卡分算分值
            districtSumScores = LCardScore.Where(x => x.AppraisalCode == Select_AppraisalCode)
                .GroupBy(x => new { x.InputName }).Select(x => (new districtSumScoreDatum
                {
                    districtName = x.Key.InputName,
                    SumScore = x.Sum(n => n.Sum)
                })).ToList();
            //绘制曲线
            //设置曲线高度
            try
            {
                chart_province.ChartAreas["ChartArea1"].AxisY.Maximum = double.Parse((districtSumScores.Select(X => X.SumScore).Max()).ToString()) + 1;
            }
            catch (Exception)
            {
                MessageBox.Show("无该次测评数据");
                return;
            }

            
            districtSumScore = 0;
            //获取区域内测评总分
            cardSumScores = LCardScore.Where(x => x.AppraisalCode == Select_AppraisalCode)
                 .GroupBy(x => new { x.InputName }).Select(x => (new CardSumScoreDatum
                 {
                     cardName = x.Key.InputName,
                     SumScore = x.Average(n => n.Sum)
                 })).ToList();
            int WJCount = LCardScore.Where(x => x.CardName.Contains("问卷")).Count();

            //绘制曲线
            //设置曲线高度
            chart_district.ChartAreas["ChartArea1"].AxisY.Maximum = double.Parse((cardSumScores.Select(X => X.SumScore).Max()).ToString()) + 0.2;
            //曲线设置
            Series cardSeries = new Series("得分");

            cardSeries.XValueType = ChartValueType.String;  //设置X轴上的值类型
            cardSeries.IsValueShownAsLabel = true;

            //设置曲线类型,这里是柱状图
            cardSeries.ChartType = SeriesChartType.Column;
            //给系列上的点进行赋值，分别对应横坐标和纵坐标的值
            bool ifwjsecond = false;//是否是问卷以及是否是第二次
            for (int i = 0; i < cardSumScores.Count; i++)
            {
                cardSeries.Points.AddXY(cardSumScores[i].cardName, cardSumScores[i].SumScore);
                districtSumScore += double.Parse(cardSumScores[i].SumScore.ToString());
            }

            //把series添加到chart上
            chart_district.Series.Add(cardSeries);
            LB_DistrictScore.Visible = false;

            tableLayoutPanel1.Controls.Remove(chart_province);
            tableLayoutPanel1.Controls.Remove(chart_cardInDistrict);
            tableLayoutPanel1.Controls.Remove(label8);
            tableLayoutPanel1.Controls.Remove(label10);

            changeItemsLocation(tableLayoutPanel1, chart_district, 0, 5, 9);

            chart_district.ChartAreas["ChartArea1"].AxisX.ScaleView.Size = cardSumScores.Count;
        }


        private void createWJScoreBySchool(List<List<string>> allChecksByItem, int j, int l, List<int> siteWJNum)
        {
            throw new NotImplementedException();
        }

        private void createWJScoreByVillage(List<List<string>> allChecksByItem, int flag, int wjIndex, List<int> WJnums)
        {
            //获取问卷的基础分值
            double CurCardScores = 0.0;
            string TempScore = "";
            double cardSumScore = 0.0;
            if (allChecksByItem.Count > 0)
            {
                for (int l = 0; l < allChecksByItem[0].Count; l++)
                {
                    CurCardScores = LCPcontents.Where(x => x.AppraisalCode == cardSumScores[0].AppraisalCode && x.CardCode == cardSumScores[0].cardCode && x.Cixu == l + 1)
                    .Select(x => x.Score).FirstOrDefault();
                    double temp = 0.0;
                    for (int m = 0; m < allChecksByItem.Count; m++)
                    {
                        if (CB_AppraisalCode.Text.Contains("校")|| CB_AppraisalCode.Text.Contains("单位"))
                        {
                            switch (allChecksByItem[m][l].ToString())
                            {
                                case "0.66":
                                    temp += 0.5;
                                    break;
                                case "1":
                                    temp += 1;
                                    break;
                                default:
                                    temp += 0;
                                    break;
                            }
                        }
                        //村镇之前算法不同，现在一样了
                        else
                        if (CB_AppraisalCode.Text.Contains("村"))
                        {
                            switch (allChecksByItem[m][l].ToString())
                            {
                                case "1":
                                    temp += 1;
                                    break;
                                case "0.66":
                                    temp += 0.33;
                                    break;
                                default:
                                    temp += 0;
                                    break;
                            }
                        }
                        else
                        {
                            temp += double.Parse(allChecksByItem[m][l].ToString());
                        }
                    }

                    TempScore += Math.Round(temp * CurCardScores / allChecksByItem.Count, 2) + ",";
                    cardSumScore += Math.Round(temp * CurCardScores / allChecksByItem.Count, 2);
                }
            }
            int currentInputIndex = 0;
            for (int i = 0; i < wjIndex; i++)
            {
                currentInputIndex += WJnums[i];
            }
            var q = from p in LCardScore
                    where p.CardCode == LCardOutlins[flag].CardCode && p.InputName == cardSumScores[currentInputIndex].InputName
                    select p;
            foreach (var p in q)
            {
                p.Checks = TempScore.Substring(0, TempScore.Length - 1);
                p.InputName = p.InputName;
                p.Sum = cardSumScore;
            }
        }
        

        //展示未成年人测评结果
        //点击测评类型与区域后生成该区域结果.
        private void showDataByArea()
        {
            LCardScore.Clear();
            //获取该次测评所有已测过的县,把县的名字放进combox里
            LdistrictInfos = LCPresults.GroupBy(x => new { x.DistrictCode, x.DistrictName }).Select(x => new districtInfoDatum { districtCodet = x.Key.DistrictCode, districtNamet = x.Key.DistrictName }).ToList();
            //获取所有卡片内容
            getAppraisalContent();
            //计算卡片的分数
            for (int s = 0; s < LCPresults.Count; s++)
            {
                //获取当前记录的基础分值
                double CurCardScore = 0.0;
                double tempscore = 0.00;//记录总分
                string scores = "";//用逗号隔开每个选项的分值
                for (int i = 0; i < LCPresults[s].Checks.Length; i++)
                {
                    CurCardScore = 1;
                    switch (LCPresults[s].Checks.Substring(i, 1))
                    {
                        case "A":
                            tempscore += Math.Round(CurCardScore, 2, MidpointRounding.AwayFromZero);
                            scores += Math.Round(CurCardScore, 2, MidpointRounding.AwayFromZero) + ",";
                            break;
                        case "B":
                            tempscore += Math.Round(CurCardScore * 0.66, 2, MidpointRounding.AwayFromZero);
                            scores += Math.Round(CurCardScore * 0.66, 2, MidpointRounding.AwayFromZero) + ",";
                            break;
                        case "C":
                            tempscore += Math.Round(CurCardScore * 0.33, 2, MidpointRounding.AwayFromZero);
                            scores += Math.Round(CurCardScore * 0.33, 2, MidpointRounding.AwayFromZero) + ",";
                            break;
                        case "D":
                            tempscore += 0;
                            scores += "0,";
                            break;

                    }
                }

                summaryDatum summary = new summaryDatum
                {
                    Id = (LCPresults[s].Id),
                    DistrictCode = LCPresults[s].DistrictCode,
                    CardCode = LCPresults[s].CardCode,
                    DistrictName = LCPresults[s].DistrictName,
                    CardName = LCPresults[s].CardName,
                    Beizhu = LCPresults[s].Beizhu,
                    PicPath = LCPresults[s].PicPath,
                    WriteTime = LCPresults[s].WriteTime,
                    LocalName = LCPresults[s].LocalName,
                    Uploader = LCPresults[s].Uploader,
                    AppraisalCode = LCPresults[s].AppraisalCode,
                    Checks = scores.Substring(0, scores.Length - 1),
                    Sum = Math.Round(tempscore, 2, MidpointRounding.AwayFromZero),
                    InputName = LCPresults[s].InputName
                };
                LCardScore.Add(summary);
                PUBLCardScore.Add(summary);
            }

            foreach (var item in LdistrictInfos)
            {
                CB_Districts.Items.Add(item.districtNamet);
                Select_DistrictCode = item.districtCodet;
                CreateJuvenilesSumScore();
            }
            
            ////不需要总分,按卡分分值
            //获取区域内测评总分
            districtSumScores = LCardScore.Where(x => x.AppraisalCode == Select_AppraisalCode)
            .GroupBy(x => new { x.DistrictName, x.Sum, x.CardName }).
            Select(x => (new districtSumScoreDatum
            {
                districtName = x.Key.DistrictName,
                    //SumScore = x.Sum(s=>s.Sum)
                    SumScore = x.Key.Sum
            })).ToList();
            districtSumScores = districtSumScores.GroupBy(x => new { x.districtName }).Select(x => (new districtSumScoreDatum
            {
                districtName = x.Key.districtName,
                SumScore = x.Sum(s => s.SumScore)
            })).ToList();

            //绘制曲线
            //设置曲线高度
            try
            {
                chart_province.ChartAreas["ChartArea1"].AxisY.Maximum = double.Parse((districtSumScores.Select(X => X.SumScore).Max()).ToString()) + 1;
            }
            catch (Exception)
            {
                MessageBox.Show("无该次测评数据");
                return;
            }

            //曲线设置
            System.Windows.Forms.DataVisualization.Charting.Series districtSeries = new Series("得分");
            districtSeries.IsValueShownAsLabel = true;
            districtSeries.XValueType = ChartValueType.String;  //设置X轴上的值类型
                                                                //设置曲线类型,这里是柱状图
            districtSeries.ChartType = SeriesChartType.Column;
            //给系列上的点进行赋值，分别对应横坐标和纵坐标的值
            for (int i = 0; i < districtSumScores.Count; i++)
            {
                ////设置x轴
                //CustomLabel label = new CustomLabel();
                //label.Text = districtSumScores[i].districtName;
                //chart_province.ChartAreas[0].AxisX.CustomLabels.Add(label);
                districtSeries.Points.AddXY(districtSumScores[i].districtName, districtSumScores[i].SumScore);
            }
            //把series添加到chart上
            chart_province.Series.Add(districtSeries);
        }



        private void DGV_result_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            e.Row.HeaderCell.Value = string.Format("{0}", e.Row.Index + 1);
        }

        private void DGV_result_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {

        }

        private void LB_PicList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        string pic_path2 = "";
        private void LB_PicList_MouseClick(object sender, MouseEventArgs e)
        {
            int index = this.LB_PicList.IndexFromPoint(e.Location);
            if (index != System.Windows.Forms.ListBox.NoMatches && LB_PicList.SelectedItem.ToString() != "没有图片" && LB_PicList.SelectedItem.ToString() != "")
            {
                string pic_path = LB_PicList.SelectedItem.ToString();
                string districtname = LCPresults.Where(x => x.PicPath.Contains(pic_path)).Select(x => x.DistrictName).FirstOrDefault();
                pic_path2 = yumin + districtname + "/(" + Select_AppraisalCode + ")" + pic_path + ".jpg";

                WebClient webClient = new WebClient();
                webClient.Credentials = CredentialCache.DefaultCredentials;
                webClient.Headers.Add("User-Agent", "Microsoft Internet Explorer");
                webClient.Headers.Add("Host", "wmcscp.gsinfo.cn");
                byte[] picByte = webClient.DownloadData(pic_path2);
                MemoryStream ms = new MemoryStream(picByte);

                pictureBox1.Image = Image.FromStream(ms);

            }
            else
            {
                LB_PicList.SelectedIndex = -1;//不做任何操作，将ListBox的选中项取消
            }
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            RegistryKey key = Registry.ClassesRoot.OpenSubKey(@"http\shell\open\command\");
            string s = key.GetValue("").ToString();
            //s就是你的默认浏览器，不过后面带了参数，把它截去，不过需要注意的是：不同的浏览器后面的参数不一样！
            //"D:\Program Files (x86)\Google\Chrome\Application\chrome.exe" -- "%1"
            try
            {
                System.Diagnostics.Process.Start(s.Substring(0, s.Length - 8), pic_path2);
            }
            catch (Exception)
            {
                System.Diagnostics.Process.Start(@"IExplore.exe", pic_path2);
            }
        }

        //
        /// <summary>
        /// 改变控件在tableLayoutPanel里的位置//暂不需要跨行设置,需要时再增加
        /// </summary>
        /// <param name="tableLayoutPanel">父控件</param>
        /// <param name="control">要改变的控件</param>
        /// <param name="Row">要放的行数</param>
        /// <param name="Column">要放置的列数</param>
        /// <param name="ColumnSpan">跨列</param>
        private void changeItemsLocation(TableLayoutPanel tableLayoutPanel, Control control, int Row, int Column, int ColumnSpan)
        {
            tableLayoutPanel.Controls.Remove(control);
            tableLayoutPanel.Controls.Add(control, Row, Column);
            tableLayoutPanel.SetColumnSpan(control, ColumnSpan);
        }



        //是否单独显示
        bool If_chart_districtShowOnly = false;
        private void chart_district_DoubleClick(object sender, EventArgs e)
        {
            if (cardSumScores == null)
            {
                return;
            }
            //如果单独显示则取消单独显示
            if (If_chart_districtShowOnly)
            {
                changeItemsLocation(tableLayoutPanel1, chart_district, 3, 5, 3);
                changeItemsLocation(tableLayoutPanel1, chart_province, 0, 5, 3);
                changeItemsLocation(tableLayoutPanel1, chart_cardInDistrict, 6, 5, 3);
                changeItemsLocation(tableLayoutPanel1, label8, 1, 4, 2);
                changeItemsLocation(tableLayoutPanel1, label10, 7, 4, 2);


                chart_district.ChartAreas["ChartArea1"].AxisX.ScrollBar.Enabled = true;
                chart_district.ChartAreas["ChartArea1"].AxisX.ScaleView.Size = 10;


            }
            //否则就单独显示
            else
            {
                tableLayoutPanel1.Controls.Remove(chart_province);
                tableLayoutPanel1.Controls.Remove(chart_cardInDistrict);
                tableLayoutPanel1.Controls.Remove(label8);
                tableLayoutPanel1.Controls.Remove(label10);

                changeItemsLocation(tableLayoutPanel1, chart_district, 0, 5, 9);

                chart_district.ChartAreas["ChartArea1"].AxisX.ScaleView.Size = cardSumScores.Count;

            }
            If_chart_districtShowOnly = !If_chart_districtShowOnly;


        }
        bool If_chart_provinceShowOnly = false;
        private void chart_province_DoubleClick(object sender, EventArgs e)
        {
            if (districtSumScores == null)
            {
                return;
            }
            //如果单独显示则取消单独显示
            if (If_chart_provinceShowOnly)
            {
                changeItemsLocation(tableLayoutPanel1, chart_district, 3, 5, 3);
                changeItemsLocation(tableLayoutPanel1, chart_province, 0, 5, 3);
                changeItemsLocation(tableLayoutPanel1, chart_cardInDistrict, 6, 5, 3);
                changeItemsLocation(tableLayoutPanel1, label8, 1, 4, 2);
                changeItemsLocation(tableLayoutPanel1, label5, 4, 4, 2);
                changeItemsLocation(tableLayoutPanel1, label10, 7, 4, 2);


                chart_province.ChartAreas["ChartArea1"].AxisX.ScrollBar.Enabled = true;
                chart_province.ChartAreas["ChartArea1"].AxisX.ScaleView.Size = 10;


            }
            //否则就单独显示
            else
            {
                tableLayoutPanel1.Controls.Remove(chart_district);
                tableLayoutPanel1.Controls.Remove(chart_cardInDistrict);
                tableLayoutPanel1.Controls.Remove(label5);
                tableLayoutPanel1.Controls.Remove(label8);
                tableLayoutPanel1.Controls.Remove(label10);

                changeItemsLocation(tableLayoutPanel1, label8, 4, 4, 2);
                changeItemsLocation(tableLayoutPanel1, chart_province, 0, 5, 9);

                chart_province.ChartAreas["ChartArea1"].AxisX.ScaleView.Size = districtSumScores.Count;

            }
            If_chart_provinceShowOnly = !If_chart_provinceShowOnly;


        }
        bool If_chart_cardInDistrictShowOnly = false;
        private void chart_cardInDistrict_DoubleClick(object sender, EventArgs e)
        {
            if (cardSumScores == null)
            {
                return;
            }
            //如果单独显示则取消单独显示
            if (If_chart_cardInDistrictShowOnly)
            {
                changeItemsLocation(tableLayoutPanel1, chart_district, 3, 5, 3);
                changeItemsLocation(tableLayoutPanel1, chart_province, 0, 5, 3);
                changeItemsLocation(tableLayoutPanel1, chart_cardInDistrict, 6, 5, 3);
                changeItemsLocation(tableLayoutPanel1, label8, 1, 4, 2);
                changeItemsLocation(tableLayoutPanel1, label5, 4, 4, 2);
                changeItemsLocation(tableLayoutPanel1, label10, 7, 4, 2);


                chart_province.ChartAreas["ChartArea1"].AxisX.ScrollBar.Enabled = true;
                chart_province.ChartAreas["ChartArea1"].AxisX.ScaleView.Size = 10;


            }
            //否则就单独显示
            else
            {
                tableLayoutPanel1.Controls.Remove(chart_district);
                tableLayoutPanel1.Controls.Remove(chart_province);
                tableLayoutPanel1.Controls.Remove(label5);
                tableLayoutPanel1.Controls.Remove(label8);
                tableLayoutPanel1.Controls.Remove(label10);

                changeItemsLocation(tableLayoutPanel1, label10, 4, 4, 2);
                changeItemsLocation(tableLayoutPanel1, chart_cardInDistrict, 0, 5, 9);

                chart_cardInDistrict.ChartAreas["ChartArea1"].AxisX.ScaleView.Size = cardInDistrict.Count;

            }
            If_chart_cardInDistrictShowOnly = !If_chart_cardInDistrictShowOnly;
        }

        private void LB_CardName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
        public string WJSumScore = "0.0";
        public void ShowWJscore()
        {
            WJSumScore = "0.0";
            string url = Form1.UrlPre + "GetDisScore?AppraisalCode=" + Select_AppraisalCode + "&DistrictCode=" + Select_DistrictCode;
            string Httpres = HttpGet.HttpGetFunc(url);
            if (Httpres == null)
            {
                MessageBox.Show("网络未连接");
                return;
            }
            List<string> s1 = new List<string>();
            //将字符串转换成json
            var Httpjsonresult = JObject.Parse(Httpres);
            //获取json中的data部分
            JToken HttpJsonvalue = Httpjsonresult.GetValue("data");
            int resultrow = HttpJsonvalue.Count();

            for (int i = 0; i < resultrow; i++)
            {
                int column = HttpJsonvalue[i].Count();
                var s = HttpJsonvalue[i].ToString();
                s1.Add(JsonConvert.DeserializeObject<string>(s));

            }
            Cur_WenJuanScore = s1;
            var score = double.Parse(s1[resultrow - 1]) >= 19 ? "20" : s1[resultrow - 1];
            WJSumScore = score;
            LB_WENjuan.Text = "问卷总分 :" + score;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            //OutPutWord();
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Execl files (*.xls)|*.xls";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.CreatePrompt = true;
            saveFileDialog.Title = "Export Excel File To";
            saveFileDialog.ShowDialog();
            if (saveFileDialog.FileName == "")
            { return; }
            string saveFileName = saveFileDialog.FileName;
            if (LCPresults.Count > 0)
            {
                Microsoft.Office.Interop.Excel.Application xlApp;
                try
                {
                    xlApp = new Microsoft.Office.Interop.Excel.Application();
                    if (xlApp == null)
                    {
                        MessageBox.Show("无法创建Excel对象，可能您的机子未安装Excel");
                        return;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("无法创建Excel对象，可能您的机子未安装Excel");
                    return;
                }


                Microsoft.Office.Interop.Excel.Workbooks workbooks = xlApp.Workbooks;
                Microsoft.Office.Interop.Excel.Workbook workbook = workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
                Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[1];//取得sheet1  

                //写入标题  
                for (int i = 0; i < DGV_result.ColumnCount + 1; i++)
                {
                    int t = i;
                    switch (i)
                    {
                        case 0:
                            worksheet.Cells[1, t + 1] = "卡片名";
                            break;
                        default:
                            worksheet.Cells[1, t + 1] = DGV_result.Columns[i - 1].HeaderText;
                            break;
                    }

                }
                //String mont = jiezhi_date.Value.GetDateTimeFormats('s')[0].ToString().Substring(5, 2);
                //mont = mont.Replace("/", "");
                //int month = int.Parse(mont);
                //写入数值  


                for (int k = 0, m = 0; k < LB_CardName.Items.Count; k++)
                {

                    //LB_CardName.Text = LB_CardName.Items[k].ToString().Trim();

                    LB_CardName.SelectedIndex = k;
                    if (LB_CardName.Text.Contains("问卷"))
                    {
                        continue;
                    }
                    setdata();
                    for (int r = 0; r < DGV_result.Rows.Count; r++)
                    {

                        for (int i = 0; i < DGV_result.ColumnCount + 1; i++)
                        {
                            int t = i;
                            switch (i)
                            {
                                case 0:
                                    worksheet.Cells[m + 2, t + 1] = LB_CardName.Text;
                                    break;
                                default:
                                    worksheet.Cells[m + 2, t + 1] = DGV_result.Rows[r].Cells[i - 1].Value;
                                    break;
                            }

                        }
                        m++;
                        System.Windows.Forms.Application.DoEvents();
                    }

                    worksheet.Cells[m + 2, 1] = TB_beizhu.Text;
                    m++;
                }

                worksheet.Columns.EntireColumn.AutoFit();//列宽自适应  
                                                         //   if (Microsoft.Office.Interop.cmbxType.Text != "Notification")  
                                                         //   {  
                                                         //       Excel.Range rg = worksheet.get_Range(worksheet.Cells[2, 2], worksheet.Cells[ds.Tables[0].Rows.Count + 1, 2]);  
                                                         //      rg.NumberFormat = "00000000";  
                                                         //   }  

                if (saveFileName != "")
                {
                    try
                    {
                        workbook.Saved = true;
                        workbook.SaveCopyAs(saveFileName);

                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show("导出文件时出错,文件可能正被打开！\n" + ex.Message);
                    }

                }

                xlApp.Quit();
                GC.Collect();//强行销毁   
                             // if (fileSaved && System.IO.File.Exists(saveFileName)) System.Diagnostics.Process.Start(saveFileName); //打开EXCEL  
                MessageBox.Show("导出文件成功", "提示", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("报表为空,无表格需要导出", "提示", MessageBoxButtons.OK);
            }
        }
        //把测评结果保存为word文档
        public void OutPutWordWithOutGoodResult()
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Execl files (*.doc)|*.docx";
                saveFileDialog.FilterIndex = 0;
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.CreatePrompt = true;
                saveFileDialog.Title = "Export Excel File To";
                saveFileDialog.ShowDialog();
                if (saveFileDialog.FileName == "")
                { return; }
                string saveFileName = saveFileDialog.FileName;

                Microsoft.Office.Interop.Word.Application winword = new Microsoft.Office.Interop.Word.Application();
                winword.Visible = false;
                object missing = System.Reflection.Missing.Value;
                MessageFilter.Register();
                Microsoft.Office.Interop.Word.Document document = winword.Documents.Add(ref missing, ref missing, ref missing, ref missing);

                ////治标不治本
                //System.Threading.Thread.Sleep(2000);

                //页边距
                document.PageSetup.LeftMargin = 40; //1.41CM
                document.PageSetup.RightMargin = 40;
                document.PageSetup.TopMargin = 40;
                document.PageSetup.BottomMargin = 40;

                ////添加内容
                ////添加内容
                document.Content.SetRange(0, 0);
                double DisSumScore = double.Parse(WJSumScore) + districtSumScore;

                document.Content.Text = CB_Districts.Text + " 测评总分: " + DisSumScore + ". 其中实地考察分值为: " + districtSumScore + ". 问卷调查分值为: " + WJSumScore;




                //插入分页   
                object oEndOfDocFirst = "\\endofdoc";
                Microsoft.Office.Interop.Word.Range wrdRngFirst = document.Bookmarks.get_Item(ref oEndOfDocFirst).Range;
                object oCollapseEndFirst = Microsoft.Office.Interop.Word.WdCollapseDirection.wdCollapseEnd;
                object oPageBreakFirst = Microsoft.Office.Interop.Word.WdBreakType.wdPageBreak;
                wrdRngFirst.Collapse(ref oCollapseEndFirst);
                wrdRngFirst.InsertBreak(ref oPageBreakFirst);
                wrdRngFirst.Collapse(ref oCollapseEndFirst);
                //wrdRng.InsertAfter("We're now on page 2. Here's my chart:");
                wrdRngFirst.InsertParagraphAfter();
                ////跳转到指定书签
                //object toMark = MSWord.WdGoToItem.wdGoToBookmark;
                ////定义分页符
                //object oPageBreak = Microsoft.Office.Interop.Word.WdBreakType.wdPageBreak;
                ////插入分页符
                //document.ActiveWindow.Selection.InsertBreak(ref oPageBreak);
                ////定义书签名称  PartB
                //object BookMarkName_b = "bmf_b";
                //document.ActiveWindow.Selection.GoTo(ref toMark, ref Nothing, ref Nothing, ref BookMarkName_b);
                ////插入分页符
                //document.ActiveWindow.Selection.InsertBreak(ref oPageBreak);
                bool ifWJPrint = true;
                for (int k = 0; k < LB_CardName.Items.Count; k++)
                {

                    //LB_CardName.Text = LB_CardName.Items[k].ToString().Trim();
                    LB_CardName.SelectedIndex = k;


                    if (LB_CardName.Text.Contains("问卷"))
                    {
                        if (ifWJPrint)
                        {
                            ifWJPrint = false;
                        }
                        else if (!ifWJPrint)
                        {
                            continue;
                        }

                    }
                    //不是多余的问卷就模拟点击,让数据显示到datagridview上
                    setdata();

                    //记录当前卡片的行数(只包括基本符合和不符合的)
                    int ThisTableRows = 0;
                    for (int s = 0; s < DGV_result.Rows.Count; s++)
                    {
                        if (DGV_result.Rows[s].Cells[1].Value.ToString().Trim() != "符合")
                        {
                            ThisTableRows++;
                        }
                    }

                    if (ThisTableRows == 0)
                    {
                        continue;
                    }

                    ////word添加段落		  
                    Microsoft.Office.Interop.Word.Paragraph para1 = document.Content.Paragraphs.Add(ref missing);
                    para1.Range.Text = LB_CardName.Text;
                    para1.Range.InsertParagraphAfter();

                    // Microsoft.Office.Interop.Word.Table firstTable = document.Tables.Add(para1.Range, DGV_result.Rows.Count + 1, 3, ref missing, ref missing);
                    Microsoft.Office.Interop.Word.Table firstTable = document.Tables.Add(para1.Range, ThisTableRows + 1, 3, ref missing, ref missing);

                    firstTable.Borders.Enable = 1;
                    int r = 0;
                    foreach (Microsoft.Office.Interop.Word.Row row in firstTable.Rows)
                    {
                        //第一次建立表头,然后再开始加数据
                        if (row.Index != 1)
                        {//符合的全部略过,只显示基本符合和不符合
                            for (; r < DGV_result.Rows.Count; r++)
                            {
                                if (DGV_result.Rows[r].Cells[1].Value.ToString().Trim() != "符合")
                                {
                                    r++;
                                    break;
                                }
                            }
                        }

                        int i = 0;
                        foreach (Microsoft.Office.Interop.Word.Cell cell in row.Cells)
                        {
                            //表头
                            if (cell.RowIndex == 1)
                            {
                                switch (cell.ColumnIndex)
                                {
                                    case 1:
                                        cell.Range.Text = "内容";
                                        cell.Range.Font.Bold = 1;
                                        cell.Range.Font.Name = "verdana";
                                        cell.Range.Font.Size = 10;
                                        cell.Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorGray25;
                                        cell.VerticalAlignment = Microsoft.Office.Interop.Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                                        cell.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                                        break;
                                    case 2:

                                        cell.Range.Text = "选项";
                                        cell.Range.Font.Bold = 1;
                                        cell.Range.Font.Name = "verdana";
                                        cell.Range.Font.Size = 10;
                                        cell.Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorGray25;
                                        cell.VerticalAlignment = Microsoft.Office.Interop.Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                                        cell.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                                        break;
                                    case 3:

                                        cell.Range.Text = "得分/比例";
                                        cell.Range.Font.Bold = 1;
                                        cell.Range.Font.Name = "verdana";
                                        cell.Range.Font.Size = 10;
                                        cell.Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorGray25;
                                        cell.VerticalAlignment = Microsoft.Office.Interop.Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                                        cell.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                                        break;
                                    default:

                                        cell.Range.Text = "Column " + cell.ColumnIndex.ToString();
                                        cell.Range.Font.Bold = 1;
                                        cell.Range.Font.Name = "verdana";
                                        cell.Range.Font.Size = 10;
                                        cell.Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorGray25;
                                        cell.VerticalAlignment = Microsoft.Office.Interop.Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                                        cell.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                                        break;

                                }
                            }
                            //行
                            else
                            {
                                cell.Range.Text = DGV_result.Rows[r - 1].Cells[i].Value.ToString();

                            }
                            i++;
                        }

                    }
                    Microsoft.Office.Interop.Word.Paragraph para2 = document.Content.Paragraphs.Add(ref missing);
                    string score = LB_CardScore.Text.Replace("卡片得分: ", "");
                    para2.Range.Text = TB_beizhu.Text + " 得分:" + score;
                    para2.Range.InsertParagraphAfter();
                    ////插入图片

                    object unite = MSWord.WdUnits.wdStory;
                    Object Nothing = Missing.Value;
                    if (LB_PicList.Items[0].ToString() == "没有图片")
                    {

                    }
                    else
                    {
                        for (int v = 0; v < LB_PicList.Items.Count; v++)
                        {
                            winword.Selection.EndKey(ref unite, ref Nothing); //将光标移动到文档末尾
                                                                              //图片文件的路径
                            string filename = yumin + CB_Districts.Text.Replace("辖区", "") + "/(" + Select_AppraisalCode + ")" + LB_PicList.Items[v].ToString() + ".jpg";
                            //要向Word文档中插入图片的位置
                            Object range = document.Paragraphs.Last.Range;
                            //定义该插入的图片是否为外部链接
                            Object linkToFile = false;               //默认,这里貌似设置为bool类型更清晰一些
                            //定义要插入的图片是否随Word文档一起保存
                            Object saveWithDocument = true;              //默认
                            //使用InlineShapes.AddPicture方法(【即“嵌入型”】)插入图片
                            MSWord.InlineShape inlineShape = document.InlineShapes.AddPicture(filename, ref linkToFile, ref saveWithDocument, ref range);
                            //wordApp.Selection.ParagraphFormat.Alignment = MSWord.WdParagraphAlignment.wdAlignParagraphCenter;//居中显示图片

                            //设置图片宽高的绝对大小

                            inlineShape.Width = 250;
                            inlineShape.Height = 200;
                            ////按比例缩放大小

                            //document.InlineShapes[1].ScaleWidth = 20;//缩小到20% ？
                            //document.InlineShapes[1].ScaleHeight = 20;

                            ////在图下方居中添加图片标题
                            //document.Content.InsertAfter("\n");//这一句与下一句的顺序不能颠倒，原因还没搞透
                            //winword.Selection.EndKey(ref unite, ref Nothing);
                            //winword.Selection.ParagraphFormat.Alignment = MSWord.WdParagraphAlignment.wdAlignParagraphCenter;
                            //winword.Selection.Font.Size = 10;//字体大小
                            //winword.Selection.TypeText(CB_Districts.Text + LB_CardName.Text+"图片 "+v);
                        }
                    }






                    //插入分页   
                    object oEndOfDoc = "\\endofdoc";
                    Microsoft.Office.Interop.Word.Range wrdRng = document.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    object oCollapseEnd = Microsoft.Office.Interop.Word.WdCollapseDirection.wdCollapseEnd;
                    object oPageBreak = Microsoft.Office.Interop.Word.WdBreakType.wdPageBreak;
                    wrdRng.Collapse(ref oCollapseEnd);
                    wrdRng.InsertBreak(ref oPageBreak);
                    wrdRng.Collapse(ref oCollapseEnd);
                    //wrdRng.InsertAfter("We're now on page 2. Here's my chart:");
                    wrdRng.InsertParagraphAfter();
                    ////跳转到指定书签
                    //object toMark = MSWord.WdGoToItem.wdGoToBookmark;
                    ////定义分页符
                    //object oPageBreak = Microsoft.Office.Interop.Word.WdBreakType.wdPageBreak;
                    ////插入分页符
                    //document.ActiveWindow.Selection.InsertBreak(ref oPageBreak);
                    ////定义书签名称  PartB
                    //object BookMarkName_b = "bmf_b";
                    //document.ActiveWindow.Selection.GoTo(ref toMark, ref Nothing, ref Nothing, ref BookMarkName_b);
                    ////插入分页符
                    //document.ActiveWindow.Selection.InsertBreak(ref oPageBreak);
                }

                //表格
                //保存


                if (saveFileName != "")
                {
                    try
                    {
                        document.SaveAs(saveFileName);
                        document.Close(ref missing, ref missing, ref missing);
                        document = null;

                        winword.Quit(ref missing, ref missing, ref missing);
                        winword = null;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("导出文件时出错,文件可能正被打开！\n" + ex.Message);
                    }
                }

                //打开文件
                System.Diagnostics.Process.Start(saveFileName);


                MessageFilter.Revoke();
            }
            catch (COMException)
            {
                MessageBox.Show("当前主机尚未安装Office Excel,无法导出word");

            }

            //////
        }
        public void OutPutWord()
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Execl files (*.doc)|*.docx";
                saveFileDialog.FilterIndex = 0;
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.CreatePrompt = true;
                saveFileDialog.Title = "Export Excel File To";
                saveFileDialog.ShowDialog();
                if (saveFileDialog.FileName == "")
                { return; }
                string saveFileName = saveFileDialog.FileName;
                this.Enabled = false;
                LoadingHelper.ShowLoadingScreen();



                Microsoft.Office.Interop.Word.Application winword = new Microsoft.Office.Interop.Word.Application();
                winword.Visible = false;
                object missing = System.Reflection.Missing.Value;
                MessageFilter.Register();
                Microsoft.Office.Interop.Word.Document document = winword.Documents.Add(ref missing, ref missing, ref missing, ref missing);

                ////治标不治本
                //System.Threading.Thread.Sleep(2000);

                //页边距
                document.PageSetup.LeftMargin = 40; //1.41CM
                document.PageSetup.RightMargin = 40;
                document.PageSetup.TopMargin = 40;
                document.PageSetup.BottomMargin = 40;

                ////添加内容
                ////添加内容
                document.Content.SetRange(0, 0);
                double DisSumScore = double.Parse(WJSumScore) + districtSumScore;

                MSWord.Paragraph Title = document.Paragraphs.Add();
                document.Paragraphs.Last.Range.Font.Bold = 1;
                document.Paragraphs.Last.Range.Font.Size = 25;
                document.Paragraphs.Last.Range.ParagraphFormat.Alignment = MSWord.WdParagraphAlignment.wdAlignParagraphCenter;
                Title.Range.Text = CB_AppraisalCode.Text + " 测评详情";


                //插入分页   
                object oEndOfDocFirst = "\\endofdoc";
                Microsoft.Office.Interop.Word.Range wrdRngFirst = document.Bookmarks.get_Item(ref oEndOfDocFirst).Range;
                object oCollapseEndFirst = Microsoft.Office.Interop.Word.WdCollapseDirection.wdCollapseEnd;
                object oPageBreakFirst = Microsoft.Office.Interop.Word.WdBreakType.wdPageBreak;
                wrdRngFirst.Collapse(ref oCollapseEndFirst);
                wrdRngFirst.InsertBreak(ref oPageBreakFirst);
                wrdRngFirst.Collapse(ref oCollapseEndFirst);
                //wrdRng.InsertAfter("We're now on page 2. Here's my chart:");
                wrdRngFirst.InsertParagraphAfter();
                ////跳转到指定书签
                //object toMark = MSWord.WdGoToItem.wdGoToBookmark;
                ////定义分页符
                //object oPageBreak = Microsoft.Office.Interop.Word.WdBreakType.wdPageBreak;
                ////插入分页符
                //document.ActiveWindow.Selection.InsertBreak(ref oPageBreak);
                ////定义书签名称  PartB
                //object BookMarkName_b = "bmf_b";
                //document.ActiveWindow.Selection.GoTo(ref toMark, ref Nothing, ref Nothing, ref BookMarkName_b);
                ////插入分页符
                //document.ActiveWindow.Selection.InsertBreak(ref oPageBreak);
                bool ifWJPrint = true;
                for (int k = 0; k < LB_CardName.Items.Count; k++)
                {
                    //LB_CardName.Text = LB_CardName.Items[k].ToString().Trim();
                    LB_CardName.SelectedIndex = k;

                    //if (LB_CardName.Text.Contains("问卷"))
                    //{
                    //    if (ifWJPrint)
                    //    {
                    //        ifWJPrint = false;
                    //    }
                    //    else if (!ifWJPrint)
                    //    {
                    //        continue;
                    //    }

                    //}
                    ////不是多余的问卷就模拟点击,让数据显示到datagridview上
                    setdata();
                    ////word添加段落		  
                    Microsoft.Office.Interop.Word.Paragraph para1 = document.Content.Paragraphs.Add(ref missing);
                    document.Paragraphs.Last.Range.Font.Bold = 1;
                    document.Paragraphs.Last.Range.Font.Size = 15;
                    document.Paragraphs.Last.Range.Font.Name = "宋体";
                    document.Paragraphs.Last.Range.ParagraphFormat.Alignment = MSWord.WdParagraphAlignment.wdAlignParagraphCenter;
                    //名称
                    para1.Range.Text = LB_CardName.Text;
                    para1.Range.InsertParagraphAfter();

                    document.Paragraphs.Last.Range.Font.Bold = 0;
                    document.Paragraphs.Last.Range.Font.Size = 10;
                    document.Paragraphs.Last.Range.Font.Name = "宋体";
                    document.Paragraphs.Last.Range.ParagraphFormat.Alignment = MSWord.WdParagraphAlignment.wdAlignParagraphLeft;

                    // Microsoft.Office.Interop.Word.Table firstTable = document.Tables.Add(para1.Range, DGV_result.Rows.Count + 1, 3, ref missing, ref missing);
                    Microsoft.Office.Interop.Word.Table firstTable = document.Tables.Add(para1.Range, DGV_result.Rows.Count + 1, 3, ref missing, ref missing);

                    firstTable.Borders.Enable = 1;
                    int r = 0;
                    foreach (Microsoft.Office.Interop.Word.Row row in firstTable.Rows)
                    {
                        int i = 0;
                        foreach (Microsoft.Office.Interop.Word.Cell cell in row.Cells)
                        {
                            //表头
                            if (cell.RowIndex == 1)
                            {
                                switch (cell.ColumnIndex)
                                {
                                    case 1:
                                        cell.Range.Text = "内容";
                                        cell.Range.Font.Bold = 1;
                                        cell.Range.Font.Name = "verdana";
                                        cell.Range.Font.Size = 10;
                                        cell.Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorGray25;
                                        cell.VerticalAlignment = Microsoft.Office.Interop.Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                                        cell.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                                        break;
                                    case 2:

                                        cell.Range.Text = "选项";
                                        cell.Range.Font.Bold = 1;
                                        cell.Range.Font.Name = "verdana";
                                        cell.Range.Font.Size = 10;
                                        cell.Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorGray25;
                                        cell.VerticalAlignment = Microsoft.Office.Interop.Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                                        cell.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                                        break;
                                    case 3:

                                        cell.Range.Text = "得分/比例";
                                        cell.Range.Font.Bold = 1;
                                        cell.Range.Font.Name = "verdana";
                                        cell.Range.Font.Size = 10;
                                        cell.Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorGray25;
                                        cell.VerticalAlignment = Microsoft.Office.Interop.Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                                        cell.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                                        break;
                                    default:

                                        cell.Range.Text = "Column " + cell.ColumnIndex.ToString();
                                        cell.Range.Font.Bold = 1;
                                        cell.Range.Font.Name = "verdana";
                                        cell.Range.Font.Size = 10;
                                        cell.Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorGray25;
                                        cell.VerticalAlignment = Microsoft.Office.Interop.Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                                        cell.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                                        break;
                                }
                            }
                            //行
                            else
                            {
                                cell.Range.Text = DGV_result.Rows[r - 1].Cells[i].Value.ToString();

                            }
                            i++;
                        }
                        r++;
                    }
                    Microsoft.Office.Interop.Word.Paragraph para2 = document.Content.Paragraphs.Add(ref missing);
                    para2.Range.Text = TB_beizhu.Text;
                    para2.Range.InsertParagraphAfter();
                    ////插入图片

                    object unite = MSWord.WdUnits.wdStory;
                    Object Nothing = Missing.Value;
                    if (LB_PicList.Items[0].ToString() == "没有图片")
                    {

                    }
                    else
                    {
                        for (int v = 0; v < LB_PicList.Items.Count; v++)
                        {
                            winword.Selection.EndKey(ref unite, ref Nothing); //将光标移动到文档末尾
                                                                              //图片文件的路径
                            string filename = yumin + CB_Districts.Text.Replace("辖区", "") + "/(" + Select_AppraisalCode + ")" + LB_PicList.Items[v].ToString() + ".jpg";
                            //要向Word文档中插入图片的位置
                            Object range = document.Paragraphs.Last.Range;
                            //定义该插入的图片是否为外部链接
                            Object linkToFile = false;               //默认,这里貌似设置为bool类型更清晰一些
                            //定义要插入的图片是否随Word文档一起保存
                            Object saveWithDocument = true;              //默认
                            //使用InlineShapes.AddPicture方法(【即“嵌入型”】)插入图片
                            MSWord.InlineShape inlineShape = document.InlineShapes.AddPicture(filename, ref linkToFile, ref saveWithDocument, ref range);
                            //wordApp.Selection.ParagraphFormat.Alignment = MSWord.WdParagraphAlignment.wdAlignParagraphCenter;//居中显示图片

                            //设置图片宽高的绝对大小

                            inlineShape.Width = 250;
                            inlineShape.Height = 200;
                            ////按比例缩放大小

                            //document.InlineShapes[1].ScaleWidth = 20;//缩小到20% ？
                            //document.InlineShapes[1].ScaleHeight = 20;

                            //////在图下方居中添加图片标题
                            ////document.Content.InsertAfter("\n");//这一句与下一句的顺序不能颠倒，原因还没搞透
                            ////winword.Selection.EndKey(ref unite, ref Nothing);
                            ////winword.Selection.ParagraphFormat.Alignment = MSWord.WdParagraphAlignment.wdAlignParagraphCenter;
                            ////winword.Selection.Font.Size = 10;//字体大小
                            ////winword.Selection.TypeText(CB_Districts.Text + LB_CardName.Text+"图片 "+v);
                        }
                    }
                    //插入分页   
                    object oEndOfDoc = "\\endofdoc";
                    Microsoft.Office.Interop.Word.Range wrdRng = document.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    object oCollapseEnd = Microsoft.Office.Interop.Word.WdCollapseDirection.wdCollapseEnd;
                    object oPageBreak = Microsoft.Office.Interop.Word.WdBreakType.wdPageBreak;
                    wrdRng.Collapse(ref oCollapseEnd);
                    wrdRng.InsertBreak(ref oPageBreak);
                    wrdRng.Collapse(ref oCollapseEnd);
                    //wrdRng.InsertAfter("We're now on page 2. Here's my chart:");
                    wrdRng.InsertParagraphAfter();
                    ////跳转到指定书签
                    //object toMark = MSWord.WdGoToItem.wdGoToBookmark;
                    ////定义分页符
                    //object oPageBreak = Microsoft.Office.Interop.Word.WdBreakType.wdPageBreak;
                    ////插入分页符
                    //document.ActiveWindow.Selection.InsertBreak(ref oPageBreak);
                    ////定义书签名称  PartB
                    //object BookMarkName_b = "bmf_b";
                    //document.ActiveWindow.Selection.GoTo(ref toMark, ref Nothing, ref Nothing, ref BookMarkName_b);
                    ////插入分页符
                    //document.ActiveWindow.Selection.InsertBreak(ref oPageBreak);
                }

                //表格
                //保存


                if (saveFileName != "")
                {
                    try
                    {
                        document.SaveAs(saveFileName);
                        document.Close(ref missing, ref missing, ref missing);
                        document = null;

                        winword.Quit(ref missing, ref missing, ref missing);
                        winword = null;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("导出文件时出错,文件可能正被打开！\n" + ex.Message);
                    }
                }

                //打开文件
                System.Diagnostics.Process.Start(saveFileName);


                MessageFilter.Revoke();
                LoadingHelper.CloseForm(); this.Enabled = true;
            }
            catch (COMException)
            {
                MessageBox.Show("当前主机尚未安装Office Excel,无法导出word");

            }
        }

        private static void CreateFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                using (FileStream fs = File.Create(filePath))
                {

                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            OutPutWordWithOutGoodResult();
            //OutPutWord();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            PUBdistrictName = CB_Districts.Text;
            SumScoreForm sumScoreForm = new SumScoreForm();
            sumScoreForm.Show();
            //string url = Form1.UrlPre + "checkiffinish?districtCode=" + Select_DistrictCode;
            //string Httpres = HttpGet.HttpGetFunc(url);
            //if (Httpres == null)
            //{
            //    MessageBox.Show("网络未连接");
            //    return;
            //}

            ////将字符串转换成json
            //var Httpjsonresult = JObject.Parse(Httpres);
            ////获取json中的data部分
            //JToken HttpJsonvalue = Httpjsonresult.GetValue("data");
            //int resultrow = HttpJsonvalue.Count();
            //List<CardAllotDatum> LcardAllotDatum = new List<CardAllotDatum>();
            //for (int i = 0; i < resultrow; i++)
            //{
            //    int column = HttpJsonvalue[i].Count();
            //    var s = HttpJsonvalue[i].ToString();
            //    CardAllotDatum cardAllotDatum = JsonConvert.DeserializeObject<CardAllotDatum>(s);
            //    LcardAllotDatum.Add(cardAllotDatum);
            //}
            //SaveFileDialog saveFileDialog = new SaveFileDialog();
            //saveFileDialog.Filter = "Execl files (*.xls)|*.xls";
            //saveFileDialog.FilterIndex = 0;
            //saveFileDialog.RestoreDirectory = true;
            //saveFileDialog.CreatePrompt = true;
            //saveFileDialog.Title = "Export Excel File To";
            //saveFileDialog.ShowDialog();
            //if (saveFileDialog.FileName == "")
            //{ return; }
            //string saveFileName = saveFileDialog.FileName;
            //if (LCPresults.Count > 0)
            //{
            //    Microsoft.Office.Interop.Excel.Application xlApp;
            //    try
            //    {
            //        xlApp = new Microsoft.Office.Interop.Excel.Application();
            //        if (xlApp == null)
            //        {
            //            MessageBox.Show("无法创建Excel对象，可能您的机子未安装Excel");
            //            return;
            //        }
            //    }
            //    catch (Exception)
            //    {
            //        MessageBox.Show("无法创建Excel对象，可能您的机子未安装Excel");
            //        return;
            //    }
            //    Microsoft.Office.Interop.Excel.Workbooks workbooks = xlApp.Workbooks;
            //    Microsoft.Office.Interop.Excel.Workbook workbook = workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
            //    Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[1];//取得sheet1  

            //    //写入标题  
            //    for (int i = 0; i < 2; i++)
            //    {
            //        int t = i;
            //        switch (i)
            //        {
            //            case 0:
            //                worksheet.Cells[1, t + 1] = "卡片名";
            //                break;
            //            default:
            //                worksheet.Cells[1, t + 1] = "当前提交数";
            //                break;
            //        }
            //    }
            //    //String mont = jiezhi_date.Value.GetDateTimeFormats('s')[0].ToString().Substring(5, 2);
            //    //mont = mont.Replace("/", "");
            //    //int month = int.Parse(mont);
            //    //写入数值  
            //    for (int k = 0; k < LcardAllotDatum.Count; k++)
            //    {
            //        for (int i = 0; i < 2; i++)
            //        {
            //            switch (i)
            //            {
            //                case 0:
            //                    worksheet.Cells[k + 2, i + 1] = LcardAllotDatum[k].CardName;
            //                    break;
            //                default:
            //                    worksheet.Cells[k + 2, i + 1] = LcardAllotDatum[k].CardCurCount;
            //                    break;
            //            }
            //        }
            //        System.Windows.Forms.Application.DoEvents();
            //    }

            //    worksheet.Columns.EntireColumn.AutoFit();//列宽自适应  
            //                                             //   if (Microsoft.Office.Interop.cmbxType.Text != "Notification")  
            //                                             //   {  
            //                                             //       Excel.Range rg = worksheet.get_Range(worksheet.Cells[2, 2], worksheet.Cells[ds.Tables[0].Rows.Count + 1, 2]);  
            //                                             //      rg.NumberFormat = "00000000";  
            //                                             //   }  
            //    if (saveFileName != "")
            //    {
            //        try
            //        {
            //            workbook.Saved = true;
            //            workbook.SaveCopyAs(saveFileName);
            //        }
            //        catch (Exception ex)
            //        {
            //            MessageBox.Show("导出文件时出错,文件可能正被打开！\n" + ex.Message);
            //        }
            //    }
            //    xlApp.Quit();
            //    GC.Collect();//强行销毁   
            //                 // if (fileSaved && System.IO.File.Exists(saveFileName)) System.Diagnostics.Process.Start(saveFileName); //打开EXCEL  
            //    MessageBox.Show("导出文件成功", "提示", MessageBoxButtons.OK);
            //}
            //else
            //{
            //    MessageBox.Show("报表为空,无表格需要导出", "提示", MessageBoxButtons.OK);
            //}
        }
    }

}
