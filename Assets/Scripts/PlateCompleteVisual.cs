using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable]
    public struct KitchenObjectSO_GameObject 
    {
        public KichenObjectsSO kichenObjectsSO;
        public GameObject gameObject;
    }
    [SerializeField] PlateKitchenObject plateKitchenObject;
    [SerializeField] List<KitchenObjectSO_GameObject> kitchenObjectSO_GameObjectsList;
    
    private void Start()
    {
        plateKitchenObject.OnAddIngredient += PlateKitchenObject_OnAddIngredient;
        
        foreach (KitchenObjectSO_GameObject kitchenObjectSO_GameObject in kitchenObjectSO_GameObjectsList)
        {
            kitchenObjectSO_GameObject.gameObject.SetActive(false);
        }
    }
    
    private void PlateKitchenObject_OnAddIngredient(object sender, PlateKitchenObject.OnAddIngredientEventArgs e)
    {
        foreach (KitchenObjectSO_GameObject kitchenObjectSO_GameObject in kitchenObjectSO_GameObjectsList)
        {
            if (e.kichenObjectsSO == kitchenObjectSO_GameObject.kichenObjectsSO)
            {
                kitchenObjectSO_GameObject.gameObject.SetActive(true);
            }
        }
    }
}
