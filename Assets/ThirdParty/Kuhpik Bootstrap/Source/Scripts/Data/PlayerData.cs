using System;
using UnityEngine;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;

namespace Kuhpik
{
    /// <summary>
    /// Used to store player's data. Change it the way you want.
    /// </summary>
    [Serializable]
    public class PlayerData
    {
        public int GameLevel;

        public int Tutorial;
        public int TutorialOrder;

        public bool IsVibration;
        public bool IsGameLaunch;

        public int MoneyAmount;

        public int UnlockAmount;

        public List<UnlockData> UnlockList;

        // OLD
        public int SessionCounter;
        public bool Ads;
        public int Level;
        [Header("--------------Player------------")]
        public float Money;
        public float Speed;
        [Header("--------------Settings------------")]
        public bool FirstLaunch;
        public bool VibroOn;
        public bool SoundOn;
        public bool MusicOn;
    }
}