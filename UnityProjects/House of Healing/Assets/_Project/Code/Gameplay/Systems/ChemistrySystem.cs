using System;
using System.Collections.Generic;
using _Project.Code.Core;
using _Project.Code.Core.Interfaces;
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
        private int currentRuleIndex;
        public Transform itemSpawnTransform;
        public List<Ingredient> currentIngredients;

        private void OnTriggerEnter(Collider other)
        {
            var newcomer = other.GetComponent<Ingredient>();
            if (newcomer == null || currentIngredients.Contains(newcomer)) return;

            // Try to pair the newly arrived ingredient with one already on the bench.
            foreach (var staged in currentIngredients)
            {
                if (staged == null) continue;

                for (int r = 0; r < combinationRules.Length; r++)
                {
                    if (!combinationRules[r].CanCombineIngredients(staged.data, newcomer.data)) continue;

                    currentRuleIndex = r;
                    CombineIngredients();

                    // Consume both ingredients of the matched pair.
                    currentIngredients.Remove(staged);
                    Destroy(staged.gameObject);
                    Destroy(newcomer.gameObject);
                    return;
                }
            }

            // No partner on the bench yet — leave it here for a future match.
            currentIngredients.Add(newcomer);
        }

        private void OnTriggerExit(Collider other)
        {
            // An ingredient that rolls off the bench should no longer count for a combination.
            var leaving = other.GetComponent<Ingredient>();
            if (leaving != null)
                currentIngredients.Remove(leaving);
        }

        void CombineIngredients()
        {
            Instantiate(combinationRules[currentRuleIndex].resultItem, itemSpawnTransform);
        }


        public void Evaluate(List<IIngredient> stagedIngredients)
        {
            throw new NotImplementedException();
        }
    }
}