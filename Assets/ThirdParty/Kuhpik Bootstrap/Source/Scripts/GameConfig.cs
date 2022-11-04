using UnityEngine;
using NaughtyAttributes;

namespace Kuhpik
{
    [CreateAssetMenu(menuName = "Config/GameConfig")]
    public sealed class GameConfig : ScriptableObject
    {
        [field: SerializeField] public bool Cheats { get; private set; }

        [field: Header("-------------Player-------------")]
        public float PlayerSpeed = 1;

        [field: Header("-------------Tags-------------")]
        [field: SerializeField] [field: Tag] public string tag { get; private set; }
    }
}