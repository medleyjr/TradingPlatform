using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using dto.endpoint.search;
using dto.endpoint.positions.create.otc.v1;
using dto.endpoint.positions.edit.v1;
using dto.endpoint.positions.close.v1;
using dto.endpoint.positions.get.otc.v1;

using IGTradingLib;
using IGTradingLib.Types;
using IGTradingLib.DataModel;
using IGTradingLib.TradePlan.Data;
using IGTradingLib.TradePlan;
using IGTradingLib.TradeAnalysis;

namespace IGTrading
{
    public partial class TradeTestFrm : Form, ITradingHost, ITradingSystem
    {
        //public ITrading m_trading;
        List<TradePlanDetails> m_TradePlans = new List<TradePlanDetails>();
        BindingList<PricePoint> m_TurningPoints = new BindingList<PricePoint>();
        List<TradePosition> m_PositionList = new List<TradePosition>();
        TradeParam m_TradeParam = null;
        DataStreamPrice m_CurrentDataItem = null;
        Action<DateTime> m_fnIntervalHandler = null;
        TradePlanBase m_TradePlan = null;
        List<PricePoint> m_ClosedPosList = new List<PricePoint>();

        decimal m_TotalGain = 0;
        decimal m_LotSize = 0;

        public TradeTestFrm()
        {
            InitializeComponent();

            RefreshTradePlanList();
            txtEpic.Text = Def.m_SelectedInstr.Name;

            cbResolution.Items.AddRange(Def.ResolutionList);
            cbResolution.SelectedIndex = 0;

            dtStart.Value = new DateTime(2016, 7, 5, 1, 0, 0);// = dtStart.Value.AddMonths(-1);
            dtEnd.Value = new DateTime(2016, 7, 7, 1, 0, 0);
        }

        private void TradeTestFrm_Load(object sender, EventArgs e)
        {                    
            dgTurningPoints.DataSource = m_TurningPoints;
            dgTurningPoints.Columns[2].Width = 200;            
        }

        private void RefreshTradePlanList()
        {
            cbTradePlans.Items.Clear();
            m_TradePlans = DB.Rep.FindAllEnities<TradePlanDetails>();

            if (m_TradePlans == null || m_TradePlans.Count == 0)
                return;

            m_TradePlans.ForEach(d => cbTradePlans.Items.Add(d.Name));
            cbTradePlans.SelectedIndex = 3;
        }

        private void btnSortTPByPrice_Click(object sender, EventArgs e)
        {

        }

        private void btnShowChart_Click(object sender, EventArgs e)
        {
            ChartFrm frm = new ChartFrm();
            var histData = DB.Rep.FindHistData(Def.m_SelectedInstr.Epic, cbResolution.Text, dtStart.Value, dtEnd.Value);
            frm.LoadBaseChartData(histData);

            frm.AddPricePointsChartData(m_TurningPoints.ToList(), Color.DeepSkyBlue, 10);

            List<PricePoint> tpCreateList = new List<PricePoint>();
            foreach (PricePoint tp in m_TurningPoints)
            {
                PricePoint newPrice = new PricePoint();
                newPrice.Price = tp.AddPrice;
                newPrice.TimeStamp = tp.AddTimeStamp;
                tpCreateList.Add(newPrice);
            }

            frm.AddPricePointsChartData(tpCreateList, Color.DarkKhaki, 10);


            List<PricePoint> m_BuyList = new List<PricePoint>();
            List<PricePoint> m_SellList = new List<PricePoint>();

            foreach (var p in m_PositionList)
            {
                if (p.PositionReqType == TradePosition.POS_REQ_TYPE_OPEN)
                {
                    PricePoint pp = new PricePoint();
                    pp.Price = p.Price;
                    pp.TimeStamp = p.PositionRequestDt;

                    if (p.PositionDirection == LibDef.BUY)
                        m_BuyList.Add(pp);
                    else
                        m_SellList.Add(pp);
                }
            }

            frm.AddPricePointsChartData(m_BuyList, Color.Blue, 10);
            frm.AddPricePointsChartData(m_SellList, Color.Black, 10);
            frm.AddPricePointsChartData(m_ClosedPosList, Color.Purple, 10);

            try
            {
                frm.AddPricePointsChartData(Indicators.GetMAList(histData, int.Parse(txtMA.Text)), Color.Black, 2, false);
            }
            catch
            {
                MessageBox.Show("Invalid MA");
            }

            frm.Show();
        }

