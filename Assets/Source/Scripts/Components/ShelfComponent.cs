using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

public class ShelfComponent : MonoBehaviour
{
    public static HashSet<ShelfComponent> Hashset = new HashSet<ShelfComponent>();

    [SerializeField, BoxGroup("Developer")] List<Transform> pointList;
    [SerializeField, BoxGroup("Developer")] List<ItemComponent> itemList = new List<ItemComponent>();

    public List<Transform> PointList => pointList;
    public List<ItemComponent> ItemList => itemList;

    void OnEnable() => Hashset.Add(this);
    void OnDisable() => Hashset.Remove(this);
}