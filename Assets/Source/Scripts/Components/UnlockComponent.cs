using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

public class UnlockComponent : MonoBehaviour
{
    public static HashSet<UnlockComponent> Hashset = new HashSet<UnlockComponent>();

    [SerializeField, BoxGroup("Settings")] int sequentialOpening;
    [SerializeField, BoxGroup("Settings")] int price;
    [SerializeField, BoxGroup("Settings")] GameObject model;

    [SerializeField, BoxGroup("Developer")] Transform bubblePoint;
    [SerializeField, BoxGroup("Developer")] TriggerZoneComponent zone;

    public Transform BubblePoint => bubblePoint;
    public TriggerZoneComponent Zone => zone;
    public GameObject Model => model;
    public int GetOpeningNumber() => sequentialOpening;
    public int GetPrice() => price;

    void OnEnable() => Hashset.Add(this);
    void OnDisable() => Hashset.Remove(this);

    public void SetPrice(int price)
    {
        this.price = price;
    }
}
