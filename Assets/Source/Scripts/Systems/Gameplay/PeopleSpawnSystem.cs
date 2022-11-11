using Kuhpik;
using NaughtyAttributes;
using Supyrb;
using System.Collections.Generic;
using UnityEngine;

public class PeopleSpawnSystem : GameSystem
{
    [SerializeField, BoxGroup("Developer")] Vector2 peopleRotate;
    [SerializeField, BoxGroup("Developer")] List<GameObject> peoplePrefabList;
    [SerializeField, BoxGroup("Developer")] List<GameObject> baggagePrefabList;

    PlatformComponent component;
    public override void OnInit()
    {
        Signals.Get<AirplaneStateSignal>().AddListener(PeopleCheck);

        component = FindObjectOfType<PlatformComponent>();

        game.PeoplePlatformList = new List<PeopleData>();
        game.PeopleOnPlaneList = new List<PeopleData>();
        game.PeoplePlaneList = new List<PeopleData>();
        game.PeopleFromPlaneList = new List<PeopleData>();

        PeopleCreate();
    }
    void PeopleCreate()
    {
        foreach (var place in game.Airplane.PlaceList)
        {
            int prefabID = Random.Range(0, peoplePrefabList.Count);

            GameObject people = Instantiate(peoplePrefabList[prefabID], component.transform);
            var peopleData = new PeopleData()
            {
                Transform = people.transform,
                Component = people.GetComponent<CharacterComponent>(),
                Place = place
            };

            bool isSpawnPosition = false;
            while (!isSpawnPosition)
            {
                people.transform.localPosition = SpawnPosition();

                bool isReady = true;
                foreach (var _people in game.PeoplePlatformList)
                {
                    float distance = Vector3.Distance(people.transform.localPosition, _people.Transform.localPosition);

                    if (distance < component.Distance)
                    {
                        isReady = false;
                        break;
                    }
                }

                if (isReady) isSpawnPosition = true;
            }

            float rotateY = Random.Range(peopleRotate.x, peopleRotate.y);
            people.transform.Rotate(new Vector3(0f, rotateY, 0f));

            game.PeoplePlatformList.Add(peopleData);

            BaggageCreate(peopleData);
        }
    }
    void BaggageCreate(PeopleData people)
    {
        int prefabID = Random.Range(0, baggagePrefabList.Count);

        Transform baggage = Instantiate(baggagePrefabList[prefabID], people.Component.BaggagePoint).transform;
        baggage.parent = game.Ground.transform;

        people.Baggage = baggage.GetComponent<ItemComponent>();
    }
    void PeopleCheck(AirplaneState state)
    {
        if (state != AirplaneState.Landing) return;

        PeopleCreate();
    }
    Vector3 SpawnPosition()
    {
        Vector3 position = new Vector3(Random.Range(component.BorderX.x, component.BorderX.y),
                0f, Random.Range(component.BorderZ.x, component.BorderZ.y));

        return position;
    }
}