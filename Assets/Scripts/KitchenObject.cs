using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KichenObjectsSO kichenObjectsSO;
    
    private IKitchenObjectParent kitchenObjectParent;

    public KichenObjectsSO GetKitchenObjectsSo()
    {
        return kichenObjectsSO;
    }
    
    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent)
    {
        if (this.kitchenObjectParent != null)
        {
            this.kitchenObjectParent.ClearKitchenObject();
        }
        

        if (kitchenObjectParent.HasKitchenObject())
        {
            Debug.LogError("Counter already has Kitchen Object!");
        }

        this.kitchenObjectParent = kitchenObjectParent;
        this.kitchenObjectParent.SetKitchenObject(this);

        transform.parent = kitchenObjectParent.GetKitchenObjectLocation();
        transform.localPosition = Vector3.zero;
    }
    
    public void DestroySelf()
    {
        kitchenObjectParent.ClearKitchenObject();
        Destroy(gameObject);
    }
    
    public bool TryGetPlate(out PlateKitchenObject plateKitchenObject)
    {
        if (this is PlateKitchenObject)
        {
            plateKitchenObject = this as PlateKitchenObject;
            return true;
        }
        else
        {
            plateKitchenObject = null;
            return false;
        }
    }
}
