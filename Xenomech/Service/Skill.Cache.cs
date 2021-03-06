using System;
using System.Collections.Generic;
using System.Linq;
using Xenomech.Core;
using Xenomech.Core.NWNX;
using Xenomech.Enumeration;
using Xenomech.Extension;
using static Xenomech.Core.NWScript.NWScript;

namespace Xenomech.Service
{
    public static partial class Skill
    {
        // All categories, including inactive
        private static readonly Dictionary<SkillCategoryType, SkillCategoryAttribute> _allCategories = new Dictionary<SkillCategoryType, SkillCategoryAttribute>();
        private static readonly Dictionary<SkillCategoryType, SkillCategoryAttribute> _allCategoriesWithSkillContributing = new Dictionary<SkillCategoryType, SkillCategoryAttribute>();

        // Active categories only
        private static readonly Dictionary<SkillCategoryType, SkillCategoryAttribute> _activeCategories = new Dictionary<SkillCategoryType, SkillCategoryAttribute>();
        private static readonly Dictionary<SkillCategoryType, SkillCategoryAttribute> _activeCategoriesWithSkillContributing = new Dictionary<SkillCategoryType, SkillCategoryAttribute>();

        // All skills, including inactive
        private static readonly Dictionary<SkillType, SkillAttribute> _allSkills = new Dictionary<SkillType, SkillAttribute>();
        private static readonly Dictionary<SkillCategoryType, List<SkillType>> _allSkillsByCategory = new Dictionary<SkillCategoryType, List<SkillType>>();
        private static readonly Dictionary<SkillType, SkillAttribute> _allSkillsContributingToCap = new Dictionary<SkillType, SkillAttribute>();

        // Active skills only
        private static readonly Dictionary<SkillType, SkillAttribute> _activeSkills = new Dictionary<SkillType, SkillAttribute>();
        private static readonly Dictionary<SkillCategoryType, List<SkillType>> _activeSkillsByCategory = new Dictionary<SkillCategoryType, List<SkillType>>();
        private static readonly Dictionary<SkillType, SkillAttribute> _activeSkillsContributingToCap = new Dictionary<SkillType, SkillAttribute>();

        /// <summary>
        /// When the module loads, skills and categories are organized into dictionaries for quick look-ups later on.
        /// </summary>
        [NWNEventHandler("mod_load")]
        public static void CacheData()
        {
            // Initialize the list of categories.
            var categories = Enum.GetValues(typeof(SkillCategoryType)).Cast<SkillCategoryType>();
            foreach (var category in categories)
            {
                var categoryDetail = category.GetAttribute<SkillCategoryType, SkillCategoryAttribute>();
                _allCategories[category] = categoryDetail;
                _allSkillsByCategory[category] = new List<SkillType>();

                if (categoryDetail.IsActive)
                {
                    _activeSkillsByCategory[category] = new List<SkillType>();
                }
            }

            // Organize skills to make later reads quicker.
            var skills = Enum.GetValues(typeof(SkillType)).Cast<SkillType>();
            foreach (var skillType in skills)
            {
                var skillDetail = skillType.GetAttribute<SkillType, SkillAttribute>();
                var categoryDetail = _allCategories[skillDetail.Category];

                // Add to the skills cache
                _allSkills[skillType] = skillDetail;

                // Add to contributing cache if the skill contributes towards the skill cap.
                if (skillDetail.ContributesToSkillCap)
                {
                    _allSkillsContributingToCap[skillType] = skillDetail;
                    _allCategoriesWithSkillContributing[skillDetail.Category] = categoryDetail;

                    if (categoryDetail.IsActive)
                    {
                        _activeCategoriesWithSkillContributing[skillDetail.Category] = categoryDetail;
                    }

                    if (skillDetail.IsActive)
                    {
                        _activeSkillsContributingToCap[skillType] = skillDetail;
                    }
                }

                // Add to active cache if the skill is active
                if (skillDetail.IsActive)
                {
                    _activeSkills[skillType] = skillDetail;

                    if(!_activeSkillsByCategory.ContainsKey(skillDetail.Category))
                        _activeSkillsByCategory[skillDetail.Category] = new List<SkillType>();

                    _activeSkillsByCategory[skillDetail.Category].Add(skillType);
                }

                // Add to active category cache if the skill and category are both active.
                if (skillDetail.IsActive && categoryDetail.IsActive)
                {
                    _activeCategories[skillDetail.Category] = categoryDetail;
                }

                // Add to the skills by category cache.
                _allSkillsByCategory[skillDetail.Category].Add(skillType);
            }

            EventsPlugin.SignalEvent("XM_CACHE_SKILLS_LOADED", GetModule());
            Console.WriteLine($"Loaded {_activeCategories.Count} skill categories.");
            Console.WriteLine($"Loaded {_allSkills.Count} skills.");
        }

