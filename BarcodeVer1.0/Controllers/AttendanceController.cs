using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BarcodeVer1._0.Models;
namespace BarcodeVer1._0.Controllers
{
    public class AttendanceController : Controller
    {
        SEPEntities db = new SEPEntities();
    
        // GET: Attendance
        public ActionResult Index()
        {
            return View();
        }

        //show danh sach diem danh chi tiet tung ngay
        [HttpGet]
        public ActionResult Detail(string id)
        {
            int mabuoi = int.Parse(id);
            var ID_Lesson = db.Lessons.FirstOrDefault(x => x.ID == mabuoi);
            var model = db.Attendances.Where(x => x.ID_Lesson == ID_Lesson.ID).ToList();
            ViewBag.Day = ID_Lesson.Day.Value.ToString("dd/MM/yyyy");
            //xuat ra si so lop
            ViewBag.Total = model.Count();
            //xuat ra si so di hoc
            ViewBag.Attend = model.Where(x => x.Status == true).Count();
            //xuat ra si so vang hoc
            ViewBag.Miss = model.Where(x => x.Status == false).Count();
            return View(model);
        }

        //show ra danh sach sau khi diem danh
        [HttpGet]
        public ActionResult List()
        {
            var day = DateTime.Now.Date;
            string makh = (string)Session["ID_Course"];
            //tim buoi hoc ngay hom nay
            var ID_lesson = db.Lessons.FirstOrDefault(x => x.Day == day && x.MaKH == makh).ID;
            //xuat ra danh sach di hoc ngay hom nay
            var listmember = db.Attendances.Where(x => x.ID_Lesson == ID_lesson).ToList();
            //xuat ra ngay diem danh
            string str = day.ToString("dd/MM/yyyy");
            ViewBag.Day = str;
            //xuat ra si so lop
            ViewBag.Total = listmember.Count();
            //xuat ra si so di hoc
            ViewBag.Attend = listmember.Where(x => x.Status == true).Count();
            //xuat ra si so vang hoc
            ViewBag.Miss = listmember.Where(x => x.Status == false).Count();
            //
            ViewBag.
            return View(listmember);
        }

        //[HttpPost]
        public ActionResult EditStatus(int id)
        {
            var item = db.Attendances.FirstOrDefault(x => x.ID == id);
            if(item.Status == true)
            {
                item.Status = false;
                
            }
            else
            {
                item.Status = true;
                
            }
            db.SaveChanges();
            return RedirectToAction("List");
        }

      
        public ActionResult EditNote(int id, Attendance p)
        {
            var idAttend = db.Attendances.FirstOrDefault(x => x.ID == id);
            var listAttend = new Attendance();
            listAttend.Note = p.Note;
            db.SaveChanges();
            return RedirectToAction("List");
        }
    }
}