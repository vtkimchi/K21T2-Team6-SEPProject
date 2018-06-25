using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BarcodeVer1._0.Controllers;
using System.Web.Mvc;
using System.Web;
using BarcodeVer1._0.Models;

namespace BarcodeVer1._0.UnitTests
{
    [TestClass]
    public class HomeControllerTests
    {
        SEPEntities db = new SEPEntities();

        [TestMethod]
        public void ValidateShowCourses_WithValidShow_ExpectValidNavigation()
        {
            // Arr
            var controller = new Controllers.HomeController();

            var moqContext = new Moq.Mock<ControllerContext>();
            var moqSession = new Moq.Mock<HttpSessionStateBase>();
            moqContext.Setup(c => c.HttpContext.Session).Returns(moqSession.Object);
            controller.ControllerContext = moqContext.Object;

            // Act
            var courseId = "TH";
            moqSession.Setup(s => s["id"]).Returns(courseId.ToString);

            var redirectRoute = controller.Index() as ViewResult;

            // Arr
            Assert.IsNotNull(redirectRoute.Model);
           // Assert.AreEqual("Index", redirectRoute.ViewName);
        }
    }
}
