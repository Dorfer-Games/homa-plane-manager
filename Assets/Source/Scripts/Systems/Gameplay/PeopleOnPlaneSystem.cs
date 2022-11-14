using Kuhpik;
using NaughtyAttributes;
using System.Linq;
using UnityEngine;

public class PeopleOnPlaneSystem : GameSystem
{
    [SerializeField, BoxGroup("Developer")] float stoppingDistance = 1f;

    float distanceOffset = 0.25f;
    public override void OnUpdate()
    {
        if (game.PeopleOnPlaneList.Count <= 0) return;

        foreach (PeopleData people in game.PeopleOnPlaneList.ToList())
                StageUpdate(people);
    }
    void StageUpdate(PeopleData people)
    {
        float distance;
        switch (people.Stage)
        {
            default:

                break;
            case 0:
                people.Component.Agent.speed = people.Component.Speed;
                people.Component.Agent.stoppingDistance = stoppingDistance;
                people.Target = people.Place.PlacePoint;
                people.Stage = 1;

                break;
            case 1:
                distance = Vector3.Distance(people.Target.position, people.Transform.position);
                if (distance - distanceOffset > stoppingDistance)
                {
                    people.Component.Agent.SetDestination(people.Target.position);
                    Extensions.PeopleAnimation(people.Component.Animator, "isRun", people.Component.Agent.speed);
                }
                else
                {
                    people.Component.Agent.ResetPath();
                    Extensions.PeopleAnimation(people.Component.Animator, "None");

                    people.Stage = 2;
                }

                break;
            case 2:
                people.Component.Agent.enabled = false;
                people.Component.Collider.enabled = false;

                people.Transform.parent = people.Place.PeoplePoint;
                people.Transform.localPosition = Vector3.zero;
                people.Transform.localEulerAngles = Vector3.zero;

                game.PeopleOnPlaneList.Remove(people);
                game.PeoplePlaneList.Add(people);

                Extensions.PeopleAnimation(people.Component.Animator, "isSit");
                people.Stage = 0;

                break;
        }
    }
}
