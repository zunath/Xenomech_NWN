using System;
using Xenomech.Core.NWScript.Enum;
using Xenomech.Core.NWScript.Enum.VisualEffect;

namespace Xenomech.Service.CombatService
{
    public enum GunType
    {
        [GunType(Animation.Invalid, 0.0f, 0.0f, VisualEffect.None, 0.0f, 0.0f, 0.0f)]
        Invalid = 0,
        [GunType(Animation.LoopingCustom11, 8.0f, 0.25f, VisualEffect.AR556Sound, 0.1f, 0.35f, 0.8f)]
        Rifle = 1,
        [GunType(Animation.LoopingCustom12, 2.0f, 1f, VisualEffect.HP9mmSound, 0.5f, 0.7f, 0.5f)]
        Handgun = 2,
        [GunType(Animation.LoopingCustom14, 8.0f, 0.1f, VisualEffect.AR762Sound, 0.1f, 0.35f, 0.8f)]
        AssaultRifle = 3,
        [GunType(Animation.LoopingCustom19, 1.20f, 1.0f, VisualEffect.Shotgun12GSound, 0.8f, 1.0f, 0.8f)]
        Shotgun = 4,
    }
    public class GunTypeAttribute: Attribute
    {
        public Animation AnimationType { get; set; }
        public float AnimationSpeed { get; set; }
        public float AnimationDuration { get; set; }
        public VisualEffect AudioVFX { get; set; }
        public float SoundDelay { get; set; }
        public float ShotDelay { get; set; }
        public float NextAttackDelay { get; set; }

        public GunTypeAttribute(
            Animation animationType,
            float animationSpeed, 
            float animationDuration, 
            VisualEffect audioVFX,
            float soundDelay,
            float shotDelay,
            float nextAttackDelay)
        {
            AnimationType = animationType;
            AnimationSpeed = animationSpeed;
            AnimationDuration = animationDuration;
            AudioVFX = audioVFX;
            SoundDelay = soundDelay;
            ShotDelay = shotDelay;
            NextAttackDelay = nextAttackDelay;
        }

    }
}
