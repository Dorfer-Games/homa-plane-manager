using System;
using UnityEngine;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;

namespace Kuhpik
{
    /// <summary>
    /// Used to store game data. Change it the way you want.
    /// </summary>
    [Serializable]
    public class GameData
    {
        public List<FoodData> FoodList;

        public ControllerType CameraController;

        public Vector3 Direction;

        public GroundComponent Ground;
        public PlatformComponent Platform;

        public AirplaneComponent Airplane;
        public float LadderCooldown;
        public List<ItemComponent> BaggageList;

        public CharacterComponent Player;
        public float PlayerSpeed;
        public List<ItemComponent> PlayerItemList;

        public ConveyorComponent Conveyor;
        public List<ItemComponent> ConveyorList;

        public List<PeopleData> PeoplePlatformList;
        public List<PeopleData> PeopleOnPlaneList;
        public List<PeopleData> PeoplePlaneList;
        public List<PeopleData> PeopleFromPlaneList;

        public GameObject MoneyPrefab;



        // OLD
        [Header("--------------Player------------")]
        public PlayerComponent PlayerOld;
        public Vector3 Joystick;
        public bool ControlBlock;

        [Header("--------------Level------------")]
        public DateTime LevelTime;
        public bool IsVictory;
        public bool GameOver;

        [Header("--------------Camera------------")]
        public Transform CameraPoint;
        public Camera Cam;

        [Header("--------------Tutorial------------")]
        public TutorialArrowComponent TutorialArrow;
        public TutorialArrow2DComponent TutorialArrow2D;

        public void Vibro(string Name, MoreMountains.NiceVibrations.HapticTypes type = MoreMountains.NiceVibrations.HapticTypes.Success, float cd = 0)
        {
            HapticSetupSystem.instance.Haptic(Name, type, cd);
        }

        // KetchappSDK
        /* public void Event(string value)
        {
            Debug.Log("+++ GA Event: " + value);
            GameAnalytics.NewDesignEvent(value);
            Bootstrap.Instance.SaveGame();
        }

        public void AMEvent(string name)
        {
            var @params = new Dictionary<string, object>()
            { { "level_number", Bootstrap.Instance.PlayerData.Level+1 } };
            AppMetrica.Instance.ReportEvent(name, @params);
            AppMetrica.Instance.SendEventsBuffer();
        }
        public void Tutorial(string name)
        {
            var @params = new Dictionary<string, object>()
            { { "step_name", name } };
            AppMetrica.Instance.ReportEvent("tutorial", @params);
            AppMetrica.Instance.SendEventsBuffer();
        }
        */
    }
}