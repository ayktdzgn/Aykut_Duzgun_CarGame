using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.PackageManager;
using UnityEngine.UI;
using SimpleTween;

public class TapAnimation : MonoBehaviour
{
    private void OnEnable()
    {
        
        GameObject animatedObject = transform.GetChild(0).gameObject;
        StartCoroutine(BlinkToggle());
    }

    private IEnumerator BlinkToggle()
    {
        while (true)
        {
            SimpleTweener.AddTween(() => transform.localScale, x => transform.localScale = x, 1.1f * Vector3.one, 0.3f).Ease(Easing.EaseKick).OnCompleted(() => {
                transform.localScale = Vector3.one;
            });
            yield return new WaitForSecondsRealtime(1f);
        }
    }
}
