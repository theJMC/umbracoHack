using Umbraco.Cms.Core.Models.PublishedContent;

namespace UmbracoHack25.ViewModels
{
    public class SupportServicesViewModel
    {
        public IPublishedContent? Content { get; set; }
        public IEnumerable<IPublishedContent>? Services { get; set; }
    }
}
