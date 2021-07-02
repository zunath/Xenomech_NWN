using System;
using Xenomech.Core.NWScript.Enum;

namespace Xenomech.Service.CombatService
{
    public enum GunType
    {
        [GunType(Animation.Invalid, 0.0f, 0.0f, "", 0.0f, 0.0f, 0.0f)]
        Invalid = 0,
        [GunType(Animation.LoopingCustom12, 2.0f, 0.5f, "cb_sh_ahandgun", 0.5f, 0.7f, 0.5f)]
        Handgun = 1,
        [GunType(Animation.LoopingCustom11, 8.0f, 0.25f, "cb_sh_alongarm", 0.1f, 0.35f, 0.8f)]
        Rifle = 2,
        [GunType(Animation.LoopingCustom14, 8.0f, 0.1f, "cb_sh_machgun", 0.1f, 0.35f, 0.8f)]
        AssaultRifle = 3,
        [GunType(Animation.LoopingCustom19, 1.20f, 1.0f, "m3-1", 0.8f, 1.0f, 0.8f)]
        Shotgun = 4,
    }
    public class GunTypeAttribute: Attribute
    {
        public Animation AnimationType { get; set; }
        public float AnimationSpeed { get; set; }
        public float AnimationDuration { get; set; }
        public string SoundFile { get; set; }
        public float SoundDelay { get; set; }
        public float ShotDelay { get; set; }
        public float NextAttackDelay { get; set; }

        public GunTypeAttribute(
            Animation animationType,
            float animationSpeed, 
            float animationDuration, 
            string soundFile,
            float soundDelay,
            float shotDelay,
            float nextAttackDelay)
        {
            AnimationType = animationType;
            AnimationSpeed = animationSpeed;
            AnimationDuration = animationDuration;
            SoundFile = soundFile;
            SoundDelay = soundDelay;
            ShotDelay = shotDelay;
            NextAttackDelay = nextAttackDelay;
        }

    }
}
