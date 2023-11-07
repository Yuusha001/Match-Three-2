using System.Collections;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class Utils
{
    public static void DoAction(MonoBehaviour root, System.Action action, float delay)
    {
        root.StartCoroutine(CoAction(action, delay));
    }

    private static IEnumerator CoAction(System.Action action, float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        if (action != null)
            action();
    }

   public static async UniTask DoAction(System.Action action, int delay)
    {
        await UniTask.WaitForSeconds(delay);
        if (action != null)
            action(); 
    } 
}


