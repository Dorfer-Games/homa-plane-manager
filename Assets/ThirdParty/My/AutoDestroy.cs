using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent] public class AutoDestroy : MonoBehaviour
{
    public float Timer = 1;
    private void Start()
    {
        Destroy(gameObject, Timer);
    }
}
