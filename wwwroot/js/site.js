function startCountdown(countdown, elementId, displayText)
{
    const maxDots = 10;
    const textElement = document.getElementById(elementId);

    let dots = Math.min(countdown, maxDots);

    function updateText()
    {
        if (dots > 0)
        {
            --dots;
        }

        let dotsText = '.'.repeat(dots);
        textElement.innerHTML = displayText + dotsText;

        if (countdown > 0)
        {
            --countdown;
            setTimeout(updateText, 1000);
        }
        else
        {
            window.location.href = "/";
        }
    }

    updateText();
}
