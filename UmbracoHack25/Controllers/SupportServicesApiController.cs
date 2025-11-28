using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Web;
using System.Linq;
using Umbraco.Cms.Core.Models.Blocks;

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

            var foo = content.Children();

            var children = content.Children()
                .ToDictionary(
                    c => c.Id, // key
                    c => new
                    {
                        // Information section
                        information = new
                        {
                            c.Id,
                            c.Name,
                            inviteOnly = c.HasValue("inviteOnly") ? c.Value<bool>("inviteOnly") : false,
                            target = c.HasValue("supportTarget") ? c.Value<string>("supportTarget") : "Everyone",
                            description = c.HasValue("shorterDescription") ? c.Value<string>("shorterDescription") : "",
                            location = c.HasValue("location") ? c.Value<string>("location") : "",
                            Url = c.Url()
                        },
                        // Booking questions section
                        bookingQuestions = new
                        {
                            available = true,
                            target = c.HasValue("additionalServiceInformation")
                                ? c.Value<BlockListModel>("additionalServiceInformation")
                                    ?.Select(b => new
                                    {
                                        funding = b.Content.HasValue("fundedPlacesAvailable") ? "Would you like to apply for funding" : null,
                                    })
                                : null
                        }
                    }
                );


            return Ok(children);
        }
    }
}
