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
        private int currentRuleIndex = 0;
        public Transform itemSpawnTransform;
        public List<Ingredient> currentIngredients;

        public void CheckIngredients(Ingredient ingredient, Ingredient ingredient2)
        {
            for (int i = 0; i < combinationRules.Length; i++)
            {
                if (combinationRules[i].CanCombineIngredients(ingredient.data, ingredient2.data))
                {
                    Debug.Log("combined");
                    currentRuleIndex = i;
                    CombineIngredients();
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Ingredient>())
            {
                currentIngredients.Add(other.GetComponent<Ingredient>());
            }

            if (currentIngredients.Count >= 2)
            {
                CheckIngredients(currentIngredients[0], currentIngredients[1]);
            }
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