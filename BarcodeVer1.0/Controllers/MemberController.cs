using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BarcodeVer1._0.Interface;
using BarcodeVer1._0.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Net;

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

        // them sinh vien vao khoa hoc
        [HttpGet]
        public ActionResult AddStudent()
        {
            ViewBag.MaSV = db.Members.ToList();
            ViewBag.MaKH = db.Members.ToList();
            ViewBag.Firstname = db.Members.ToList();
            ViewBag.Lastname = db.Members.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddStudent([Bind(Include ="MaSV")] Member mssv)
        {
                var data = connect.Getstudent(mssv.MaSV).Split('/');
            //string Makh = (string)Session["ID_Course"];
            //string Makh = "TH2";
            string Makh = (string)Session["ID_Course"];
            var student = new Member();
            var studentID = db.Members.FirstOrDefault(x => x.MaSV == mssv.MaSV && x.MaKH == Makh);
            if (data[0].Equals(""))
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                ViewBag.mess = "Student is not exist";
                return View();
            }
            else
            {
                if (studentID == null)
                {
                    student.MaSV = mssv.MaSV;
                    student.MaKH = Makh;
                    student.Firstname = data[0];
                    student.Lastname = data[1];
                    db.Members.Add(student);
                    db.SaveChanges();
                    
                }
                else
                {
                    ViewBag.mess = "Student is exist in course";
                    return View();
                }                                   
            }
            return RedirectToAction("Detail", "Lesson", new { id = Makh });

        }
         
        }
}