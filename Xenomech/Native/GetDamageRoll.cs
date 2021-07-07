using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using NWN.Native.API;
using Xenomech.Core;
using Xenomech.Core.NWNX;
using Xenomech.Core.NWScript.Enum.Item;
using static Xenomech.Core.NWScript.NWScript;
using BaseItem = NWN.Native.API.BaseItem;
using EquipmentSlot = NWN.Native.API.EquipmentSlot;
using Random = Xenomech.Service.Random;

namespace Xenomech.Native
{
    public static unsafe class GetDamageRoll
    {
        private static Dictionary<int, float> _dmgValues = new Dictionary<int, float>();

        internal delegate int GetDamageRollHook(void* thisPtr, void* pTarget, int bOffHand, int bCritical, int bSneakAttack, int bDeathAttack, int bForceMax);
        // ReSharper disable once NotAccessedField.Local
        private static GetDamageRollHook _callOriginal;

        [NWNEventHandler("mod_load")]
        public static void CacheDMGValues()
        {
            var rowCount = UtilPlugin.Get2DARowCount("iprp_dmg");

            for (var row = 0; row < rowCount; row++)
            {
                var label = Get2DAString("iprp_dmg", "Label", row);

                if (float.TryParse(label, out var dmgValue))
                {
                    _dmgValues[row] = dmgValue;
                }
            }
        }

        [NWNEventHandler("mod_load")]
        public static void RegisterHook()
        {
            delegate* unmanaged<void*, void*, int, int, int, int, int, int> pHook = &OnGetDamageRoll;
            var hookPtr = Internal.NativeFunctions.RequestHook(new IntPtr(FunctionsLinux._ZN17CNWSCreatureStats13GetDamageRollEP10CNWSObjectiiiii), (IntPtr)pHook, -1000000);
            _callOriginal = Marshal.GetDelegateForFunctionPointer<GetDamageRollHook>(hookPtr);
        }

        [UnmanagedCallersOnly]
        private static int OnGetDamageRoll(void* thisPtr, void* pTarget, int bOffHand, int bCritical, int bSneakAttack, int bDeathAttack, int bForceMax)
        {
            var creatureStats = CNWSCreatureStats.FromPointer(thisPtr);
            var creature = CNWSCreature.FromPointer(creatureStats.m_pBaseCreature);
            var target = CNWSCreature.FromPointer(pTarget);
            var damageFlags = creatureStats.m_pBaseCreature.GetDamageFlags();
            var damagePower = creatureStats.m_pBaseCreature.CalculateDamagePower(target, bOffHand);

            var defense = 0f;
            var dmg = 0f;
            var attackAttribute = 0f;
            var damage = 0;

            // Calculate attacker's DMG
            if (creature != null)
            {
                var weapon = bOffHand == 1
                    ? creature.m_pInventory.GetItemInSlot((int) EquipmentSlot.LeftHand)
                    : creature.m_pInventory.GetItemInSlot((int) EquipmentSlot.RightHand);

                // Nothing equipped - check gloves.
                if (weapon == null)
                {
                    weapon = creature.m_pInventory.GetItemInSlot((int) EquipmentSlot.Arms);
                }

                if (weapon != null)
                {
                    // Iterate over properties and take the highest DMG rating.
                    for (var index = 0; index < weapon.m_lstPassiveProperties.array_size; index++)
                    {
                        var ip = weapon.GetPassiveProperty(index);
                        if (ip != null && ip.m_nPropertyName == (int)ItemPropertyType.DMG)
                        {
                            if (ip.m_nCostTableValue > dmg)
                            {
                                dmg = _dmgValues[ip.m_nCostTableValue];
                            }
                        }
                    }

                    // Ranged weapons use Perception (NWN's DEX)
                    if (weapon.m_nBaseItem == (uint) BaseItem.HeavyCrossbow ||
                        weapon.m_nBaseItem == (uint) BaseItem.LightCrossbow ||
                        weapon.m_nBaseItem == (uint) BaseItem.Shortbow ||
                        weapon.m_nBaseItem == (uint) BaseItem.Longbow ||
                        weapon.m_nBaseItem == (uint) BaseItem.Sling)
                    {
                        attackAttribute = creatureStats.GetDEXMod(0);
                    }
                    // All others use Might (NWN's STR)
                    else
                    {
                        attackAttribute = creatureStats.m_nStrengthModifier;
                    }
                }
            }

            // Safety check - DMG minimum is 0.5
            if (dmg < 0.5f)
            {
                dmg = 0.5f;
            }

            // Calculate total defense on the target.
            if (target != null)
            {
                float vitality = target.m_pStats.m_nConstitutionModifier;

                foreach (var slotItemId in target.m_pInventory.m_pEquipSlot)
                {
                    if (slotItemId != NWNXLib.OBJECT_INVALID)
                    {
                        var item = NWNXLib.AppManager().m_pServerExoApp.GetItemByGameObjectID(slotItemId);
                        for (var index = 0; index < item.m_lstPassiveProperties.array_size; index++)
                        {
                            var ip = item.GetPassiveProperty(index);
                            if (ip != null && ip.m_nPropertyName == (int)ItemPropertyType.Defense)
                            {
                                defense += ip.m_nCostTableValue;
                            }
                        }
                    }
                }
                
                // Formula: ((DMG+MightOrPerception/3)*6)-((DEF+Vitality/3)/3)
                var maxDamage = (dmg + attackAttribute / 3f) * 6f - ((defense + vitality / 3f) / 3f);
                var minDamage = maxDamage * 0.75f;

                // Criticals - 25% bonus to damage range
                if (bCritical == 1)
                {
                    minDamage = maxDamage;
                    maxDamage *= 1.25f;
                }

                damage = (int)Random.NextFloat(minDamage, maxDamage);

                // Apply NWN mechanics to damage reduction
                damage = target.DoDamageImmunity(creature, damage, damageFlags, 0, 1);
                damage = target.DoDamageResistance(creature, damage, damageFlags, 0, 1, 1);
                damage = target.DoDamageReduction(creature, damage, damagePower, 0, 1);
                if (damage < 0)
                    damage = 0;
            }

            return damage;
        }
    }
}
