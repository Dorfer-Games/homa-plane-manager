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

public class SignalShowObject : Signal<Transform> { }
public class SignalCameraShake : Signal<float, float> { }
public class GameCamera : GameSystem
{
    [SerializeField] float Smooth = 5f;
    [SerializeField] bool LookForward = false;
    [SerializeField] float LookSmooth = 5f;
    Transform ShowObj;
    float showTimer;
    public override void OnInit()
    {
        game.CameraPoint = GameObject.Find("CameraPoint").transform;
        Signals.Get<SignalShowObject>().AddListener(ShowObject);
        Signals.Get<SignalCameraShake>().AddListener(Shake);
    }
    
    public override void OnLateUpdate()
    {
        if (ShowObj)
            if (showTimer > 0)
            {
                showTimer -= Time.deltaTime;
                game.CameraPoint.position = Vector3.Lerp(game.CameraPoint.position, ShowObj.position, Smooth * Time.deltaTime / 5f);
                return;
            }
            else ShowObj = null;

        if (game.PlayerOld)
            game.CameraPoint.position = Vector3.Lerp(game.CameraPoint.position, game.PlayerOld.transform.position, Smooth * Time.deltaTime);

        if (LookForward && game.PlayerOld.Rigid)
            if (LookSmooth == 1)
                game.CameraPoint.transform.eulerAngles = new Vector3(0, -Mathf.Atan(game.PlayerOld.Rigid.velocity.z / game.PlayerOld.Rigid.velocity.x) * Mathf.Rad2Deg + Mathf.Sign(game.PlayerOld.Rigid.velocity.x) * 90, 0);
            else
                game.CameraPoint.transform.rotation = Quaternion.Lerp(game.CameraPoint.transform.rotation,
                    Quaternion.Euler(new Vector3(0, -Mathf.Atan(game.PlayerOld.Rigid.velocity.z / game.PlayerOld.Rigid.velocity.x) * Mathf.Rad2Deg + Mathf.Sign(game.PlayerOld.Rigid.velocity.x) * 90, 0)),
                    LookSmooth * Time.deltaTime);
    }
    void ShowObject(Transform obj)
    {
        ShowObj = obj;
        showTimer = 3;
    }
    void Shake(float time, float magnitude)
    {
        StartCoroutine(Shaking(time, magnitude));
    }
    IEnumerator Shaking(float time, float magnitude)
    {
        Vector3 RandomVec;
        for (float i = 1.0f; i >= 0; i -= 0.1f / time)
        {
            RandomVec = RandomVector3() * magnitude * i;
            game.CameraPoint.GetChild(0).DOLocalMove(RandomVec, 0.1f);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
