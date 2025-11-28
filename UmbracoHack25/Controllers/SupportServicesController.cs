using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;
using UmbracoHack25.ViewModels;
using System.Linq;

namespace UmbracoHack25.Controllers
{
    public class SupportServicesController : RenderController
    {
        public SupportServicesController(ILogger<RenderController> logger, ICompositeViewEngine compositeViewEngine, IUmbracoContextAccessor umbracoContextAccessor)
            : base(logger, compositeViewEngine, umbracoContextAccessor)
        {
        }

        // This is your existing page-rendering action
        public override IActionResult Index()
        {
            var viewModel = new SupportServicesViewModel();
            var currentPage = CurrentPage;
            if (currentPage is null)
                return CurrentTemplate(viewModel);

            var supportServices = currentPage.Children();
            viewModel.Content = currentPage;
            viewModel.Services = supportServices;

            return CurrentTemplate(viewModel);
        }
    }
}
