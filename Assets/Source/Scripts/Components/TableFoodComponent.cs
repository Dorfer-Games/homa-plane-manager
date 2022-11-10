using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

public class TableFoodComponent : MonoBehaviour
{
    public static HashSet<TableFoodComponent> Hashset = new HashSet<TableFoodComponent>();

    [SerializeField, BoxGroup("Developer")] ItemType type;
    [SerializeField, BoxGroup("Developer")] GameObject triggerZone;
    [SerializeField, BoxGroup("Developer")] Transform spawnPoint;

    public ItemType ItemType => type;
    public GameObject TriggerZone => triggerZone;
    public Transform SpawnPoint => spawnPoint;

    void OnEnable() => Hashset.Add(this);
    void OnDisable() => Hashset.Remove(this);
}