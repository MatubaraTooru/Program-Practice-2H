﻿using TMPro;
using UnityEngine;

public class MessagePrinter : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _textUi = default;

    [SerializeField]
    private string _message = "";

    [SerializeField]
    private float _speed = 1.0F;

    private float _elapsed = 0; // 文字を表示してからの経過時間
    private float _interval; // 文字毎の待ち時間

    // _message フィールドから表示する現在の文字インデックス。
    // 何も指していない場合は -1 とする。
    private int _currentIndex = -1;

    /// <summary>
    /// 文字出力中かどうか。
    /// </summary>
    public bool IsPrinting
    {
        get
        {
            return _currentIndex + 1 < _message.Length;
        }
    }

    private void Start()
    {
        ShowMessage(_message);
    }

    private void Update()
    {
        if (_textUi is null || _message is null || _currentIndex + 1 >= _message.Length) { return; }

        _elapsed += Time.deltaTime;
        if (_elapsed > _interval)
        {
            _elapsed = 0;
            _currentIndex++;
            _textUi.text += _message[_currentIndex];
        }
    }

    /// <summary>
    /// 指定のメッセージを表示する。
    /// </summary>
    /// <param name="message">テキストとして表示するメッセージ。</param>
    public void ShowMessage(string message)
    {
        if (_textUi is null) { return; }

        _textUi.text = "";
        _interval = _speed / _message.Length;
        _message = message;
        _currentIndex = -1;
    }

    /// <summary>
    /// 現在再生中の文字出力を省略する。
    /// </summary>
    public void Skip()
    {
        _currentIndex = _message.Length;
        _textUi.text = _message;
    }
}