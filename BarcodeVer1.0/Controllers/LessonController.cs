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
        SEP_DBEntities1 db = new SEP_DBEntities1();
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
            var item = db.Lessons.Where(x => x.Day == nLesson.Day && x.MaKH == id).ToList();
            //
            if (item.Count() == 0)
            {
                db.SaveChanges();
                AddDay(id);
            }

            return RedirectToAction("Detail/" + id, "Lesson");
        }

        public void AddDay(string id)
        {
            var day = DateTime.Now.Date;
            Attendance nAttendance = new Attendance();
            var danhsachdiemdanh = connect.GetMember(id).ToList();
            //tap danh sach sv trong buoi diem danh
            foreach (var sv in danhsachdiemdanh)
            {
                nAttendance.ID_Lesson = db.Lessons.FirstOrDefault(x => x.Day == day && x.MaKH == id).ID;
                nAttendance.ID_Student = sv.id;
                var nLesson = db.Lessons.Where(x => x.MaKH == id).Count();
                nAttendance.Count_Lesson = nLesson;
                db.Attendances.Add(nAttendance);
                db.SaveChanges();
            }

        }

        public ActionResult Detail(string id)
        {
            var member = connect.GetMember(id).ToList();
            return View(member);
        }
    }
}