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
        SEPEntities db = new SEPEntities();
        // GET: Member

        public ActionResult ListStudent(string id)
        {
            return Json(db.Members.Where(x => x.MaKH == id).Select(s => new { id = s.MaSV, name = s.Lastname+" "+s.Firstname }).ToList(), JsonRequestBehavior.AllowGet);
        }

        //lay du lieu danh sach sinh vien tren api luu xuong database
        [HttpGet]
        public ActionResult GetSynsMember(string id)
        {
            var nAttendance = new Attendance();
            var item = AccountController.API.GetMember(id);
            var lesson = db.Lessons.Where(x => x.MaKH == id);
            foreach (var sv in item)
            {
                if (db.Members.Where(x => x.MaSV == sv.id && x.MaKH == id).Count() == 0)
                {
                    Member nTD = new Member();
                    nTD.Lastname = sv.lastname;
                    nTD.Firstname = sv.firstname;
                    nTD.MaSV = sv.id;
                    nTD.MaKH = id;
                    nTD.Status = true;
                    nTD.Birthday = DateTime.Parse(sv.birthday);
                    db.Members.Add(nTD);
                    db.SaveChanges();
                    //kiem tra luc syns ve co buoi diem danh nao chua
                    if (lesson.Count() != 0)
                    {
                        foreach (var ls in lesson.ToList())
                        {
                            nAttendance.ID_Lesson = ls.ID;
                            nAttendance.ID_Student = nTD.ID;
                            nAttendance.Status = false;
                            db.Attendances.Add(nAttendance);
                            db.SaveChanges();
                        }
                    }
                }
            }
            return RedirectToAction("Detail", "Lesson", new { id = id });
        }

        public ActionResult PostSynsMember(string id)
        {
            var uid = Session["id"].ToString();
            var secret = Session["secret"].ToString();
            var response = AccountController.API.SyncMembers(id,uid,secret);
            var model = db.Members.Where(x => x.MaKH == id && x.Status==false).ToList();
            if (response.code == 0)
            {
                foreach(var item in model)
                {
                    item.Status = true;
                    db.SaveChanges();
                }
            }
            TempData["message"] = response.message;
            return RedirectToAction("Detail", "Lesson", new { id = id });
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
        public ActionResult AddStudent([Bind(Include = "MaSV")] Member mssv)
        {
            var value = AccountController.API.Getstudent(mssv.MaSV);
            string Makh = (string)Session["ID_Course"];
            //string Makh = (string)ViewData["idCourse"];
            var student = new Member();
            var studentID = db.Members.FirstOrDefault(x => x.MaSV == mssv.MaSV && x.MaKH == Makh);
            var nAttendance = new Attendance();
            var lesson = db.Lessons.Where(x => x.MaKH == Makh);
            if (value.code==1)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                ViewBag.mess = "Student is not exist";
                return View();
            }
            else
            {
                if (studentID == null)
                {
                    student.MaSV = value.data.id;
                    student.MaKH = Makh;
                    student.Status = false;
                    student.Firstname = value.data.firstname;
                    student.Lastname = value.data.lastname;
                    student.Birthday = DateTime.Parse(value.data.birthday);

                    db.Members.Add(student);
                    db.SaveChanges();
                    foreach( var item in lesson.ToList())
                    {
                        nAttendance.ID_Lesson = item.ID;
                        nAttendance.ID_Student = student.ID;
                        nAttendance.Status = false;
                        db.Attendances.Add(nAttendance);
                        db.SaveChanges();
                    }
                }
                else
                {
                    ViewBag.mess = "Student has existed in course";
                    return View();
                }
            }
            return RedirectToAction("Detail", "Lesson", new { id = Makh });

        }
    }
         
}