using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorComponent : MonoBehaviour
{
    [SerializeField, BoxGroup("Developer")] Transform zone;
    [SerializeField, BoxGroup("Developer")] List<Transform> baggagePointList;

    public Transform Zone => zone;
    public List<Transform> BaggagePointList => baggagePointList;
}
