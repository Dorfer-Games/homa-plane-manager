using Cinemachine;
using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineComponent : MonoBehaviour
{
    public static HashSet<CinemachineComponent> Hashset = new HashSet<CinemachineComponent>();

    [SerializeField, BoxGroup("Developer")] ControllerType type;

    [SerializeField, BoxGroup("Debug"), ReadOnly] CinemachineVirtualCamera virtualCamera;

    public CinemachineVirtualCamera VirtualCamera => virtualCamera;
    public ControllerType GetCinemachineType() => type;

    void OnEnable() => Hashset.Add(this);
    void OnDisable() => Hashset.Remove(this);

    void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }
}