using System.Collections.Generic;
using System.Linq;
using _Project.Code.Core;
using _Project.Code.Core.Interfaces;

namespace _Project.Code.Utilities
{
    /// <summary>
    ///     Pre-processes CombinationRuleData assets into a hash-keyed dictionary for O(1) lookup.
    ///     Order of ingredients does not matter — sets are used for matching.
    /// </summary>
    public class CombinationLookup
    {
        private readonly Dictionary<string, CombinationRuleData> rules = new();

      /*  public CombinationLookup(CombinationRuleData[] rules)
        {
            foreach (var rule in rules)
            {
                var key = BuildKey(rule.ingredients);
                if (!this.rules.ContainsKey(key))
                    this.rules[key] = rule;
            }
        }
*/
        public CombinationRuleData FindRule(List<IIngredient> ingredients)
        {
            var ingredientData = ingredients.Select(i => i.GetData()).ToArray();
            var key = BuildKey(ingredientData);
            return rules.TryGetValue(key, out var rule) ? rule : null;
        }

        /// <summary>
        ///     Builds a normalised, order-independent key from ingredient asset names.
        ///     Sorting by name ensures Fruit+Water and Water+Fruit produce the same key.
        /// </summary>
        private static string BuildKey(IngredientData[] ingredients)
        {
            var names = ingredients.Select(i => i.ingredientName).OrderBy(n => n);
            return string.Join("|", names);
        }
    }
}