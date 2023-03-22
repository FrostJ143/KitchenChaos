using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public static DeliveryManager Instance {get; private set;}
    
    public event EventHandler OnAddDelivery;
    public event EventHandler OnRemoveDeliver;
    public event EventHandler OnSucceedDelivery;
    public event EventHandler OnFailedDelivery;
    
    [SerializeField] private RecipeListSO recipeListSO;
    
    private List<RecipeSO> waitingRecipeSOList;
    
    private int completedRecipe;
    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 2f;
    private int waitingRecipesMax = 4;
    
    private void Awake()
    {
        waitingRecipeSOList = new List<RecipeSO>();
        Instance = this;
    }
    

    private void Update()
    {
        spawnRecipeTimer += Time.deltaTime;
        
        if (spawnRecipeTimer >= spawnRecipeTimerMax)
        {
            int randId = UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count);
            if (GameManager.Instance.IsPlaying() && waitingRecipeSOList.Count < waitingRecipesMax)
            {
                RecipeSO recipeSO = recipeListSO.recipeSOList[randId];
                waitingRecipeSOList.Add(recipeSO);
                
                OnAddDelivery?.Invoke(this, EventArgs.Empty);
            }
            
            spawnRecipeTimer = 0f;
        }
    }
    
    public void DeliveryRecipe(PlateKitchenObject plateKitchenObject)
    {
        for (int i = 0; i < waitingRecipeSOList.Count; ++i)
        {
            if (waitingRecipeSOList[i].ingredientsList.Count == plateKitchenObject.GetIngredientList().Count)
            {
                bool matchRecipe = true;
                // Has the same number of ingredients
                foreach (KichenObjectsSO recipeKichenObjectsSO in waitingRecipeSOList[i].ingredientsList)
                {
                    bool found = false;
                    // check all ingredients in recipe
                    foreach (KichenObjectsSO plateKitchenObjectsSo in plateKitchenObject.GetIngredientList())
                    {
                        // check all ingredients on the plate
                        if (recipeKichenObjectsSO == plateKitchenObjectsSo)
                        {
                            found = true;
                            break;
                        }
                    }
                    
                    if (!found)
                    {
                        matchRecipe = false;
                        break;
                    }
                }
                if (matchRecipe)
                {
                    ++completedRecipe;
                    waitingRecipeSOList.RemoveAt(i);

                    OnSucceedDelivery?.Invoke(this, EventArgs.Empty);
                    OnRemoveDeliver?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
        }

        OnFailedDelivery?.Invoke(this, EventArgs.Empty);
    }
    
    public List<RecipeSO> GetWaitingRecipeSOList()
    {
        return waitingRecipeSOList;
    }
    
    public int GetNumberOfCompletedRecipe()
    {
        return completedRecipe;
    }
}
