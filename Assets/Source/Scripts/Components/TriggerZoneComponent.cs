using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZoneComponent : MonoBehaviour
{
    public static HashSet<TriggerZoneComponent> Hashset = new HashSet<TriggerZoneComponent>();

    [SerializeField, BoxGroup("Developer")] GameObject model;
    [SerializeField, BoxGroup("Developer")] Collider colliderZone;

    public GameObject Model => model;
    public Collider ColliderZone => colliderZone;

    void OnEnable() => Hashset.Add(this);
    void OnDisable() => Hashset.Remove(this);
}