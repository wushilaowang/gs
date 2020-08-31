namespace gswmgzback
{
    partial class ResultShow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.CB_AppraisalCode = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.DTP_startTime = new System.Windows.Forms.DateTimePicker();
            this.DTP_endTime = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.DGV_result = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chart_province = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chart_district = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.CB_Districts = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.LB_CardName = new System.Windows.Forms.ListBox();
            this.LB_CardTitle = new System.Windows.Forms.Label();
            this.LB_DistrictScore = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.LB_CardScore = new System.Windows.Forms.Label();
            this.TB_beizhu = new System.Windows.Forms.TextBox();
            this.LB_PicList = new System.Windows.Forms.ListBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.chart_cardInDistrict = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.score = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.result = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cardItem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label10 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label11 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.LB_WENjuan = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_result)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_province)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_district)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_cardInDistrict)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // CB_AppraisalCode
            // 
            this.CB_AppraisalCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CB_AppraisalCode.FormattingEnabled = true;
            this.CB_AppraisalCode.Location = new System.Drawing.Point(165, 5);
            this.CB_AppraisalCode.Margin = new System.Windows.Forms.Padding(5);
            this.CB_AppraisalCode.Name = "CB_AppraisalCode";
            this.CB_AppraisalCode.Size = new System.Drawing.Size(211, 28);
            this.CB_AppraisalCode.TabIndex = 0;
            this.CB_AppraisalCode.SelectedIndexChanged += new System.EventHandler(this.CB_AppraisalCode_SelectedIndexChanged);
            this.CB_AppraisalCode.MouseClick += new System.Windows.Forms.MouseEventHandler(this.CB_AppraisalCode_MouseClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("宋体", 10F);
            this.label1.Location = new System.Drawing.Point(5, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 42);
            this.label1.TabIndex = 1;
            this.label1.Text = "请选择测评编号";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // DTP_startTime
            // 
            this.DTP_startTime.CustomFormat = "yyyy-MM-dd";
            this.DTP_startTime.Enabled = false;
            this.DTP_startTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTP_startTime.Location = new System.Drawing.Point(1029, 5);
            this.DTP_startTime.Margin = new System.Windows.Forms.Padding(5);
            this.DTP_startTime.Name = "DTP_startTime";
            this.DTP_startTime.Size = new System.Drawing.Size(150, 30);
            this.DTP_startTime.TabIndex = 2;
            // 
            // DTP_endTime
            // 
            this.DTP_endTime.CustomFormat = "yyyy-MM-dd";
            this.DTP_endTime.Dock = System.Windows.Forms.DockStyle.Left;
            this.DTP_endTime.Enabled = false;
            this.DTP_endTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTP_endTime.Location = new System.Drawing.Point(1371, 5);
            this.DTP_endTime.Margin = new System.Windows.Forms.Padding(5);
            this.DTP_endTime.Name = "DTP_endTime";
            this.DTP_endTime.Size = new System.Drawing.Size(150, 30);
            this.DTP_endTime.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("宋体", 15F);
            this.label2.Location = new System.Drawing.Point(1200, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(161, 42);
            this.label2.TabIndex = 4;
            this.label2.Text = "结束时间";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("宋体", 15F);
            this.label3.Location = new System.Drawing.Point(858, 0);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(161, 42);
            this.label3.TabIndex = 5;
            this.label3.Text = "开始时间";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // DGV_result
            // 
            this.DGV_result.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.DGV_result.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV_result.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            this.tableLayoutPanel1.SetColumnSpan(this.DGV_result, 3);
            this.DGV_result.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DGV_result.Location = new System.Drawing.Point(163, 87);
            this.DGV_result.Name = "DGV_result";
            this.DGV_result.RowHeadersVisible = false;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGV_result.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.tableLayoutPanel1.SetRowSpan(this.DGV_result, 2);
            this.DGV_result.RowTemplate.Height = 23;
            this.DGV_result.Size = new System.Drawing.Size(516, 415);
            this.DGV_result.TabIndex = 7;
            this.DGV_result.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.DGV_result_CellPainting);
            this.DGV_result.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(this.DGV_result_RowStateChanged);
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.HeaderText = "内容";
            this.Column1.Name = "Column1";
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "选项";
            this.Column2.Name = "Column2";
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "得分/比例";
            this.Column3.Name = "Column3";
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // chart_province
            // 
            chartArea3.AxisX.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea3.AxisX.IsLabelAutoFit = false;
            chartArea3.AxisX.LabelStyle.IsStaggered = true;
            chartArea3.Name = "ChartArea1";
            this.chart_province.ChartAreas.Add(chartArea3);
            this.tableLayoutPanel1.SetColumnSpan(this.chart_province, 3);
            this.chart_province.Dock = System.Windows.Forms.DockStyle.Fill;
            legend3.Name = "Legend1";
            this.chart_province.Legends.Add(legend3);
            this.chart_province.Location = new System.Drawing.Point(3, 550);
            this.chart_province.Name = "chart_province";
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            this.chart_province.Series.Add(series3);
            this.chart_province.Size = new System.Drawing.Size(490, 292);
            this.chart_province.TabIndex = 8;
            this.chart_province.Text = "省内所有区域评分";
            this.chart_province.DoubleClick += new System.EventHandler(this.chart_province_DoubleClick);
            // 
            // chart_district
            // 
            chartArea2.AxisX.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea2.AxisX.IsLabelAutoFit = false;
            chartArea2.AxisX.LabelStyle.IsStaggered = true;
            chartArea2.Name = "ChartArea1";
            this.chart_district.ChartAreas.Add(chartArea2);
            this.tableLayoutPanel1.SetColumnSpan(this.chart_district, 3);
            this.chart_district.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.Name = "Legend1";
            this.chart_district.Legends.Add(legend2);
            this.chart_district.Location = new System.Drawing.Point(499, 550);
            this.chart_district.Name = "chart_district";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chart_district.Series.Add(series2);
            this.chart_district.Size = new System.Drawing.Size(522, 292);
            this.chart_district.TabIndex = 9;
            this.chart_district.Text = "区域内评分";
            this.chart_district.DoubleClick += new System.EventHandler(this.chart_district_DoubleClick);
            // 
            // CB_Districts
            // 
            this.CB_Districts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CB_Districts.FormattingEnabled = true;
            this.CB_Districts.Location = new System.Drawing.Point(501, 5);
            this.CB_Districts.Margin = new System.Windows.Forms.Padding(5);
            this.CB_Districts.Name = "CB_Districts";
            this.CB_Districts.Size = new System.Drawing.Size(176, 28);
            this.CB_Districts.TabIndex = 10;
            this.CB_Districts.SelectedIndexChanged += new System.EventHandler(this.CB_Districts_SelectedIndexChanged);
            this.CB_Districts.MouseClick += new System.Windows.Forms.MouseEventHandler(this.CB_Districts_MouseClick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("宋体", 15F);
            this.label4.Location = new System.Drawing.Point(386, 0);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 42);
            this.label4.TabIndex = 11;
            this.label4.Text = "请选择测评地点";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LB_CardName
            // 
            this.LB_CardName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LB_CardName.Font = new System.Drawing.Font("宋体", 9F);
            this.LB_CardName.FormattingEnabled = true;
            this.LB_CardName.ItemHeight = 12;
            this.LB_CardName.Location = new System.Drawing.Point(3, 87);
            this.LB_CardName.Name = "LB_CardName";
            this.tableLayoutPanel1.SetRowSpan(this.LB_CardName, 2);
            this.LB_CardName.Size = new System.Drawing.Size(154, 415);
            this.LB_CardName.TabIndex = 12;
            this.LB_CardName.MouseClick += new System.Windows.Forms.MouseEventHandler(this.LB_CardName_MouseClick);
            this.LB_CardName.SelectedIndexChanged += new System.EventHandler(this.LB_CardName_SelectedIndexChanged);
            // 
            // LB_CardTitle
            // 
            this.LB_CardTitle.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.LB_CardTitle, 2);
            this.LB_CardTitle.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.LB_CardTitle.Font = new System.Drawing.Font("宋体", 15F);
            this.LB_CardTitle.Location = new System.Drawing.Point(165, 64);
            this.LB_CardTitle.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.LB_CardTitle.Name = "LB_CardTitle";
            this.LB_CardTitle.Size = new System.Drawing.Size(326, 20);
            this.LB_CardTitle.TabIndex = 13;
            this.LB_CardTitle.Text = "卡片详情";
            this.LB_CardTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LB_DistrictScore
            // 
            this.LB_DistrictScore.AutoSize = true;
            this.LB_DistrictScore.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LB_DistrictScore.Font = new System.Drawing.Font("宋体", 15F);
            this.LB_DistrictScore.Location = new System.Drawing.Point(687, 0);
            this.LB_DistrictScore.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.LB_DistrictScore.Name = "LB_DistrictScore";
            this.LB_DistrictScore.Size = new System.Drawing.Size(161, 42);
            this.LB_DistrictScore.TabIndex = 14;
            this.LB_DistrictScore.Text = "总分";
            this.LB_DistrictScore.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label7.Font = new System.Drawing.Font("宋体", 15F);
            this.label7.Location = new System.Drawing.Point(5, 64);
            this.label7.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(150, 20);
            this.label7.TabIndex = 15;
            this.label7.Text = "卡片名";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label5, 2);
            this.label5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label5.Font = new System.Drawing.Font("宋体", 15F);
            this.label5.Location = new System.Drawing.Point(687, 527);
            this.label5.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(332, 20);
            this.label5.TabIndex = 16;
            this.label5.Text = "地区内项目得分情况";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label8, 2);
            this.label8.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label8.Font = new System.Drawing.Font("宋体", 15F);
            this.label8.Location = new System.Drawing.Point(165, 527);
            this.label8.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(326, 20);
            this.label8.TabIndex = 17;
            this.label8.Text = "所有测评点总览";
            // 
            // LB_CardScore
            // 
            this.LB_CardScore.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.LB_CardScore.Font = new System.Drawing.Font("宋体", 15F);
            this.LB_CardScore.Location = new System.Drawing.Point(499, 64);
            this.LB_CardScore.Name = "LB_CardScore";
            this.LB_CardScore.Size = new System.Drawing.Size(180, 20);
            this.LB_CardScore.TabIndex = 18;
            this.LB_CardScore.Text = "卡片得分";
            this.LB_CardScore.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TB_beizhu
            // 
            this.TB_beizhu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TB_beizhu.Location = new System.Drawing.Point(1369, 87);
            this.TB_beizhu.Multiline = true;
            this.TB_beizhu.Name = "TB_beizhu";
            this.tableLayoutPanel1.SetRowSpan(this.TB_beizhu, 2);
            this.TB_beizhu.Size = new System.Drawing.Size(168, 415);
            this.TB_beizhu.TabIndex = 19;
            // 
            // LB_PicList
            // 
            this.LB_PicList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LB_PicList.FormattingEnabled = true;
            this.LB_PicList.ItemHeight = 20;
            this.LB_PicList.Location = new System.Drawing.Point(685, 87);
            this.LB_PicList.Name = "LB_PicList";
            this.tableLayoutPanel1.SetRowSpan(this.LB_PicList, 2);
            this.LB_PicList.Size = new System.Drawing.Size(165, 415);
            this.LB_PicList.TabIndex = 20;
            this.LB_PicList.MouseClick += new System.Windows.Forms.MouseEventHandler(this.LB_PicList_MouseClick);
            this.LB_PicList.SelectedIndexChanged += new System.EventHandler(this.LB_PicList_SelectedIndexChanged);
            // 
            // pictureBox1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.pictureBox1, 3);
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(856, 87);
            this.pictureBox1.Name = "pictureBox1";
            this.tableLayoutPanel1.SetRowSpan(this.pictureBox1, 2);
            this.pictureBox1.Size = new System.Drawing.Size(507, 415);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 21;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.DoubleClick += new System.EventHandler(this.pictureBox1_DoubleClick);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("宋体", 15F);
            this.label6.Location = new System.Drawing.Point(687, 42);
            this.label6.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(161, 42);
            this.label6.TabIndex = 22;
            this.label6.Text = "图片列表";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Font = new System.Drawing.Font("宋体", 15F);
            this.label9.Location = new System.Drawing.Point(1029, 42);
            this.label9.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(161, 42);
            this.label9.TabIndex = 23;
            this.label9.Text = "图片";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chart_cardInDistrict
            // 
            chartArea1.AxisX.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea1.AxisX.IsLabelAutoFit = false;
            chartArea1.AxisX.LabelStyle.IsStaggered = true;
            chartArea1.Name = "ChartArea1";
            this.chart_cardInDistrict.ChartAreas.Add(chartArea1);
            this.tableLayoutPanel1.SetColumnSpan(this.chart_cardInDistrict, 3);
            this.chart_cardInDistrict.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chart_cardInDistrict.Legends.Add(legend1);
            this.chart_cardInDistrict.Location = new System.Drawing.Point(1027, 550);
            this.chart_cardInDistrict.Name = "chart_cardInDistrict";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart_cardInDistrict.Series.Add(series1);
            this.chart_cardInDistrict.Size = new System.Drawing.Size(510, 292);
            this.chart_cardInDistrict.TabIndex = 24;
            this.chart_cardInDistrict.Text = "省内所有区域评分";
            this.chart_cardInDistrict.DoubleClick += new System.EventHandler(this.chart_cardInDistrict_DoubleClick);
            // 
            // score
            // 
            this.score.HeaderText = "得分";
            this.score.Name = "score";
            this.score.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.score.Width = 80;
            // 
            // result
            // 
            this.result.HeaderText = "选项";
            this.result.Name = "result";
            this.result.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // cardItem
            // 
            this.cardItem.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.cardItem.HeaderText = "内容";
            this.cardItem.Name = "cardItem";
            this.cardItem.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label10, 2);
            this.label10.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label10.Font = new System.Drawing.Font("宋体", 15F);
            this.label10.Location = new System.Drawing.Point(1200, 527);
            this.label10.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(335, 20);
            this.label10.TabIndex = 25;
            this.label10.Text = "所有地区卡片得分情况";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 9;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.42261F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.37057F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.531129F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.11182F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.11278F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.11278F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.11278F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.11278F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.11278F));
            this.tableLayoutPanel1.Controls.Add(this.chart_cardInDistrict, 6, 5);
            this.tableLayoutPanel1.Controls.Add(this.label10, 7, 4);
            this.tableLayoutPanel1.Controls.Add(this.chart_district, 3, 5);
            this.tableLayoutPanel1.Controls.Add(this.CB_AppraisalCode, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.chart_province, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.label4, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label5, 4, 4);
            this.tableLayoutPanel1.Controls.Add(this.label8, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox1, 5, 2);
            this.tableLayoutPanel1.Controls.Add(this.CB_Districts, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label6, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.LB_DistrictScore, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.LB_CardName, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.DTP_startTime, 6, 0);
            this.tableLayoutPanel1.Controls.Add(this.DGV_result, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label2, 7, 0);
            this.tableLayoutPanel1.Controls.Add(this.LB_CardScore, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.DTP_endTime, 8, 0);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.LB_CardTitle, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.LB_PicList, 4, 2);
            this.tableLayoutPanel1.Controls.Add(this.TB_beizhu, 8, 2);
            this.tableLayoutPanel1.Controls.Add(this.label11, 8, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label9, 6, 1);
            this.tableLayoutPanel1.Controls.Add(this.button2, 5, 1);
            this.tableLayoutPanel1.Controls.Add(this.LB_WENjuan, 3, 4);
            this.tableLayoutPanel1.Controls.Add(this.button1, 7, 1);
            this.tableLayoutPanel1.Controls.Add(this.button3, 0, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1540, 845);
            this.tableLayoutPanel1.TabIndex = 26;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Location = new System.Drawing.Point(1369, 42);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(168, 42);
            this.label11.TabIndex = 26;
            this.label11.Text = "备注";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(856, 45);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(164, 36);
            this.button2.TabIndex = 28;
            this.button2.Text = "导出该县数据";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // LB_WENjuan
            // 
            this.LB_WENjuan.AutoSize = true;
            this.LB_WENjuan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LB_WENjuan.Location = new System.Drawing.Point(499, 505);
            this.LB_WENjuan.Name = "LB_WENjuan";
            this.LB_WENjuan.Size = new System.Drawing.Size(180, 42);
            this.LB_WENjuan.TabIndex = 29;
            this.LB_WENjuan.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1198, 45);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(165, 36);
            this.button1.TabIndex = 30;
            this.button1.Text = "导出word文件";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(3, 508);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(154, 36);
            this.button3.TabIndex = 31;
            this.button3.Text = "查看测评总分";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // ResultShow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1540, 845);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("宋体", 15F);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "ResultShow";
            this.Text = "ResultShow";
            this.Load += new System.EventHandler(this.ResultShow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGV_result)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_province)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_district)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_cardInDistrict)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox CB_AppraisalCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker DTP_startTime;
        private System.Windows.Forms.DateTimePicker DTP_endTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView DGV_result;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_province;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_district;
        private System.Windows.Forms.ComboBox CB_Districts;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox LB_CardName;
        private System.Windows.Forms.Label LB_CardTitle;
        private System.Windows.Forms.Label LB_DistrictScore;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label LB_CardScore;
        private System.Windows.Forms.TextBox TB_beizhu;
        private System.Windows.Forms.ListBox LB_PicList;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_cardInDistrict;
        private System.Windows.Forms.DataGridViewTextBoxColumn score;
        private System.Windows.Forms.DataGridViewTextBoxColumn result;
        private System.Windows.Forms.DataGridViewTextBoxColumn cardItem;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label LB_WENjuan;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
    }
}