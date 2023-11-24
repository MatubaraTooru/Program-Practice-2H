using System.Collections;
using UnityEngine;

public class MessageSequencer : MonoBehaviour
{
    [SerializeField]
    private MessagePrinter _printer = default;

    [SerializeField]
    private string[] _messages = default;

    private void Start()
    {
        StartCoroutine(RunCoroutine());
    }

    private IEnumerator RunCoroutine()
    {
        if (_messages is null or { Length: 0 }) { yield break; }

        var index = 0;
        while (index < _messages.Length)
        {
            if (_printer.IsPrinting) { _printer.Skip(); }
            else { _printer?.ShowMessage(_messages[index++]); }
            while (!Input.GetMouseButtonDown(0)) { yield return null; }
            yield return null;
        }
    }
}