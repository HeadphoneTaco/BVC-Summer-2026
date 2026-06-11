using System;
using System.Collections.Generic;
using _Project.Code.Core.Enums;
using _Project.Code.Gameplay;
using UnityEngine;

namespace _Project.Code.Core
{
    /// <summary>
    ///     ScriptableObject defining one combination rule.
    ///     Author one asset per known combination in ScriptableObjects/GameData/.
    ///     Order of ingredients does not matter — lookup normalises before matching.
    /// </summary>
    [CreateAssetMenu(fileName = "NewCombinationRule", menuName = "House of Healing/Combination Rule")]
    public class CombinationRuleData : ScriptableObject
    {
        
        public IngredientData[]  ingredients;
        public OutcomeType outcomeType;
        public string resultName;
        public Item resultItem;

        public bool CanCombineIngredients(IngredientData ingredientData, IngredientData otherIngredientData)
        {
            // A rule pairs exactly two ingredients. Require the two supplied ingredients to be
            // that exact pair (order doesn't matter) — not just "each appears somewhere in the rule".
            // This stops a single ingredient (passed as both) from matching a two-ingredient rule.
            if (ingredients.Length != 2) return false;

            var a = ingredients[0];
            var b = ingredients[1];

            return (ingredientData == a && otherIngredientData == b) ||
                   (ingredientData == b && otherIngredientData == a);
        }
    }
}