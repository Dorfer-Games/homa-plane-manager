using NaughtyAttributes;
using UnityEngine;

public class PlaceComponent : MonoBehaviour
{
    [SerializeField, BoxGroup("Developer")] Transform placePoint;
    [SerializeField, BoxGroup("Developer")] Transform peoplePoint;

    public Transform PlacePoint => placePoint;
    public Transform PeoplePoint => peoplePoint;
}