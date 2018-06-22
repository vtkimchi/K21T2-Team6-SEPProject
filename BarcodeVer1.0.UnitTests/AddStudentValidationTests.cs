using System;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BarcodeVer1._0.Controllers;
using BarcodeVer1._0.Models;
using BarcodeVer1._0.UnitTests.Support;
using System.Web.Routing;
using System.Web;
using System.Linq;

namespace BarcodeVer1._0.UnitTests
{

    [TestClass]
    public class AddStudentValidationTests
    {
        SEPEntities get = new SEPEntities();

        /// <summary>
        /// Purpose of TC:
        /// - Validate whether get valid data with input id of student, a new student is added and saved into database,
        ///     and the user is redirected to the Detail action
        /// </summary>
        [TestMethod]
        public void ValidateAddStudent_WithValidStudentID_ExpectValidNavigation()
        {
           
            var controller = new Controllers.MemberController();

            var moqContext = new Moq.Mock<ControllerContext>();
            var moqSession = new Moq.Mock<HttpSessionStateBase>();
            moqContext.Setup(c => c.HttpContext.Session).Returns(moqSession.Object);
            controller.ControllerContext = moqContext.Object;

            // Arr
            var atten = get.Members.FirstOrDefault(x => x.MaKH == "TH2");
            moqSession.Setup(s => s["ID_Course"]).Returns(atten.MaKH);
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
        public void ValidateAddStudent_WithStudentIDIsExisted()
        {
            var controller = new Controllers.MemberController();

            var moqContext = new Moq.Mock<ControllerContext>();
            var moqSession = new Moq.Mock<HttpSessionStateBase>();
            moqContext.Setup(c => c.HttpContext.Session).Returns(moqSession.Object);
            controller.ControllerContext = moqContext.Object;

            // Arr         

            var atten = get.Members.FirstOrDefault(x => x.MaKH == "TH2");
            moqSession.Setup(s => s["ID_Course"]).Returns(atten.MaKH);
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
        public void ValidateAddStudent_WithInvalidStudentID_ExpectValidationError()
        {
            var controller = new Controllers.MemberController();

            var moqContext = new Moq.Mock<ControllerContext>();
            var moqSession = new Moq.Mock<HttpSessionStateBase>();
            moqContext.Setup(c => c.HttpContext.Session).Returns(moqSession.Object);
            controller.ControllerContext = moqContext.Object;

            // Arr
            var atten = get.Members.FirstOrDefault(x => x.MaKH == "TH2");
            moqSession.Setup(s => s["ID_Course"]).Returns(atten.MaKH);
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
