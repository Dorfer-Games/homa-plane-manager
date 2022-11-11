using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

public class ExitComponent : MonoBehaviour
{
    [SerializeField, BoxGroup("Developer")] List<Transform> pointList;

    public List<Transform> PointList => pointList;
}