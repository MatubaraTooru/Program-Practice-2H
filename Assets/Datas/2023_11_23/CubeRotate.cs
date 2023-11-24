using System.Threading;
using UnityEngine;
using UnityEditor;

public class CubeRotate : MonoBehaviour
{
    private CancellationTokenSource _cts = new CancellationTokenSource();
    private void Start()
    {
        var ct = _cts.Token;
        for (int i = 0; i < 7; i++)
        {
            var thread = new Thread(() =>
            {
                while (!ct.IsCancellationRequested) { }
            });
            thread.Start();
        }
    }
    private void Update()
    {
        transform.Rotate(0, 1, 0);
        for (var i = 0; i < 1000000000; i++) { }
    }
    private void OnDestroy()
    {
        _cts.Cancel();
    }
}

public class SampleMenu
{
    [MenuItem("Sample/Test")]
    public static void Test()
    {
        Debug.Log($"ThreadID : {Thread.CurrentThread.ManagedThreadId}");
    }
}
