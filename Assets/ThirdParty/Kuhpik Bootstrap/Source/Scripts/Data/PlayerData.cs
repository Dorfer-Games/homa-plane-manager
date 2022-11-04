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