using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using IGTradingLib;

namespace IGTrading
{
    public partial class ImportResolutionFrm : Form
    {
        public ImportResolutionFrm()
        {
            InitializeComponent();
            cbResolution.Items.AddRange(Def.ResolutionList);
            cbResolution.SelectedIndex = 1;

            cbSourceResolution.Items.AddRange(Def.ResolutionList);
            cbSourceResolution.SelectedIndex = 3;

            lblEpic.Text = Def.m_SelectedInstr.Name;
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if(chkDataStream.Checked)
                    TradingDBManage.ImportDataStreamToResolution(Def.m_SelectedInstr.Epic, cbResolution.Text, dtDownloadFrom.Value, dtDownloadTo.Value);
                else
                    TradingDBManage.ImportToResolution(Def.m_SelectedInstr.Epic, cbSourceResolution.Text, cbResolution.Text, dtDownloadFrom.Value, dtDownloadTo.Value);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            this.Cursor = Cursors.Default;
        }
    }
}
