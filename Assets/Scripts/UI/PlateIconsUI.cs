using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateIconsUI : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private Transform iconTemplate;
    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }
    
    private void Start()
    {
        plateKitchenObject.OnAddIngredient += PlateKitchenObject_OnAddIngredient;
    }
    private void PlateKitchenObject_OnAddIngredient(object sender, PlateKitchenObject.OnAddIngredientEventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach (Transform child in transform)
        {
            if (child == iconTemplate) continue;
            Destroy(child.gameObject);
        }
        foreach (KichenObjectsSO kichenObjectsSO in plateKitchenObject.GetIngredientList())
        {
            Transform iconTemplateTransform = Instantiate(iconTemplate, transform);
            iconTemplateTransform.gameObject.SetActive(true);
            iconTemplateTransform.GetComponent<IconTemplateUI>().ChangeIcon(kichenObjectsSO);
        }
    }
    
    
}
