using System;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BarcodeVer1._0.Controllers;
using BarcodeVer1._0.Models;
using BarcodeVer1._0.UnitTests.Support;
using System.Web.Routing;

namespace BarcodeVer1._0.UnitTests
{
    [TestClass]
    public class CreateDayAttendanceTests
    {

        /// <summary>
        /// Purpose of TC:
        /// - Validate whether get valid data with input id of student
        ///     and data necessary include: MaKh and DateTime
        /// </summary 
        [TestMethod]
        public void ValidateCreateDayRollUp_WithValidDateTime()
        {
            // Arrange
            var helper = new MockHelper();
            var context = helper.MakeFakeContext();
            var controller = new Controllers.LessonController();
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

            // Action
            var nLesson = new Lesson
            {
                MaKH = "TH1",
                Day = DateTime.Now.Date
            };
            var validationResults = TestModelHelper.ValidateModel(controller, nLesson);
            var redirectRoute = controller.CreateDate("T154966") as ViewResult;

            //Assert
            //Assert.AreEqual("Detail", redirectRoute.RouteValues["action"]);
            //Assert.AreEqual("Attendance", redirectRoute.RouteValues["controller"]);
            Assert.AreEqual("CreateDate", redirectRoute.ViewName);
        }

        /// <summary>
        /// Purpose of TC:
        /// - Validate whether the attendance lesson has been created before
        /// </summary>
        [TestMethod]
        public void ValidateCreateDay_WithInvaliAlreadyExist()
        {
            // Arrange
            var helper = new MockHelper();
            var context = helper.MakeFakeContext();
            var controller = new Controllers.LessonController();
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

            // Action
            var nLesson = new Lesson
            {
                MaKH = "TH1",
                Day = Convert.ToDateTime("14/06/18")
            };

            var validationResults = TestModelHelper.ValidateModel(controller, nLesson);
            var redirectRoute = controller.CreateDate("T154966") as ViewResult;

            //Assert
            Assert.AreEqual("CreateDate", redirectRoute.ViewName);
            Assert.AreEqual("Lesson was created earlier", redirectRoute.ViewBag.mess);
        }
    }
}
