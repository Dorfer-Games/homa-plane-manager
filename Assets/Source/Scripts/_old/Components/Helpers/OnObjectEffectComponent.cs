using Kuhpik;
using static My;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using NaughtyAttributes;
using Supyrb;
using System.Linq;

public class OnObjectEffectComponent : GameSystem
{
    [SerializeField] GameObject effect;
    [SerializeField] Transform target;
    [SerializeField] bool parenting;
    [Button] public void PlayEffect()
    {
        if (effect == null) return;

        if (string.IsNullOrEmpty(effect.scene.name))
        {
            GameObject newEffect = Instantiate(effect, target ? target.position : game.PlayerOld.transform.position, target ? target.rotation : game.PlayerOld.transform.rotation);
            if (parenting) newEffect.transform.SetParent(target ? target : game.PlayerOld.transform);
        }
        else
        {
            effect.SetActive(true);
            ParticleSystem PS = effect.GetComponent<ParticleSystem>();
            if (PS) PS.Play();
            else effect.GetComponentInChildren<ParticleSystem>().Play();
        }
    }
}
