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
            Session["maBuoi"] = id;
            string khoahoc = (string)Session["ID_Course"];
            var ID_Lesson = db.Lessons.FirstOrDefault(x => x.ID == mabuoi);
            var model = db.Attendances.Where(x => x.ID_Lesson == ID_Lesson.ID).ToList();
            ViewBag.Day = ID_Lesson.Day.Value.ToString("dd/MM/yyyy");
            //xuat ra si so lop
            ViewBag.Total = model.Count();
            //xuat ra si so di hoc
            ViewBag.Attend = model.Where(x => x.Status == true).Count();
            //xuat ra si so vang hoc
            ViewBag.Miss = model.Where(x => x.Status == false).Count();
            //
            ViewBag.Lesson = new SelectList(db.Lessons, "ID", "Count");
            return View(model);
        }

        [HttpPost]
        public ActionResult Change(string Count_Lesson)
        {
            var a = Count_Lesson;
            return RedirectToAction("Detail", new { id = Count_Lesson });
        }
                
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
            return RedirectToAction("Detail", new { id=item.ID_Lesson });
        }

        [HttpPost]
        public ActionResult WriteNote(string note, string id)
        {
            int ID_Attendance = int.Parse(id);
            var item = db.Attendances.FirstOrDefault(x => x.ID == ID_Attendance);
            item.Note = note;
            db.SaveChanges();
            return RedirectToAction("Detail", new { id = item.ID_Lesson });
        }

        public ActionResult ChangeNote(string id)
        {
            int ID = int.Parse(id);
            var model = db.Attendances.FirstOrDefault(x => x.ID == ID);
            return PartialView("EditPartial_",model);
        }

    }
}