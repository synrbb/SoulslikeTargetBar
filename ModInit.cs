using HarmonyLib;

namespace SoulslikeTargetBar
{
    public class ModInit : IModApi
    {
        public void InitMod(Mod _modInstance)
        {
            var harmony = new Harmony("io.github.synrbb.SoulslikeTargetBar");
            harmony.PatchAll();
        }
    }
}
