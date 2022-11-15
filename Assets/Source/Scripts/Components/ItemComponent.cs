using NaughtyAttributes;
using UnityEngine;

public class ItemComponent : MonoBehaviour
{
    [SerializeField, BoxGroup("Developer")] ItemType itemType;
    [SerializeField, BoxGroup("Developer")] Transform model;
    [SerializeField, BoxGroup("Developer")] Transform stackPoint;

    [SerializeField, BoxGroup("Baggage")] Transform conveyorPoint;

    public ItemType ItemType => itemType;
    public Transform Model => model;
    public Transform StackPoint => stackPoint;
    public Transform ConveyorPoint => conveyorPoint;

    public void SetConveyorPoint(Transform point)
    {
        conveyorPoint = point;
    }
}