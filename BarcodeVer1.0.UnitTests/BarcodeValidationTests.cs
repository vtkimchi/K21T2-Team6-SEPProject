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
    public class BarcodeValidationTests
    {    

        /// <summary>
        /// Purpose of TC:
        /// - Validate whether get valid data with input id of student, a new student is added and saved into database,
        ///     and the user is redirected to the Detail action
        /// </summary>
        [TestMethod]
        public void ValidateAddStudentModel_WithValidModel_ExpectValidNavigation()
        {
            var helper = new MockHelper();
            var context = helper.MakeFakeContext();
            var controller = new Controllers.MemberController();
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
            // Arr
           // var controller = new MemberController();
            var student = new Member
            {
                MaSV = "T153346",
            };
            var validationResults = TestModelHelper.ValidateModel(controller, student);

            // Act
            var redirectRoute = controller.AddStudent(student) as RedirectToRouteResult;

            //Assert
           // Assert.IsNotNull(redirectRoute);
            Assert.AreEqual("Detail", redirectRoute.RouteValues["action"]);
            Assert.AreEqual("Lesson", redirectRoute.RouteValues["controller"]);
            Assert.AreEqual(0, validationResults.Count);
        }

        /// <summary>
        /// Purpose of TC:
        /// - Validate whether a ID of student input existed
        ///     and data will not be saved into database 
        /// </summary>
        [TestMethod]
        public void ValidateAddStudentModel_WithIDIsExisted()
        {
            // Arr
            var helper = new MockHelper();
            var context = helper.MakeFakeContext();
            var controller = new Controllers.MemberController();
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
            //var controller = new MemberController();
            var student = new Member
            {
                MaSV = "T153556",                
            };
            var validationResults = TestModelHelper.ValidateModel(controller, student);

            // Act
            var redirectRoute = controller.AddStudent(student) as ViewResult;

            //Assert          
           
            Assert.AreEqual(0, validationResults.Count);
            Assert.AreEqual("Student is exist in course", redirectRoute.ViewBag.mess);
        }

        /// <summary>
        /// Purpose of TC:
        /// - Validate whether with invalid iput data
        ///     the student cannot be finded and cannot be saved into database and an error message should be shown.
        /// </summary>
        [TestMethod]
        public void ValidateAddStudentModel_WithInvalidID_ExpectValidationError()
        {
            var helper = new MockHelper();
            var context = helper.MakeFakeContext();
            var controller = new Controllers.MemberController();
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
            // Arr
            //var controller = new MemberController();
            var student = new Member
            {
                MaSV = "T199999",
            };
            var validationResults = TestModelHelper.ValidateModel(controller, student);

            // Act
            var viewResult = controller.AddStudent(student) as ViewResult;

            //Assert
            
            //if (typeof(Member).Equals(new Exception().GetType()))
            //{
            //    Assert.Fail("Student is not exist");
            //}
           // Assert.IsNotNull(viewResult);
//            Assert.IsFalse(viewResult.ViewData.ModelState.IsValid);
            Assert.AreEqual(0, validationResults.Count);
            Assert.AreEqual("Student is not exist", viewResult.ViewBag.mess);
        }
    }
}
