using System;
using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using UnityEngine;

namespace ThrowCalc
{
    [BepInPlugin(Guid, "ThrowCalc", VersionNumber)]
    [BepInDependency("monky.plugins.SimpleAntiCheat")]
    [BepInProcess("StickFight.exe")]
    public class Plugin : BaseUnityPlugin
    {
        public const string VersionNumber = "1.0.0"; // Version string of plugin
        public const string Guid = "monky.plugins.ThrowCalc";

        public static ConfigEntry<KeyCode> ConfigDisTrajKey { get; private set; }

        private void Awake()
        {
            // Plugin startup logic
            Logger.LogInfo("Plugin " + Guid + " is loaded! [v" + VersionNumber + "]");
            try
            {
                Harmony harmony = new Harmony(Guid); // Creates harmony instance with identifier

                Logger.LogInfo("Applying Fighting patch...");
                FightingPatch.Patch(harmony);
                Logger.LogInfo("Applying GameManager patch...");
                GameManagerPatch.Patch(harmony);
            }
            catch (Exception ex)
            {
                Logger.LogError("Exception on applying patches: " + ex.InnerException + " " + ex.Message + " " +
                                ex.TargetSite + " " + ex.Source);
            }

            Logger.LogInfo("Loading configuration options...");
            try
            {
                ConfigDisTrajKey = Config.Bind("Keybind",
                    "ToggleKey",
                    KeyCode.P,
                    "Change the keyboard key used to disable/enable other players trajectories from being shown?");
            }
            catch (Exception ex)
            {
                Logger.LogError("Exception on loading configuration: " + ex.StackTrace + ex.Message + ex.Source +
                                ex.InnerException);
            }
        }
    }
}
