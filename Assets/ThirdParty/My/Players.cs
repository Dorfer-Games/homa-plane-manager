using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerController : System.Object
{
    public List<KeyCode> Key = new List<KeyCode>();
    public string PlayerControllerName;
}

[System.Serializable]
public class Player : System.Object
{
    public int PlayerNumber;
    public int PlayerControllerType;
    public string PlayerControllerName;
    public Color PlayerColor = Color.black;
}
public class Players : MonoBehaviour
{
    public List<Player> PlayersList = new List<Player>();
    public List<PlayerController> PlayerControllers = new List<PlayerController>();
    public int MaxPlayersCount = 8;

    List<Color> AllColors = new List<Color>()
    {Color.red, new Color(1,0.415f,0,1), Color.yellow, Color.green, Color.cyan,Color.blue, new Color(0.47f,0,1,1), new Color(1,0,0.862f,1), Color.white,Color.black};
    
    private void Update()
    {
        for (int i = 1; i <= 8; i++)
            if (Mathf.Abs(Input.GetAxis("DPadX " + i)) > 0.2f ||
                Mathf.Abs(Input.GetAxis("DPadY " + i)) > 0.2f ||
                Mathf.Abs(Input.GetAxis("TriggerL " + i)) > 0.2f ||
                Mathf.Abs(Input.GetAxis("TriggerR " + i)) > 0.2f ||
                Mathf.Abs(Input.GetAxis("RightStickX " + i)) > 0.2f ||
                Mathf.Abs(Input.GetAxis("RightStickY " + i)) > 0.2f ||
                Mathf.Abs(Input.GetAxis("LeftStickX " + i)) > 0.2f ||
                Mathf.Abs(Input.GetAxis("LeftStickY " + i)) > 0.2f ||
                Input.GetButtonDown("ButtonA " + i) ||
                Input.GetButtonDown("ButtonB " + i) ||
                Input.GetButtonDown("ButtonX " + i) ||
                Input.GetButtonDown("ButtonY " + i) ||
                Input.GetButtonDown("LB " + i) ||
                Input.GetButtonDown("RB " + i))

                AddPlayer(i - 1, ("Joystick " + i));

        for (int i = 0; i < PlayerControllers.Count; i++)
            for (int j = 0; j < PlayerControllers[i].Key.Count; j++)
                if (Input.GetKeyDown(PlayerControllers[i].Key[j]))
                    AddPlayer(i + 8, PlayerControllers[i].PlayerControllerName);

    }

    public void AddPlayer(int PlayerControllerType, string PlayerControllerName)
    {
        if (PlayersList.Count < MaxPlayersCount)
        {
            bool good = true;
            for (int i = 0; i < PlayersList.Count; i++)
                if (PlayersList[i].PlayerControllerType == PlayerControllerType)
                    good = false;

            if (good)
            {
                Player NewPlayer = new Player();
                NewPlayer.PlayerNumber = PlayersList.Count;
                NewPlayer.PlayerControllerType = PlayerControllerType;
                NewPlayer.PlayerControllerName = PlayerControllerName;

                PlayersList.Add(NewPlayer); 
                PlayerColorChange(NewPlayer);
            }
        }
    }

    public void PlayerColorChange(Player CurrentPlayer)
    {
        int startpos = AllColors.IndexOf(CurrentPlayer.PlayerColor);
        if (startpos == AllColors.Count - 1)
            startpos = 0;
        for (int i = startpos; i < AllColors.Count; i++)
        {
            bool good = true;
            foreach (Player CurPlayer in PlayersList)
            {
                if (CurPlayer.PlayerColor == AllColors[i])
                {
                    good = false;
                    break;
                }
            }
            if (good)
            {
                CurrentPlayer.PlayerColor = AllColors[i];
                gameObject.SendMessage("UpdatePlayersList");
                return;
            }
        }
        for (int i = 0; i < AllColors.Count; i++)
        {
            bool good = true;
            foreach (Player CurPlayer in PlayersList)
            {
                if (CurPlayer.PlayerColor == AllColors[i])
                {
                    good = false;
                    break;
                }
            }
            if (good)
            {
                CurrentPlayer.PlayerColor = AllColors[i];
                gameObject.SendMessage("UpdatePlayersList");
                break;
            }
        }
    }

    public void RemovePlayer(int PlayerNumber)
    {
        foreach (Player CurrentPlayer in PlayersList)
            if (CurrentPlayer.PlayerNumber == PlayerNumber)
            {
                PlayersList.Remove(CurrentPlayer);
                break;
            }
        gameObject.SendMessage("UpdatePlayersList");
    }
    public void RemovePlayer(Player Player)
    {
        PlayersList.Remove(Player);
        gameObject.SendMessage("UpdatePlayersList");
    }

        
}
