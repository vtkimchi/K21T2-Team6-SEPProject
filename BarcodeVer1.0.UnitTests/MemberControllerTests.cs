using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using BarcodeVer1._0.Models;
using System.Web;
using System.Linq;
using BarcodeVer1._0.UnitTests.Support;

namespace BarcodeVer1._0.UnitTests
{
    [TestClass]
    public class MemberControllerTests
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
            Assert.AreEqual("Student has existed in course", redirectRoute.ViewBag.mess);
            Assert.IsNotNull(redirectRoute);
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
            Assert.IsNotNull(viewResult);
        }

        /// <summary>
        /// Purpose of TC:
        /// - Validate whether get members from server IT department,
        ///     and the user is redirected to Detail action and Lesson controller
        /// </summary>
        [TestMethod]
        public void ValidateSyn_WithIdValid_ExpectValidNavigate()
        {
            // Arr
            var controller = new Controllers.MemberController();

            var moqContext = new Moq.Mock<ControllerContext>();
            var moqSession = new Moq.Mock<HttpSessionStateBase>();
            moqContext.Setup(c => c.HttpContext.Session).Returns(moqSession.Object);
            controller.ControllerContext = moqContext.Object;

            string courseId = "TH1";
            moqSession.Setup(s => s["ID_Course"]).Returns(courseId.Trim);

            // Act
            var redirectRoute = controller.GetSynsMember(courseId) as RedirectToRouteResult;

            // Assert
            Assert.AreEqual("Detail", redirectRoute.RouteValues["action"]);
            Assert.AreEqual("Lesson", redirectRoute.RouteValues["controller"]);
        }

        /// <summary>
        /// Purpose of TC:
        /// - Validate whether with post member to server of IT department,
        ///     and the members are posted to server IT department,
        ///         and the user is redirected to Detail action and Lesson controller
        /// </summary>
        [TestMethod]
        public void ValidPostSynsMember_WithValidSyn_ExpectNavigate()
        {
            // Arr
            var controller = new Controllers.MemberController();
            var moqContext = new Moq.Mock<ControllerContext>();
            var moqSession = new Moq.Mock<HttpSessionStateBase>();
            moqContext.Setup(c => c.HttpContext.Session).Returns(moqSession.Object);
            controller.ControllerContext = moqContext.Object;

            // Act
            string id = "TH";
            string secret = "-1781996535";

            moqSession.Setup(s => s["id"]).Returns(id.Trim);
            moqSession.Setup(s => s["secret"]).Returns(secret.Trim);
            string courseId = "TH2";
            var redirectRoute = controller.PostSynsMember(courseId) as RedirectToRouteResult;

            // Assert
            Assert.AreEqual("Detail", redirectRoute.RouteValues["action"]);
            Assert.AreEqual("Lesson", redirectRoute.RouteValues["controller"]);
        }
    }
}
