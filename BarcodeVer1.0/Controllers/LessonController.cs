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

        [HttpGet]
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
            //ViewBag.idCourse = id;
            ViewData["idCourse"] = id;
            //tao 1 buoi hoc moi
            Lesson nLesson = new Lesson();
            //ma khoa hoc duoc giao vien nhap vo
            nLesson.MaKH = id;
            //ngay hoc se duoc lay tu he thong
            nLesson.Day = DateTime.Now.Date;
            //dem coi do la buoi hoc thu bao nhiu
            nLesson.Count = db.Lessons.Where(x => x.MaKH == id).Count() + 1;//neu khong +1 thi so buoi diem danh se bat dau tu 0
            //luu lai duoi database
            db.Lessons.Add(nLesson);
            //
            var item = db.Lessons.Where(x => x.Day == nLesson.Day && x.MaKH ==id).ToList();
            //kiem tra coi buoi hoc do da duoc tao truoc hay chua (chi cho diem danh 1 lan)
            if (item.Count() == 0)
            {
                db.SaveChanges();
                AddAttendance(id);
                CheckStudent(data,id);
            }

            return RedirectToAction("Detail","Attendance", new { id= db.Lessons.FirstOrDefault(x => x.MaKH==id && x.Day==nLesson.Day).ID});
        }


        //them sinh vien vao danh sach diem danh
        [NonAction]
        public void AddAttendance(string id)
        {
            //lay danh sach sinh vien cua lop do them vao danh sach diem danh
            var item = db.Members.Where(x => x.MaKH == id).ToList();
            //them tung sinh vien vao danh sach
            foreach(var sv in item)
            {
                Attendance nAttendance = new Attendance();
                //cac truong co trong db
                nAttendance.ID_Student = sv.ID;
                var day = DateTime.Now.Date;
                nAttendance.ID_Lesson = db.Lessons.FirstOrDefault(x => x.MaKH == id && x.Day == day).ID;
                //them sv vao bang va luu lai
                db.Attendances.Add(nAttendance);
                db.SaveChanges();
            }
        }


        //kiem tra sinh vien do co di hoc hay khong
        [NonAction]
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


        //xem chi tiet khoa hoc
        [HttpGet]
        public ActionResult Detail(string id)
        {
            //xuat ra danh sach sinh vien lop do
            Session["ID_Course"] = id;
            var item = db.Members.Where(x => x.MaKH == id).ToList();
            //kiem tra coi da tao buoi hoc nao chua de xuat chi tiet buoi hoc
            int count = db.Lessons.Where(x => x.MaKH == id).Count();
            if (count != 0)
            {
                
                ViewBag.Count = db.Lessons.FirstOrDefault(x => x.Count == count).ID;
            }
            else
            {
                ViewBag.Count = 0;
            }
            return View(item);
        }
    }
}