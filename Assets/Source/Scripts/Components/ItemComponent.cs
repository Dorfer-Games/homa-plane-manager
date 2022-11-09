using NaughtyAttributes;
using UnityEngine;

public class ItemComponent : MonoBehaviour
{
    [SerializeField, BoxGroup("Developer")] Transform model;
    [SerializeField, BoxGroup("Developer")] Transform stackPoint;

    public Transform Model => model;
    public Transform StackPoint => stackPoint;
}