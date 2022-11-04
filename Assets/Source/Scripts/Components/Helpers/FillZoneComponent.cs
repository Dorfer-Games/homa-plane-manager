using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using NaughtyAttributes;
using Supyrb;
using System.Linq;

public class FillZoneComponent : MonoBehaviour
{
    [SerializeField] Image fillImage;

    [Tag] public string targetTag = "Player";
    public float value = 0;
    public float time = 1f;
    public float zeroTime = 1f;

    Sequence sequence;
    private Guid uid;

    private void Start()
    {
        fillImage.fillAmount = value;
        Zero(value*zeroTime);
    }
    void Done()
    {
        KillSequence();
        value = 0;
        fillImage.fillAmount = 0;
        Signals.Get<SignalFillZone>().Dispatch(this);
    }
    public void Launch(float timer = 1)
    {
        KillSequence();

        sequence.Append(DOTween.To(() => value, x => value = x, 1f, timer).OnComplete(Done));
        fillImage.DOFillAmount(1, timer);

        SetSequence();
    }
    public void Zero(float time = 1)
    {
        KillSequence();

        sequence.Append(DOTween.To(() => value, x => value = x, 0, time));
        fillImage.DOFillAmount(0, time);

        SetSequence();
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
            Launch((1f - value) * time);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(targetTag))
            Zero(value * zeroTime);
    }
    void KillSequence()
    {
        sequence.Kill();
        sequence = null;
        fillImage.DOKill();
        sequence = DOTween.Sequence();
    }

    void SetSequence()
    {
        uid = System.Guid.NewGuid();
        sequence.id = uid;
    }
}