using Xenomech.Core;
using static Xenomech.Core.NWScript.NWScript;

namespace Xenomech.Feature
{
    public class AttributeNameOverrides
    {
        [NWNEventHandler("mod_load")]
        public static void OverrideAttributeNames()
        {
            SetTlkOverride(131, "Diplomacy"); // Charisma
            SetTlkOverride(132, "Vitality"); // Constitution
            SetTlkOverride(133, "Perception"); // Dexterity
            SetTlkOverride(134, "Unused"); // Intelligence
            SetTlkOverride(135, "Might"); // Strength
            SetTlkOverride(136, "Spirit"); // Wisdom

            SetTlkOverride(328, "Increased Might By"); // Strength
            SetTlkOverride(329, "Increased Perception By"); // Dexterity
            SetTlkOverride(330, "Unused"); // Intelligence
            SetTlkOverride(331, "Increased Vitality By"); // Constitution
            SetTlkOverride(332, "Increased Spirit By"); // Wisdom
            SetTlkOverride(333, "Increased Diplomacy By"); // Charisma

            SetTlkOverride(473, "Might Information"); // Strength
            SetTlkOverride(474, "Perception Information"); // Dexterity
            SetTlkOverride(475, "Vitality Information"); // Constitution
            SetTlkOverride(476, "Spirit Information"); // Wisdom
            SetTlkOverride(477, "Unused"); // Intelligence
            SetTlkOverride(479, "Diplomacy Information"); // Charisma

            SetTlkOverride(457, BuildRecommendedButtonText());

            SetTlkOverride(459, "Might measures the physical power of your character. It improves your melee power and carrying capacity.");
            SetTlkOverride(460, "Perception measures the intuition of your character. It improves your ranged power and evasion.");
            SetTlkOverride(461, "Vitality represents the health and stamina of your character. It improves your max HP and stamina.");
            SetTlkOverride(462, "Spirit represents the ether attunement of your character. It improves your ether attack and ether defense.");
            SetTlkOverride(478, "Diplomacy measures the ability to negotiate. It improves your ability to negotiate mission rewards.");

            SetTlkOverride(321, "EV");
            SetTlkOverride(7099, "Evasion");
        }

        private static string BuildRecommendedButtonText()
        {
            return "Your character is guided by five core attributes: Might, Vitality, Perception, Spirit, and Diplomacy.\n\n" +
                   "Might: Improves your melee power and carrying capacity.\n" +
                   "Vitality: Improves your max hit points, ether points, and stamina.\n" +
                   "Perception: Improves your ranged power and evasion.\n" +
                   "Spirit: Improves your ether attack and ether defense.\n" +
                   "Diplomacy: Improves your ability to negotiate mission rewards.\n\n";
        }
    }
}
