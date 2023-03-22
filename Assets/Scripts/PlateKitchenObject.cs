using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    public event EventHandler<OnAddIngredientEventArgs> OnAddIngredient;
    public class OnAddIngredientEventArgs 
    {
        public KichenObjectsSO kichenObjectsSO;
    }

    [SerializeField] private List<KichenObjectsSO> ValidIngredientsList;
    private List<KichenObjectsSO> ingredientList;

    private void Awake()
    {
        ingredientList = new List<KichenObjectsSO>();
    }

    public bool TryAddIngredient(KichenObjectsSO kichenObjectsSO)
    {
        if (!ValidIngredientsList.Contains(kichenObjectsSO))
        {
            // Not in valid ingredient
            return false;
        }
        if (ingredientList.Contains(kichenObjectsSO))
        {
            // ALready has ingredient
            return false;
        }

        ingredientList.Add(kichenObjectsSO);
        OnAddIngredient?.Invoke(this, new OnAddIngredientEventArgs {
            kichenObjectsSO = kichenObjectsSO
        });
        return true;
    }
    
    public List<KichenObjectsSO> GetIngredientList()
    {
        return ingredientList;
    }
}