        /// <summary>
        /// Retrieves a list of all skills, including inactive ones.
        /// </summary>
        /// <returns>A list of all skills.</returns>
        public static Dictionary<SkillType, SkillAttribute> GetAllSkills()
        {
            return _allSkills.ToDictionary(x => x.Key, y => y.Value);
        }

        /// <summary>
        /// Retrieves a list of all skills, excluding inactive ones.
        /// </summary>
        /// <returns>A list of active skills.</returns>
        public static Dictionary<SkillType, SkillAttribute> GetAllActiveSkills()
        {
            return _activeSkills.ToDictionary(x => x.Key, y => y.Value);
        }

        /// <summary>
        /// Retrieves a list of all skills which contribute towards the skill cap.
        /// </summary>
        /// <returns>A list of skills contributing towards the skill cap.</returns>
        public static Dictionary<SkillType, SkillAttribute> GetAllContributingSkills()
        {
            return _allSkillsContributingToCap.ToDictionary(x => x.Key, y => y.Value);
        }

        /// <summary>
        /// Retrieves a list of active skills which contribute towards the skill cap.
        /// </summary>
        /// <returns>A list of active skills contributing towards the skill cap.</returns>
        public static Dictionary<SkillType, SkillAttribute> GetActiveContributingSkills()
        {
            return _activeSkillsContributingToCap.ToDictionary(x => x.Key, y => y.Value);
        }

        /// <summary>
        /// Retrieves a list of all skill categories, including inactive ones.
        /// </summary>
        /// <returns>A list of all skill categories</returns>
        public static Dictionary<SkillCategoryType, SkillCategoryAttribute> GetAllSkillCategories()
        {
            return _allCategories.ToDictionary(x => x.Key, y => y.Value);
        }

        /// <summary>
        /// Retrieves a list of all skill categories, excluding inactive ones.
        /// </summary>
        /// <returns>A list of active skill categories.</returns>
        public static Dictionary<SkillCategoryType, SkillCategoryAttribute> GetAllActiveSkillCategories()
        {
            return _activeCategories.ToDictionary(x => x.Key, y => y.Value);
        }

        /// <summary>
        /// Retrieves a list of all skill categories which have skills that contribute towards the skill cap.
        /// </summary>
        /// <returns>A list of skill categories which have skills that contribute towards the skill cap.</returns>
        public static Dictionary<SkillCategoryType, SkillCategoryAttribute> GetAllContributingSkillCategories()
        {
            return _allCategoriesWithSkillContributing.ToDictionary(x => x.Key, y => y.Value);
        }

        /// <summary>
        /// Retrieves a list of active skill categories which have skills that contribute towards the skill cap.
        /// </summary>
        /// <returns>A list of skill categores which have skills that contribute towards the skill cap.</returns>
        public static Dictionary<SkillCategoryType, SkillCategoryAttribute> GetActiveContributingSkillCategories()
        {
            return _activeCategoriesWithSkillContributing.ToDictionary(x => x.Key, y => y.Value);
        }

        /// <summary>
        /// Retrieves all skills by a given category, including inactive ones.
        /// </summary>
        /// <param name="category">The category of skills to retrieve.</param>
        /// <returns>A dictionary containing skills in the specified category.</returns>
        public static Dictionary<SkillType, SkillAttribute> GetAllSkillsByCategory(SkillCategoryType category)
        {
            return _allSkillsByCategory[category].ToDictionary(x => x, y => _allSkills[y]);
        }

        /// <summary>
        /// Retrieves active skills by a given category, excluding inactive ones.
        /// </summary>
        /// <param name="category">The category of skills to retrieve.</param>
        /// <returns>A dictionary containing active skills in the specified category.</returns>
        public static Dictionary<SkillType, SkillAttribute> GetActiveSkillsByCategory(SkillCategoryType category)
        {
            return _activeSkillsByCategory[category].ToDictionary(x => x, y => _activeSkills[y]);
        }

        /// <summary>
        /// Retrieves details about a specific skill.
        /// </summary>
        /// <param name="skillType">The skill whose details we will retrieve.</param>
        /// <returns>An object containing details about a skill.</returns>
        public static SkillAttribute GetSkillDetails(SkillType skillType)
        {
            return _allSkills[skillType];
        }

        /// <summary>
        /// Retrieves details about a specific skill category.
        /// </summary>
        /// <param name="category">The category whose details we will retrieve.</param>
        /// <returns>An object containing details about a skill category.</returns>
        public static SkillCategoryAttribute GetSkillCategoryDetails(SkillCategoryType category)
        {
            return _allCategories[category];
        }
    }
}
