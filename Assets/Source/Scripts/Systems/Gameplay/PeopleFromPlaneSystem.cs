using Kuhpik;
using NaughtyAttributes;
using Supyrb;
using System.Linq;
using UnityEngine;

public class PeopleFromPlaneSystem : GameSystem
{
    [SerializeField, BoxGroup("Developer")] float stoppingDistance = 1f;

    float distanceOffset = 0.25f;
    ExitComponent component;
    public override void OnInit()
    {
        component = FindObjectOfType<ExitComponent>();
    }
    public override void OnUpdate()
    {
        if (game.PeopleFromPlaneList.Count <= 0) return;

        foreach (PeopleData people in game.PeopleFromPlaneList.ToList())
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
                people.Component.Agent.speed = people.Component.Speed * 1.5f;
                people.Component.Agent.stoppingDistance = stoppingDistance;
                people.Target = component.PointList[Random.Range(0, component.PointList.Count)];

                people.Transform.parent = people.Place.PlacePoint;
                people.Transform.localPosition = Vector3.zero;
                people.Transform.localEulerAngles = Vector3.zero;

                /*
                people.Baggage.transform.parent = people.Component.BaggagePoint;
                people.Baggage.transform.localPosition = Vector3.zero;
                foreach (var shelf in ShelfComponent.Hashset.ToList()) 
                    shelf.ItemList.Clear();
                */

                people.Component.Agent.enabled = true;
                people.Component.Collider.enabled = true;

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
                game.PeopleFromPlaneList.Remove(people);
                Destroy(people.Transform.gameObject);

                break;
        }
    }
}
