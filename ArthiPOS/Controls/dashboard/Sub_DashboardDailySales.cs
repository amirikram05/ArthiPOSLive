using BAL;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Axis = LiveCharts.Wpf.Axis;
using SeriesCollection = LiveCharts.SeriesCollection;

namespace ArthiPOS.Controls.dashboard
{
    public partial class Sub_DashboardDailySales : UserControl
    {
        public string date;
        BLogic bal;
        public Sub_DashboardDailySales()
        {
            InitializeComponent();
            bal = new BLogic();
            pan_Date.Enabled = false;
        }

        private void Sub_DashboardDailySales_Load(object sender, EventArgs e)
        {
            this.chart1.Series.Clear();

            DataTable dt = bal.getAllSales_ProfitDetail("", "");
            DrawPieChart(dt);
            loadBarchart("", "");
            loadCartisian("", "");
            this.chart1.Palette = ChartColorPalette.BrightPastel;



            //addX_Axis(sale);
            //addX_Axis(balance);


        }
        public void loadBarchart(string sdate, string ldate)
        {
            DataTable sales20 = bal.getDashboardSales20(sdate, ldate);
            DataTable cash20 = bal.getDashboardCash20(sdate, ldate);
            // Set title
            this.chart1.Series.Clear();
            this.chart1.Titles.Clear();
            this.chart1.Titles.Add("Sales/Recevings");
            System.Windows.Forms.DataVisualization.Charting.Series sale = this.chart1.Series.Add("Sales");
            System.Windows.Forms.DataVisualization.Charting.Series balance = this.chart1.Series.Add("Receving");
            SeriesChartType charttype = SeriesChartType.StackedColumn;

            sale.ChartType = charttype;
            balance.ChartType = charttype;


            foreach (DataRow row in sales20.Rows)
            {
                sale.Points.AddXY(row[0].ToString(), row[1].ToString());
                sale.Label = row[1].ToString();

            }
            foreach (DataRow row in cash20.Rows)
            {
                balance.Points.AddXY(row[0].ToString(), row[1].ToString());
                balance.Label = row[1].ToString();
            }
        }
        public void loadCartisian(string sdate, string ldate)
        {
            DataTable sales20 = bal.getDashboardSales20(sdate, ldate);
            DataTable cash20 = bal.getDashboardCash20(sdate, ldate);
            // Set title
            this.cartesianChart1.Series.Clear();
            this.cartesianChart1.AxisX.Clear();
            this.cartesianChart1.AxisY.Clear();
            this.cartesianChart1.Refresh();

            ChartValues<int> values = new ChartValues<int>();
            //create a list of string to store labels (names) of each user. 
            List<String> labels = new List<string>();
            foreach (DataRow row in sales20.Rows)
            {
                values.Add(int.Parse(row[1].ToString()));
                labels.Add(row[0].ToString());
            }
            ChartValues<int> values1 = new ChartValues<int>();
            //create a list of string to store labels (names) of each user. 
            List<String> labels1 = new List<string>();
            foreach (DataRow row in cash20.Rows)
            {
                values1.Add(int.Parse(row[1].ToString()));
                labels1.Add(row[0].ToString());
            }
            cartesianChart1.Series = new SeriesCollection
            {
                new StackedColumnSeries
                {
                    Title="Sales",
                    Values = values,
                    StackMode = StackMode.Values, // this is not necessary, values is the default stack mode
                    DataLabels = true,
                    Fill = System.Windows.Media.Brushes.Red
                },
                new StackedColumnSeries
                {
                    Title="Recevings",
                    Values = values1,
                    StackMode = StackMode.Values,
                    DataLabels = true,
                    Fill = System.Windows.Media.Brushes.Green
                }
            };


            cartesianChart1.AxisX.Add(new Axis
            {
                Title = "Date",
                Labels = labels,
                Separator = DefaultAxes.CleanSeparator,

            });

            cartesianChart1.AxisY.Add(new Axis
            {
                Title = "Amount",
                LabelFormatter = value => "Rs " + value
            });




            //addDataChart(labels, values, System.Windows.Media.Brushes.Re);
            //addDataChart(labels1, values1, System.Windows.Media.Brushes.Green);



        }

