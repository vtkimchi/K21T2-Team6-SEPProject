using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BarcodeVer1._0.Interface;

namespace BarcodeVer1._0.Controllers
{
    public class AccountController : Controller
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
            //get id when login 
            string id = API.Check_Login(username, password);
            //check connect if id !="" mean login success          
            if (id != "")
            {
                Session["id"] = id;               
                Session["username"] = username;
                //when login success return view Index in HomeController
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.error = "Wrong Username or Password";
                return View();
            }

        }

        [HttpGet]
        public ActionResult LogOut()
        {
            Session["id"] = null;
            Session["username"] = null;
            return RedirectToAction("Login", "Account");
        }
    }
}