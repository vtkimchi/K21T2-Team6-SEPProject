using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarcodeVer1._0.Controllers;
using BarcodeVer1._0.Models;
using BarcodeVer1._0.UnitTests.Support;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BarcodeVer1._0.UnitTests
{
    [TestClass]
    public class EditAttendanceInformationValidationTests
    {
        SEPEntities db = new SEPEntities();
        /// <summary>
        /// Purpose of TC:
        /// - Validate whether change status of roll-call, the status of a member is changed and saved into database,
        ///     and the user is redirected to the Detail action
        /// </summary>
        [TestMethod]
        public void ValidateEditStatus_WithValidProgress_ExpectValidNavigation()
        {
            // Arr
            var controller = new Controllers.AttendanceController();
            string msv = "T153556";            
            var b = db.Attendances.FirstOrDefault(x => x.Member.MaSV == msv).ID;

            // Act
            var redirectRoute = controller.EditStatus(b) as RedirectToRouteResult;

            // Assert
            Assert.AreEqual("Detail", redirectRoute.RouteValues["action"]);
            
        }

        /// <summary>
        /// Purpose of TC:
        /// - Validate whether write the note of attendance table, the note is saved into database,
        ///     and the user is redirected to the Detail action
        /// </summary>
        [TestMethod]
        public void ValidateWriteNote_WithValidModel_ExpectValidNavigation()
        {
            // Arr
            var controller = new Controllers.AttendanceController();
            string studentId = "T153556";
            var attendanceId = db.Attendances.FirstOrDefault(x => x.Member.MaSV == studentId).ID;
            Attendance TestAtten = new Attendance();
            TestAtten.Note = "đi trễ";
            TestAtten.ID_Student = attendanceId;

            // Act
            var redirecRoute = controller.WriteNote(TestAtten) as RedirectToRouteResult;

            // Assert
            Assert.AreEqual("Detail", redirecRoute.RouteValues["action"]);
        }


        /// <summary>
        /// Purpose of TC:
        /// - Validate whether edit the note of attendance table, the note is changed and saved into database,
        ///     and the user is redirected to the Detail action
        /// </summary>
        [TestMethod]
        public void ValidateEditNote_WithValidateModel_ExpectValidNavigation()
        {
            // Arr
            var controller = new Controllers.AttendanceController();
            string studentId = "T153556";
            var attendanceId = db.Attendances.FirstOrDefault(x => x.Member.MaSV == studentId).ID;
            string id = attendanceId.ToString();

            // Act
            var redirecRoute = controller.ChangeNote(id) as PartialViewResult;

            // Assert
            Assert.AreEqual("EditPartial_", redirecRoute.ViewName);
        }
    }
}
