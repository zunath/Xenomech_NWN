using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xenomech.Core;
using Xenomech.Entity;
using Xenomech.Enumeration;
using Xenomech.Service.LanguageService;
using static Xenomech.Core.NWScript.NWScript;
using SkillType = Xenomech.Enumeration.SkillType;

namespace Xenomech.Service
{
    public static class Language
    {
        private static Dictionary<SkillType, ITranslator> _translators = new Dictionary<SkillType, ITranslator>();
        private static readonly TranslatorGeneric _genericTranslator = new TranslatorGeneric();

        /// <summary>
        /// When the module loads, create translators for every language and store them into cache.
        /// </summary>
        [NWNEventHandler("mod_load")]
        public static void LoadTranslators()
        {
            _translators = new Dictionary<SkillType, ITranslator>
            {
            };
        }

        public static string TranslateSnippetForListener(uint speaker, uint listener, SkillType language, string snippet)
        {
            var translator = _translators.ContainsKey(language) ? _translators[language] : _genericTranslator;
            var languageSkill = Skill.GetSkillDetails(language);

            if (GetIsPC(speaker) && !GetIsDM(speaker))
            {
                var playerId = GetObjectUUID(speaker);
                var dbSpeaker = DB.Get<Player>(playerId);

                // Get the rank and max rank for the speaker, and garble their English text based on it.
                var speakerSkillRank = dbSpeaker.Skills[language].Rank;

                if (speakerSkillRank != languageSkill.MaxRank)
                {
                    var garbledChance = 100 - (int)((speakerSkillRank / (float)languageSkill.MaxRank) * 100);

                    var split = snippet.Split(' ');
                    for (var i = 0; i < split.Length; ++i)
                    {
                        if (Random.Next(100) <= garbledChance)
                        {
                            split[i] = new string(split[i].ToCharArray().OrderBy(s => (Random.Next(2) % 2) == 0).ToArray());
                        }
                    }

                    snippet = split.Aggregate((a, b) => a + " " + b);
                }
            }

            if (!GetIsPC(listener) || GetIsDM(listener))
            {
                // Short circuit for a DM or NPC - they will always understand the text.
                return snippet;
            }

            // Let's grab the max rank for the listener skill, and then we roll for a successful translate based on that.
            var listenerId = GetObjectUUID(listener);
            var dbListener = DB.Get<Player>(listenerId);
            var rank = dbListener.Skills[language].Rank;
            var maxRank = languageSkill.MaxRank;

            // Check for the Comprehend Speech concentration ability.
            var grantSenseXP = false;

            // Ensure we don't go over the maximum.
            if (rank > maxRank)
                rank = maxRank;

            if (rank == maxRank || speaker == listener)
            {
                // Guaranteed success - return original.
                return snippet;
            }

            var textAsForeignLanguage = translator.Translate(snippet);

            if (rank != 0)
            {
                var englishChance = (int)((rank / (float)maxRank) * 100);

                var originalSplit = snippet.Split(' ');
                var foreignSplit = textAsForeignLanguage.Split(' ');

                var endResult = new StringBuilder();

                // WARNING: We're making the assumption that originalSplit.Length == foreignSplit.Length.
                // If this assumption changes, the below logic needs to change too.
                for (var i = 0; i < originalSplit.Length; ++i)
                {
                    if (Random.Next(100) <= englishChance)
                    {
                        endResult.Append(originalSplit[i]);
                    }
                    else
                    {
                        endResult.Append(foreignSplit[i]);
                    }

                    endResult.Append(" ");
                }

                textAsForeignLanguage = endResult.ToString();
            }

            var now = DateTime.Now.Ticks;
            var lastSkillUpLow = GetLocalInt(listener, "LAST_LANGUAGE_SKILL_INCREASE_LOW");
            var lastSkillUpHigh = GetLocalInt(listener, "LAST_LANGUAGE_SKILL_INCREASE_HIGH");
            long lastSkillUp = lastSkillUpHigh;
            lastSkillUp = (lastSkillUp << 32) | (uint)lastSkillUpLow;
            var differenceInSeconds = (now - lastSkillUp) / 10000000;

            if (differenceInSeconds / 60 >= 2)
            {
                var amount = Math.Max(10, Math.Min(150, snippet.Length) / 3);
                // Reward exp towards the language - we scale this with character count, maxing at 50 exp for 150 characters.
                Skill.GiveSkillXP(listener, language, amount);

                // Grant Force XP if player is concentrating Comprehend Speech.
                if (grantSenseXP)
                    Skill.GiveSkillXP(listener, SkillType.Elemental, amount * 10);

                SetLocalInt(listener, "LAST_LANGUAGE_SKILL_INCREASE_LOW", (int)(now & 0xFFFFFFFF));
                SetLocalInt(listener, "LAST_LANGUAGE_SKILL_INCREASE_HIGH", (int)((now >> 32) & 0xFFFFFFFF));
            }

            return textAsForeignLanguage;
        }

        public static int GetColor(SkillType language)
        {
            byte r = 0;
            byte g = 0;
            byte b = 0;

            switch (language)
            {
            }

            return r << 24 | g << 16 | b << 8;
        }

        public static string GetName(SkillType language)
        {
            switch (language)
            {
            }

            return "Basic";
        }

        public static SkillType GetActiveLanguage(uint obj)
        {
            var ret = GetLocalInt(obj, "ACTIVE_LANGUAGE");

            if (ret == 0)
            {
                return SkillType.Basic;
            }

            return (SkillType)ret;
        }

        public static void SetActiveLanguage(uint obj, SkillType language)
        {
            if (language == SkillType.Basic)
            {
                DeleteLocalInt(obj, "ACTIVE_LANGUAGE");
            }
            else
            {
                SetLocalInt(obj, "ACTIVE_LANGUAGE", (int)language);
            }
        }

        private static IEnumerable<LanguageCommand> _languages;

        public static IEnumerable<LanguageCommand> Languages
        {
            get
            {
                if (_languages == null)
                {
                    var languages = new List<LanguageCommand>
                    {
                    };

                    _languages = languages;
                }

                return _languages;
            }
        }
    }
}