        public void piechart(float augrai, float cash, float discount)
        {
            Func<ChartPoint, string> labelPoint = chartPoint =>
                string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);
            pieChart1.Series = new SeriesCollection
            {

                new PieSeries
                {
                    Title = "Augrai",
                    Values = new ChartValues<double> {augrai},
                    PushOut = 15,
                    DataLabels = true,
                    LabelPoint = labelPoint,
                    Fill = System.Windows.Media.Brushes.Red
                },
                new PieSeries
                {
                    Title = "Cash Receive",
                    Values = new ChartValues<double> {cash},
                    DataLabels = true,
                    LabelPoint = labelPoint,
                    Fill = System.Windows.Media.Brushes.Green
                },
                new PieSeries
                {
                    Title = "Discount",
                    Values = new ChartValues<double> {discount},
                    DataLabels = true,
                    LabelPoint = labelPoint,
                    Fill = System.Windows.Media.Brushes.OrangeRed
                },

            };
            pieChart1.LegendLocation = LegendLocation.Bottom;
        }
        private void addDataChart(List<String> labels, ChartValues<int> values, System.Windows.Media.SolidColorBrush color)
        {
            //create series to display ages of user. 
            cartesianChart1.Series = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Cash", //Title of series
                    Values = values, //list of values (age)
                    DataLabels = true, //display bar value on top of bar
                    Fill = color //color of bar
                }
            };

            //x axis labels - this creates the x axis labels. 
            cartesianChart1.AxisX.Add(new Axis
            {
                Title = "Date",
                Labels = labels,
                Unit = 1
            });
            //y axis label - this creates the y axis labels. 
            cartesianChart1.AxisY.Add(new Axis
            {
                Title = "Amount",
                LabelFormatter = value => value.ToString()
            });
        }


        /*
        private void addX_Axis(System.Windows.Forms.DataVisualization.Charting.Series series)
        {

            DataPoint point = new DataPoint();
            point.SetValueXY("2019-09-01", 1000);
            point.ToolTip = string.Format("{0}, {1}", "2019-09-01", 1000);
            series.Points.Add(point);

            series.Points.AddXY("2019-09-01", 1000);
            series.Points.AddXY("2019-09-02", 20);
            series.Points.AddXY("2019-09-03", 3000);
            series.Points.AddXY("2019-09-04", 5000);
            series.Points.AddXY("2019-09-05", 5400);
            series.Points.AddXY("2019-09-06", 2000);
            series.Points.AddXY("2019-09-07", 3000);
            series.Points.AddXY("2019-09-08", 10000);
            series.Points.AddXY("2019-09-09", 11110);
            series.Points.AddXY("2019-09-10", 12000);

            series.Points.AddXY("2019-09-11", 0);
            series.Points.AddXY("2019-09-12", 0);
            series.Points.AddXY("2019-09-13", 0);
            series.Points.AddXY("2019-09-14", 0);
            series.Points.AddXY("2019-09-15", 0);
            series.Points.AddXY("2019-09-16", 0);
            series.Points.AddXY("2019-09-17", 0);
            series.Points.AddXY("2019-09-18", 2000);
            series.Points.AddXY("2019-09-19", 3000);
            series.Points.AddXY("2019-09-20", 0);
            series.Points.AddXY("2019-09-21", 0);
            series.Points.AddXY("2019-09-22", 0);
            series.Points.AddXY("2019-09-23", 0);
            series.Points.AddXY("2019-09-14", 7560);
            series.Points.AddXY("2019-09-25", 5600);
            series.Points.AddXY("2019-09-26", 8000);
            series.Points.AddXY("2019-09-27", 0);
            series.Points.AddXY("2019-09-28", 0);
            series.Points.AddXY("2019-09-29", 0);
            series.Points.AddXY("2019-09-30", 4055);
        }
        private void SplineChartExample()
        {
            this.chart1.Series.Clear();

            this.chart1.Titles.Add("Total Income");

            System.Windows.Forms.DataVisualization.Charting.Series series = this.chart1.Series.Add("Total Income");
            series.ChartType = SeriesChartType.Spline;
            series.Points.AddXY("September", 100);
            series.Points.AddXY("Obtober", 300);
            series.Points.AddXY("November", 800);
            series.Points.AddXY("December", 200);
            series.Points.AddXY("January", 600);
            series.Points.AddXY("February", 400);
        }
       */
        Point? prevPosition = null;
        ToolTip tooltip = new ToolTip();

        private void chart_MouseMove(object sender, MouseEventArgs e)
        {
            var pos = e.Location;
            if (prevPosition.HasValue && pos == prevPosition.Value)
                return;
            tooltip.RemoveAll();
            prevPosition = pos;
            var results = chart1.HitTest(pos.X, pos.Y, false, ChartElementType.DataPoint); // set ChartElementType.PlottingArea for full area, not only DataPoints
            foreach (var result in results)
            {
                if (result.ChartElementType == ChartElementType.DataPoint) // set ChartElementType.PlottingArea for full area, not only DataPoints
                {
                    var yVal = result.ChartArea.AxisY.PixelPositionToValue(pos.Y);
                    tooltip.Show(((int)yVal).ToString(), chart1, pos.X, pos.Y - 15);
                }
            }
        }
        // Display One Week Profit and Credit
        private void DrawPieChart(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
                return;

            DataRow dr = dt.Rows[0];
            float augrai = 0;
            float recevings = 0;
            float discount = 0;
            if (dr[9].ToString() != "" && dr[10].ToString() != "" && dr[11].ToString() != "")
            {
                augrai = mTruncate(float.Parse(dr[9].ToString()), 2);
                recevings = mTruncate(float.Parse(dr[10].ToString()), 2);
                discount = mTruncate(float.Parse(dr[11].ToString()), 2);
            }
            piechart(augrai, recevings, discount);

            //reset your chart series and legends
            pie.Series.Clear();
            pie.Legends.Clear();

            //Add a new Legend(if needed) and do some formating
            pie.Legends.Add("MyLegend");
            pie.Legends[0].LegendStyle = LegendStyle.Table;
            pie.Legends[0].Docking = Docking.Right;
            pie.Legends[0].Alignment = StringAlignment.Center;
            pie.Legends[0].Title = "Week Sales/Purchase";
            pie.Legends[0].BorderColor = Color.Black;

            //Add a new chart-series
            string seriesname = "MySeriesName";
            pie.Series.Add(seriesname);
            //set the chart-type to "Pie"
            pie.Series[seriesname].ChartType = SeriesChartType.Pie;

            //Add some datapoints so the series. in this case you can pass the values to this method
            pie.Series[seriesname].Points.AddXY("Augrai: " + (float.Parse(augrai.ToString("0.00")) - float.Parse(recevings.ToString("0.00"))),
                float.Parse(augrai.ToString("0.00")) - float.Parse(recevings.ToString("0.00")));
            pie.Series[seriesname].Points.AddXY("CashRecevings: " + recevings.ToString("0.00"), recevings.ToString("0.00"));
            pie.Series[seriesname].Points.AddXY("Discount: " + discount.ToString("0.00"), float.Parse(discount.ToString("0.00")));
        }
        public float mTruncate(float value, int digits)
        {
            double mult = Math.Pow(10.0, digits);
            double result = Math.Truncate(mult * value) / mult;
            return (float)result;
        }
        private void _check_date_CheckedChanged(object sender, EventArgs e)
        {
            if (_check_date.Checked)
            {
                pan_Date.Enabled = true;

            }
            else
            {
                pan_Date.Enabled = false;


            }
        }

        private void btn_add_client_Click(object sender, EventArgs e)
        {
            string sdate = "";
            string ldate = "";
            if (_check_date.Checked)
            {
                sdate = date_start.Text;
                ldate = date_last.Text;
                DataTable dt = bal.getAllSales_ProfitDetail(sdate, ldate);
                DrawPieChart(dt);
                loadBarchart(sdate, ldate);
                loadCartisian(sdate, ldate);
            }
            else
            {
                DataTable dt = bal.getAllSales_ProfitDetail(sdate, ldate);
                DrawPieChart(dt);
                loadBarchart(sdate, ldate);
                loadCartisian(sdate, ldate);
            }

        }
    }
}
