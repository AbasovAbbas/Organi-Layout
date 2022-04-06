using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrganiWebUI.appCode.TagHelpers
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("social")]
    public class SocialTagHelper : TagHelper
    {
        [HtmlAttributeName("asp-link")]
        public string Link { get; set; }
        [HtmlAttributeName("asp-icon-class")]
        public string IconClass { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (string.IsNullOrWhiteSpace(Link))
            {
                return;
            }
            output.TagName = "a";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.Add("href", Link);

            output.Content.SetHtmlContent($"<i class='{IconClass}'></i>");
        }
    }
}
