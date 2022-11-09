using Kuhpik;
public class AirplaneLoadingSystem : GameSystem
{
    public override void OnInit()
    {
        game.Airplane = FindObjectOfType<AirplaneComponent>();
    }
}