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
using dto.endpoint.positions.create.otc.v1;
using dto.endpoint.positions.get.otc.v1;
using dto.endpoint.positions.close.v1;
using dto.endpoint.positions.edit.v1;
using IGTradingLib.Types;
using IGTradingLib.DataModel;

namespace IGTrading
{
    public partial class TradeFrm : Form
    {
        List<TradePlanDetails> m_TradePlans = new List<TradePlanDetails>();
        private decimal? m_TradeLimit = null;

        public TradeFrm()
        {
            InitializeComponent();

            RefreshTradePlanList();
           
            /*cbTradePlans.DataSource = m_TradePlans;
            cbTradePlans.DisplayMember = "Name";
            cbTradePlans.ValueMember = "ID";*/
            
        }

        private void TradeFrm_Load(object sender, EventArgs e)
        {
            LibDef.OnStreamEvent += HandleDataStream;
        }

        private void TradeFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            LibDef.OnStreamEvent -= HandleDataStream;
        }       

        private void btnGetOpenPositions_Click(object sender, EventArgs e)
        {
            RefreshPositions();
        }

        private void btnOpenPosition_Click(object sender, EventArgs e)
        {
            if (txtTradelimit.Text != "")
            {
                try
                {
                    m_TradeLimit = decimal.Parse(txtTradelimit.Text);
                    //txtTradelimit.Text = "";
                    btnOpenPosition.Text = "Cancel Limit Trade";
                }
                catch { }

            }
            else if (m_TradeLimit.HasValue)
            {
                btnOpenPosition.Text = "Open Position";
                m_TradeLimit = null;
            }
            else
            {
                OpenPosition();
            }
        }

        private void OpenPosition()
        {
            CreatePositionRequest req = new CreatePositionRequest();
            req.currencyCode = Def.m_SelectedInstr.CurrencyCode;
            req.direction = cbDirection.Text;
            try
            {
                req.size = int.Parse(txtQty.Text);

                if (txtLimitDistance.Text != "")
                    req.limitDistance = decimal.Parse(txtLimitDistance.Text);

                if (txtLimitLevel.Text != "")
                    req.limitLevel = decimal.Parse(txtLimitLevel.Text);

                if (txtStopLevel.Text != "")
                    req.stopLevel = decimal.Parse(txtStopLevel.Text);

                if (txtStopDistance.Text != "")
                    req.stopDistance = decimal.Parse(txtStopDistance.Text);

            }
            catch
            {
                MessageBox.Show("Invalid Value Entered");
            }

            req.epic = Def.m_SelectedInstr.Epic;
            req.forceOpen = false;
            req.guaranteedStop = false;
            req.orderType = "MARKET";
            req.expiry = "-";

            var posRsp = LibDef.m_trading.CreatePosition(req);
            if (posRsp != null)
            {
                RefreshPositions();
            }
            else
            {
                MessageBox.Show("Failed to create position");
            }
        }

        private void CheckTradeLimit(DataStreamUpdateEvent dataEvent)
        {
            if (m_TradeLimit == null || m_TradeLimit.Value == 0)
                return;

            if (cbDirection.Text == "BUY" && dataEvent.PriceData.GetMidPrice() < m_TradeLimit.Value)
            {
                m_TradeLimit = null;
                btnOpenPosition.Text = "Open Position";
                OpenPosition();                
            }
            else if (cbDirection.Text == "SELL" && dataEvent.PriceData.GetMidPrice() > m_TradeLimit.Value)
            {
                m_TradeLimit = null;
                btnOpenPosition.Text = "Open Position";
                OpenPosition();                
            }



        }

