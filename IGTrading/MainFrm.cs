using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

using dto.endpoint.search;
using IGPublicPcl;

using IGTradingLib;
using IGTradingLib.Types;
using IGTradingLib.DataModel;
using dto.endpoint.accountbalance;

using Newtonsoft.Json;

namespace IGTrading
{
    public partial class MainFrm : Form, IIGStatusEvent
    {
        
        bool m_LoggedIn = false;
        List<Market> m_SearchedMarkets = null;        
        delegate void RaiseErrorTS(String myString);
        RaiseErrorTS delegeteRaiseErrorTS = null;

        delegate void DataStreamEventTS(DataStreamUpdateEvent dataStreamEvent);
        DataStreamEventTS delegateDataStreamEventTS = null;

        Dictionary<long, int> m_DataIDListViewMapping = new Dictionary<long, int>();

        public MainFrm()
        {           
            InitializeComponent();
            cbDisplayType.SelectedIndex = 0;            

            delegeteRaiseErrorTS = new RaiseErrorTS(RaiseError);
            delegateDataStreamEventTS = new DataStreamEventTS(DataStreamEvent);

            try
            {
                Def.m_trading = new TradingImpl();
                Def.m_trading.SetIGEvent(this);
                LibDef.m_trading = Def.m_trading;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            try
            {
                string strApiConfig = File.ReadAllText("config\\ApiConfig.json");
                Def.ApiConfig = JsonConvert.DeserializeObject<IGApiConfig>(strApiConfig);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            LoadFavourites();

        }

        private void loginDemoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Def.ApiConfig == null || string.IsNullOrEmpty(Def.ApiConfig.DemoAPIKey))
            {
                MessageBox.Show("Api Config not set");
                return;
            }

            if (!Def.m_trading.Login(Def.ApiConfig.DemoAPIKey, Def.ApiConfig.DemoUsername, Def.ApiConfig.DemoPwd, false))
                MessageBox.Show("Login Failed");
            else
                m_LoggedIn = true;
        }

        private void loginLiveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (Def.ApiConfig == null || string.IsNullOrEmpty(Def.ApiConfig.LiveAPIKey))
            {
                MessageBox.Show("Api Config not set");
                return;
            }

            if (!Def.m_trading.Login(Def.ApiConfig.LiveAPIKey, Def.ApiConfig.LiveUsername, Def.ApiConfig.LivePwd, true))
                MessageBox.Show("Login Failed");
            else
                m_LoggedIn = true;
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Def.m_trading.Logout();
            m_LoggedIn = false;            
        }

        private void changeAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Def.m_trading.SwapAccount();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void lbSearchResult_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void btnDoMarketSearch_Click(object sender, EventArgs e)
        {
            if (Def.m_trading == null)
                return;

            lbEpicList.Items.Clear();
            m_SearchedMarkets = Def.m_trading.SearchMarket(txtSearch.Text);

            if (m_SearchedMarkets == null || m_SearchedMarkets.Count == 0)
                return;

            foreach (Market market in m_SearchedMarkets)
            {
                lbEpicList.Items.Add(market.instrumentName);
            }
        }        

