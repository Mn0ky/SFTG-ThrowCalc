using System;
using HarmonyLib;

namespace ThrowCalc
{
    class FightingPatch
    {
        public static void Patch(Harmony harmonyInstance) // Fighting() methods to patch with the harmony instance
        {
            var startMethod = AccessTools.Method(typeof(Fighting), "Start");
            var startMethodPostfix = new HarmonyMethod(typeof(FightingPatch).GetMethod(nameof(StartMethodPostfix))); 
            harmonyInstance.Patch(startMethod, postfix: startMethodPostfix); // Patches Start() with postfix method
        }

        public static void StartMethodPostfix(Fighting __instance)
        {
            ThrowTrajectoryDrawer drawer = __instance.gameObject.AddComponent<ThrowTrajectoryDrawer>();
            drawer.FightingInstance = __instance;
        }
    }
}
