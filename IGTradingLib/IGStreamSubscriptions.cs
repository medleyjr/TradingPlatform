using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IGPublicPcl;
using Lightstreamer.DotNet.Client;
using dto.endpoint.auth.session;

using IGTradingLib.DataModel;
using Newtonsoft.Json;

namespace IGTradingLib
{
    public class IGStreamSubscriptions : HandyTableListenerAdapter
    {
        protected IIGStatusEvent m_IIGStatusEvent = null;
        public SubscribedTableKey m_SubscriptionKey = null;

        public IGStreamSubscriptions(IIGStatusEvent statusEvent)
        {
            m_IIGStatusEvent = statusEvent;
        }

        protected void RaiseErrorMessage(string msg)
        {
            if (m_IIGStatusEvent == null)
                return;

            m_IIGStatusEvent.RaiseError(msg);
        }
    }

    public class PriceDataSubscription : IGStreamSubscriptions
    {
        
        public PriceDataSubscription(IIGStatusEvent statusEvent)
            : base(statusEvent)
        {
        }

        public DateTime LastPriceUpdate { get; set; }

        public override void OnUpdate(int itemPos, string itemName, IUpdateInfo update)
        {
            try
            {
                itemName = itemName.Remove(0, 3);
                var lsL1PriceData = L1LsPriceUpdateData(itemPos, itemName, update);

                LastPriceUpdate = DateTime.Now;

                //offline state
                if (lsL1PriceData.MarketState == "EDIT")
                    return;

                DataStreamPrice dataPrice = new DataStreamPrice();
                //dataPrice.PriceDatetime = DateTime.Now;

                if (lsL1PriceData.Offer.HasValue && lsL1PriceData.Offer.Value != 0)
                    dataPrice.AskPrice = lsL1PriceData.Offer.Value;
                else
                    return;

                if (lsL1PriceData.Bid.HasValue && lsL1PriceData.Bid.Value != 0)
                    dataPrice.BidPrice = lsL1PriceData.Bid.Value;
                else
                    return;

                TradingDataStreamMgr.AddDataPrice(itemName, dataPrice);

                //string msg = string.Format("Item : {0}, Bid : {1}, Offer : {2}", itemName, lsL1PriceData.Bid, lsL1PriceData.Offer);
                //RaiseErrorMessage(msg);
                
            }
            catch (Exception ex)
            {
                RaiseErrorMessage(ex.Message);
            }
        }
    }

    public class PositionSubscription : IGStreamSubscriptions
    {
        AccountDetails m_AccountID = null;

        public PositionSubscription(IIGStatusEvent statusEvent, AccountDetails accountID)
            : base(statusEvent)
        {
            m_AccountID = accountID;
        }
        

        public override void OnUpdate(int itemPos, string itemName, IUpdateInfo update)
        {
            try
            {
                var confirms = update.GetNewValue("CONFIRMS");
                var opu = update.GetNewValue("OPU");
                var wou = update.GetNewValue("WOU");

                LsTradeSubscriptionData confirmsUpdate = null;

                if (confirms != null)
                {
                    confirmsUpdate = JsonConvert.DeserializeObject<LsTradeSubscriptionData>(confirms);
                    if (m_AccountID != null && confirms != null)
                        LibDef.m_StatusEvent.PositionChangeEvent(m_AccountID, confirmsUpdate);
                }

                if (opu != null)
                {
                    var opuUpdate = JsonConvert.DeserializeObject<LsTradeSubscriptionData>(opu);
                    if (m_AccountID != null && opuUpdate != null)
                        LibDef.m_StatusEvent.PositionChangeEvent(m_AccountID, opuUpdate);
                }

                if (wou != null)
                {
                    var wouUpdate = JsonConvert.DeserializeObject<LsTradeSubscriptionData>(wou);
                    if (m_AccountID != null && wouUpdate != null)
                        LibDef.m_StatusEvent.PositionChangeEvent(m_AccountID, wouUpdate);
                }

                
                //confirmsUpdate.

            }
            catch (Exception ex)
            {
                RaiseErrorMessage(ex.Message);
            }
        }
    }

    public class AccountSubscription : IGStreamSubscriptions
    {
        AccountDetails m_AccountID =  null;

        public AccountSubscription(IIGStatusEvent statusEvent, AccountDetails accountID)
            : base(statusEvent)
        {
            m_AccountID = accountID;
        }


        public override void OnUpdate(int itemPos, string itemName, IUpdateInfo update)
        {
            try
            {

                var accData = StreamingAccountDataUpdates(itemPos, itemName, update);
                LibDef.m_StatusEvent.AccountChangeEvent(m_AccountID, accData);

            }
            catch (Exception ex)
            {
                RaiseErrorMessage(ex.Message);
            }
        }
    }
}
