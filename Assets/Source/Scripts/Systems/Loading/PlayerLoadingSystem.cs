using UnityEngine;
using Kuhpik;
using NaughtyAttributes;
public class PlayerLoadingSystem : GameSystem
{
    [SerializeField, BoxGroup("Developer")] [Tag] string playerTag;
    public override void OnInit()
    {
        game.Player = GameObject.FindGameObjectWithTag(playerTag).GetComponent<CharacterComponent>();
    }
}