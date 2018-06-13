using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BarcodeVer1._0.UnitTests.Support
{
    public class MockHelper
    {
        public Mock<HttpContextBase> Context { get; set; }
        public Mock<HttpSessionStateBase> Session { get; set; }
        public Mock<HttpRequestBase> Request { get; set; }
        public MockHelper()
        {
            Context = new Mock<HttpContextBase>();
            Session = new Mock<HttpSessionStateBase>();
        }
        public Mock<HttpContextBase> MakeFakeContext()
        {
            //            Context.Setup(c => c.Request).Returns(Request.Object);
            Context.Setup(c => c.Session).Returns(Session.Object);
            return Context;
        }
    }
}
