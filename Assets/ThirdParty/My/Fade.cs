using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIt : MonoBehaviour
{
    public float TimeToStart = 1;
    public float FadeTime = 1;
    public bool destroy = false;
    void Start()
    {
        StartCoroutine(Fading());
    }
    IEnumerator Fading()
    {
        yield return new WaitForSeconds(TimeToStart);
        transform.Fade(FadeTime);
        Destroy(gameObject, FadeTime);
    }
}
