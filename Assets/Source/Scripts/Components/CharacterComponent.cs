using NaughtyAttributes;
using UnityEngine;
using UnityEngine.AI;

public class CharacterComponent : MonoBehaviour
{
    [SerializeField, BoxGroup("Developer")] Rigidbody rb;
    [SerializeField, BoxGroup("Developer")] Animator animator;
    [SerializeField, BoxGroup("Developer")] TriggerListenerComponent trigger;
    [SerializeField, BoxGroup("Developer")] NavMeshAgent agent;

    [SerializeField, BoxGroup("Agent")] float speed = 3f;
    [SerializeField, BoxGroup("Agent")] Collider coll;

    [SerializeField, BoxGroup("Point")] Transform baggagePoint;
    [SerializeField, BoxGroup("Point")] Transform stackPoint;
    [SerializeField, BoxGroup("Point")] Transform bubblePoint;

    public Rigidbody Rigidbody => rb;
    public Animator Animator => animator;
    public TriggerListenerComponent Trigger => trigger;
    public NavMeshAgent Agent => agent;
    public float Speed => speed;
    public Collider Collider => coll;
    public Transform BaggagePoint => baggagePoint;
    public Transform StackPoint => stackPoint;
    public Transform BubblePoint => bubblePoint;
}