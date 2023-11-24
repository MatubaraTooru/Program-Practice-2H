using System.Collections;
using System.Threading;
using UnityEngine;

public class CoroutineCompletionSource<T>
{
    public bool IsCompleted { get; private set; }
    public T Value { get; private set; }

    public void SetValue(T value)
    {
        Value = value;
        IsCompleted = true;
    }

    public CoroutineAwaiter<T> Result => new(this);
}

public struct CoroutineAwaiter<T>
{
    private CoroutineCompletionSource<T> _source;

    public bool IsCompleted => _source.IsCompleted;

    public T Result => _source.Value;

    public CoroutineAwaiter(CoroutineCompletionSource<T> source) : this()
    {
        _source = source;
    }
}

public class CoroutineProvider : MonoBehaviour
{
    /// <summary>
    /// 外から、何か継続的な処理の開始を指示する。
    public CoroutineAwaiter<int> Run(string name, CancellationToken token)
    {
        var ccs = new CoroutineCompletionSource<int>();
        StartCoroutine(RunCoroutine(name, token, ccs));
        return ccs.Result;
    }

    private IEnumerator RunCoroutine(string name, CancellationToken token, CoroutineCompletionSource<int> ccs)
    {
        var elased = 0F;
        while (!token.IsCancellationRequested && elased < 5)
        {
            // 何か継続処理…
            elased += Time.deltaTime;
            Debug.Log($"{name}: {Time.frameCount}");
            yield return null;
        }

        Debug.Log($"{name}が終了しました");

        // 何か結果を返したい
        ccs.SetValue(Time.frameCount);

    }
}