using System.Collections.Generic;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Web.Routing;
using BarcodeVer1._0.Controllers;
using BarcodeVer1._0.Models;

namespace BarcodeVer1._0.UnitTests.Support
{
    public static class TestModelHelper
    {
        
        public static IList<ValidationResult> ValidateModel(this Controller controller, object viewModel)
        {
            controller.ModelState.Clear();

            var validationContext = new ValidationContext(viewModel, null, null);
            var validationResults = new List<ValidationResult>();

            Validator.TryValidateObject(viewModel, validationContext, validationResults, true);
            foreach (var result in validationResults)
            {
                foreach (var name in result.MemberNames)
                {
                    controller.ModelState.AddModelError(name, result.ErrorMessage);
                }
            }
            return validationResults;
        }

        //public void Navigate(string mail, string pass)
        //{
        //    var routeData = new RouteData();
        //    routeData.Values.Add("action", "Index");
        //    routeData.Values.Add("controller", "Home");

        //    var accountController = GetAccountController(routeData);

        //    _result = accountController.Login(mail, pass);

        //    if (((RedirectToRouteResult)_result).RouteValues["action"].Equals("Index"))
        //    {
        //        _result = agencyController.Index();

        //        var shownLesson = _result.Model<IEnumerable<Lesson>>();
        //        ScenarioContext.Current.Add("agencyProperty", shownLesson);
        //    }
        //}

        //private static AccountController GetAccountController(RouteData routedata)
        //{
        //    var controller = new AccountController();
        //    HttpContextStub.SetupController(controller, routedata);
        //    return controller;
        //}

    }
}
