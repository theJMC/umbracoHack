using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;

namespace UmbracoHack25.Controllers
{
    public class SupportServicesController : RenderController
    {
        public SupportServicesController(ILogger<RenderController> logger, ICompositeViewEngine compositeViewEngine, IUmbracoContextAccessor umbracoContextAccessor)
            : base(logger, compositeViewEngine, umbracoContextAccessor)
        {
        }
        public override IActionResult Index()
        {
            var foo = "bar";
            return CurrentTemplate(CurrentPage);
        }
    }
}
