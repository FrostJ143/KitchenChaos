using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgressBar
{
    public event EventHandler<IHasProgressBar.OnCuttingProgressEventArgs> OnProgressChanged;
    
    public event EventHandler OnCut;
    
    public static event EventHandler OnAnyCut;
    new public static void ResetStaticData()
    {
        OnAnyCut = null;
    }

    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;
    
    // private AudioSource audioSource;
    private int cuttingProgress;

    private void Awake()
    {
        // audioSource = GetComponent<AudioSource>();
    }

    public override void Interacte(Player player)
    {
        if (HasKitchenObject())
        {
            // There is a KitchenObject
            if (player.HasKitchenObject())
            {
                // Player is carrying somthing
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectsSo()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                }
            }
            else 
            {
                // Player not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
        else 
        {
            // There is not a KitchenObject here
            if (player.HasKitchenObject())
            {
                // Player is carrying something
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectsSo()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);

                    cuttingProgress = 0;

                    CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectsSo());
                    OnProgressChanged?.Invoke(this, new IHasProgressBar.OnCuttingProgressEventArgs {
                        progressNormalized = (float)cuttingProgress / cuttingRecipeSO.maxRecipeCut
                    });
                }
            }
            else
            {
                // Player is not carrying anything
            }
        }
    }
    
    public override void AlternateInteracte(Player player)
    {
        if (HasKitchenObject() )
        {
            KichenObjectsSO kichenObjectsSO = GetKitchenObject().GetKitchenObjectsSo();
            if (HasRecipeWithInput(kichenObjectsSO))
            {
                // There is KichenObject and it has a recipe
                    ++cuttingProgress;

                    CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(kichenObjectsSO);
                    OnProgressChanged?.Invoke(this, new IHasProgressBar.OnCuttingProgressEventArgs {
                        progressNormalized = (float)cuttingProgress / cuttingRecipeSO.maxRecipeCut});
                    
                    OnCut?.Invoke(this, EventArgs.Empty);
                    
                    OnAnyCut?.Invoke(this, EventArgs.Empty);
                    
                    // audioSource.Play();

                    if (cuttingProgress >= cuttingRecipeSO.maxRecipeCut)
                    {
                        GetKitchenObject().DestroySelf();
                        Transform kitchenObjectTransform = Instantiate(cuttingRecipeSO.output.prefab);
                        kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
                    }
            }
        }
    }
    
    private KichenObjectsSO GetOutputForInput(KichenObjectsSO input)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(input);
        if (cuttingRecipeSO != null)
        {
            return cuttingRecipeSO.output;
        }
        else 
        {
            return null;
        }
    }
    
    private bool HasRecipeWithInput(KichenObjectsSO input)
    {
        if (GetCuttingRecipeSOWithInput(input)) return true;
        return false;
    }
    
    private CuttingRecipeSO GetCuttingRecipeSOWithInput(KichenObjectsSO input)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
        {
            if (cuttingRecipeSO.input == input)
            {
                return cuttingRecipeSO;
            }
        }
        return null;
    }
}
