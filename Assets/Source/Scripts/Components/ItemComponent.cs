using NaughtyAttributes;
using UnityEngine;

public class ItemComponent : MonoBehaviour
{
    [SerializeField, BoxGroup("Developer")] ItemType itemType;
    [SerializeField, BoxGroup("Developer")] Transform model;
    [SerializeField, BoxGroup("Developer")] Transform stackPoint;

    public ItemType ItemType => itemType;
    public Transform Model => model;
    public Transform StackPoint => stackPoint;
}