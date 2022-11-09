using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

public class AirplaneComponent : MonoBehaviour
{
    [SerializeField, BoxGroup("Developer")] GameObject ladderZone;
    [SerializeField, BoxGroup("Developer")] Transform ladder;
    [SerializeField, BoxGroup("Developer")] Vector2 ladderRotate;
    [SerializeField, BoxGroup("Developer")] List<PlaceComponent> placeList;

    [SerializeField, BoxGroup("Debug"), ReadOnly] bool isLadderOpen;

    public bool IsLadderOpen => isLadderOpen;
    public GameObject LadderZone => ladderZone;
    public Transform Ladder => ladder;
    public Vector2 LadderRotate => ladderRotate;
    public List<PlaceComponent> PlaceList => placeList;

    public void SetLadderStatus(bool status)
    {
        isLadderOpen = status;
    }
}