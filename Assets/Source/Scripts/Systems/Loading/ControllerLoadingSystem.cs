using Cinemachine;
using Kuhpik;
using Supyrb;
using System.Collections;

public class ControllerLoadingSystem : GameSystem
{
    public override void OnInit()
    {
        Signals.Get<ControllerChangeSignal>().AddListener(ControllerChange);

        foreach (CinemachineComponent component in CinemachineComponent.Hashset)
            SetSettings(component);

        ControllerChange(ControllerType.Player);
    }
    void SetSettings(CinemachineComponent component)
    {
        switch (component.GetCinemachineType())
        {
            default:

                break;

            case ControllerType.Player:
                component.VirtualCamera.LookAt = game.Player.transform;
                component.VirtualCamera.Follow = game.Player.transform;

                break;

            case ControllerType.Airplane:
                component.VirtualCamera.LookAt = game.Airplane.transform;
                component.VirtualCamera.Follow = game.Airplane.transform;

                break;
        }
    }
    void ControllerChange(ControllerType type)
    {
        foreach (CinemachineComponent component in CinemachineComponent.Hashset)
        {
            component.VirtualCamera.Priority = 0;

            if (component.GetCinemachineType() == type)
            {
                component.VirtualCamera.Priority = 1;
                game.CameraController = type;
            }
        }
    }
}
