namespace Xenomech.Core.NWScript.Enum
{
    public enum EventScript
    {
        Invalid = 0,
        Module_OnHeartbeat = 3000,
        Module_OnUserDefined = 3001,
        Module_OnModuleLoad = 3002,
        Module_OnModuleStart = 3003,
        Module_OnClientEnter = 3004,
        Module_OnClientExit = 3005,
        Module_OnActivateItem = 3006,
        Module_OnAcquireItem = 3007,
        Module_OnLoseItem = 3008,
        Module_OnPlayerDeath = 3009,
        Module_OnPlayerDying = 3010,
        Module_OnRespawnButtonPressed = 3011,
        Module_OnPlayerRest = 3012,
        Module_OnPlayerLevelUp = 3013,
        Module_OnPlayerCancelCutscene = 3014,
        Module_OnEquipItem = 3015,
        Module_OnUnequipItem = 3016,
        Module_OnPlayerChat = 3017,
        Module_OnPlayerTarget = 3018,
        Module_OnPlayerGuiEvent = 3019,
        Module_OnPlayerTileEvent = 3020,
        Module_OnNuiEvent = 3021,

        Area_OnHeartbeat = 4000,
        Area_OnUserDefined = 4001,
        Area_OnEnter = 4002,
        Area_OnExit = 4003,

        AreaOfEffect_OnHeartbeat = 11000,
        AreaOfEffect_OnUserDefined = 11001,
        AreaOfEffect_OnObjectEnter = 11002,
        AreaOfEffect_OnObjectExit = 11003,

        Creature_OnHeartbeat = 5000,
        Creature_OnNotice = 5001,
        Creature_OnSpellCastAt = 5002,
        Creature_OnMeleeAttacked = 5003,
        Creature_OnDamaged = 5004,
        Creature_OnDisturbed = 5005,
        Creature_OnEndCombatRound = 5006,
        Creature_OnDialogue = 5007,
        Creature_OnSpawnIn = 5008,
        Creature_OnRested = 5009,
        Creature_OnDeath = 5010,
        Creature_OnUserDefined = 5011,
        Creature_OnBlockedByDoor = 5012,

        Trigger_OnHeartbeat = 7000,
        Trigger_OnObjectEnter = 7001,
        Trigger_OnObjectExit = 7002,
        Trigger_OnUserDefined = 7003,
        Trigger_OnTrapTriggered = 7004,
        Trigger_OnDisarmed = 7005,
        Trigger_OnClicked = 7006,

        Placeable_OnClosed = 9000,
        Placeable_OnDamaged = 9001,
        Placeable_OnDeath = 9002,
        Placeable_OnDisarm = 9003,
        Placeable_OnHeartbeat = 9004,
        Placeable_OnInventoryDisturbed = 9005,
        Placeable_OnLock = 9006,
        Placeable_OnMeleeAttacked = 9007,
        Placeable_OnOpen = 9008,
        Placeable_OnSpellCastAt = 9009,
        Placeable_OnTrapTriggered = 9010,
        Placeable_OnUnlock = 9011,
        Placeable_OnUsed = 9012,
        Placeable_OnUserDefined = 9013,
        Placeable_OnDialogue = 9014,
        Placeable_OnLeftClick = 9015,

        Door_OnOpen = 10000,
        Door_OnClose = 10001,
        Door_OnDamage = 10002,
        Door_OnDeath = 10003,
        Door_OnDisarm = 10004,
        Door_OnHeartbeat = 10005,
        Door_OnLock = 10006,
        Door_OnMeleeAttacked = 10007,
        Door_OnSpellcastat = 10008,
        Door_OnTrapTriggered = 10009,
        Door_OnUnlock = 10010,
        Door_OnUserDefined = 10011,
        Door_OnClicked = 10012,
        Door_OnDialogue = 10013,
        Door_OnFailToOpen = 10014,

        Encounter_OnObjectEnter = 13000,
        Encounter_OnObjectExit = 13001,
        Encounter_OnHeartbeat = 13002,
        Encounter_OnEncounterExhausted = 13003,
        Encounter_OnUserDefined = 13004,

        Store_OnOpen = 14000,
        Store_OnClose = 14001
    }
}