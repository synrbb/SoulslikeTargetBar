using HarmonyLib;

namespace SoulslikeTargetBar
{
    [HarmonyPatch(typeof(EntityAlive))]
    [HarmonyPatch(nameof(EntityAlive.DamageEntity))]
    class Patch_EntityAlive_DamageEntity
    {
        static void Postfix(EntityAlive __instance)
        {
            int strength = __instance.RecordedDamage.ModStrength;
            if (strength <= 0)
            {
                return;
            }
            DamageSource source = __instance.RecordedDamage.Source;
            switch (source.CreatorEntityId)
            {
                case -2:
                    // Blade Trap, SMG Auto Turret, Shotgun Auto Turret
                    return;
                case -1:
                    if (source.getEntityId() == -1)
                    {
                        // Burning, Bleeding, Electric Fence Post, Spike
                        return;
                    }
                    if (source.ItemClass?.Name == "ammoDartIron")
                    {
                        // Dart Trap
                        return;
                    }
                    XUiC_SoulslikeTargetBar.ShowDamage(strength, __instance);
                    return;
                default:
                    // Robotic Sledge, Robotic Turret
                    return;
            }
        }
    }
}
