using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BarcodeVer1._0.Models;
using BarcodeVer1._0.Interface;
using System.Web.Security;

namespace BarcodeVer1._0.Controllers
{
    public class AccountController : Controller
    {
        public static Connect_API API = new Connect_API();

        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        //Post: Login
        [HttpPost]
        public ActionResult Login(string username, string password, string Link_connect)
        {
            API.Set(Link_connect);
            //get id when login 
            var id = API.Check_Login(username, password);
            if (id != null)
            {
                //check connect if id !="" mean login success          
                if (id.code == 0)
                {
                    Session["id"] = id.data.id;
                    Session["secret"] = id.data.secret;
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
            else
            {
                ViewBag.error = "Wrong Link API";
                return View();
            }
        }

        [HttpGet]
        public ActionResult LogOut()
        {
            Session["id"] = null;
            Session["username"] = null;
            Session["ID_Course"] = null;
            Session["secret"] = null;
            return RedirectToAction("Login", "Account");
        }
    }
}