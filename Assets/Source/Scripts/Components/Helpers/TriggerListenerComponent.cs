using Kuhpik;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerListenerComponent : MonoBehaviour
{
    public event Action<Transform, Transform> CollisionEnterEvent2, CollisionExitEvent2;
    public event Action<Transform> CollisionEnterEvent, CollisionExitEvent;
    public event Action<Transform, Transform> TriggerEnterEvent2, TriggerExitEvent2;
    public event Action<Transform> TriggerEnterEvent, TriggerExitEvent;

    void OnCollisionEnter(Collision collision)
    {
        CollisionEnterEvent?.Invoke(collision.transform);
        CollisionEnterEvent2?.Invoke(collision.transform, transform);
    }

    void OnCollisionExit(Collision collision)
    {
        CollisionExitEvent2?.Invoke(collision.transform, transform);
        CollisionExitEvent?.Invoke(collision.transform);
    }

    void OnTriggerEnter(Collider other)
    {
        TriggerEnterEvent2?.Invoke(other.transform, transform);
        TriggerEnterEvent?.Invoke(other.transform);
    }

    void OnTriggerExit(Collider other)
    {
        TriggerExitEvent2?.Invoke(other.transform, transform);
        TriggerExitEvent?.Invoke(other.transform);
    }
}
