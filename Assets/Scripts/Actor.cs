using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Actor : MonoBehaviour
{
    [SerializeField]
    private Image _backImage = null;

    [SerializeField]
    private Image _foreImage = null;

    [SerializeField]
    private Vector2 _fromPos;

    [SerializeField]
    private Vector2 _toPos;

    [SerializeField]
    private Sprite[] _sprites = null; // 表情差分一覧

    private void Start()
    {
        //StartCoroutine(ShowAsync(_foreImage, 2));
        //StartCoroutine(MoveAsync(_foreImage, 2));
        StartCoroutine(Test());
    }

    IEnumerator Test()
    {
        for (int i = 0; i < _sprites.Length; i++)
        {
            yield return SetImageAsync(i, 2);
        }
    }

    public IEnumerator ShowAsync(Image image, float duration = 0)
    {
        if (duration == 0)
        {
            var c = image.color;
            c.a = 1;
            image.color = c;
        }
        yield return image.FadeInAsync(duration);
    }

    public IEnumerator HideAsync(Image image, float duration = 0)
    {
        if (duration == 0)
        {
            var c = image.color;
            c.a = 0;
            image.color = c;
        }
        yield return image.FadeOutAsync(duration);
    }

    public IEnumerator MoveAsync(Image image, float duration)
    {
        yield return image.GetComponent<RectTransform>().MoveAsync(_fromPos, _toPos, duration);
    }

    
    public Coroutine SetImageAsync(int index, float duration = 0)
    {
        var sprite = _sprites[index];
        if (_foreImage.sprite == null) // 初期化時
        {
            // 表情フェードせず表示のみ
            _foreImage.sprite = sprite;
            return StartCoroutine(ShowAsync(_foreImage, duration));
        }

        // 現在の表情が指定された場合は無視
        if (_foreImage.sprite == sprite) { return null; }

        return StartCoroutine(SetImageInternalAsync());

        IEnumerator SetImageInternalAsync()
        {
            // 前面画像を後ろに
            _foreImage.transform.SetSiblingIndex(0);

            // 背面画像に次の画像を設定
            var image = _backImage;
            image.sprite = sprite;

            // 次の画像をフェードイン
            yield return StartCoroutine(ShowAsync(image, duration));

            // 背面画像を非表示に
            yield return StartCoroutine(HideAsync(_foreImage, 0));

            // 前面画像と背面画像を入れ替える
            _backImage = _foreImage;
            _foreImage = image;
        }
    }
}
