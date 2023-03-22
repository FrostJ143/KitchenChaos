using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    public static event EventHandler OnAnyObjectPlacedHere;
    public static void ResetStaticData()
    {
        OnAnyObjectPlacedHere = null;
    }
    
    [SerializeField] private Transform kitchenObjectLocation;
    
    private KitchenObject kitchenObject;
    
    public virtual void Interacte(Player player) {}
    
    public virtual void AlternateInteracte(Player player) {}

    public Transform GetKitchenObjectLocation()
    {
        return kitchenObjectLocation;
    }
    
    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }
    
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
        OnAnyObjectPlacedHere?.Invoke(this, EventArgs.Empty);
    }
    
    public void ClearKitchenObject()
    {
        this.kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
