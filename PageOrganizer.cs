using System;
using System.Collections.Generic;
using Umbraco.Core.Events;
using Umbraco.Core.Logging;
using Umbraco.Core.Models;
using Umbraco.Core.Services;

namespace Camelonta.Utilities
{
    /// <summary>
    /// Organizes pages
    /// </summary>
    public class PageOrganizer
    {
        public void MoveToDatefolder(SaveEventArgs<IContent> e, IContentService contentService,
            string contentTypeToMove = "News", string contentTypeOfContainer = "NewsList", bool moveToMonth = false)
        {

            MoveToDatefolder(e, contentService, new List<string> { contentTypeToMove }, contentTypeOfContainer, moveToMonth);
        }

        /// <summary>
        /// Moves pages into year/month folders
        /// </summary>
        public void MoveToDatefolder(SaveEventArgs<IContent> e, IContentService contentService, List<string> contentTypeToMove, string contentTypeOfContainer = "NewsList", bool moveToMonth = false)
        {
            foreach (var page in e.SavedEntities)
            {
                // Not interested in anything but "create" events.
                if (!page.IsNewEntity()) return;

                // Not interested if the item being added is not a news-page.
                if (!contentTypeToMove.Contains(page.ContentType.Alias)) return;

                var now = page.ReleaseDate.HasValue ? page.ReleaseDate.Value : DateTime.Now;
                var year = now.ToString("yyyy");
                var month = now.ToString("MM");

                IContent yearDocument = null;

                // Get year-document by container (if it is a 4 digit number)
                int n;
                if (int.TryParse(page.Parent().Name, out n))
                {
                    if (n.ToString().Length == 4)
                        yearDocument = page.Parent();
                }
                // Get year-document by parent-siblings
                if (yearDocument == null)
                    foreach (var child in page.Parent().Children())
                    {
                        if (child.Name == year)
                        {
                            yearDocument = child;
                            break;
                        }
                    }

                if (moveToMonth)
                {
                    LogHelper.Warn(this.GetType(), "Move to month is not yet implemented");
                }

                // If the year folder doesn't exist, create it.
                if (yearDocument == null)
                {
                    yearDocument = contentService.CreateContentWithIdentity(year, page.ParentId, contentTypeOfContainer);
                    contentService.Publish(yearDocument);
                }

                // Move the document into the year folder
                contentService.Move(page, yearDocument.Id);
            }
        }
    }
}