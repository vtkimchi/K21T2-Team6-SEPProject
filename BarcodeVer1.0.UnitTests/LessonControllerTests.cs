using BarcodeVer1._0.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BarcodeVer1._0.UnitTests
{
    [TestClass]
    public class LessonControllerTests
    {
        SEPEntities db = new SEPEntities();

        [TestMethod]
        public void ValidateCreateDate_WithValidModel_ExpectValidNavigate()
        {
            var controller = new Controllers.LessonController();

            var moqContext = new Moq.Mock<ControllerContext>();
            var moqSession = new Moq.Mock<HttpSessionStateBase>();
            moqContext.Setup(c => c.HttpContext.Session).Returns(moqSession.Object);
            controller.ControllerContext = moqContext.Object;

            // Arr
            var atten = db.Members.FirstOrDefault(x => x.MaKH == "TH2");
            moqSession.Setup(s => s["ID_Course"]).Returns(atten.MaKH);

            string data = "T153556";

            // Act
            var redirectRoute = controller.CreateDate(data) as RedirectToRouteResult;

            // Assert
            Assert.AreEqual("Detail", redirectRoute.RouteValues["action"]);
            Assert.AreEqual("Attendance", redirectRoute.RouteValues["controller"]);
        }

        /// <summary>
        /// Purpose of TC:
        /// - Validate whether show detail of lesson,
        ///     and the user see list of student in this cousrse
        /// </summary>
        [TestMethod]
        public void ValidateViewDetail_WithValidModel_ExpectValidShow()
        {
            // Arr
            var controller = new Controllers.LessonController();

            var moqContext = new Moq.Mock<ControllerContext>();
            var moqSession = new Moq.Mock<HttpSessionStateBase>();
            moqContext.Setup(c => c.HttpContext.Session).Returns(moqSession.Object);
            controller.ControllerContext = moqContext.Object;

            // Arr
            string courseId = "TH2";

            var redirectRoute = controller.Detail(courseId) as ViewResult;

            Assert.IsNotNull(redirectRoute);
        }
    }
}
