using NaughtyAttributes;
using UnityEngine;

public class PlatformComponent : MonoBehaviour
{
    [SerializeField, BoxGroup("Developer")] float distance;
    [SerializeField, BoxGroup("Developer")] Vector2 borderX;
    [SerializeField, BoxGroup("Developer")] Vector2 borderZ;

    public float Distance => distance;
    public Vector2 BorderX => borderX;
    public Vector2 BorderZ => borderZ;
}