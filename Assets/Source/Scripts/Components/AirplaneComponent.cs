using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

public class AirplaneComponent : MonoBehaviour
{
    [SerializeField, BoxGroup("Developer")] Collider doorCollider;
    [SerializeField, BoxGroup("Developer")] GameObject ladderLowerZone;
    [SerializeField, BoxGroup("Developer")] GameObject ladderRaiseZone;
    [SerializeField, BoxGroup("Developer")] Transform ladder;
    [SerializeField, BoxGroup("Developer")] Vector2 ladderRotate;
    [SerializeField, BoxGroup("Developer")] List<PlaceComponent> placeList;

    [SerializeField, BoxGroup("Debug"), ReadOnly] bool isLadderOpen;

    public Collider DoorCollider => doorCollider;
    public bool IsLadderOpen => isLadderOpen;
    public GameObject LadderLowerZone => ladderLowerZone;
    public GameObject LadderRaiseZone => ladderRaiseZone;
    public Transform Ladder => ladder;
    public Vector2 LadderRotate => ladderRotate;
    public List<PlaceComponent> PlaceList => placeList;

    public void SetLadderStatus(bool status)
    {
        isLadderOpen = status;
    }
}