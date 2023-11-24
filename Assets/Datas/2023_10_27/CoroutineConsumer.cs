using System.Collections;
using System.Threading;
using UnityEngine;

public class CoroutineConsumer : MonoBehaviour
{
    [SerializeField]
    private CoroutineProvider _provider;

    void Start()
    {
        StartCoroutine(RunCoroutine());
    }

    private IEnumerator RunCoroutine()
    {
        using var ctsA = new CancellationTokenSource();
        using var ctsB = new CancellationTokenSource();

        var a = _provider.Run("処理1", ctsA.Token);
        var b = _provider.Run("処理2", ctsB.Token);

        while (!a.IsCompleted || !b.IsCompleted)
        {
            if (Input.GetKeyDown(KeyCode.A)) { ctsA.Cancel(); }
            if (Input.GetKeyDown(KeyCode.B)) { ctsB.Cancel(); }
            yield return null;
        }

        Debug.Log($"処理1 Result={a.Result}");
        Debug.Log($"処理2 Result={b.Result}");
    }
}