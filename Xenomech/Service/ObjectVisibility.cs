using System.Collections.Generic;
using System.Linq;
using Xenomech.Core;
using Xenomech.Core.NWNX;
using Xenomech.Core.NWNX.Enum;
using Xenomech.Entity;
using static Xenomech.Core.NWScript.NWScript;

namespace Xenomech.Service
{
    public static class ObjectVisibility
    {
        private static readonly Dictionary<string, uint> _visibilityObjects = new Dictionary<string, uint>();
        private static readonly List<uint> _defaultHiddenObjects = new List<uint>();

        /// <summary>
        /// When the module loads, cycle through every area and every object to identify the visibility objects.
        /// </summary>
        [NWNEventHandler("mod_load")]
        public static void LoadVisibilityObjects()
        {
            for (var area = GetFirstArea(); GetIsObjectValid(area); area = GetNextArea())
            {
                for (var obj = GetFirstObjectInArea(area); GetIsObjectValid(obj); obj = GetNextObjectInArea(area))
                {
                    var visibilityObjectId = GetLocalString(obj, "VISIBILITY_OBJECT_ID");
                    if (!string.IsNullOrWhiteSpace(visibilityObjectId))
                    {
                        _visibilityObjects[visibilityObjectId] = obj;

                        if (GetLocalBool(obj, "VISIBILITY_HIDDEN_DEFAULT"))
                        {
                            _defaultHiddenObjects.Add(obj);
                        }

                    }
                }
            }
        }

        /// <summary>
        /// When a player enters the server, toggle visibility on all objects
        /// </summary>
        [NWNEventHandler("mod_enter")]
        public static void LoadPlayerVisibilityObjects()
        {
            var player = GetEnteringObject();
            if (!GetIsPC(player) || GetIsDM(player)) return;

            // Toggle visibility of all hidden objects 
            foreach (var hiddenObject in _defaultHiddenObjects)
            {
                VisibilityPlugin.SetVisibilityOverride(player, hiddenObject, VisibilityType.Hidden);
            }

            // Now iterate over the player's objects and adjust visibility.
            var playerId = GetObjectUUID(player);
            var visibilities = (DB.Get<PlayerVisibilityObject>(playerId) ?? new PlayerVisibilityObject());
            for(var index = visibilities.ObjectVisibilities.Count-1; index >= 0; index--)
            {
                var visibility = visibilities.ObjectVisibilities.ElementAt(index);
                if (!_visibilityObjects.ContainsKey(visibility.Key))
                {
                    // This object is no longer tracked. Remove it from the player's data.
                    visibilities.ObjectVisibilities.Remove(visibility.Key);
                    continue;
                }

                var obj = _visibilityObjects[visibility.Key];
                VisibilityPlugin.SetVisibilityOverride(player, obj, visibility.Value);
            }
        }

        /// <summary>
        /// Modifies the visibility of an object for a specific player.
        /// </summary>
        /// <param name="player">The player to adjust.</param>
        /// <param name="target">The target object to adjust.</param>
        /// <param name="type">The new type of visibility to use.</param>
        public static void AdjustVisibility(uint player, uint target, VisibilityType type)
        {
            if (!GetIsPC(player) || GetIsDM(player)) return;
            if (GetIsPC(target)) return;

            var visibilityObjectID = GetLocalString(target, "VISIBILITY_OBJECT_ID");
            if (string.IsNullOrWhiteSpace(visibilityObjectID))
            {
                Log.Write(LogGroup.Error, $"{GetName(target)} is missing the local variable VISIBILITY_OBJECT_ID. The visibility of this object cannot be modified for player {GetName(player)}", true);
                return;
            }

            var playerId = GetObjectUUID(player);
            var dbVisibility = DB.Get<PlayerVisibilityObject>(playerId) ?? new PlayerVisibilityObject();
            dbVisibility.ObjectVisibilities[visibilityObjectID] = type;
            DB.Set(playerId, dbVisibility);

            VisibilityPlugin.SetVisibilityOverride(player, target, type);
        }

        /// <summary>
        /// Adjusts the visibility of a given object for a given player.
        /// </summary>
        /// <param name="player">The player to adjust.</param>
        /// <param name="visibilityObjectId">The visibility object Id of the object to adjust.</param>
        /// <param name="type">The new visibility type to adjust to.</param>
        public static void AdjustVisibilityByObjectId(uint player, string visibilityObjectId, VisibilityType type)
        {
            if (!_visibilityObjects.ContainsKey(visibilityObjectId))
            {
                Log.Write(LogGroup.Error, $"No object matching visibility object Id '{visibilityObjectId}' can be found. This is likely due to an object with an Id being created after module load.");
                return;
            }

            var obj = _visibilityObjects[visibilityObjectId];
            AdjustVisibility(player, obj, type);
        }

    }
}
