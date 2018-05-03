using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BarcodeVer1._0.Models;

namespace BarcodeVer1._0.Controllers
{
    public class LessonController : Controller
    {
        SEP_DBEntities db = new SEP_DBEntities();
        // GET: Lesson
        public ActionResult Index()
        {
            return View();
        }


        //demo tao buoi hoc (giao vien tao tung ngay chua xu ly bat loi)
        [HttpPost]
        public ActionResult CreateDate(string course)
        {
            //tao 1 buoi hoc moi
            Lesson nLesson = new Lesson();
            //ma khoa hoc duoc giao vien nhap vo
            nLesson.MaKH = course;
            //ngay hoc se duoc lay tu he thong
            nLesson.Day = DateTime.Now.Date;
            //luu lai duoi database
            db.Lessons.Add(nLesson);
            db.SaveChanges();
            return View("Index");
        }
    }
}