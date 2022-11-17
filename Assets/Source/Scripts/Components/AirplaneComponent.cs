using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

public class AirplaneComponent : MonoBehaviour
{
    [SerializeField, BoxGroup("Developer")] Collider doorCollider;
    [SerializeField, BoxGroup("Developer")] GameObject ladderLowerZone;
    [SerializeField, BoxGroup("Developer")] GameObject ladderRaiseZone;
    [SerializeField, BoxGroup("Developer")] GameObject baggageZone;
    [SerializeField, BoxGroup("Developer")] Transform ladder;
    [SerializeField, BoxGroup("Developer")] Transform baggageDoor;
    [SerializeField, BoxGroup("Developer")] Vector2 ladderRotate;
    [SerializeField, BoxGroup("Developer")] Vector2 baggageDoorRotate;
    [SerializeField, BoxGroup("Developer")] List<PlaceBlockComponent> placeList;
    [SerializeField, BoxGroup("Developer")] List<Transform> baggagePointList;
    [SerializeField, BoxGroup("Developer")] List<Transform> paymentPointList;
    [SerializeField, BoxGroup("Developer")] List<Transform> tutorialPointList;

    [SerializeField, BoxGroup("Debug"), ReadOnly] bool isLadderOpen;

    public Collider DoorCollider => doorCollider;
    public bool IsLadderOpen => isLadderOpen;
    public GameObject LadderLowerZone => ladderLowerZone;
    public GameObject LadderRaiseZone => ladderRaiseZone;
    public GameObject BaggageZone => baggageZone;
    public Transform Ladder => ladder;
    public Transform BaggageDoor => baggageDoor;
    public Vector2 LadderRotate => ladderRotate;
    public Vector2 BaggageDoorRotate => baggageDoorRotate;
    public List<PlaceBlockComponent> PlaceList => placeList;
    public List<Transform> PaymentPointList => paymentPointList;
    public List<Transform> TutorialPointList => tutorialPointList;
    public List<Transform> BaggagePointList => baggagePointList;

    public void SetLadderStatus(bool status)
    {
        isLadderOpen = status;
    }
}