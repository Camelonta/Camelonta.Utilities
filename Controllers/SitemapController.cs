using System.Collections.Generic;
using System.Linq;
using System.Web;
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
                //RootNode = GetRootNode(id)
                RootNodes =  GetRootNodes(id)
            });
        }


        private List<IPublishedContent> GetRootNodes(string id)
        {
            var rootNodes = Umbraco.TypedContentAtRoot();
            var rootList = new List<IPublishedContent>();
            if (string.IsNullOrEmpty(id))
            {
                // If there are multiple rootnodes
                if (rootNodes.Count() > 1)
                {
                    // Get current host (ex. saljarnas.se)
                    var host = HttpContext.Request.Url.Host;

                    foreach (var site in rootNodes)
                    {
                        // Get domains for this site
                        var siteDomains = UmbracoContext.Application.Services.DomainService.GetAssignedDomains(site.Id, false);

                        // Check if current site contains current url (host)
                        var siteHasCurrentDomain = siteDomains.FirstOrDefault(s => s.DomainName.Contains(host));

                        if (siteHasCurrentDomain != null)
                            rootList.Add(site);
                        //return site;
                    }
                    return rootList;
                }
                else
                {
                    // If there is only one root-node = return it as list
                    rootList.Add(rootNodes.First());
                    return rootList;
                }
            }

            return null;
        }

        private IPublishedContent GetRootNode(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                var rootNodes = Umbraco.TypedContentAtRoot();

                // If there are multiple rootnodes
                if (rootNodes.Count() > 1)
                {
                    // Get current host (ex. saljarnas.se)
                    var host = HttpContext.Request.Url.Host;

                    foreach (var site in Umbraco.TypedContentAtRoot())
                    {
                        // Get domains for this site
                        var siteDomains = UmbracoContext.Application.Services.DomainService.GetAssignedDomains(site.Id, false);

                        // Check if current site contains current url (host)
                        var siteHasCurrentDomain = siteDomains.FirstOrDefault(s => s.DomainName.Contains(host));

                        if (siteHasCurrentDomain != null)
                            return site;
                    }
                }
                else
                {
                    // If there is only one root-node = return it
                    return rootNodes.First();
                }
            }
            else
            {
                return Umbraco.TypedContent(id);
            }

            return null;
        }
    }
}