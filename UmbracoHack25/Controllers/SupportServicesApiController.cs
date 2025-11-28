using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Web;
using System.Linq;

namespace UmbracoHack25.Controllers.Api
{
    [Route("api/supportservices")]
    [ApiController]
    public class SupportServicesApiController : ControllerBase
    {
        private readonly IUmbracoContextAccessor _umbracoContextAccessor;

        public SupportServicesApiController(IUmbracoContextAccessor umbracoContextAccessor)
        {
            _umbracoContextAccessor = umbracoContextAccessor;
        }

        // GET /api/supportservices/1234
        [HttpGet("{id:int}")]
        public IActionResult GetChildren(int id)
        {
            if (!_umbracoContextAccessor.TryGetUmbracoContext(out var umbracoContext))
                return NotFound("Umbraco context not available.");

            var content = umbracoContext.Content.GetById(id);
            if (content == null)
                return NotFound("Content not found.");

            var children = content.Children()
                .Select(c => new
                {
                    c.Id,
                    c.Name,
                    Url = c.Url()
                })
                .ToList();

            return Ok(children);
        }
    }
}
