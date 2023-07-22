using Microsoft.AspNetCore.Razor.TagHelpers;

namespace BookStore.Helpers
{
    [HtmlTargetElement("big")]
    [HtmlTargetElement(Attributes = "big")]
    public class BigTagHelper :TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
           output.TagName= "h2";
           output.Attributes.RemoveAll("big");
            //output.Content.SetHtmlContent();
            output.Attributes.SetAttribute("Class","h2");
        }
    }
}
