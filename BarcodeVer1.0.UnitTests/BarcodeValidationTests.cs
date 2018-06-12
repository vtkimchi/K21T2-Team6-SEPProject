using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BarcodeVer1._0.Controllers;
using System.Web.Mvc;
using System.Web;
using Moq;

namespace BarcodeVer1._0.UnitTests
{
    [TestClass]
    public class BarcodeValidationTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            Controllers.LessonController controller = new LessonController();
            string act = "T154810";
            var viewResult = controller.CreateDate(act) as RedirectToRouteResult;
            Assert.IsNotNull(viewResult);
            Assert.Fail();


        }
    }
}
