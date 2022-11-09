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
        looker.SetParent(game.PlayerOld.transform);
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
            game.PlayerOld.transform.rotation = Quaternion.Lerp(game.PlayerOld.transform.rotation, looker.rotation, Time.deltaTime * RotationSmooth);

            //       game.Player.Anim.transform.localPosition = -Vector3.up;
            //       game.Player.Anim.transform.localEulerAngles = Vector3.zero;
        }

        curSpeed = Mathf.Lerp(curSpeed, game.Joystick.magnitude > 0 ? player.Speed : 0f, Time.deltaTime * (game.Joystick.magnitude > 0 ? AccelerationSmooth : BrakeSmooth));
        game.PlayerOld.Char.Move(game.PlayerOld.transform.forward * curSpeed * Time.deltaTime);
    }

    private void Falling()
    {
        fallSpeed = game.PlayerOld.Char.isGrounded ? 0 : Mathf.Max(fallSpeed + Time.deltaTime * Physics.gravity.y, 2 * Physics.gravity.y);
        game.PlayerOld.Char.Move(fallSpeed * Vector3.up * Time.deltaTime);
    }
}
