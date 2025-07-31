using HarmonyLib;

namespace SoulslikeTargetBar
{
    [HarmonyPatch(typeof(EntityAlive))]
    [HarmonyPatch(nameof(EntityAlive.DamageEntity))]
    class Patch_EntityAlive_DamageEntity
    {
        static void Postfix(EntityAlive __instance, int __result, DamageSource _damageSource)
        {
            if (__result <= 0)
            {
                return;
            }
            if (_damageSource.GetSource() == EnumDamageSource.Internal)
            {
                // Electric Fence Post, Burning, Bleeding, Radiation
                return;
            }
            switch (_damageSource.CreatorEntityId)
            {
                case -2:
                    // Blade Trap, SMG Auto Turret, Shotgun Auto Turret
                    return;
                case -1:
                    if (_damageSource.getEntityId() != __instance.world.GetPrimaryPlayerId())
                    {
                        // Spike, Cop's explosion, Damage taken
                        return;
                    }
                    if (_damageSource.ItemClass?.Name == "ammoDartIron")
                    {
                        // Dart Trap
                        return;
                    }
                    XUiC_SoulslikeTargetBar.ShowDamage(__result, __instance);
                    return;
                default:
                    // Robotic Sledge, Robotic Turret
                    return;
            }
        }
    }
}
