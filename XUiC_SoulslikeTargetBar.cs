using System;
using UnityEngine;

namespace SoulslikeTargetBar
{
    class XUiC_SoulslikeTargetBar : XUiC_TargetBar
    {
        private float DamageDuration;

        private int DamageStrength;

        private EntityAlive DamageTarget;

        public override void Update(float _dt)
        {
            base.Update(_dt);

            if (Target == null || Target != DamageTarget)
            {
                DamageDuration = 0;
            }
            else if (DamageDuration > 0)
            {
                DamageDuration -= Time.deltaTime;
            }

            if (DamageDuration <= 0)
            {
                DamageTarget = null;
            }

            if (Target != null)
            {
                Camera camera = xui.playerUI.entityPlayer.playerCamera;
                Vector3 screenPoint = camera.WorldToScreenPoint(Target.getHeadPosition() - Origin.position);
                int newX = (int)screenPoint.x - camera.pixelWidth / 2;
                int newY = (int)screenPoint.y - camera.pixelHeight;
                Vector2i oldPosition = ViewComponent.Position;
                if (Math.Abs(oldPosition.x - newX) > 1 || Math.Abs(oldPosition.y - newY) > 1)
                {
                    ViewComponent.Position = new Vector2i(newX, newY);
                }
            }
        }

        public override bool GetBindingValue(ref string value, string bindingName)
        {
            switch (bindingName)
            {
                case "damage":
                    value = statcurrentFormatterInt.Format(DamageStrength);
                    return true;
                case "damagevisible":
                    value = (DamageDuration > 0).ToString();
                    return true;
            }
            return base.GetBindingValue(ref value, bindingName);
        }

        public static void ShowDamage(int strength, EntityAlive target)
        {
            LocalPlayerUI localPlayerUI = target.world.GetPrimaryPlayer().PlayerUI;
            if (localPlayerUI == null || localPlayerUI.xui == null)
            {
                return;
            }
            XUiC_SoulslikeTargetBar targetBar = localPlayerUI.xui.FindWindowGroupByName("compass")?.GetChildByType<XUiC_SoulslikeTargetBar>();
            if (targetBar != null)
            {
                targetBar.DamageStrength = strength;
                targetBar.DamageTarget = target;
                targetBar.DamageDuration = 1.5f;
            }
        }
    }
}
