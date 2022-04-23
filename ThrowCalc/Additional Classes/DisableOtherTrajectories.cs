using System;
using UnityEngine;

namespace ThrowCalc
{
    class DisableOtherTrajectories : MonoBehaviour
    {
        public static bool Toggled { get; private set; }
        private readonly KeyCode _toggleKey = (KeyCode) Enum.Parse(typeof(KeyCode), Plugin.ConfigDisTrajKey.Value);

        void Update()
        {
            // Stops/starts rendering trajectories for other players
            if (Input.GetKeyDown(_toggleKey) && !ChatManager.isTyping)
            {
                int localID = GameManager.Instance.mMultiplayerManager.LocalPlayerIndex;

                foreach (var drawer in Resources.FindObjectsOfTypeAll<ThrowTrajectoryDrawer>())
                {
                    if (drawer.SpawnID != localID) drawer.enabled = !drawer.enabled;
                }

                Toggled = !Toggled;
            }
        }
    }
}