        private void btnClosePosition_Click(object sender, EventArgs e)
        {
            if (lvPositions.SelectedItems.Count == 0)
                return;

            OpenPosition pos = (OpenPosition)lvPositions.SelectedItems[0].Tag;

            ClosePositionRequest req = new ClosePositionRequest();
            req.dealId = pos.position.dealId;
            //req.epic = pos.market.epic;
            req.orderType = "MARKET";

            if (pos.position.direction == LibDef.BUY)
                req.direction = LibDef.SELL;
            else
                req.direction = LibDef.BUY;

            try
            {
                req.size = int.Parse(txtCloseQty.Text);
            }
            catch
            {
                MessageBox.Show("Invalid Value Entered");
            }

            var posRsp = LibDef.m_trading.ClosePosition(req);

            if (posRsp == null)
            {
                MessageBox.Show("Failed to close position");
            }
            else
            {
                RefreshPositions();
            }

        }

        private void btnChangePosition_Click(object sender, EventArgs e)
        {
            if (lvPositions.SelectedItems.Count == 0)
                return;

            OpenPosition pos = (OpenPosition)lvPositions.SelectedItems[0].Tag;

            EditPositionRequest req = new EditPositionRequest();

            try
            {
                if (txtUpdateStopLevel.Text != "")
                    req.stopLevel = decimal.Parse(txtUpdateStopLevel.Text);

                if (txtUpdateLimitLevel.Text != "")
                    req.limitLevel = decimal.Parse(txtUpdateLimitLevel.Text);
            }
            catch
            {
                MessageBox.Show("Invalid value");
                return;
            }


            var posRsp = LibDef.m_trading.EditPosition(pos.position.dealId, req);

            if (posRsp == null)
            {
                MessageBox.Show("Failed to edit position");
            }
            else
            {
                RefreshPositions();
            }
            
        }

        private void RefreshPositions()
        {
            var posRsp = LibDef.m_trading.GetOpenPositions();

            if (posRsp == null || posRsp.positions == null)
            {
                LibDef.m_StatusEvent.RaiseError("Failed to get positions");
                return;
            }

            lvPositions.Items.Clear();
            
            foreach (var pos in posRsp.positions)
            {
                if (pos.market == null || pos.position == null)
                    continue;

                //decimal contractSize = 0;

                ListViewItem lvItem = new ListViewItem();
                lvItem.Tag = pos;

                //columns 1
                lvItem.Text = pos.market.instrumentName;
                lvItem.SubItems.Add(pos.position.direction);
                
                //column 2
                if (pos.position.contractSize.HasValue)
                    lvItem.SubItems.Add(pos.position.dealSize.Value.ToString());
                else
                    lvItem.SubItems.Add("");

                //column 3
                if (pos.position.openLevel.HasValue)
                    lvItem.SubItems.Add(pos.position.openLevel.Value.ToString());
                else
                    lvItem.SubItems.Add("");

                //column 4
                //current value
                lvItem.SubItems.Add("");

                //column 5
                lvItem.SubItems.Add(pos.position.createdDate);

                //column 6
                // P/L value
                lvItem.SubItems.Add("");

                //column 7
                //Gain value
                lvItem.SubItems.Add("");

                lvPositions.Items.Add(lvItem);
            }

        }

