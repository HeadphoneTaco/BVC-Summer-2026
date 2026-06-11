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
            // Local flags — reset every call, so a previous match can't leak into the next one.
            bool hasFirst = false;
            bool hasSecond = false;

            for (int i = 0; i < ingredients.Length; i++)
            {
                if (ingredients[i] == otherIngredientData) hasFirst = true;
                if (ingredients[i] == ingredientData) hasSecond = true;
            }

            return hasFirst && hasSecond;
        }
    }
}