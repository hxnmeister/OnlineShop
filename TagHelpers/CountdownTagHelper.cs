using Microsoft.AspNetCore.Razor.TagHelpers;

namespace OnlineShop.TagHelpers
{
    [HtmlTargetElement(tag: "countdown", Attributes = "countdown-time", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class CountdownTagHelper : TagHelper
    {
        private const int MAX_COUNTDOWN_DOTS = 10;

        [HtmlAttributeName(name: "text")]
        public string? Text { get; set; } = "Wait till dots disapear";

        [HtmlAttributeName(name: "countdown-time")]
        public int CountdownTime { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string uniqueId = $"countdown-{Guid.NewGuid()}";
            string dots = new('.', Math.Min(CountdownTime, MAX_COUNTDOWN_DOTS));

            output.TagName = "div";
            output.Attributes.SetAttribute("id", uniqueId);
            output.Content.SetHtmlContent($"{Text}{dots}");

            output.PostElement.SetHtmlContent($@"
                <script>
                    document.addEventListener('DOMContentLoaded', () => {{
                        startCountdown({CountdownTime}, '{uniqueId}', '{Text}');
                    }});
                </script>");
        }
    }
}
