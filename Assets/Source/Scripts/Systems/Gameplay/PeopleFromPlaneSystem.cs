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
                people.Component.Agent.speed = people.Component.Speed;
                people.Target = game.Player.transform;

                people.Transform.parent = people.Place.PlacePoint;
                people.Transform.localPosition = Vector3.zero;
                people.Transform.localEulerAngles = Vector3.zero;

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

                    distance = Vector3.Distance(people.Place2.PlacePoint.position, people.Transform.position);
                    if (distance < 5f) people.Target = people.Place2.PlacePoint;
                }
                else
                {
                    people.Component.Agent.ResetPath();
                    Extensions.PeopleAnimation(people.Component.Animator, "None");

                    if (people.Target != game.Player.transform) people.Stage = 2;
                }


                distance = Vector3.Distance(people.Target.position, people.Transform.position);
                if (distance - distanceOffset > stoppingDistance)
                {
                    people.Component.Agent.SetDestination(people.Target.position);
                    Extensions.PeopleAnimation(people.Component.Animator, "isRun", people.Component.Agent.speed);

                    distance = Vector3.Distance(people.Place2.PlacePoint.position, people.Transform.position);
                    if (distance < 7f) {
                        people.Component.Crea.SetActive(false);
                        people.Target = people.Place2.PlacePoint; 
                    }
                }
                else
                {
                    people.Component.Agent.ResetPath();
                    Extensions.PeopleAnimation(people.Component.Animator, "None");

                    if (people.Target != game.Player.transform) people.Stage = 2;
                }

                break;
            case 2:
                people.Component.Agent.enabled = false;
                people.Component.Collider.enabled = false;

                people.Transform.parent = people.Place2.PeoplePoint;
                people.Transform.localPosition = Vector3.zero;
                people.Transform.localEulerAngles = Vector3.zero;

                game.PeopleFromPlaneList.Remove(people);

                Extensions.PeopleAnimation(people.Component.Animator, "isSit");
                people.Stage = 0;

                break;
        }
    }
}
