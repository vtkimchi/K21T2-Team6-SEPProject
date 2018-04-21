using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BarcodeVer1._0.Interface;

namespace BarcodeVer1._0.Controllers
{
    public class LoginController : Controller
    {
        private Connect_API API = new Connect_API();
 
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        //Post: Login
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            string id = API.Check_Login(username, password);
                      
            if (id != "")
            {
                Session["id"] = id;
                string test = API.GetCourse(id);
                Session["username"] = username;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }

        }
    }
}