        private void MainFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (m_LoggedIn)
                Def.m_trading.Logout();
        }       
    
       

        private void downloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataDownloadFrm frm = new DataDownloadFrm();
            frm.ShowDialog();
        }

        private void resolutionImportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImportResolutionFrm frm = new ImportResolutionFrm();
            frm.ShowDialog();
        }

        private void cbDisplayType_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbEpicList.Items.Clear();
            if (cbDisplayType.SelectedIndex == 0)
            {
                btnDoMarketSearch.Enabled = false;
                LoadFavourites();
                //lbEpicList.Items.AddRange(m_FavList.ToArray());// ("IX.D.SAF.IFMM.IP");
                //lbEpicList.Items.Add("IX.D.STXE.IFM.IP");
            }
            else if (cbDisplayType.SelectedIndex == 1)
            {
                btnDoMarketSearch.Enabled = true;
            }
        }

        private void LoadFavourites()
        {
            if (DB.Rep != null)
                DB.Rep.GetInstruments().ForEach(i => lbEpicList.Items.Add(i.Name));
        }

        private void lbEpicList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ( lbEpicList.SelectedIndex < 0)
                return;

            if (cbDisplayType.SelectedIndex == 1)
            {
                Market market = m_SearchedMarkets[lbEpicList.SelectedIndex];
                instrumentDetail1.ShowMarketDetails(market);
                //Def.m_SelectedInstr = DB.Rep.GetInstrument market.epic;
            }
            else
            {
                InstrumentDetails detail = DB.Rep.GetInstruments()[lbEpicList.SelectedIndex];
                Def.m_SelectedInstr = detail;

                Market market = new Market();
                market.epic = detail.Epic;
                market.instrumentName = detail.Name;
                market.instrumentType = detail.InstrumentType;
                instrumentDetail1.ShowMarketDetails(market);
               // Def.m_SelectedEpic = lbEpicList.Text;
            }
        }

        private void chartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChartFrm frm = new ChartFrm();
            frm.Show();
        }

        private void tradeSimulationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TradeTestFrm frm = new TradeTestFrm();
            frm.ShowDialog();
        }


        public void UpdateIGStatus(IGStatusData status)
        {
            if (status.LoggedOn)
            {
                lblLogonStatus.Text = string.Format("Account Name: {0} - Fund Balance: {1} - Margin: {2} - Profit Loss: {3}", status.AccountName, status.FundBalance, status.Margin, status.ProfitLoss);
            }
            else
            {
                lblLogonStatus.Text = "Not Logged On";
            }
        }

        

        public void RaiseError(string errorStr)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(delegeteRaiseErrorTS, errorStr);
            }
            else
            {
                if (lbErrorLog.Items.Count == 100)
                    lbErrorLog.Items.RemoveAt(lbErrorLog.Items.Count - 1);

                lbErrorLog.Items.Insert(0, DateTime.Now.ToString() + " - " + errorStr);
            }
        }

        public void DataStreamEvent(DataStreamUpdateEvent dataStreamEvent)
        {
            if (this.InvokeRequired)
            {
                //this.Invoke(delegateDataStreamEventTS, dataStreamEvent);
                this.Invoke(new Action(delegate() { DataStreamEvent(dataStreamEvent); }));
            }
            else
            {
                ListViewItem lvItem = null;

                if (m_DataIDListViewMapping.ContainsKey(dataStreamEvent.DataID.ID))
                {
                    lvItem = lvDataStream.Items[m_DataIDListViewMapping[dataStreamEvent.DataID.ID]];
                }
                else
                {
                    lvItem = new ListViewItem();
                    
                    lvDataStream.Items.Add(lvItem);
                    m_DataIDListViewMapping[dataStreamEvent.DataID.ID] = lvDataStream.Items.Count - 1;
                }

                lvItem.SubItems.Clear();
                lvItem.Text = dataStreamEvent.DataID.Name;
                lvItem.SubItems.Add(dataStreamEvent.PriceData.AskPrice.ToString());
                lvItem.SubItems.Add(dataStreamEvent.PriceData.BidPrice.ToString());
                lvItem.SubItems.Add(dataStreamEvent.PriceData.PriceDatetime.ToString());
            }
            
        }

        public void PositionChangeEvent(dto.endpoint.auth.session.AccountDetails accountID, LsTradeSubscriptionData eventData)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(delegate() { PositionChangeEvent(accountID, eventData); }));
            }
            else
            {
                if (eventData.status.HasValue == false)
                    return;

                ListViewItem lvItem = null;

                foreach (ListViewItem iLvItem in lvPositions.Items)
                {
                    if (iLvItem.Tag.ToString() == eventData.dealId)
                    {
                        lvItem = iLvItem;
                        break;
                    }
                }

                if (eventData.status.Value == StreamingStatusEnum.OPEN 
                    || eventData.status.Value == StreamingStatusEnum.AMENDED 
                    || eventData.status.Value == StreamingStatusEnum.UPDATED)
                {      
                    if (lvItem == null)
                    {
                        lvItem = new ListViewItem();
                        lvItem.Tag = eventData.dealId;
                        lvItem.Text = accountID.accountName;
                        lvItem.SubItems.Add(eventData.epic);
                        lvItem.SubItems.Add(eventData.size);
                        lvPositions.Items.Add(lvItem);
                    }
                    else
                    {
                        lvItem.SubItems[2].Text = eventData.size;
                    }
                }
                else if (eventData.status.Value == StreamingStatusEnum.CLOSED
                    || eventData.status.Value == StreamingStatusEnum.DELETED)
                {
                    if (lvItem != null)
                    {
                        lvPositions.Items.Remove(lvItem);
                    }
                }
            }
        }

        public void AccountChangeEvent(dto.endpoint.auth.session.AccountDetails accountID, StreamingAccountData eventData)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(delegate() { AccountChangeEvent(accountID, eventData); }));
            }
            else
            {
                ListViewItem lvItem = null;

                foreach (ListViewItem tmpItem in lvAccounts.Items)
                {
                    if (tmpItem.Text == accountID.accountName)
                    {
                        lvItem = tmpItem;
                        break;
                    }
                }

                decimal totalFunds = 0;
                decimal pl = 0;
                decimal margin = 0;
                decimal available = 0;

                if (eventData.AmountDue.HasValue)
                {
                    available = eventData.AmountDue.Value;
                    totalFunds += eventData.AmountDue.Value;
                }

                if (eventData.UsedMargin.HasValue)
                {
                    totalFunds += eventData.UsedMargin.Value;
                    margin = eventData.UsedMargin.Value;
                }

                if (eventData.ProfitAndLoss.HasValue)
                    pl = eventData.ProfitAndLoss.Value;

                if (lvItem == null)
                {
                    lvItem = new ListViewItem();
                    lvItem.Text = accountID.accountName;
                    lvItem.SubItems.Add(totalFunds.ToString());
                    lvItem.SubItems.Add(pl.ToString());
                    lvItem.SubItems.Add(margin.ToString());
                    lvItem.SubItems.Add(available.ToString());

                    lvAccounts.Items.Add(lvItem);
                }
                else
                {
                    lvItem.SubItems[1].Text = totalFunds.ToString();
                    lvItem.SubItems[2].Text = pl.ToString();
                    lvItem.SubItems[3].Text = margin.ToString();
                    lvItem.SubItems[4].Text = available.ToString();
                }

            }
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*DataStreamParam p = new DataStreamParam();
            p.PriceData = true;
            var epicList = from row in DB.Rep.GetInstruments() where row.EnableLiveStream == true select row.Epic;
            p.EpicList = epicList.ToArray();*/
            Def.m_trading.StartDataStreaming();
            timer1.Start();
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Def.m_trading.StopDataStreaming();
            timer1.Stop();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Def.m_trading.PingIGServer();
        }

        private void liveTradeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TradeFrm frm = new TradeFrm();
            frm.Show();
        }
    }
}
