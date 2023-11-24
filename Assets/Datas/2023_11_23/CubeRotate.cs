using System.Threading;
using UnityEngine;
using UnityEditor;
using System.Threading.Tasks;

public class CubeRotate : MonoBehaviour
{
    private void Start()
    {
        // スレッドの最適化をアプリケーションが行うのは困難。
        // ハードウェア環境などにより最適なスレッド数が異なる。

        // そこで、一般的に C# では Thread の代わりに Task を使う。
        // Task は実行したい処理から空いているスレッドを割り当てる管理を最適化してくれる。
        Debug.Log($"Start: {Thread.CurrentThread.ManagedThreadId}");
        for (var i = 0; i < 100; i++)
        {
            var taskID = i;
            Task.Run(() =>
            {
                // 毎回新しいスレッドを作るのではなく、空いているスレッドを再利用するはず。
                Debug.Log($"Task{taskID}: {Thread.CurrentThread.ManagedThreadId}");
            });
        }
    }
    private void Update()
    {
        transform.Rotate(0, 1, 0);
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
