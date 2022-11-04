using Kuhpik;
using static My;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using NaughtyAttributes;
using Random = UnityEngine.Random;
using Supyrb;

public class PlayerControl : GameSystem
{
    [SerializeField] float AccelerationSmooth = 1;
    [SerializeField] float BrakeSmooth = 3;
    [SerializeField] float RotationSmooth = 1;

    float curSpeed;
    float fallSpeed;
    Transform looker;

    public override void OnInit()
    {
        looker = new GameObject("Looker").transform;
        looker.SetParent(game.Player.transform);
        looker.localPosition = Vector3.zero;
    }

    public override void OnUpdate()
    {
        game.TutorialArrow.Upd();

        Falling();

        // game.Anim.SetBool("Run", game.Joystick.magnitude > 0);
        if (game.Joystick.magnitude > 0)
        {
            looker.forward = game.Joystick.normalized;
            game.Player.transform.rotation = Quaternion.Lerp(game.Player.transform.rotation, looker.rotation, Time.deltaTime * RotationSmooth);

            //       game.Player.Anim.transform.localPosition = -Vector3.up;
            //       game.Player.Anim.transform.localEulerAngles = Vector3.zero;
        }

        curSpeed = Mathf.Lerp(curSpeed, game.Joystick.magnitude > 0 ? player.Speed : 0f, Time.deltaTime * (game.Joystick.magnitude > 0 ? AccelerationSmooth : BrakeSmooth));
        game.Player.Char.Move(game.Player.transform.forward * curSpeed * Time.deltaTime);
    }

    private void Falling()
    {
        fallSpeed = game.Player.Char.isGrounded ? 0 : Mathf.Max(fallSpeed + Time.deltaTime * Physics.gravity.y, 2 * Physics.gravity.y);
        game.Player.Char.Move(fallSpeed * Vector3.up * Time.deltaTime);
    }
}
