using UnityEngine;
using Kuhpik;
using NaughtyAttributes;

public class PlayerMovementSystem : GameSystem
{
    [SerializeField, BoxGroup("Settings")] float playerSpeedStart = 5f;
    [SerializeField, BoxGroup("Settings")] float playerSensitivity = 70f;

    public override void OnInit()
    {
        game.PlayerSpeed = playerSpeedStart;
    }
    public override void OnFixedUpdate()
    {
        if (game.CameraController != ControllerType.Player && game.CameraController != ControllerType.Airplane) return;

        if (game.Direction != Vector3.zero)
        {
            Move();
            Rotate();

            if (game.PlayerItemList.Count <= 0)
            {
                game.PlayerSpeed = playerSpeedStart;

                if (game.Player.transform.localPosition.y > 0.2f) Extensions.PeopleAnimation(game.Player.Animator, "isWalk", game.PlayerSpeed);
                else Extensions.PeopleAnimation(game.Player.Animator, "isRun", game.PlayerSpeed);
            } else
            {
                game.PlayerSpeed = playerSpeedStart / 1.5f;
                Extensions.PeopleAnimation(game.Player.Animator, "isTakeRun", game.PlayerSpeed);
            }

            game.Player.Animator.transform.localPosition = Vector3.zero;
            game.Player.Animator.transform.localEulerAngles = Vector3.zero;
        }
        else
        {
            game.Player.Rigidbody.velocity = new Vector3(0f, game.Player.Rigidbody.velocity.y, 0f);
            game.Player.Rigidbody.angularVelocity = Vector3.zero;

            if (game.PlayerItemList.Count <= 0) Extensions.PeopleAnimation(game.Player.Animator, "None");
            else Extensions.PeopleAnimation(game.Player.Animator, "isTake");
        }
    }
    void Move()
    {
        Vector3 vector = game.Player.transform.forward * game.PlayerSpeed;
        if (game.Player.Rigidbody.velocity.y < -0.1f) vector += new Vector3(0f, -game.PlayerSpeed, 0f);

        game.Player.Rigidbody.velocity = vector;
    }
    void Rotate()
    {
        game.Player.Rigidbody.MoveRotation(Quaternion.Lerp(game.Player.Rigidbody.rotation,
            Quaternion.Euler(game.Player.Rigidbody.rotation.x, GetEulerAnglesFromJoystick(),
            game.Player.Rigidbody.rotation.z), playerSensitivity * Time.deltaTime));
    }
    float GetEulerAnglesFromJoystick()
    {
        return Mathf.Atan2(game.Direction.x, game.Direction.z) * Mathf.Rad2Deg;
    }
}
