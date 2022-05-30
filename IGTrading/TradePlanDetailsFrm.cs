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
    public partial class TradePlanDetailsFrm : Form
    {
        public TradePlanDetailsFrm()
        {
            InitializeComponent();

            cbPlanType.SelectedIndex = 0;
        }

        public long ID
        {
            set
            {
                txtID.Text = value.ToString();
            }
        }

        public string PlanName
        {
            set
            {
                txtName.Text = value;
            }

            get
            {
                return txtName.Text;
            }
        }

        public int TradePlanType
        {
            set
            {
                cbPlanType.SelectedIndex = (value - 1);
            }

            get
            {
                return (cbPlanType.SelectedIndex + 1);
            }
        }

        public string Data
        {
            set
            {
                txtData.Text = value;
            }

            get
            {
                return txtData.Text;
            }
        }
    }
}
