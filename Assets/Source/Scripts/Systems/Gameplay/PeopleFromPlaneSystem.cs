using DG.Tweening;
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
                people.Target = game.Airplane.TutorialPointList[2];

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

                game.Airplane.transform.DORotate(Vector3.zero, 2f)
                .OnComplete(() =>
                {

                });

                break;
            case 1:
                distance = Vector3.Distance(people.Target.position, people.Transform.position);
                if (distance - distanceOffset > stoppingDistance)
                {
                    people.Component.Agent.SetDestination(people.Target.position);
                    Extensions.PeopleAnimation(people.Component.Animator, "isRunFood", people.Component.Agent.speed);
                }
                else
                {
                    people.Component.Agent.ResetPath();
                    Extensions.PeopleAnimation(people.Component.Animator, "None");

                    people.Target = game.Airplane.TutorialPointList[3];
                    people.Stage = 2;

                    game.Airplane.DoorTualet.transform.DORotate(Vector3.zero, 1f)
                        .OnComplete(() =>
                        {
                            Signals.Get<EffectSignal>().Dispatch(game.Airplane.TutorialPointList[3], EffectType.Crea, Vector3.zero);

                            people.Component.Renderer.SetBlendShapeWeight(0, 0);
                        });
                }

                break;

            case 2:
                distance = Vector3.Distance(people.Target.position, people.Transform.position);
                if (distance - distanceOffset > stoppingDistance)
                {
                    people.Component.Agent.SetDestination(people.Target.position);
                    Extensions.PeopleAnimation(people.Component.Animator, "isRunFood", people.Component.Agent.speed);
                }
                else
                {
                    people.Component.Agent.ResetPath();
                    Extensions.PeopleAnimation(people.Component.Animator, "None");

                    people.Stage = 3;
                }

                break;
            case 3:
                people.Component.Agent.enabled = false;
                people.Component.Collider.enabled = false;

                people.Transform.parent = game.Airplane.TutorialPointList[4];
                people.Transform.localPosition = Vector3.zero;
                people.Transform.localEulerAngles = Vector3.zero;

                Extensions.PeopleAnimation(people.Component.Animator, "isSit");

                game.Airplane.DoorTualet.transform.DORotate(Vector3.zero, 1f)
                        .OnComplete(() =>
                        {
                            people.Stage = 4;
                        });   

                people.Component.Renderer.SetBlendShapeWeight(0, 0);

                break;

            case 4:
                game.Airplane.DoorTualet.transform.DORotate(new Vector3(0f, -120f, 0f), 1f)
                    .OnComplete(() =>
                    {

                    });

                people.Component.Agent.speed = people.Component.Speed;
                people.Target = game.Airplane.TutorialPointList[2];

                people.Transform.parent = game.Airplane.TutorialPointList[3];
                people.Transform.localPosition = Vector3.zero;
                people.Transform.localEulerAngles = Vector3.zero;

                people.Component.Agent.enabled = true;
                people.Component.Collider.enabled = true;

                Extensions.PeopleAnimation(people.Component.Animator, "None");

                people.Stage = 5;

                Signals.Get<OrderUpdateSignal>().Dispatch();

                break;

            case 5:
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

                    people.Target = people.Place.PlacePoint;
                    people.Stage = 6;
                }

                break;

            case 6:
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

                    people.Stage = 7;
                }

                break;

            case 7:
                people.Component.Agent.enabled = false;
                people.Component.Collider.enabled = false;

                people.Transform.parent = people.Place.PeoplePoint;
                people.Transform.localPosition = Vector3.zero;
                people.Transform.localEulerAngles = Vector3.zero;

                game.PeopleFromPlaneList.Remove(people);

                Extensions.PeopleAnimation(people.Component.Animator, "isSit");
                people.Stage = 0;

                break;
        }
    }
}
