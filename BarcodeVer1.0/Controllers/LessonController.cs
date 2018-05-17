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
        SEPEntities db = new SEPEntities();
        // GET: Lesson
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateDate()
        {
            return View();
        }


        // tao buoi hoc (giao vien tao tung ngay chua xu ly bat loi)
        [HttpPost]
        public ActionResult CreateDate(string data)
        {
            data = data.Trim();
            string id = (string)Session["ID_Course"];
            //tao 1 buoi hoc moi
            Lesson nLesson = new Lesson();
            //ma khoa hoc duoc giao vien nhap vo
            nLesson.MaKH = id;
            //ngay hoc se duoc lay tu he thong
            nLesson.Day = DateTime.Now.Date;
            //luu lai duoi database
            db.Lessons.Add(nLesson);
            //
            var item = db.Lessons.Where(x => x.Day == nLesson.Day && x.MaKH ==id).ToList();
            //
            if (item.Count() == 0)
            {
                db.SaveChanges();
                AddAttendance(id);
                CheckStudent(data,id);
            }

            return RedirectToAction("ListAttendance");
        }


        //them sinh vien vao danh sach diem danh
        public void AddAttendance(string id)
        {
            //lay danh sach sinh vien cua lop do them vao danh sach diem danh
            var item = db.Members.Where(x => x.MaKH == id).ToList();
            foreach(var sv in item)
            {
                Attendance nAttendance = new Attendance();
                nAttendance.ID_Student = sv.ID;
                var day = DateTime.Now.Date;
                nAttendance.ID_Lesson = db.Lessons.FirstOrDefault(x => x.MaKH == id && x.Day == day).ID;
                nAttendance.Count_Lesson = db.Lessons.Where(x => x.MaKH == id).Count();
                db.Attendances.Add(nAttendance);
                db.SaveChanges();
            }
        }


        //kiem tra sinh vien do co di hoc hay khong
        public void CheckStudent(string data, string makhoahoc)
        {
            var list = data.Split(' ');
            foreach(var sv in list)
            {
                var day = DateTime.Now.Date;
                var item = db.Attendances.FirstOrDefault(x => x.Member.MaSV == sv && x.Member.MaKH == makhoahoc && x.Lesson.Day == day);
                //kiem tra sinh vien do co thuoc lop hoc do khong
                if (item != null)
                {
                    item.Status = true;
                    db.SaveChanges();
                }
            }
            
        }


        //show ra danh sach sau khi diem danh
        public ActionResult ListAttendance()
        {
            var day = DateTime.Now.Date;
            string makh = (string)Session["ID_Course"];
            var ID_lesson = db.Lessons.FirstOrDefault(x => x.Day == day && x.MaKH == makh).ID;
            var listmember = db.Attendances.Where(x => x.ID_Lesson ==ID_lesson).ToList();
            string str = day.ToString("dd/MM/yyyy");
            Session["Dayyyy"] = str;
            return View(listmember);
        }

        public ActionResult Detail(string id)
        {
            Session["ID_Course"] = id;
            var item = db.Members.Where(x => x.MaKH == id).ToList();
            return View(item);
        }
    }
}