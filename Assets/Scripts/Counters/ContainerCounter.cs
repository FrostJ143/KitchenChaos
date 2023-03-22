using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    public event EventHandler OnPlayerGrabbedObject;
    [SerializeField] private KichenObjectsSO kichenObjectsSO;

    public override void Interacte(Player player)
    {
        if (!player.HasKitchenObject())
        {
            Transform kitchenObjectTransform = Instantiate(kichenObjectsSO.prefab);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(player);
            
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
    }
}
