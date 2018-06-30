using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using BarcodeVer1._0.Models;
using System.Linq;
using System.Web;
using BarcodeVer1._0.UnitTests.Support;
using System.Web.Routing;

namespace BarcodeVer1._0.UnitTests
{
    [TestClass]
    public class AttendanceControllerTests
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
            var TestAtten = new Attendance();
            //{

            //    Note = "đi trễ",
            //    ID_Student = attendanceId,
            //};

            TestAtten.ID = attendanceId;
            TestAtten.Note = "đi trễ";

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


        /// <summary>
        /// Purpose of TC:
        /// - Validate whether change the session, the session is changed,
        ///     and the user is redirected to the Detail action of this session
        /// </summary>
        [TestMethod]
        public void ValidateChangeSession_WithValidateModel_ExpectValidNavigation()
        {
            // Arr
            var controller = new Controllers.AttendanceController();
            string sessionId = "2";

            // Act
            var redirectRoute = controller.Change(sessionId) as RedirectToRouteResult;

            // Assert
            Assert.AreEqual("Detail", redirectRoute.RouteValues["action"]);
        }


        /// <summary>
        /// Purpose of TC:
        /// - Validate whether see detail of roll-call for a day
        ///     and the user is redirected to the Detail action and Lesson controller
        /// </summary>
        [TestMethod]
        public void ValidateDetailOfRollCall_WithValidShow_ExpectValidNavigation()
        {
            // 
            var controller = new Controllers.AttendanceController();

            //var moqContext = new Moq.Mock<ControllerContext>();
            //var moqSession = new Moq.Mock<HttpSessionStateBase>();
            //moqContext.Setup(c => c.HttpContext.Session).Returns(moqSession.Object);
            //controller.ControllerContext = moqContext.Object;

            var helper = new MockHelper();
            var context = helper.MakeFakeContext();
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

            var attenID = db.Lessons.FirstOrDefault(x => x.MaKH == "TH2").ID;
            string id = attenID.ToString();

            // Act
            var redirectRoute = controller.Detail(id) as ViewResult;

            // Assert
            Assert.AreEqual("26/06/2018", redirectRoute.ViewBag.Day);
            Assert.AreEqual(1, redirectRoute.ViewBag.Session);
            Assert.AreEqual(6, redirectRoute.ViewBag.Total);
            //Assert.AreEqual(4, redirectRoute.ViewBag.Attend);
            Assert.AreEqual(2, redirectRoute.ViewBag.ID);
            Assert.IsNotNull(redirectRoute);
        }

        /// <summary>
        /// Purpose of TC:
        /// - Validate whether don't have session id,
        ///     and website will load Detail action and Lession controller
        /// </summary>
        [TestMethod]
        public void ValidateDetailOfRollCall_WithInValidId_ExpectValidLoading()
        {
            // Arr
            var controller = new Controllers.AttendanceController();
            var helper = new MockHelper();
            var context = helper.MakeFakeContext();
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

            var sessionId = 0;
            string id = sessionId.ToString();

            // Act
            var redirectRoute = controller.Detail(id) as RedirectToRouteResult;

            // Assert            
            Assert.AreEqual("Detail", redirectRoute.RouteValues["action"]);
            Assert.AreEqual("Lesson", redirectRoute.RouteValues["controller"]);
        }

        /// <summary>
        /// Purpose of TC:
        /// - Validate whether see detail of roll-call for a day
        ///     and the user is redirected to the Detail action and Lesson controller
        /// </summary>
        [TestMethod]
        public void ValidateStudentInfor_WithValidShow_ExpectValidNavigation()
        {
            // Arr
            var controller = new Controllers.AttendanceController();
            string stuId = "T153556";
            var memberId = db.Members.FirstOrDefault(x => x.MaSV == stuId).ID;

            string id = memberId.ToString();

            // Act
            var redirectRoute = controller.StudentInfo(id) as PartialViewResult;

            // Assert
            Assert.AreEqual("StudentPartial_", redirectRoute.ViewName);
        }
    }
}
