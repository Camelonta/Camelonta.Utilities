using System.Linq;
using Camelonta.Utilities.ViewModels;
using Umbraco.Core.Models;
using Umbraco.Web.Mvc;
using System.Web.Mvc;

namespace Camelonta.Utilities.Controllers
{
    public class SitemapController : SurfaceController // UmbracoController throws "Value cannot be null. Parameter name: umbracoContext". Issue: http://issues.umbraco.org/issue/U4-5445
    {
        public ActionResult Index(string id)
        {
            return View("~/App_Plugins/Camelonta.Utilities/Sitemap.cshtml", new Sitemap
            {
                RootNode = GetRootNode(id)
            });
        }

        private IPublishedContent GetRootNode(string id)
        {
            IPublishedContent rootNode;
            if (string.IsNullOrEmpty(id))
            {
                rootNode = Umbraco.TypedContentAtRoot().First();
            }
            else
            {
                rootNode = Umbraco.TypedContent(id);
            }
            return rootNode;
        }
    }
}