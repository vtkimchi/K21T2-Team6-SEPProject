using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BarcodeVer1._0.Controllers;
using System.Web.Mvc;
using System.Web;
using BarcodeVer1._0.Models;
using BarcodeVer1._0.UnitTests.Support;
using System.Web.Routing;
using BarcodeVer1._0.Interface;

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

            //var moqContext = new Moq.Mock<ControllerContext>();
            //var moqSession = new Moq.Mock<HttpSessionStateBase>();
            //moqContext.Setup(c => c.HttpContext.Session).Returns(moqSession.Object);
            //controller.ControllerContext = moqContext.Object;

            var helper = new MockHelper();
            var context = helper.MakeFakeContext();
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

            // Act
            string courseId = "TH";
            context.Setup(s => s.Session["id"]).Returns(courseId);

            var redirectRoute = controller.Index() as ViewResult;

            // Arr
            Assert.IsNotNull(redirectRoute.Model);
           // Assert.AreEqual("Index", redirectRoute.ViewName);
        }

        /// <summary>
        /// Purpose of TC:
        /// - Validate whether navigate to Left menu,
        ///     and the user is redirected to Left menu
        /// </summary>
        [TestMethod]
        public void ValidateLeftMenu_WithValid_ExpectValidNavigation()
        {
            // Arr
            var controller = new Controllers.HomeController();
            var helper = new MockHelper();
            var context = helper.MakeFakeContext();
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

            // Act
            var redirectRoute = controller.LeftMenu() as PartialViewResult;

            // Assert
            Assert.IsNotNull(redirectRoute);
        }

    }
}
