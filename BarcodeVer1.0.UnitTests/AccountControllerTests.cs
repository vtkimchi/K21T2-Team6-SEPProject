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
using System.Web;

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
            var url = "https://entool.azurewebsites.net/SEP21";
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
            //act kiem tra
            var redirectRoute = controller.Login(Username, password, url) as RedirectToRouteResult;
            //
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
            var url = "https://entool.azurewebsites.net/SEP21";
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
            //act
            var redirectRoute = controller.Login(Username, password, url) as ViewResult;
       
            Assert.AreEqual("Wrong Username or Password", redirectRoute.ViewBag.error);
            Assert.IsNotNull(redirectRoute);

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
            var url = "https://entool.azurewebsites.net/SEP21";
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
            //act
            var redirectRoute = controller.Login(Username, password, url) as ViewResult;
            //Assert
            Assert.AreEqual("Wrong Username or Password", redirectRoute.ViewBag.error);
            Assert.IsNotNull(redirectRoute);

        }

        /// <summary>
        /// Purpose of TC:
        /// - Validate whether logout of valid account
        ///     and the user is redirected to the Login action and Account controller
        /// </summary>
        [TestMethod]
        public void ValidateLogout_WithValid_ExpectValidNavigation()
        {
            // Arr
            var helper = new MockHelper();
            var context = helper.MakeFakeContext();
            var controller = new Controllers.AccountController();

            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

            // Act
            var redirectRoute = controller.LogOut() as RedirectToRouteResult;

            // Assert
            Assert.AreEqual("Login", redirectRoute.RouteValues["action"]);
            Assert.AreEqual("Account", redirectRoute.RouteValues["controller"]);
        }
    }
}
