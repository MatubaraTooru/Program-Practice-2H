using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public static class ImageExtensions
{
    public static IEnumerator FadeInAsync(this Image image, float duration)
    {
        var from = image.color;
        from.a = 0;

        var to = image.color;
        to.a = 1;
        return FadeAsync(image, from, to, duration);
    }

    public static IEnumerator FadeAsync(Image image, Color from, Color to, float duration)
    {
        for (var t = 0F; t < duration; t += Time.deltaTime)
        {
            image.color = Color.Lerp(from, to, t / duration);
            yield return null;
        }
    }
}
