
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyTo : MonoBehaviour
{
    private Transform trsfFinish;
    private float duration = 1;
    private Ease ease;
    private void OnEnable()
    {
        transform.DOMove(trsfFinish.position, duration).SetEase(ease).OnComplete(delegate
        {
            DOVirtual.DelayedCall(1, delegate
            {
                gameObject.SetActive(false);
            });
          //  SoundManager.instance.PlayCoin();
        });
    }

    public void SetUpAndActive(float delay, Transform from, Transform to, float dur, Ease _ease)
    {
        var posInstantiate = Random.insideUnitSphere * 0.58f;
        posInstantiate += from.position;
        posInstantiate.z = to.position.z;
        transform.position = posInstantiate;
        trsfFinish = to;
        duration = dur;
        ease = _ease;

        DOVirtual.DelayedCall(delay, delegate { gameObject.SetActive(true); });
    }

    public void SetUpAndActive(float delay, Vector3 from, Transform to, float dur, Ease _ease)
    {
        var posInstantiate = Random.insideUnitSphere * 0.58f;
        posInstantiate.z = from.z;
        posInstantiate += from;
        transform.position = posInstantiate;
        trsfFinish = to;
        duration = dur;
        ease = _ease;

        DOVirtual.DelayedCall(delay, delegate { gameObject.SetActive(true); });
    }
}
