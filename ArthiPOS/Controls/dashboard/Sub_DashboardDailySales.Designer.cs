namespace ArthiPOS.Controls.dashboard
{
    partial class Sub_DashboardDailySales
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint1 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.label1 = new System.Windows.Forms.Label();
            this.pie = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label2 = new System.Windows.Forms.Label();
            this.pan_Date = new System.Windows.Forms.Panel();
            this.date_last = new MetroFramework.Controls.MetroDateTime();
            this.date_start = new MetroFramework.Controls.MetroDateTime();
            this.btn_add_client = new Bunifu.Framework.UI.BunifuImageButton();
            this._lbl_from = new System.Windows.Forms.Label();
            this._check_date = new MetroFramework.Controls.MetroCheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cartesianChart1 = new LiveCharts.WinForms.CartesianChart();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.pieChart1 = new LiveCharts.WinForms.PieChart();
            ((System.ComponentModel.ISupportInitialize)(this.pie)).BeginInit();
            this.pan_Date.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btn_add_client)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(1009, 374);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label1.Size = new System.Drawing.Size(125, 22);
            this.label1.TabIndex = 4;
            this.label1.Text = "Daily Total Sales";
            // 
            // pie
            // 
            chartArea1.Name = "ChartArea1";
            this.pie.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.pie.Legends.Add(legend1);
            this.pie.Location = new System.Drawing.Point(527, 66);
            this.pie.Name = "pie";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.pie.Series.Add(series1);
            this.pie.Size = new System.Drawing.Size(387, 311);
            this.pie.TabIndex = 15;
            this.pie.Text = "chart2";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(946, 4);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label2.Size = new System.Drawing.Size(188, 22);
            this.label2.TabIndex = 16;
            this.label2.Text = "Month Sales/Purchases";
            // 
            // pan_Date
            // 
            this.pan_Date.Controls.Add(this.date_last);
            this.pan_Date.Controls.Add(this.date_start);
            this.pan_Date.Enabled = false;
            this.pan_Date.Location = new System.Drawing.Point(3, 26);
            this.pan_Date.Name = "pan_Date";
            this.pan_Date.Size = new System.Drawing.Size(232, 34);
            this.pan_Date.TabIndex = 251;
            // 
            // date_last
            // 
            this.date_last.CustomFormat = "yyyy-MM-dd";
            this.date_last.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.date_last.Location = new System.Drawing.Point(121, 1);
            this.date_last.MinimumSize = new System.Drawing.Size(0, 29);
            this.date_last.Name = "date_last";
            this.date_last.Size = new System.Drawing.Size(111, 29);
            this.date_last.TabIndex = 250;
            // 
            // date_start
            // 
            this.date_start.CustomFormat = "yyyy-MM-dd";
            this.date_start.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.date_start.Location = new System.Drawing.Point(3, 1);
            this.date_start.MinimumSize = new System.Drawing.Size(0, 29);
            this.date_start.Name = "date_start";
            this.date_start.Size = new System.Drawing.Size(111, 29);
            this.date_start.TabIndex = 249;
            // 
            // btn_add_client
            // 
            this.btn_add_client.BackColor = System.Drawing.Color.Transparent;
            this.btn_add_client.Image = global::ArthiPOS.Properties.Resources.search;
            this.btn_add_client.ImageActive = null;
            this.btn_add_client.Location = new System.Drawing.Point(241, 29);
            this.btn_add_client.Name = "btn_add_client";
            this.btn_add_client.Size = new System.Drawing.Size(30, 27);
            this.btn_add_client.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btn_add_client.TabIndex = 254;
            this.btn_add_client.TabStop = false;
            this.btn_add_client.Zoom = 10;
            this.btn_add_client.Click += new System.EventHandler(this.btn_add_client_Click);
            // 
            // _lbl_from
            // 
            this._lbl_from.AutoSize = true;
            this._lbl_from.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lbl_from.Location = new System.Drawing.Point(124, 4);
            this._lbl_from.Name = "_lbl_from";
            this._lbl_from.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this._lbl_from.Size = new System.Drawing.Size(38, 18);
            this._lbl_from.TabIndex = 251;
            this._lbl_from.Text = "Last";
            // 
            // _check_date
            // 
            this._check_date.AutoSize = true;
            this._check_date.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._check_date.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this._check_date.Location = new System.Drawing.Point(181, 3);
            this._check_date.Name = "_check_date";
            this._check_date.Size = new System.Drawing.Size(54, 19);
            this._check_date.TabIndex = 253;
            this._check_date.Text = "Date";
            this._check_date.UseSelectable = true;
            this._check_date.CheckedChanged += new System.EventHandler(this._check_date_CheckedChanged);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(204, 393);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(650, 3);
            this.label3.TabIndex = 254;
            this.label3.Text = "label3";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(6, 4);
            this.label4.Name = "label4";
            this.label4.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label4.Size = new System.Drawing.Size(42, 18);
            this.label4.TabIndex = 255;
            this.label4.Text = "Start";
            // 
            // cartesianChart1
            // 
            this.cartesianChart1.BackColor = System.Drawing.Color.Transparent;
            this.cartesianChart1.Location = new System.Drawing.Point(79, 743);
            this.cartesianChart1.Name = "cartesianChart1";
            this.cartesianChart1.Size = new System.Drawing.Size(867, 310);
            this.cartesianChart1.TabIndex = 256;
            this.cartesianChart1.Text = "cartesianChart1";
            // 
            // chart1
            // 
            chartArea2.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chart1.Legends.Add(legend2);
            this.chart1.Location = new System.Drawing.Point(3, 399);
            this.chart1.Name = "chart1";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
            series2.Legend = "Legend1";
            series2.Name = "Sales";
            series2.Points.Add(dataPoint1);
            series2.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Date;
            series2.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Balance";
            this.chart1.Series.Add(series2);
            this.chart1.Series.Add(series3);
            this.chart1.Size = new System.Drawing.Size(1131, 338);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            this.chart1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.chart_MouseMove);
            // 
            // pieChart1
            // 
            this.pieChart1.BackColor = System.Drawing.Color.Transparent;
            this.pieChart1.Location = new System.Drawing.Point(35, 93);
            this.pieChart1.Name = "pieChart1";
            this.pieChart1.Size = new System.Drawing.Size(401, 284);
            this.pieChart1.TabIndex = 257;
            this.pieChart1.Text = "pieChart1";
            // 
            // Sub_DashboardDailySales
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pieChart1);
            this.Controls.Add(this.cartesianChart1);
            this.Controls.Add(this.btn_add_client);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this._check_date);
            this.Controls.Add(this.pan_Date);
            this.Controls.Add(this._lbl_from);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pie);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chart1);
            this.Name = "Sub_DashboardDailySales";
            this.Size = new System.Drawing.Size(1139, 1110);
            this.Load += new System.EventHandler(this.Sub_DashboardDailySales_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pie)).EndInit();
            this.pan_Date.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btn_add_client)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataVisualization.Charting.Chart pie;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel pan_Date;
        private Bunifu.Framework.UI.BunifuImageButton btn_add_client;
        private System.Windows.Forms.Label _lbl_from;
        private MetroFramework.Controls.MetroDateTime date_last;
        private MetroFramework.Controls.MetroDateTime date_start;
        private MetroFramework.Controls.MetroCheckBox _check_date;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private LiveCharts.WinForms.CartesianChart cartesianChart1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private LiveCharts.WinForms.PieChart pieChart1;
    }
}
