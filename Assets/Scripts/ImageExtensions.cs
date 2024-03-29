using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public static class ImageExtensions
{
    /// <summary>
    /// 第一引数を this キーワードで修飾して拡張メソッドにする
    /// </summary>
    /// <param name="image"></param>
    /// <param name="duration"></param>
    /// <returns></returns>
    public static IEnumerator FadeInAsync(this Image image, float duration)
    {
        var from = image.color;
        from.a = 0;

        var to = image.color;
        to.a = 1;
        return FadeAsync(image, from, to, duration);
    }

    public static IEnumerator FadeOutAsync(this Image image, float duration)
    {
        var from = image.color;
        from.a = 1;
        
        var to = image.color;
        to.a = 0;
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

    public static IEnumerator MoveAsync(this RectTransform rectTransform, Vector2 from, Vector2 to, float duration)
    {
        for (var t = 0f; t < duration; t += Time.deltaTime)
        {
            rectTransform.position = Vector2.Lerp(from, to, t / duration);
            yield return null;
        }
    }
}
