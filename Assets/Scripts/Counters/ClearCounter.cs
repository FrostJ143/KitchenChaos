using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    
    public override void Interacte(Player player)
    {
        if (HasKitchenObject())
        {
            // There is a KitchenObject here
            if (player.HasKitchenObject())
            {
                // Player is carrying somthing
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    // Player is carrying plate
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectsSo()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                }
                else
                {
                    // Player is not carrying Plate but something else
                    if (GetKitchenObject().TryGetPlate(out plateKitchenObject))
                    {
                        // Counter contains plate
                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectsSo()))
                        {
                            player.GetKitchenObject().DestroySelf();
                        }
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
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {
                // Player is not carrying anything
            }
        }
    }
    
}