        private void btnRefreshTradePlans_Click(object sender, EventArgs e)
        {
            RefreshTradePlanList();
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

        private void btnStartTest_Click(object sender, EventArgs e)
        {
            if (cbTradePlans.SelectedIndex < 0)
            {
                MessageBox.Show("Select Trade plan");
                return;
            }

            try
            {
                
                this.Cursor = Cursors.WaitCursor;

                ClearTradeTestData();
                try
                {
                    m_LotSize = decimal.Parse(txtLotSize.Text);
                }
                catch
                {
                    MessageBox.Show("Invalid value");
                }
                TradeMgr tradeMgr = new TradeMgr();

                m_TradeParam = new TradeParam();
                m_TradeParam.CurrentDateTime = dtStart.Value;
              //  m_TradeParam.PreLoadDataDateTime = m_TradeParam.CurrentDateTime.AddMonths(-2);              
                m_TradeParam.TradePlan = m_TradePlans[cbTradePlans.SelectedIndex];

                m_fnIntervalHandler = null;
                tradeMgr.Init(this, this);
                m_TradePlan = tradeMgr.AddTradePlan(m_TradeParam);
                InstrumentDataID dataID = DB.Rep.GetIntrumentDataID(m_TradePlan.TradePlanData.Epic);

                if (chkUseStreamData.Checked)
                {
                    var dataStreamObj = TradingDataStreamMgr.GetObj(m_TradePlan.TradePlanData.Epic);
                    var histData = dataStreamObj.LoadHistory(m_TradeParam.CurrentDateTime, dtEnd.Value);

                    DateTime prevBarDt = new DateTime();

                    if (histData.Count > 0)
                        prevBarDt = histData[0].PriceDatetime;

                    foreach (var data in histData)
                    {
                        if (TradingDBManage.IsNewBar(m_TradePlan.TradePlanData.Resolution, prevBarDt, data.PriceDatetime))
                        {
                            m_fnIntervalHandler(TradingDBManage.GetRoundedBarDt(m_TradePlan.TradePlanData.Resolution, prevBarDt));
                            prevBarDt = data.PriceDatetime;
                        }

                        DataStreamUpdateEvent dataPrice = new DataStreamUpdateEvent();
                        dataPrice.DataID = dataID;
                        dataPrice.PriceData = data;
                        m_CurrentDataItem = data;

                        LibDef.NotifyOnStreamPrice(dataPrice);
                    }
                }
                else
                {
                    List<InstrumentHistoryData> tradeData = DB.Rep.FindHistData(m_TradePlan.TradePlanData.Epic, m_TradePlan.TradePlanData.Resolution,
                        m_TradeParam.CurrentDateTime, dtEnd.Value);

                    foreach (var dataItem in tradeData)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            //DataStreamPrice
                            DataStreamUpdateEvent dataPrice = new DataStreamUpdateEvent();
                            dataPrice.DataID = dataID;
                            dataPrice.PriceData = new DataStreamPrice();

                            if (i == 0)
                            {
                                if (!dataItem.OpenAskPrice.HasValue || !dataItem.OpenBidPrice.HasValue)
                                    continue;

                                dataPrice.PriceData.AskPrice = dataItem.OpenAskPrice.Value;
                                dataPrice.PriceData.BidPrice = dataItem.OpenBidPrice.Value;
                                dataPrice.PriceData.PriceDatetime = dataItem.PriceDateTime;
                            }
                            else if (i == 1)
                            {
                                if (!dataItem.LowAskPrice.HasValue || !dataItem.LowBidPrice.HasValue)
                                    continue;

                                dataPrice.PriceData.AskPrice = dataItem.LowAskPrice.Value;
                                dataPrice.PriceData.BidPrice = dataItem.LowBidPrice.Value;
                                dataPrice.PriceData.PriceDatetime = dataItem.PriceDateTime;
                            }
                            else if (i == 2)
                            {
                                if (!dataItem.HighAskPrice.HasValue || !dataItem.HighBidPrice.HasValue)
                                    continue;

                                dataPrice.PriceData.AskPrice = dataItem.HighAskPrice.Value;
                                dataPrice.PriceData.BidPrice = dataItem.HighBidPrice.Value;
                                dataPrice.PriceData.PriceDatetime = dataItem.PriceDateTime;
                            }
                            else if (i == 3)
                            {
                                if (!dataItem.CloseAskPrice.HasValue || !dataItem.CloseBidPrice.HasValue)
                                    continue;

                                dataPrice.PriceData.AskPrice = dataItem.CloseAskPrice.Value;
                                dataPrice.PriceData.BidPrice = dataItem.CloseBidPrice.Value;
                                dataPrice.PriceData.PriceDatetime = dataItem.PriceDateTime;
                            }


                            m_CurrentDataItem = dataPrice.PriceData;
                            LibDef.NotifyOnStreamPrice(dataPrice);
                        }

                        m_fnIntervalHandler(dataItem.PriceDateTime);
                    }

                }

                txtTotalGain.Text = m_TotalGain.ToString();
                decimal profit = 0;
                InstrumentDetails instrumentDetails = DB.Rep.GetIntrumentDetails(m_TradePlan.TradePlanData.Epic);

                if (instrumentDetails.InstrumentType == LibDef.IT_CURRENCY)
                    profit = (m_TotalGain * 10000) * m_LotSize;
                else
                    profit = m_TotalGain * m_LotSize;

                txtTotalProfit.Text = profit.ToString();

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            this.Cursor = Cursors.Default;
        }

