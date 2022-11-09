using Kuhpik;
using UnityEngine;

public class PlayerInputSystem : GameSystem
{
    Joystick joystick;
    public override void OnUpdate()
    {
        if (joystick) game.Direction = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
        else joystick = FindObjectOfType<Joystick>();
    }
}