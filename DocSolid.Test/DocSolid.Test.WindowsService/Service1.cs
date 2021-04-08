using DocSolid.Test.Model.Model;
using DocSolid.Test.WindowsService.BitcoinPriceService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace DocSolid.Test.WindowsService
{
    public partial class Service1 : ServiceBase
    {
        private Timer _timer = new Timer();
        private string _endpointBitcoin = string.Empty;
        private double _interval = 60000;

        public Service1()
        {
            InitializeComponent();
            _endpointBitcoin = ConfigurationManager.AppSettings["endpointBitCoin"];
            _interval = Convert.ToDouble(ConfigurationManager.AppSettings["triggerInterval"]);
        }

        protected override void OnStart(string[] args)
        {
            SetupProcessingTimer();
        }

        protected override void OnStop()
        {
            this._timer.Stop();
            this._timer = null;
        }

        private void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {

                CallService();
            }
            catch (Exception ex)
            {
                // log here
            }
        }

        public void SetupProcessingTimer()
        {
            this._timer = new System.Timers.Timer();
            this._timer.Interval = _interval;
            this._timer.AutoReset = true;
            this._timer.Enabled = true;
            this._timer.Elapsed += new System.Timers.ElapsedEventHandler(this.timer_Elapsed);
            this._timer.Start();
        }

        public void CallService()
        {
            var bitCoin = new BitCoin(_endpointBitcoin);
            
            using (SolidDocTestEntities db = new SolidDocTestEntities())
            {
                var bitcoinPriceHistory = new BitcoinPriceHistory();
                bitcoinPriceHistory.CreateDate = bitCoin.CreateDate;
                bitcoinPriceHistory.LastPrice = bitCoin.LastPrice;
                bitcoinPriceHistory.LastChange = bitCoin.LastChange;
                bitcoinPriceHistory.Volume = bitCoin.Volume;
                bitcoinPriceHistory.HighPrice = bitCoin.HighPrice;
                bitcoinPriceHistory.LowPrice = bitCoin.LowPrice;

                db.BitcoinPriceHistories.Add(bitcoinPriceHistory);
                db.SaveChanges();
            }
        }
    }
}
