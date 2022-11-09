using Kuhpik;
using Supyrb;
using System.Linq;
using UnityEngine.AI;

public class NavigationUpdateSystem : GameSystem
{
    public override void OnInit()
    {
        Signals.Get<NavigationUpdateSignal>().AddListener(NavigationUpdate);

        NavigationUpdate();
    }
    void NavigationUpdate()
    {
        foreach (NavMeshSurface navMeshSurface in NavMeshSurface.activeSurfaces.ToList())
            navMeshSurface.BuildNavMesh();
    }
}