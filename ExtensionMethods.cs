using Umbraco.Core.Models;
using Umbraco.Web;
using Umbraco.Web.Models;

namespace Camelonta.Utilities
{
    public static class ExtensionMethods
    {
        #region IPublishedContent

        public static string NavName(this IPublishedContent page)
        {
            if (page.GetProperty("navName").HasValue)
                return page.GetProperty("navName").Value.ToString();
            return page.Name;
        }

        public static string IfPageIsActive(this IPublishedContent page, RenderModel model, string cssClass)
        {
            if (model.Content.Id == page.Id)
                return cssClass;
            return null;
        }

        public static string IfPageIsActive(this IPublishedContent page, IPublishedContent currentPage, string cssClass)
        {
            if (currentPage.Id == page.Id)
                return cssClass;
            return null;
        }

        public static string IfPageIsCurrent(this IPublishedContent page, RenderModel model, string cssClass)
        {
            if (page.IsAncestorOrSelf(model.Content))
                return cssClass;
            return null;
        }

        public static string IfPageIsCurrent(this IPublishedContent page, IPublishedContent currentPage, string cssClass)
        {
            if (page.IsAncestorOrSelf(currentPage))
                return cssClass;
            return null;
        }

        public static string GetUrl(this IPublishedContent page)
        {
            var url = string.Empty;
            if (page != null)
            {
                if (page.HasProperty("externalLink"))
                {
                    url = page.GetPropertyValue<string>("externalLink");
                }
                else
                {
                    url = page.Url;
                }
            }

            return url;
        }

        #endregion

        #region String

        /// <summary>
        /// Truncate string at first word after set length
        /// </summary>
        public static string TruncateAtWord(this string value, int length, string endWith = "...")
        {
            if (value == null || value.Length < length || value.IndexOf(" ", length) == -1)
                return value;

            return value.Substring(0, value.IndexOf(" ", length)) + endWith;
        }

        #endregion
    }
}