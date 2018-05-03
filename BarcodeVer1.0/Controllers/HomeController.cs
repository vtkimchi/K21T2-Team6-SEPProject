using BarcodeVer1._0.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BarcodeVer1._0.Controllers
{
    public class HomeController : Controller
    {
        private Connect_API API = new Connect_API();

        //note: chua dat dieu kien neu chua login khong duoc vao trang chinh

        public ActionResult Index()
        {
            //get id course and show it on view
            string id = (string)Session["id"];
            string test = API.GetLecturer(id);
            test = test.Trim();
            ViewBag.test = test;
            return View();
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