        public void AddTurningPoint(PricePoint turnPoint)
        {
            m_TurningPoints.Add(turnPoint);
            
            
            //dgTurningPoints.DataSource = m_TurningPoints;
            //dgTurningPoints.Refresh();
        }

        public void RegisterIntervalEventHandler(string resolution, Action<DateTime> fn)
        {
            m_fnIntervalHandler = fn;
            
        }

        public dto.endpoint.confirms.ConfirmsResponse CreatePosition(CreatePositionRequest req)
        {
            dto.endpoint.confirms.ConfirmsResponse rsp = new dto.endpoint.confirms.ConfirmsResponse();
            rsp.dealReference = Guid.NewGuid().ToString();
            rsp.dealId = (m_PositionList.Count + 1).ToString();
            
            if (m_CurrentDataItem == null)
                return null;
            
            TradePosition tradePos = new TradePosition();
            tradePos.DealID = rsp.dealId;
            tradePos.PositionRequestDt = m_CurrentDataItem.PriceDatetime;
            tradePos.PositionReqType = TradePosition.POS_REQ_TYPE_OPEN;
            tradePos.PositionDirection = req.direction;

            if (req.direction == LibDef.BUY)
                tradePos.Price = m_CurrentDataItem.AskPrice;
            else
                tradePos.Price = m_CurrentDataItem.BidPrice;

            AddPosToListView(tradePos.DealID, req.direction, TradePosition.POS_REQ_TYPE_OPEN, (int)req.size.Value, m_CurrentDataItem.PriceDatetime, tradePos.Price, 0);

            m_PositionList.Add(tradePos);

            rsp.level = tradePos.Price;
            return rsp;
            
        }

        public EditPositionResponse EditPosition(string dealId, EditPositionRequest req)
        {
            return null;
        }

        public ClosePositionResponse ClosePosition(ClosePositionRequest req)
        {
            ClosePositionResponse rsp = new ClosePositionResponse();

            var pos = m_PositionList.FirstOrDefault(p => p.DealID == req.dealId);

            if (pos == null)
                return null;

            pos.DealSize -= req.size.Value;

            decimal gain = 0;
            
            string dealDir = "";
            decimal price = 0;
            if (pos.PositionDirection == LibDef.BUY)
            {
                gain = m_CurrentDataItem.BidPrice - pos.Price;
                dealDir = LibDef.SELL;
                price = m_CurrentDataItem.BidPrice;
            }
            else
            {
                gain = pos.Price - m_CurrentDataItem.AskPrice;
                dealDir = LibDef.BUY;
                price = m_CurrentDataItem.AskPrice;
            }

            m_TotalGain += (gain*req.size.Value );

            AddPosToListView(req.dealId, dealDir, TradePosition.POS_REQ_TYPE_CLOSE, (int)req.size.Value, m_CurrentDataItem.PriceDatetime, price, gain);

            PricePoint pp = new PricePoint();
            pp.Price = price;
            pp.TimeStamp = m_CurrentDataItem.PriceDatetime;
            m_ClosedPosList.Add(pp);

            if (pos.DealSize == 0)
                m_PositionList.Remove(pos);

            rsp.dealReference = Guid.NewGuid().ToString();
            return rsp;            
        }                    
        
