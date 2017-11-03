using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

namespace AspNetCoreActionDiscovery.Controllers
{
    public class TestController : ControllerBase
    {
        private readonly IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider;

        public TestController(IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
        {
            _actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
        }

        public IActionResult A()
        {
            return Ok("Test A");
        }

        [HttpGet]
        public IActionResult B()
        {
            return Ok("Test B");
        }

        [HttpPost]
        [Route("api/test/c")]
        public IActionResult C(int param)
        {
            return Ok("Test C");
        }

        [HttpPost]
        public IActionResult D(TestModel model)
        {
            return Ok("Test D");
        }

        public IActionResult GetActions()
        {
            return Ok(_actionDescriptorCollectionProvider
                .ActionDescriptors
                .Items
                .OfType<ControllerActionDescriptor>()
                .Select(a => new
                {
                    a.DisplayName,
                    a.ControllerName,
                    a.ActionName,
                    AttributeRouteTemplate = a.AttributeRouteInfo?.Template,
                    HttpMethods = string.Join(", ", a.ActionConstraints?.OfType<HttpMethodActionConstraint>().SingleOrDefault()?.HttpMethods ?? new string[] { "any" }),
                    Parameters = a.Parameters?.Select(p => new
                    {
                        Type = p.ParameterType.Name,
                        p.Name
                    }),
                    ControllerClassName = a.ControllerTypeInfo.FullName,
                    ActionMethodName = a.MethodInfo.Name,
                    Filters = a.FilterDescriptors?.Select(f => new
                    {
                        ClassName = f.Filter.GetType().FullName,
                        f.Scope //10 = Global, 20 = Controller, 30 = Action
                    }),
                    Constraints = a.ActionConstraints?.Select(c => new
                    {
                        Type = c.GetType().Name
                    }),
                    RouteValues = a.RouteValues.Select(r => new
                    {
                        r.Key,
                        r.Value
                    }),
                }));
        }

        public IActionResult GetPages()
        {
            return Ok(_actionDescriptorCollectionProvider
                .ActionDescriptors
                .Items
                .OfType<PageActionDescriptor>()
                .Select(a => new
                {
                    a.DisplayName,
                    a.ViewEnginePath,
                    a.RelativePath,
                }));
        }
    }
}
