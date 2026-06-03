using System.Collections.Generic;
using _Project.Code.Core.Interfaces;
using UnityEngine;

namespace _Project.Code.Utilities
{
    /// <summary>
    ///     Stateless validation helpers for ChemistrySystem.
    ///     Enforces the spec rules before any combination lookup is attempted.
    /// </summary>
    public static class IngredientValidator
    {
        private const int minIngredients = 2;
        private const int maxIngredients = 3;

        /// <summary>Combination requires 2 or 3 ingredients.</summary>
        public static bool ValidateCount(List<IIngredient> ingredients)
        {
            if (ingredients == null || ingredients.Count < minIngredients || ingredients.Count > maxIngredients)
            {
                Debug.LogWarning(
                    $"[IngredientValidator] Invalid ingredient count: {ingredients?.Count}. Expected {minIngredients}–{maxIngredients}.");
                return false;
            }

            return true;
        }

        /// <summary>
        ///     All ingredients must be processed before a combination is evaluated.
        ///     Any raw ingredient locks the combination out of a Success outcome per spec.
        /// </summary>
        public static bool ValidateAllProcessed(List<IIngredient> ingredients)
        {
            foreach (var ingredient in ingredients)
                if (!ingredient.IsProcessed())
                {
                    Debug.LogWarning(
                        $"[IngredientValidator] {ingredient.GetData().IngredientName} is not processed. Combination blocked.");
                    return false;
                }

            return true;
        }
    }
}