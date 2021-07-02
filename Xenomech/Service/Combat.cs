using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Xenomech.Core;
using Xenomech.Core.Bioware;
using Xenomech.Core.NWNX;
using Xenomech.Core.NWScript.Enum;
using Xenomech.Core.NWScript.Enum.Item;
using Xenomech.Extension;
using Xenomech.Service.CombatService;
using static Xenomech.Core.NWScript.NWScript;

namespace Xenomech.Service
{
    public static class Combat
    {
        private static readonly Dictionary<GunType, GunTypeAttribute> _gunTypes = new Dictionary<GunType, GunTypeAttribute>();

        /// <summary>
        /// When the module loads, cache all combat-related data.
        /// </summary>
        [NWNEventHandler("mod_load")]
        public static void CacheCombatData()
        {
            var achievementTypes = Enum.GetValues(typeof(GunType)).Cast<GunType>();
            foreach (var gunType in achievementTypes)
            {
                var gunTypeDetail = gunType.GetAttribute<GunType, GunTypeAttribute>();
                _gunTypes[gunType] = gunTypeDetail;
            }
        }

        /// <summary>
        /// When the player enters the server, clear any temporary data which may still reside on their PC.
        /// </summary>
        [NWNEventHandler("mod_enter")]
        public static void ClearTemporaryData()
        {
            var player = GetEnteringObject();
            DeleteLocalBool(player, "IS_ATTACKING");
            SetCommandable(true, player);
        }

        /// <summary>
        /// When a creature inputs an attack command, if they are equipped with a gun, skip normal NWN combat and use
        /// this custom system.
        /// </summary>
        [NWNEventHandler("input_atk_bef")]
        public static void StartRangedCombat()
        {
            var attacker = OBJECT_SELF;
            var target = StringToObject(Events.GetEventData("TARGET"));
            var weapon = GetItemInSlot(InventorySlot.RightHand, attacker);
            var gunType = GetGunType(weapon);
            if (gunType == GunType.Invalid) return;
            if (GetLocalBool(attacker, "IS_ATTACKING")) return;

            var firingMode = GetCurrentFiringMode(weapon);
            var bullets = firingMode == FiringModeType.ThreeRoundBurst ? 3 : 1;
            var gunDetail = _gunTypes[gunType];
            var totalAttackTime = gunDetail.AnimationDuration * bullets + 0.1f;

            ClearAllActions();
            Events.SkipEvent();
            BiowarePosition.TurnToFaceObject(target, attacker);

            AssignCommand(attacker, () =>
            {
                ActionPlayAnimation(gunDetail.AnimationType, gunDetail.AnimationSpeed, gunDetail.AnimationDuration * bullets);
            });

            for (var shotCount = 1; shotCount <= bullets; shotCount++)
            {
                FireBullet(attacker, target, shotCount, gunType);
            }

            SetLocalBool(attacker, "IS_ATTACKING", true);
            DelayCommand(totalAttackTime, () =>
            {
                DeleteLocalBool(attacker, "IS_ATTACKING");
                ClearAllActions();
            });
        }

        /// <summary>
        /// Retrieves the gun type of a particular item. Returns GunType.Invalid if item property is not found.
        /// </summary>
        /// <param name="item">The item to check</param>
        /// <returns>A GunType</returns>
        private static GunType GetGunType(uint item)
        {
            for (var ip = GetFirstItemProperty(item); GetIsItemPropertyValid(ip); ip = GetNextItemProperty(item))
            {
                if (GetItemPropertyType(ip) != ItemPropertyType.GunType) 
                    continue;

                return (GunType) GetItemPropertySubType(ip);
            }

            return GunType.Invalid;
        }

        /// <summary>
        /// Retrieves the current firing mode type of a gun.
        /// If item is not a gun, FiringModeType.Invalid will be returned.
        /// </summary>
        /// <param name="item">The item to check</param>
        /// <returns>A FiringModeType</returns>
        private static FiringModeType GetCurrentFiringMode(uint item)
        {
            if (GetGunType(item) == GunType.Invalid) return FiringModeType.Invalid;

            var firingModeId = GetLocalInt(item, "FIRING_MODE");
            if (firingModeId <= 0)
                return FiringModeType.SingleShot;

            return (FiringModeType) firingModeId;
        }

        /// <summary>
        /// Retrieves a list of available firing modes on a gun.
        /// </summary>
        /// <param name="item">The item to check</param>
        /// <returns>A list of available firing modes</returns>
        private static IEnumerable<FiringModeType> GetAvailableFiringModes(uint item)
        {
            for (var ip = GetFirstItemProperty(item); GetIsItemPropertyValid(ip); ip = GetNextItemProperty(item))
            {
                if (GetItemPropertyType(ip) != ItemPropertyType.AvailableFiringMode)
                    continue;

                yield return (FiringModeType) GetItemPropertySubType(ip);
            }
        }

        private static void FireBullet(uint attacker, uint target, int shotCount, GunType gunType)
        {
            var gunDetail = _gunTypes[gunType];
            
            // Todo: Determine target based on line of sight / cone in front of attacker
            DelayCommand(gunDetail.ShotDelay, () =>
            {
                ApplyEffectToObject(DurationType.Instant, EffectDamage(1), target);
            });
            
            DelayCommand(gunDetail.SoundDelay * shotCount, () =>
            {
                ApplyEffectAtLocation(DurationType.Instant, EffectVisualEffect(gunDetail.AudioVFX), GetLocation(attacker));
            });
        }

    }
}
