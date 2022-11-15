using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

public class PlaceBlockComponent : MonoBehaviour
{
    [SerializeField, BoxGroup("Developer")] List<PlaceComponent> placeList;
    [SerializeField, BoxGroup("Developer")] GameObject zone;

    public GameObject Zone => zone;
    public List<PlaceComponent> PlaceList => placeList;

    void Start()
    {
        zone.SetActive(false);
    }
}