        private void HandleDataStream(DataStreamUpdateEvent dataEvent)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(delegate() { UpdatePositionListFromStream(dataEvent); }));
            }
        }

        

        private void UpdatePositionListFromStream(DataStreamUpdateEvent dataEvent)
        {
            if (m_TradeLimit != null && Def.m_SelectedInstr.DataID == dataEvent.DataID.ID)
            {
                CheckTradeLimit(dataEvent);
            }

            if (lvPositions.Items.Count == 0)
                return;

            foreach (ListViewItem lvItem in lvPositions.Items)
            {
                OpenPosition pos = (OpenPosition)lvItem.Tag;

                if (DB.Rep.GetIntrumentDataID(pos.market.epic).ID == dataEvent.DataID.ID)
                {
                    decimal gain = 0;
                    if (pos.position.direction == LibDef.BUY)
                    {
                        lvItem.SubItems[4].Text = dataEvent.PriceData.BidPrice.ToString();
                        gain = pos.position.openLevel.HasValue ? (dataEvent.PriceData.BidPrice - pos.position.openLevel.Value) : 0;
                    }
                    else
                    {
                        lvItem.SubItems[4].Text = dataEvent.PriceData.AskPrice.ToString();
                        gain = pos.position.openLevel.HasValue ? (pos.position.openLevel.Value - dataEvent.PriceData.AskPrice) : 0;
                    }
                    
                    decimal profit = 0;
                    decimal dealSize = pos.position.dealSize.HasValue ? pos.position.dealSize.Value : 0;

                    if (pos.market.lotSize.HasValue)
                    {
                        if (pos.market.instrumentType == LibDef.IT_CURRENCY)
                            profit = (gain * 10000) * pos.market.lotSize.Value;
                        else
                            profit = gain * pos.market.lotSize.Value;

                        profit *= dealSize;
                    }

                    lvItem.SubItems[6].Text = profit.ToString();
                    lvItem.SubItems[7].Text = gain.ToString();

                    break;
                }
            }
            

        }

        private void lvPositions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvPositions.SelectedItems.Count == 0)
                return;

            OpenPosition pos = (OpenPosition)lvPositions.SelectedItems[0].Tag;

            if (pos.position.limitLevel.HasValue)
                txtUpdateLimitLevel.Text = pos.position.limitLevel.Value.ToString();

            if (pos.position.stopLevel.HasValue)
                txtUpdateStopLevel.Text = pos.position.stopLevel.Value.ToString();
        }

        private void btnAddTradePlan_Click(object sender, EventArgs e)
        {
            TradePlanDetailsFrm frm = new TradePlanDetailsFrm();

            frm.Data = "{\r\n\t\"epic\" : \" \"\r\n }";

            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                TradePlanDetails d = new TradePlanDetails();
                d.Data = frm.Data;
                d.Name = frm.PlanName;
                d.TradePlanType = frm.TradePlanType;
                DB.Rep.AddEntity(d);
                //m_TradePlans.Add(d);
                //cbTradePlans.Items.Add(d.Name);
                RefreshTradePlanList();
            }
        }

        private void btnEditTradePlan_Click(object sender, EventArgs e)
        {
            if (cbTradePlans.SelectedIndex < 0)
                return;

            TradePlanDetailsFrm frm = new TradePlanDetailsFrm();

            TradePlanDetails d = m_TradePlans[cbTradePlans.SelectedIndex];
            frm.ID = d.ID;
            frm.PlanName = d.Name;
            frm.TradePlanType = d.TradePlanType;
            frm.Data = d.Data;

            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {                
                d.Data = frm.Data;
                d.Name = frm.PlanName;
                d.TradePlanType = frm.TradePlanType;
                DB.Rep.UpdateEntity(d);
                RefreshTradePlanList();                
            }

        }

        private void btnDeleteTradePlan_Click(object sender, EventArgs e)
        {
            if (cbTradePlans.SelectedIndex < 0)
                return;

            if (MessageBox.Show("Are you sure you want to delete " + cbTradePlans.Text, "", MessageBoxButtons.YesNo) != System.Windows.Forms.DialogResult.Yes)
                return;

            TradePlanDetails d = m_TradePlans[cbTradePlans.SelectedIndex];

            DB.Rep.DeleteEntity(d);

            RefreshTradePlanList();
        }

        private void RefreshTradePlanList()
        {
            cbTradePlans.Items.Clear();
            m_TradePlans = DB.Rep.FindAllEnities<TradePlanDetails>();

            if (m_TradePlans == null || m_TradePlans.Count == 0)
                return;

            m_TradePlans.ForEach(d => cbTradePlans.Items.Add(d.Name));

            cbTradePlans.SelectedIndex = 0;
        }

        private void btnStartTradePlan_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Cursor = Cursors.Default;

        }

       

        

        

        

        

        
    }
}
