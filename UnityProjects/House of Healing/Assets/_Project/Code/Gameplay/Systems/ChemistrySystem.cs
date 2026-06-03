using System;
using System.Collections.Generic;
using _Project.Code.Core;
using _Project.Code.Core.Enums;
using _Project.Code.Core.Interfaces;
using _Project.Code.Utilities;
using UnityEngine;

namespace _Project.Code.Gameplay.Systems
{
    /// <summary>
    ///     Evaluates ingredient combinations and raises OnCombinationResolved.
    ///     Does not manage inventory — that is InventorySystem's responsibility.
    ///     Accepts 2 or 3 processed ingredients only.
    /// </summary>
    public class ChemistrySystem : MonoBehaviour
    {
        [Tooltip("All authored CombinationRuleData assets.")] [SerializeField]
        private CombinationRuleData[] combinationRules;

        private CombinationLookup lookup;

        private void Awake()
        {
            lookup = new CombinationLookup(combinationRules);
        }

        public static event Action<OutcomeResult> OnCombinationResolved;

        /// <summary>
        ///     Entry point called by UI. Validates, looks up rule, raises result event.
        /// </summary>
        public void Evaluate(List<IIngredient> ingredients)
        {
            if (!IngredientValidator.ValidateCount(ingredients)) return;
            if (!IngredientValidator.ValidateAllProcessed(ingredients)) return;

            var rule = lookup.FindRule(ingredients);

            var result = rule != null
                ? new OutcomeResult(rule.OutcomeType, rule.ResultName)
                : new OutcomeResult(OutcomeType.Neutral, "Unknown Mixture");

            Debug.Log($"[ChemistrySystem] Combination resolved: {result}");
            OnCombinationResolved?.Invoke(result);
        }
    }
}