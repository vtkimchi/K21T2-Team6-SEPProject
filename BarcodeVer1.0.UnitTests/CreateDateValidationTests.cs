using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using System.Web;
using BarcodeVer1._0.Models;
using System.Linq;

namespace BarcodeVer1._0.UnitTests
{
    [TestClass]
    public class CreateDateValidationTests
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
    }
}
