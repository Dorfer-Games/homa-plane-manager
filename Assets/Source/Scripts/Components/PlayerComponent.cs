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
using Nrjwolf.Tools.AttachAttributes;

public class PlayerComponent : MonoBehaviour
{
    [GetComponent] public Rigidbody Rigid;
    [GetComponent] public CharacterController Char;
    [GetComponent] public Animator Anim;
}
