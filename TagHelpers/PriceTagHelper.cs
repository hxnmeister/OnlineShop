using Microsoft.AspNetCore.Razor.TagHelpers;

namespace OnlineShop.TagHelpers
{
    [HtmlTargetElement("price", Attributes = "current-price, previous-price", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class PriceTagHelper : TagHelper
    {
        [HtmlAttributeName(name: "current-price")]
        public decimal CurrentPrice { get; set; }

        [HtmlAttributeName(name: "previous-price")]
        public decimal? PreviousPrice { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string previousPriceHtml = PreviousPrice > 0 ? $"<span class='text-decoration-line-through text-danger'>{PreviousPrice:F2}$</span>" : string.Empty;
            string currentPriceHtml = $"<span class='ms-2'>{CurrentPrice:F2}$</span>";

            output.TagName = "p";
            output.Content.SetHtmlContent($"Price: {previousPriceHtml} {currentPriceHtml}");
        }
    }
}