        public PositionsResponse GetOpenPositions()
        {
            PositionsResponse rsp = new PositionsResponse();
            rsp.positions = new List<OpenPosition>();

            foreach (var pos in m_PositionList)
            {
                var openPos = new OpenPosition();
                openPos.position.dealId = pos.DealID;
                openPos.position.dealSize = pos.DealSize;

                rsp.positions.Add(openPos);
            }
            return rsp;
        }

        public OpenPosition GetPosition(string dealId)
        {
            OpenPosition rsp = new OpenPosition();

            var pos = m_PositionList.FirstOrDefault(p => p.DealID == dealId);

            if (pos == null)
                return null;
                        
            rsp.position.dealId = pos.DealID;
            rsp.position.dealSize = pos.DealSize;           

            return rsp;
        }
                

        private void ClearTradeTestData()
        {
            m_TotalGain = 0;
            m_TurningPoints.Clear();
            m_PositionList.Clear();
            m_CurrentDataItem = null;
            txtTotalGain.Text = "";
            txtTotalProfit.Text = "";
            lvPosList.Items.Clear();
        }

        private void AddPosToListView(string dealID, string dir, string type, int size, DateTime dt, decimal price, decimal gain)
        {
            ListViewItem lvItem = new ListViewItem(dealID);
            lvItem.SubItems.Add(dir);
            lvItem.SubItems.Add(type);
            lvItem.SubItems.Add(size.ToString());
            lvItem.SubItems.Add(dt.ToString("dd HH:mm:ss"));            
            lvItem.SubItems.Add(price.ToString());

            if (type == TradePosition.POS_REQ_TYPE_CLOSE)
            {
                lvItem.SubItems.Add(gain.ToString());

                decimal profit = 0;
                InstrumentDetails instrumentDetails = DB.Rep.GetIntrumentDetails(m_TradePlan.TradePlanData.Epic);

                if (instrumentDetails.InstrumentType == LibDef.IT_CURRENCY)
                    profit = (gain * 10000) * m_LotSize;
                else
                    profit = gain * m_LotSize;

                profit *= size;

                lvItem.SubItems.Add(profit.ToString());
            }

            lvPosList.Items.Add(lvItem);


        }

        private void btnAnalyzeTurnPoints_Click(object sender, EventArgs e)
        {
            try
            {
                txtAnalyzeMovement.Text = "";
                decimal aveMovement = 0;
                m_TurningPoints.Clear();
                var tmpHistData = DB.Rep.FindHistData(Def.m_SelectedInstr.Epic, cbResolution.Text,
                            dtStart.Value, dtEnd.Value);

                InstrDataList histData = new InstrDataList();
                histData.AddRange(tmpHistData);
                //AnalyzeTurningPoints p = new AnalyzeTurningPoints(histData);
                //List<PricePoint> points = p.CalcNextTurnPoints(decimal.Parse(txtMinorMove.Text));

                AnalyzePriceAction analyzePriceAction = new AnalyzePriceAction();
                analyzePriceAction.Init(histData, chkAnalyzeTrendOnly.Checked, chkAnalyzeWithMajorPullback.Checked, decimal.Parse(txtPullbackPercRequired.Text));

                for (int i = 1; i < histData.Count - 1; i++)
                {
                    analyzePriceAction.ProcessCurrentBar(i);
                }


                foreach (var point in analyzePriceAction.m_TurnPointlist)
                {
                    m_TurningPoints.Add(point);
                    aveMovement += Math.Abs(point.Movement);
                }

                aveMovement /= analyzePriceAction.m_TurnPointlist.Count;
                txtAnalyzeMovement.Text = aveMovement.ToString();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            
        }
    }
}
