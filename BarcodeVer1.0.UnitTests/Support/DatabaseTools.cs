using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BarcodeVer1._0.Models;   

namespace BarcodeVer1._0.UnitTests.Support
{
    public static class DatabaseTools
    {
        [AssemblyInitialize]
        public static void CleanDatabase(TestContext context)
        {
            using (var db = new DatabaseContext())
            {
                db.Attendance.RemoveRange(db.Attendance);
                db.Lesson.RemoveRange(db.Lesson);
                db.Member.RemoveRange(db.Member);
                db.SaveChanges();
            }
        }
    }
}
