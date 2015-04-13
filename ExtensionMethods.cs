using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core.Models;

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