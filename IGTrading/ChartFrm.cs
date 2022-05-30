using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Windows.Forms.DataVisualization.Charting;

using IGTradingLib.DataModel;
using IGTradingLib;
using IGTradingLib.Types;

namespace IGTrading
{
    public partial class ChartFrm : Form
    {
        List<InstrumentHistoryData> m_HistList = null;
        Dictionary<DateTime, int> m_XValMap = new Dictionary<DateTime, int>();

        public ChartFrm()
        {
            InitializeComponent();
            cbResolution.Items.AddRange(Def.ResolutionList);

            cbResolution.SelectedIndex = 0;
        }

        private void ChartFrm_Load(object sender, EventArgs e)
        {
            //chart1.BackGradientStyle = 
           // chart1.Series["My Data"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType
         //           SeriesChartType.Line;

            if(m_HistList == null)
                LoadBaseChartData(DB.Rep.FindHistData(Def.m_SelectedInstr.Epic, cbResolution.Text, DateTime.Now.AddDays(-21), DateTime.Now));              

            chart1.ChartAreas[0].AxisY.ScaleView.SmallScrollSize = chart1.ChartAreas[0].AxisY.ScaleView.SmallScrollMinSize / 10;

            
            
            chart1.ChartAreas[0].CursorX.IsUserEnabled = true;
            chart1.ChartAreas[0].CursorY.IsUserEnabled = true;
            chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            chart1.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;

            this.WindowState = FormWindowState.Maximized;
        }

        public void LoadBaseChartData(List<InstrumentHistoryData> histList)
        {
            if (histList == null || histList.Count == 0)
                return;

            dtDownloadFrom.Value = histList[0].PriceDateTime;
            dtDownloadTo.Value = histList[histList.Count -1].PriceDateTime;
            cbResolution.Text = histList[0].Resolution;

            m_HistList = histList;
            m_XValMap.Clear();

            chart1.Series["Series1"].Points.Clear();
            chart1.Series["Series1"].Color = Color.Black;
            

            decimal minVal = decimal.MaxValue;
            for (int i = 0; i < histList.Count; i++)
            {
                var data = histList[i];
                int pos = chart1.Series["Series1"].Points.AddXY(i+1, data.GetMidLowPrice(), data.GetMidHighPrice(), data.GetMidOpenPrice(), data.GetMidClosePrice());
                m_XValMap[data.PriceDateTime] = i + 1;
                string label = data.PriceDateTime.ToString("ddMMM HHmm");
                chart1.Series["Series1"].Points[pos].AxisLabel = label;
                //chart1.Series["Series1"].Points[pos].
               

                if (data.GetMidLowPrice() < minVal)
                    minVal = data.GetMidLowPrice();
            }

            chart1.ChartAreas[0].AxisY.Minimum = (double)minVal;

            TimeSpan timeDiff = histList[histList.Count - 1].PriceDateTime - histList[0].PriceDateTime;
            if(timeDiff.Days > 0)
                chart1.ChartAreas[0].AxisX.LabelStyle.Interval = (int)Math.Max(histList.Count / timeDiff.Days, 1);
        }

       /* public void AddTurnPointChartData(List<PricePoint> dataList)
        {
            Series series2 = new Series("");
            series2.ChartType = SeriesChartType.Point;
            series2.Color = Color.Blue;
            series2.BorderWidth = 4;
            chart1.Series.Add(series2);

            foreach (var pricePoint in dataList)
            {
                if(m_XValMap.ContainsKey(pricePoint.TimeStamp))
                    series2.Points.AddXY(m_XValMap[pricePoint.TimeStamp], pricePoint.Price);
            }
        }*/

        public void AddPricePointsChartData(List<PricePoint> dataList, Color color, int size, bool bPoint = true)
        {
            Series series = new Series("");
            if (bPoint)
                series.ChartType = SeriesChartType.Point;
            else
                series.ChartType = SeriesChartType.Line;

            series.MarkerColor = color;
            series.MarkerSize = size;
            chart1.Series.Add(series);

            foreach (var pricePoint in dataList)
            {
                if (m_XValMap.ContainsKey(pricePoint.TimeStamp))
                    series.Points.AddXY(m_XValMap[pricePoint.TimeStamp], pricePoint.Price);
            }
        }

        private void chart1_MouseClick(object sender, MouseEventArgs e)
        {
            double val = chart1.ChartAreas[0].AxisX.PixelPositionToValue(e.X);
            int i = (int)Math.Round(val);

            if(i > 0 && i < m_HistList.Count)
            {
                var data = m_HistList[i-1];
                txtBarDt.Text = data.PriceDateTime.ToString();
                txtBarValue.Text = data.GetMidClosePrice().ToString();
                if(data.LastTradedVolume.HasValue)
                    txtVolume.Text = data.LastTradedVolume.Value.ToString();
            }
            
        }

        private void chart1_AxisViewChanged(object sender, ViewEventArgs e)
        {
            chart1.ChartAreas[0].AxisX.LabelStyle.Interval = (int)Math.Max(chart1.ChartAreas[0].AxisX.ScaleView.Size / 30, 1);
            //chart1.ChartAreas[0].AxisX.LabelStyle.IntervalType = DateTimeIntervalType.
            //chart1.ChartAreas[0].AxisX.ScaleView.
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (m_HistList != null)
                m_HistList.Clear();

            LoadBaseChartData(DB.Rep.FindHistData(Def.m_SelectedInstr.Epic, cbResolution.Text, dtDownloadFrom.Value, dtDownloadTo.Value));

            chart1.ChartAreas[0].AxisY.ScaleView.SmallScrollSize = chart1.ChartAreas[0].AxisY.ScaleView.SmallScrollMinSize / 10;
        }

        private void btnMinYup_Click(object sender, EventArgs e)
        {
            double diff = chart1.ChartAreas[0].AxisY.Maximum - chart1.ChartAreas[0].AxisY.Minimum;
            chart1.ChartAreas[0].AxisY.Minimum = chart1.ChartAreas[0].AxisY.Minimum + (diff * 0.1);
        }

        private void btnMaxYdown_Click(object sender, EventArgs e)
        {
            double diff = chart1.ChartAreas[0].AxisY.Maximum - chart1.ChartAreas[0].AxisY.Minimum;
            chart1.ChartAreas[0].AxisY.Maximum = chart1.ChartAreas[0].AxisY.Maximum - (diff * 0.1);
        }

        private void btnMinYdown_Click(object sender, EventArgs e)
        {
            double diff = chart1.ChartAreas[0].AxisY.Maximum - chart1.ChartAreas[0].AxisY.Minimum;
            chart1.ChartAreas[0].AxisY.Minimum = chart1.ChartAreas[0].AxisY.Minimum - (diff * 0.1);
        }

        private void btnMaxYup_Click(object sender, EventArgs e)
        {
            double diff = chart1.ChartAreas[0].AxisY.Maximum - chart1.ChartAreas[0].AxisY.Minimum;
            chart1.ChartAreas[0].AxisY.Maximum = chart1.ChartAreas[0].AxisY.Maximum + (diff * 0.1);
        }
    }
}
