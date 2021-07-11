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
            ConfigureLongswords();
            ConfigureKnives();
            ConfigureGreatswords();
            ConfigurePolearms();
            ConfigureTwinBlades();
            ConfigureKatars();
            ConfigureStaves();
            ConfigurePistols();
            ConfigureThrowingWeapons();
            ConfigureRifles();
        }

        private static void ConfigureMartialArts()
        {
            WeaponPlugin.SetWeaponIsMonkWeapon(BaseItem.Club);
            WeaponPlugin.SetWeaponIsMonkWeapon(BaseItem.QuarterStaff);
            WeaponPlugin.SetWeaponIsMonkWeapon(BaseItem.LightMace);
        }

        private static void ConfigureLongswords()
        {
            foreach (var itemType in Item.VibrobladeBaseItemTypes)
            {
                WeaponPlugin.SetWeaponFocusFeat(itemType, FeatType.WeaponFocusLongswords);
                WeaponPlugin.SetWeaponSpecializationFeat(itemType, FeatType.WeaponSpecializationLongswords);
                WeaponPlugin.SetWeaponImprovedCriticalFeat(itemType, FeatType.ImprovedCriticalLongswords);
            }
        }

        private static void ConfigureKnives()
        {
            foreach (var itemType in Item.FinesseVibrobladeBaseItemTypes)
            {
                WeaponPlugin.SetWeaponFocusFeat(itemType, FeatType.WeaponFocusKnives);
                WeaponPlugin.SetWeaponSpecializationFeat(itemType, FeatType.WeaponSpecializationKnives);
                WeaponPlugin.SetWeaponImprovedCriticalFeat(itemType, FeatType.ImprovedCriticalKnives);
            }
        }
        
        private static void ConfigureGreatswords()
        {
            foreach (var itemType in Item.HeavyVibrobladeBaseItemTypes)
            {
                WeaponPlugin.SetWeaponFocusFeat(itemType, FeatType.WeaponFocusGreatswords);
                WeaponPlugin.SetWeaponSpecializationFeat(itemType, FeatType.WeaponSpecializationGreatswords);
                WeaponPlugin.SetWeaponImprovedCriticalFeat(itemType, FeatType.ImprovedCriticalGreatswords);
            }
        }

        private static void ConfigurePolearms()
        {
            foreach (var itemType in Item.PolearmBaseItemTypes)
            {
                WeaponPlugin.SetWeaponFocusFeat(itemType, FeatType.WeaponFocusPolearms);
                WeaponPlugin.SetWeaponSpecializationFeat(itemType, FeatType.WeaponSpecializationPolearms);
                WeaponPlugin.SetWeaponImprovedCriticalFeat(itemType, FeatType.ImprovedCriticalPolearms);
            }
        }

        private static void ConfigureTwinBlades()
        {
            foreach (var itemType in Item.TwinBladeBaseItemTypes)
            {
                WeaponPlugin.SetWeaponFocusFeat(itemType, FeatType.WeaponFocusTwinBlades);
                WeaponPlugin.SetWeaponSpecializationFeat(itemType, FeatType.WeaponSpecializationTwinBlades);
                WeaponPlugin.SetWeaponImprovedCriticalFeat(itemType, FeatType.ImprovedCriticalTwinBlades);
            }
        }

        private static void ConfigureKatars()
        {
            foreach (var itemType in Item.KatarBaseItemTypes)
            {
                WeaponPlugin.SetWeaponFocusFeat(itemType, FeatType.WeaponFocusKatars);
                WeaponPlugin.SetWeaponSpecializationFeat(itemType, FeatType.WeaponSpecializationKatars);
                WeaponPlugin.SetWeaponImprovedCriticalFeat(itemType, FeatType.ImprovedCriticalKatars);
            }
        }

        private static void ConfigureStaves()
        {
            foreach (var itemType in Item.StaffBaseItemTypes)
            {
                WeaponPlugin.SetWeaponFocusFeat(itemType, FeatType.WeaponFocusStaves);
                WeaponPlugin.SetWeaponSpecializationFeat(itemType, FeatType.WeaponSpecializationStaves);
                WeaponPlugin.SetWeaponImprovedCriticalFeat(itemType, FeatType.ImprovedCriticalStaff);
            }
        }
        private static void ConfigurePistols()
        {
            foreach (var itemType in Item.PistolBaseItemTypes)
            {
                WeaponPlugin.SetWeaponFocusFeat(itemType, FeatType.WeaponFocusPistol);
                WeaponPlugin.SetWeaponSpecializationFeat(itemType, FeatType.WeaponSpecializationPistol);
                WeaponPlugin.SetWeaponImprovedCriticalFeat(itemType, FeatType.ImprovedCriticalPistol);
            }
        }

        private static void ConfigureThrowingWeapons()
        {
            foreach (var itemType in Item.ThrowingWeaponBaseItemTypes)
            {
                WeaponPlugin.SetWeaponFocusFeat(itemType, FeatType.WeaponFocusThrowingWeapons);
                WeaponPlugin.SetWeaponSpecializationFeat(itemType, FeatType.WeaponSpecializationThrowingWeapons);
                WeaponPlugin.SetWeaponImprovedCriticalFeat(itemType, FeatType.ImprovedCriticalThrowingWeapons);
            }
        }
        
        private static void ConfigureRifles()
        {
            foreach (var itemType in Item.RifleBaseItemTypes)
            {
                WeaponPlugin.SetWeaponFocusFeat(itemType, FeatType.WeaponFocusRifles);
                WeaponPlugin.SetWeaponSpecializationFeat(itemType, FeatType.WeaponSpecializationRifles);
                WeaponPlugin.SetWeaponImprovedCriticalFeat(itemType, FeatType.ImprovedCriticalRifles);
            }
        }
    }
}
