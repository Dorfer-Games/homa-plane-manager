using System;
using UnityEngine;

public class TriggerListenerComponent : MonoBehaviour
{
    public event Action<Transform, Transform> OnTriggerEnterEvent;
    public event Action<Transform> OnTriggerStayEvent;
    public event Action<Transform> OnTriggerExitEvent;
    public event Action<Transform> OnCollisionEnterEvent;
    public event Action<Transform> OnCollisionStayEvent;
    public event Action<Transform> OnCollisionExitEvent;

    void OnTriggerEnter(Collider other)
    {
        OnTriggerEnterEvent?.Invoke(other.transform, transform);
    }
    void OnTriggerStay(Collider other)
    {
        OnTriggerStayEvent?.Invoke(other.transform);
    }
    void OnTriggerExit(Collider other)
    {
        OnTriggerExitEvent?.Invoke(other.transform);
    }
    void OnCollisionEnter(Collision collision)
    {
        OnCollisionEnterEvent?.Invoke(collision.transform);
    }
    void OnCollisionStay(Collision collision)
    {
        OnCollisionStayEvent?.Invoke(collision.transform);
    }
    void OnCollisionExit(Collision collision)
    {
        OnCollisionExitEvent?.Invoke(collision.transform);
    }
}