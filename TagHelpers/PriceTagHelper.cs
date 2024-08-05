using Microsoft.AspNetCore.Razor.TagHelpers;
using OnlineShop.Services.Implementations;

namespace OnlineShop.TagHelpers
{
    [HtmlTargetElement(tag: "price", Attributes = "current-price, previous-price", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class PriceTagHelper : TagHelper
    {
        [HtmlAttributeName(name: "current-price")]
        public decimal CurrentPrice { get; set; }

        [HtmlAttributeName(name: "previous-price")]
        public decimal? PreviousPrice { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string previousPriceHtml = PreviousPrice > 0 ? $"<span class='text-decoration-line-through text-danger'>{string.Format("{0:C}", PreviousPrice)}</span>" : string.Empty;
            string currentPriceHtml = $"<span class='ms-2'>{string.Format("{0:C}", CurrentPrice)}</span>";

            output.TagName = "p";
            output.Content.SetHtmlContent($"Price: {previousPriceHtml} {currentPriceHtml}");
        }
    }
}
