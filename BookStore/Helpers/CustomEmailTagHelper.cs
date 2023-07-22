using Microsoft.AspNetCore.Razor.TagHelpers;

namespace BookStore.Helpers
{
#nullable disable
    public class CustomEmailTagHelper : TagHelper
    {
        public string MyEmail { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName= "email";
            output.Attributes.SetAttribute("href", $"Emailto:{MyEmail}");
            output.Attributes.Add("Id","My-Email-Id");
            output.Content.SetContent("My-Mail");

        }
    }
}
