using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using BarcodeVer1._0.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Routing;
using BarcodeVer1._0.UnitTests.Support;

namespace BarcodeVer1._0.UnitTests
{
    [TestClass]
    public class AccountControllerTests
    {
        [TestMethod]
        public void ValidateLogin_WithValidAccount_ExpectValidNavigation()
        {
            //Arrange
            var helper = new MockHelper();
            var context = helper.MakeFakeContext();
            var controller = new Controllers.AccountController();
            var Username = "phanthihong";
            var password = "brepresper";
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
            //act kiem tra
            var redirectRoute = controller.Login(Username, password) as RedirectToRouteResult;
            //
            Assert.IsNotNull(redirectRoute);
            Assert.AreEqual("Index", redirectRoute.RouteValues["action"]);
            Assert.AreEqual("Home", redirectRoute.RouteValues["controller"]);
        }
        [TestMethod]
        public void ValidateLogin_WithInValidUserName_ExpectValidNavigation()
        {
            //Arrange
            var helper = new MockHelper();
            var context = helper.MakeFakeContext();
            var controller = new Controllers.AccountController();
            var Username = "nguyenthiphuongtrang";
            var password = "brepresper";
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
            //act
            var redirectRoute = controller.Login(Username, password) as ViewResult;
       
            //Assert.AreEqual("Login", redirectRoute.RouteValues["action"]);
            //Assert.AreEqual("Account", redirectRoute.RouteValues["controller"]);
            
            Assert.AreEqual("Wrong Username or Password", redirectRoute.ViewBag.error);

        }
        [TestMethod]
        public void ValidateLogin_WithInValidPassWord_ExpectValidNavigation()
        {
            //Arrange
            var helper = new MockHelper();
            var context = helper.MakeFakeContext();
            var controller = new Controllers.AccountController();
            var Username = "phanthihong";
            var password = "1234567";
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
            //act
            var redirectRoute = controller.Login(Username, password) as ViewResult;
            //Assert
            //Assert.AreEqual("Login", redirectRoute.ViewName);
            Assert.AreEqual("Wrong Username or Password", redirectRoute.ViewBag.error);

        }
    }
}
