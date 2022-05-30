using IGTradingLib;
using IGTradingLib.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGTrading
{
    public partial class DataDownloadFrm : Form
    {
        public DataDownloadFrm()
        {
            InitializeComponent();
            lblEpic.Text = Def.m_SelectedInstr.Name;
            dtDownloadFrom.Value = DateTime.Today;
            dtDownloadTo.Value = DateTime.Now;
            dtDownloadTo.Value = dtDownloadTo.Value.AddSeconds(dtDownloadTo.Value.Second * -1);
            cbResolution.Items.AddRange(Def.ResolutionList);

            cbResolution.SelectedIndex = 0;
        }

        private void btnDownloadPriceHist_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DateTime dtFrom = dtDownloadFrom.Value;               
                DateTime dtTo = dtDownloadTo.Value;
                int allowance = Def.m_trading.DownloadPriceHistory(Def.m_SelectedInstr.Epic, cbResolution.Text, dtFrom, dtTo);
                MessageBox.Show("Allowance remaining " + allowance.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            this.Cursor = Cursors.Default;
        }

        private void btnGetLastDate_Click(object sender, EventArgs e)
        {
            InstrumentHistoryData model = IGTradingLib.DB.Rep.GetLastInstrumentHistoryItem(Def.m_SelectedInstr.Epic, cbResolution.Text);
            
            if (model == null)
                return;

            dtDownloadFrom.Value = model.PriceDateTime.AddMinutes(TradingDBManage.GetMinutesFromResolution(cbResolution.Text));

        }

        private void btnGetFirstDate_Click(object sender, EventArgs e)
        {
            InstrumentHistoryData model = IGTradingLib.DB.Rep.GetFirstInstrumentHistoryItem(Def.m_SelectedInstr.Epic, cbResolution.Text);

            if (model == null)
                return;

            dtDownloadTo.Value = model.PriceDateTime.AddMinutes(-1 * TradingDBManage.GetMinutesFromResolution(cbResolution.Text));
            dtDownloadFrom.Value = dtDownloadTo.Value.AddMonths(-1);
        }
    }
}
