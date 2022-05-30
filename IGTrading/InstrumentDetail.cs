using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using dto.endpoint.search;

namespace IGTrading
{
    public partial class InstrumentDetail : UserControl
    {
        public InstrumentDetail()
        {
            InitializeComponent();
        }

        public void ShowMarketDetails(Market market)
        {
            txtEpic.Text = market.epic;
            txtName.Text = market.instrumentName;
            txtType.Text = market.instrumentType;
        }

        private void ClearCtls()
        {
            txtEpic.Text = "";
            txtName.Text = "";
            txtType.Text = "";
        }
    }
}
