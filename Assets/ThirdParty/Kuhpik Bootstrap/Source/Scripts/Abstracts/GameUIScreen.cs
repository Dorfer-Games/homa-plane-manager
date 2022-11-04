using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

namespace Kuhpik
{
    public class GameUIScreen : MonoBehaviour, IUIScreen
    {
        [SerializeField] [BoxGroup("Settings")] bool shouldOpenWithState;
        [SerializeField] [BoxGroup("Settings")] bool getScreenFromChild = true;
        [SerializeField] [BoxGroup("Settings")] [HideIf("getScreenFromChild")] GameObject screen;
        [SerializeField] [BoxGroup("Settings")] [ShowIf("shouldOpenWithState")] GameStateID[] statesToOpenWith;

        HashSet<GameStateID> statesMap;

        void Awake()
        {
            screen = getScreenFromChild ? transform.GetChild(0).gameObject : screen;
            statesMap = new HashSet<GameStateID>(statesToOpenWith);
        }

        public virtual void Open()
        {
            screen.SetActive(true);
        }

        public virtual void Close()
        {
            screen.SetActive(false);
        }

        internal void TryOpenWithState(GameStateID id)
        {
            if (shouldOpenWithState && statesMap.Contains(id))
                Open();
        }
    }
}