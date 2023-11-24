using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class Sample : MonoBehaviour
{
    [SerializeField]
    private int _count = 5;

    [SerializeField]
    private float _duration = 1.0f;

    [SerializeField]
    private Color _from = Color.white;

    [SerializeField]
    private Color _to = Color.white;

    // private Image _image;
    private GameObject[] _gameObjects;
    private int _currentImage = 0;
    void Start()
    { 
        _gameObjects = new GameObject[_count];
        // Imageコンポーネントを持つオブジェクトを生成
        for (int i = 0; i < _count; i++)
        {
            var obj = new GameObject($"Image{i}");
            obj.transform.parent = transform;
            var image = obj.AddComponent<Image>();
            image.color = _from;
            _gameObjects[i] = obj;
        }

        StartCoroutine(ImageFadeIn(_gameObjects[_currentImage].GetComponent<Image>()));
    }

    private IEnumerator ImageFadeIn(Image image)
    { 
        float t = 0;
        while (image.color != _to)
        {
            t += Time.deltaTime;
            float p = t / _duration;
            // image.color = _from + ((_to - _from) * p);
            image.color = Color.Lerp(_from, _to, p);
            yield return null;
        }

        image.color = _to;
        StartCoroutine(RotateAnim(_gameObjects[_currentImage].transform));

        if (_currentImage < _gameObjects.Length)
        {
            StartCoroutine(ImageFadeIn(_gameObjects[_currentImage++].GetComponent<Image>()));
        }
    }

    private IEnumerator RotateAnim(Transform transform)
    {
        quaternion r = transform.rotation;
        while (r.value.z < 60)
        {
            r.value.z++;
            transform.rotation = r;
            yield return null;
        }
    }
}
