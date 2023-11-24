using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Actor : MonoBehaviour
{
    [SerializeField]
    private Image _image = null;

    private void Start()
    {
        StartCoroutine(ShowAsync(2));
    }

    public IEnumerator ShowAsync(float duration)
    {
        var from = _image.color;
        from.a = 0;

        var to = _image.color;
        to.a = 1;

        for (var t = 0F; t < duration; t += Time.deltaTime)
        {
            _image.color = Color.Lerp(from, to, t / duration);
            yield return null;
        }
        _image.color = to;
    }
}
