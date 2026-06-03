using _Project.Code.Core.Enums;
using UnityEngine;

namespace _Project.Code.Core
{
    /// <summary>
    ///     ScriptableObject defining a single ingredient type.
    ///     Author one asset per ingredient in ScriptableObjects/GameData/.
    /// </summary>
    [CreateAssetMenu(fileName = "NewIngredient", menuName = "House of Healing/Ingredient Data")]
    public class IngredientData : ScriptableObject
    {
        [SerializeField] private string ingredientName;
        [SerializeField] private IngredientCategory category;
        [SerializeField] private Sprite rawSprite;
        [SerializeField] private Sprite processedSprite;

        public string IngredientName => ingredientName;
        public IngredientCategory Category => category;
        public Sprite RawSprite => rawSprite;
        public Sprite ProcessedSprite => processedSprite;
    }
}