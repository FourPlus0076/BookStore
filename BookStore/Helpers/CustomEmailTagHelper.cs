using Microsoft.AspNetCore.Razor.TagHelpers;

namespace BookStore.Helpers
{
    public class CustomEmailTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.Process(context, output);
        }
    }
}
