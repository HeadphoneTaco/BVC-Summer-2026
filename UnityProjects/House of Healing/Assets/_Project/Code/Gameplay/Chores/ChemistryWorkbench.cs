using System.Collections.Generic;
using _Project.Code.Core.Interfaces;
using _Project.Code.Gameplay.Systems;
using UnityEngine;

namespace _Project.Code.Gameplay.Chores
{
    /// <summary>
    ///     A world-space chemistry workbench. The player adds 2–3 processed ingredients,
    ///     then interacts to trigger ChemistrySystem evaluation.
    ///     Interacting again while empty clears any lingering mess via CleaningSystem.
    /// </summary>
    public class ChemistryWorkbench : MonoBehaviour, IInteractable
    {
        [SerializeField] private ChemistrySystem chemistrySystem;
        [SerializeField] private CleaningSystem cleaningSystem;

        private readonly List<IIngredient> stagedIngredients = new();

        public IReadOnlyList<IIngredient> StagedIngredients => stagedIngredients;

        // --- IInteractable ---

        public string GetInteractionPrompt()
        {
            if (cleaningSystem != null && cleaningSystem.HasMess())
                return "Clean up mess";

            return stagedIngredients.Count switch
            {
                0 => "Workbench (add ingredients first)",
                1 => $"Workbench ({stagedIngredients.Count}/2 — need at least 2)",
                _ => $"Combine {stagedIngredients.Count} ingredient{(stagedIngredients.Count > 1 ? "s" : "")}"
            };
        }

        public void Interact(GameObject interactor)
        {
            // Prioritise cleaning if there's a mess
            if (cleaningSystem != null && cleaningSystem.HasMess())
            {
                cleaningSystem.Clean();
                return;
            }

            if (stagedIngredients.Count < 2)
            {
                Debug.Log("[ChemistryWorkbench] Not enough ingredients to combine.");
                return;
            }

            if (chemistrySystem != null)
            {
                chemistrySystem.Evaluate(stagedIngredients);
                ConsumeStagedIngredients();
            }
            else
            {
                Debug.LogWarning("[ChemistryWorkbench] No ChemistrySystem assigned.");
            }
        }

        /// <summary>
        ///     Called when the player places a processed ingredient on the workbench.
        ///     Accepts up to 3 ingredients per combination.
        /// </summary>
        public bool StageIngredient(IIngredient ingredient)
        {
            if (stagedIngredients.Count >= 3)
            {
                Debug.LogWarning("[ChemistryWorkbench] Workbench is full (max 3 ingredients).");
                return false;
            }

            stagedIngredients.Add(ingredient);
            Debug.Log(
                $"[ChemistryWorkbench] Staged: {ingredient.GetData().ingredientName} ({stagedIngredients.Count}/3)");
            return true;
        }

        public void ClearStaged()
        {
            stagedIngredients.Clear();
        }

        /// <summary>
        ///     A combination consumes its ingredients regardless of outcome —
        ///     destroy the staged ingredient objects and empty the list.
        /// </summary>
        private void ConsumeStagedIngredients()
        {
            foreach (var ingredient in stagedIngredients)
                if (ingredient is Component component && component != null)
                    Destroy(component.gameObject);

            stagedIngredients.Clear();
        }
    }
}