using System;
using HarmonyLib;

namespace ThrowCalc
{
    class GameManagerPatch
    {
        public static void Patch(Harmony harmonyInstance) // Fighting() methods to patch with the harmony instance
        {
            var startMethod = AccessTools.Method(typeof(GameManager), "Start");
            var startMethodMethodPostfix = new HarmonyMethod(typeof(GameManagerPatch).GetMethod(nameof(StartMethodMethodPostfix)));
            harmonyInstance.Patch(startMethod, postfix: startMethodMethodPostfix); // Patches JoinServer() with prefix method
        }

        public static void StartMethodMethodPostfix(GameManager __instance)
        {
            __instance.gameObject.AddComponent<DisableOtherTrajectories>();
        }
    }
}
