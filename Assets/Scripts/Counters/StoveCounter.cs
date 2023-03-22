using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class StoveCounter : BaseCounter, IHasProgressBar
{
    public event EventHandler<IHasProgressBar.OnCuttingProgressEventArgs> OnProgressChanged;
    public event EventHandler<OnFryingEventArgs> OnFrying;
    public class OnFryingEventArgs
    {
        public State state;
    }

    public enum State
    {
        Idle,
        Frying,
        Fried,
        Burned,
    }

    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
    [SerializeField] private BurningRecipeSO[] burningRecipeSOArray;
    
    private FryingRecipeSO fryingRecipeSO;
    private BurningRecipeSO burningRecipeSO;

    private float fryingTimer;
    private float burningTimer;

    private State state;

    private void Start()
    {
        state = State.Idle;
    }
    
    private void Update()
    {
        if (HasKitchenObject())
            switch (state)
            {
                case State.Idle:
                break;
                case State.Frying:
                    fryingTimer += Time.deltaTime;

                    OnProgressChanged?.Invoke(this, new IHasProgressBar.OnCuttingProgressEventArgs {
                        progressNormalized = (float) fryingTimer / fryingRecipeSO.fryingTimerMax
                    });

                    if (fryingTimer >= fryingRecipeSO.fryingTimerMax)
                    {
                        GetKitchenObject().DestroySelf();
                        Transform kitchenObjectTransform = Instantiate(fryingRecipeSO.output.prefab);
                        kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this);

                        burningRecipeSO = GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectsSo());
                        burningTimer = 0f;
                        state = State.Fried;
                        OnFrying?.Invoke(this, new OnFryingEventArgs {
                            state = state
                        });
                    }
                    break;
                case State.Fried:
                    burningTimer += Time.deltaTime;
                    if (burningTimer >= burningRecipeSO.burningTimerMax)
                    {
                        GetKitchenObject().DestroySelf();
                        Transform kitchenObjectTransform = Instantiate(burningRecipeSO.output.prefab);
                        kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this);

                        state = State.Burned;
                        OnFrying?.Invoke(this, new OnFryingEventArgs {
                            state = state
                        });
                    }

                    OnProgressChanged?.Invoke(this, new IHasProgressBar.OnCuttingProgressEventArgs {
                        progressNormalized = (float) burningTimer / burningRecipeSO.burningTimerMax
                    });

                    break;
                case State.Burned:
                break;
            }
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
                        state = State.Idle;
                        
                        OnProgressChanged?.Invoke(this, new IHasProgressBar.OnCuttingProgressEventArgs {
                                progressNormalized = 0f
                        });
                        OnFrying?.Invoke(this, new OnFryingEventArgs {
                            state = state
                        });
                    }
                }

            }
            else 
            {
                // Player not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);

                state = State.Idle;
                OnFrying?.Invoke(this, new OnFryingEventArgs {
                    state = state
                });
                
                OnProgressChanged?.Invoke(this, new IHasProgressBar.OnCuttingProgressEventArgs {
                        progressNormalized = 0f
                });
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
                    fryingRecipeSO = GetFryingRecipeSOWithInput(player.GetKitchenObject().GetKitchenObjectsSo());
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    

                    state = State.Frying;
                    fryingTimer = 0f;
                    OnFrying?.Invoke(this, new OnFryingEventArgs {
                        state = state
                    });
                }
            }
            else
            {
                // Player is not carrying anything
            }
        }
    }
    
    private KichenObjectsSO GetOutputForInput(KichenObjectsSO input)
    {
        FryingRecipeSO FryingRecipeSO = GetFryingRecipeSOWithInput(input);
        if (FryingRecipeSO != null)
        {
            return FryingRecipeSO.output;
        }
        else 
        {
            return null;
        }
    }
    
    private bool HasRecipeWithInput(KichenObjectsSO input)
    {
        if (GetFryingRecipeSOWithInput(input)) return true;
        return false;
    }
    
    public bool IsInFriedState()
    {
        return state == State.Fried;
    }
    
    private FryingRecipeSO GetFryingRecipeSOWithInput(KichenObjectsSO input)
    {
        foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOArray)
        {
            if (fryingRecipeSO.input == input)
            {
                return fryingRecipeSO;
            }
        }
        return null;
    }

    private BurningRecipeSO GetBurningRecipeSOWithInput(KichenObjectsSO input)
    {
        foreach (BurningRecipeSO burningRecipeSO in burningRecipeSOArray)
        {
            if (burningRecipeSO.input == input)
            {
                return burningRecipeSO;
            }
        }
        return null;
    }
    
    
}
