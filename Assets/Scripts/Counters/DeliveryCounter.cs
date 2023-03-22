using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    public static DeliveryCounter Instance {get; private set;}
    
    private void Awake()
    {
        Instance = this;
    }

    public override void Interacte(Player player)
    {
        if (player.GetKitchenObject() is PlateKitchenObject)
        {
            DeliveryManager.Instance.DeliveryRecipe(player.GetKitchenObject() as PlateKitchenObject);
            player.GetKitchenObject().DestroySelf();
        }
    }
}
