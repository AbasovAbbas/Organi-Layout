using Microsoft.AspNetCore.Mvc;
using Organi.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organi.Domain.ViewModel
{
    public class PagedViewModel
    {
        public PagedViewModel(IOrderedQueryable<Contact> query, int pageIndex, int pageSize)
        {
            PageSize = pageSize;
            PageIndex = pageIndex;
            TotalCount = query.Count();

            Contacts = query
                .Skip((PageIndex - 1) * PageSize)
                .Take(PageSize);
        }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int MaxPageCount
        {
            get
            {
                return (int)Math.Ceiling(TotalCount * 1.0 / PageSize);
            }
        }
        public IEnumerable<Contact> Contacts { get;private set; }

        public string GetPagenation(IUrlHelper url, string action)
        {
            if (TotalCount <= PageSize)
                return "";

            StringBuilder sb = new StringBuilder();
            sb.Append("<ul>");
            if (PageIndex > 1)
            {
                var link = url.Action(action, values: new
                {
                    pageSize = PageSize,
                    pageIndex = PageIndex - 1
                });
                sb.Append($"<li class='prev'><a href='{link}'><i class='fa fa-chevron-left'></i></a></li>");
            }
            else
            {
                sb.Append("<li class='prev disabled'><a href='#'><i class='fa fa-chevron-left'></i></a></li>");
            }
           
            for (int i = 1; i <= MaxPageCount; i++)
            {
                if (i == PageIndex)
                {
                    sb.Append($"<li class = 'active'><a href='#'>{i}</a></li>");
                }
                else
                {
                    var link = url.Action(action, values: new {
                        pageSize = this.PageSize,
                        pageIndex = i });
                    sb.Append($"<li><a href='{link}'>{i}</a></li>");
                }
            }
            if (PageIndex < MaxPageCount)
            {
                var link = url.Action(action, values: new
                {
                    pageSize = this.PageSize,
                    pageIndex = PageIndex + 1
                });
                sb.Append($"<li class='next'><a href='{link}'><i class='fa fa-chevron-right'></i></a></li>");
            }
            else
            {
                sb.Append("<li class='next disabled'><a href='#'><i class='fa fa-chevron-right'></i></a></li>");
            }
            sb.Append("</ul>");

            return sb.ToString();
        }
    }
}
