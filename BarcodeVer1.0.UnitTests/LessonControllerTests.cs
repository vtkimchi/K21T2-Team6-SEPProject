using BarcodeVer1._0.Models;
using BarcodeVer1._0.UnitTests.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

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


        [TestMethod]
        public void ValidateCreateDate_WithDateHasExisted_ExpectShowMessage()
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
            var redirectRoute = controller.CreateDate(data) as ViewResult;

            // Assert
            Assert.AreEqual("Lesson was created earlier", redirectRoute.ViewBag.mess);
            Assert.IsNotNull(redirectRoute);
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

            var helper = new MockHelper();
            var context = helper.MakeFakeContext();
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

            // Arr
            string courseId = "TH2";

            var redirectRoute = controller.Detail(courseId) as ViewResult;

            // Assert
            Assert.IsNotNull(redirectRoute);
            Assert.AreEqual("TH2", redirectRoute.ViewBag.MaKH);
            Assert.AreEqual(5, redirectRoute.ViewBag.Total);
        }

        /// <summary>
        /// Purpose of TC:
        /// - Validate whether post to server of IT department,
        ///     and the user is redirected to ListLesson action and Lesson controller
        /// </summary>
        [TestMethod]
        public void ValidatePostSyncAttendance_WithValid_ExpectValidNaigate()
        {
            // Arr
            var controller = new Controllers.LessonController();

            var moqContext = new Moq.Mock<ControllerContext>();
            var moqSession = new Moq.Mock<HttpSessionStateBase>();
            moqContext.Setup(c => c.HttpContext.Session).Returns(moqSession.Object);
            controller.ControllerContext = moqContext.Object;

            // Act
            string id = "TH";
            string secret = "-1781996535";

            moqSession.Setup(s => s["id"]).Returns(id);
            moqSession.Setup(s => s["secret"]).Returns(secret);
            string courseId = "TH2";

            var redirectRoute = controller.PostSyncAttendance(courseId) as RedirectToRouteResult;

            // Assert
            Assert.AreEqual("ListLesson", redirectRoute.RouteValues["action"]);
            Assert.AreEqual("Lesson", redirectRoute.RouteValues["controller"]);                     

        }
    }
}
