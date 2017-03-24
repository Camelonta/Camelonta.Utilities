using System.Collections.Generic;
using Umbraco.Core.Models;

namespace Camelonta.Utilities.ViewModels
{
    public class Sitemap
    {
        public List<IPublishedContent> RootNodes { get; set; }
    }
}