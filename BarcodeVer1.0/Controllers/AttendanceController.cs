using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BarcodeVer1._0.Models;
using BarcodeVer1._0.Interface;

namespace BarcodeVer1._0.Controllers
{
    public class AttendanceController : Controller
    {
        SEPEntities db = new SEPEntities();
        Connect_API connect = new Connect_API();
        // GET: Attendance

        //show danh sach diem danh chi tiet tung ngay
        [HttpGet]
        public ActionResult Detail(string id)
        {
            int mabuoi = int.Parse(id); 
            Session["maBuoi"] = id;
            string khoahoc = (string)Session["ID_Course"];
            var ID_Lesson = db.Lessons.FirstOrDefault(x => x.ID == mabuoi);
            if (ID_Lesson != null)
            {
                var model = db.Attendances.Where(x => x.ID_Lesson == ID_Lesson.ID).ToList();
                ViewBag.Day = ID_Lesson.Day.Value.ToString("dd/MM/yyyy");
                ViewBag.Session = ID_Lesson.Count;
                //xuat ra si so lop
                ViewBag.Total = model.Count();
                //xuat ra si so di hoc
                ViewBag.Attend = model.Where(x => x.Status == true).Count();
                //
                ViewBag.Lesson = db.Lessons.Where(x => x.MaKH == khoahoc).ToList();
                //
                ViewBag.ID = ID_Lesson.ID;
                return View(model);
            }
            return RedirectToAction("Detail", "Lesson", new { id = khoahoc });
        }

        public ActionResult Change(string Count_Lesson)
        {
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
        public ActionResult WriteNote(Attendance atten)
        {
         
            var item = db.Attendances.FirstOrDefault(x => x.ID == atten.ID);
            item.Note = atten.Note;
            db.SaveChanges();
            return RedirectToAction("Detail", new { id = item.ID_Lesson });
        }

        public ActionResult StudentInfo(string id)
        {
            int ID = int.Parse(id);
            var model = db.Members.FirstOrDefault(x => x.Attendances.Where(at => at.ID==ID).Count() == 1);
            return PartialView("StudentPartial_",model);
        }


        //tra ve view partial note cua attendance
        public ActionResult ChangeNote(string id)
        {
            int ID = int.Parse(id);
            var model = db.Attendances.FirstOrDefault(x => x.ID == ID);
            return PartialView("EditPartial_",model);
        }


    }
}