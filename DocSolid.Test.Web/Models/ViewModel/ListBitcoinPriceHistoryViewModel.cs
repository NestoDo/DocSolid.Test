using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Docsolid.Test.Web.Models.ViewModel
{
    public class ListBitcoinPriceHistoryViewModel
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public decimal LastPrice { get; set; }
        public decimal LastChange { get; set; }
        public decimal Volume { get; set; }
        public decimal HighPrice { get; set; }
        public decimal LowPrice { get; set; }
    }
}