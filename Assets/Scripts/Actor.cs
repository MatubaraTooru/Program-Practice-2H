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
        yield return _image.FadeInAsync(duration);
    }
}
