using Xenomech.Core;
using Xenomech.Core.NWNX;
using Xenomech.Core.NWScript.Enum;
using Xenomech.Core.NWScript.Enum.Item;
using Item = Xenomech.Service.Item;

namespace Xenomech.Feature
{
    public class WeaponFeatConfiguration
    {
        /// <summary>
        /// When the module loads, set all of the weapon-related feat and item configurations.
        /// </summary>
        [NWNEventHandler("mod_load")]
        public static void ConfigureWeaponFeats()
        {
            ConfigureMartialArts();

            // Weapon Focus, Specialization, Improved Critical
            ConfigureVibroblades();
            ConfigureFinesseVibroblades();
            ConfigureHeavyVibroblades();
            ConfigurePolearms();
            ConfigureTwinBlades();
            ConfigureKnuckles();
            ConfigureStaves();
            ConfigurePistols();
            ConfigureThrowingWeapons();
            ConfigureRifles();
        }

        private static void ConfigureMartialArts()
        {
            Weapon.SetWeaponIsMonkWeapon(BaseItem.Club);
            Weapon.SetWeaponIsMonkWeapon(BaseItem.QuarterStaff);
            Weapon.SetWeaponIsMonkWeapon(BaseItem.LightMace);
        }

        private static void ConfigureVibroblades()
        {
            foreach (var itemType in Item.VibrobladeBaseItemTypes)
            {
                Weapon.SetWeaponFocusFeat(itemType, FeatType.WeaponFocusLongswords);
                Weapon.SetWeaponSpecializationFeat(itemType, FeatType.WeaponSpecializationLongswords);
                Weapon.SetWeaponImprovedCriticalFeat(itemType, FeatType.ImprovedCriticalLongswords);
            }
        }

        private static void ConfigureFinesseVibroblades()
        {
            foreach (var itemType in Item.FinesseVibrobladeBaseItemTypes)
            {
                Weapon.SetWeaponFocusFeat(itemType, FeatType.WeaponFocusKnives);
                Weapon.SetWeaponSpecializationFeat(itemType, FeatType.WeaponSpecializationKnives);
                Weapon.SetWeaponImprovedCriticalFeat(itemType, FeatType.ImprovedCriticalKnives);
            }
        }
        
        private static void ConfigureHeavyVibroblades()
        {
            foreach (var itemType in Item.HeavyVibrobladeBaseItemTypes)
            {
                Weapon.SetWeaponFocusFeat(itemType, FeatType.WeaponFocusGreatswords);
                Weapon.SetWeaponSpecializationFeat(itemType, FeatType.WeaponSpecializationGreatswords);
                Weapon.SetWeaponImprovedCriticalFeat(itemType, FeatType.ImprovedCriticalGreatswords);
            }
        }

        private static void ConfigurePolearms()
        {
            foreach (var itemType in Item.PolearmBaseItemTypes)
            {
                Weapon.SetWeaponFocusFeat(itemType, FeatType.WeaponFocusPolearms);
                Weapon.SetWeaponSpecializationFeat(itemType, FeatType.WeaponSpecializationPolearms);
                Weapon.SetWeaponImprovedCriticalFeat(itemType, FeatType.ImprovedCriticalPolearms);
            }
        }

        private static void ConfigureTwinBlades()
        {
            foreach (var itemType in Item.TwinBladeBaseItemTypes)
            {
                Weapon.SetWeaponFocusFeat(itemType, FeatType.WeaponFocusTwinBlades);
                Weapon.SetWeaponSpecializationFeat(itemType, FeatType.WeaponSpecializationTwinBlades);
                Weapon.SetWeaponImprovedCriticalFeat(itemType, FeatType.ImprovedCriticalTwinBlades);
            }
        }

        private static void ConfigureKnuckles()
        {
            foreach (var itemType in Item.KnucklesBaseItemTypes)
            {
                Weapon.SetWeaponFocusFeat(itemType, FeatType.WeaponFocusKnuckles);
                Weapon.SetWeaponSpecializationFeat(itemType, FeatType.WeaponSpecializationKnuckles);
                Weapon.SetWeaponImprovedCriticalFeat(itemType, FeatType.ImprovedCriticalKnuckles);
            }
        }

        private static void ConfigureStaves()
        {
            foreach (var itemType in Item.StaffBaseItemTypes)
            {
                Weapon.SetWeaponFocusFeat(itemType, FeatType.WeaponFocusStaves);
                Weapon.SetWeaponSpecializationFeat(itemType, FeatType.WeaponSpecializationStaves);
                Weapon.SetWeaponImprovedCriticalFeat(itemType, FeatType.ImprovedCriticalStaff);
            }
        }
        private static void ConfigurePistols()
        {
            foreach (var itemType in Item.PistolBaseItemTypes)
            {
                Weapon.SetWeaponFocusFeat(itemType, FeatType.WeaponFocusPistol);
                Weapon.SetWeaponSpecializationFeat(itemType, FeatType.WeaponSpecializationPistol);
                Weapon.SetWeaponImprovedCriticalFeat(itemType, FeatType.ImprovedCriticalPistol);
            }
        }

        private static void ConfigureThrowingWeapons()
        {
            foreach (var itemType in Item.ThrowingWeaponBaseItemTypes)
            {
                Weapon.SetWeaponFocusFeat(itemType, FeatType.WeaponFocusThrowingWeapons);
                Weapon.SetWeaponSpecializationFeat(itemType, FeatType.WeaponSpecializationThrowingWeapons);
                Weapon.SetWeaponImprovedCriticalFeat(itemType, FeatType.ImprovedCriticalThrowingWeapons);
            }
        }
        
        private static void ConfigureRifles()
        {
            foreach (var itemType in Item.RifleBaseItemTypes)
            {
                Weapon.SetWeaponFocusFeat(itemType, FeatType.WeaponFocusRifles);
                Weapon.SetWeaponSpecializationFeat(itemType, FeatType.WeaponSpecializationRifles);
                Weapon.SetWeaponImprovedCriticalFeat(itemType, FeatType.ImprovedCriticalRifles);
            }
        }
    }
}
