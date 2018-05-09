using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BarcodeVer1._0.Models;
using BarcodeVer1._0.Interface;

namespace BarcodeVer1._0.Controllers
{
    public class LessonController : Controller
    {
        Connect_API connect = new Connect_API();
        SEP_DBEntities db = new SEP_DBEntities();
        // GET: Lesson
        public ActionResult Index()
        {
            return View();
        }


        //demo tao buoi hoc (giao vien tao tung ngay chua xu ly bat loi)
        public ActionResult CreateDate(string id)
        {
            //tao 1 buoi hoc moi
            Lesson nLesson = new Lesson();
            //ma khoa hoc duoc giao vien nhap vo
            nLesson.MaKH = id;
            //ngay hoc se duoc lay tu he thong
            nLesson.Day = DateTime.Now.Date;
            //luu lai duoi database
            db.Lessons.Add(nLesson);
            //
            var item =db.Lessons.Where(x => x.Day == nLesson.Day && x.MaKH==id);
            //
            if (item ==null)
            {
                db.SaveChanges();
            }
            
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Detail(string id)
        {
            var member = connect.GetMember(id).ToList();
            List<object> model = new List<object>();
            model.Add(member);
            return View(model);
        }
    }
}