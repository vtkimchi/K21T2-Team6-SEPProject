using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Moq;
using System.Web.Routing;

namespace BarcodeVer1._0.UnitTests.Support
{
    public class HttpContextStub
    {
        private static StubSession SessionStub;

        public void CleanReferenceBooks()
        {
            SessionStub = null;
        }

        public static HttpContextBase Get()
        {
            var httpContextStub = new Mock<HttpContextBase>();
            if (SessionStub == null)
            {
                SessionStub = new StubSession();
            }

            httpContextStub.SetupGet(m => m.Session).Returns(SessionStub);
            return httpContextStub.Object;
        }

        public static void SetupController(Controller controller)
        {
            controller.ControllerContext = new ControllerContext { HttpContext = Get() };
        }

        public static void SetupController(Controller controller, RouteData routeData)
        {
            controller.ControllerContext = new ControllerContext { HttpContext = Get(), RouteData = routeData };
        }

        private class StubSession : HttpSessionStateBase
        {
            private readonly Dictionary<string, object> _state = new Dictionary<string, object>();

            public override object this[string name]
            {
                get
                {
                    if (!_state.ContainsKey(name))
                    {
                        return null;
                    }
                    else
                    {
                        return _state[name];
                    }
                }
                set
                {
                    _state[name] = value;
                }
            }
        }
    }
}
