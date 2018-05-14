using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BarcodeVer1._0.Interface;
using BarcodeVer1._0.Models;

namespace BarcodeVer1._0.Controllers
{
    public class MemberController : Controller
    {
        Connect_API connect = new Connect_API();
        SEPEntities db = new SEPEntities();
        // GET: Member
        public ActionResult Index()
        {
            return View();
        }

        //lay du lieu danh sach sinh vien tren api luu xuong database
        [HttpPost]
        public ActionResult Syns(string id)
        {
            var item = connect.GetMember(id);
            foreach (var sv in item)
            {
                if (db.Members.Where(x => x.MaSV == sv.id && x.MaKH == id).Count() == 0)
                {
                    Member nTD = new Member();
                    nTD.Lastname = sv.lastname;
                    nTD.Firstname = sv.firstname;
                    nTD.MaSV = sv.id;
                    nTD.MaKH = id;
                    db.Members.Add(nTD);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Detail", "Lesson", new { id = Session["ID_Course"] });
        }
    }
}