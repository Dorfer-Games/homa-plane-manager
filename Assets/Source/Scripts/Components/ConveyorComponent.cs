using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorComponent : MonoBehaviour
{
    [SerializeField, BoxGroup("Developer")] List<Transform> baggagePointList;

    public List<Transform> BaggagePointList => baggagePointList;
}
