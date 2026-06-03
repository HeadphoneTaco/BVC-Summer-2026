using _Project.Code.Core.Enums;
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
        [Tooltip("2 or 3 ingredients. Order does not matter.")] [SerializeField]
        private IngredientData[] ingredients;

        [SerializeField] private OutcomeType outcomeType;

        [Tooltip("Displayed name of the remedy, potion, or mess produced.")] [SerializeField]
        private string resultName;

        public IngredientData[] Ingredients => ingredients;
        public OutcomeType OutcomeType => outcomeType;
        public string ResultName => resultName;
    }
}