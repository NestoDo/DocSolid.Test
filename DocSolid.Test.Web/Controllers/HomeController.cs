using Docsolid.Test.Web.Models.ViewModel;
using DocSolid.Test.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DocSolution.Test.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<ListBitcoinPriceHistoryViewModel> lst;

            using (SolidDocTestEntities db = new SolidDocTestEntities())
            {
                lst = (from d in db.BitcoinPriceHistories
                           select new ListBitcoinPriceHistoryViewModel
                           {
                               Id = d.Id,
                               CreateDate = d.CreateDate,
                               LastPrice = d.LastPrice,
                               LastChange = d.LastChange,
                               Volume = d.Volume,
                               HighPrice = d.HighPrice,
                               LowPrice = d.LowPrice
                           }).ToList();
            }

            return View(lst);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}