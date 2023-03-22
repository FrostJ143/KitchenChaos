using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private Transform iconTemplate;

    
    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }
    
    private void Start()
    {
        DeliveryManager.Instance.OnAddDelivery += DeliveryManager_OnAddDelivery;
        DeliveryManager.Instance.OnRemoveDeliver += DeliveryManager_OnRemoveDelivery;
        
        UpdateVisual();
    }
    
    private void DeliveryManager_OnAddDelivery(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void DeliveryManager_OnRemoveDelivery(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }
    
    private void UpdateVisual()
    {
        foreach (Transform child in container)
        {
            if (child != iconTemplate)
            {
                Destroy(child.gameObject);
            }
        }
        
        foreach (RecipeSO recipeSO in DeliveryManager.Instance.GetWaitingRecipeSOList())
        {
            Transform iconTemplateTransform = Instantiate(iconTemplate, container);
            iconTemplateTransform.gameObject.SetActive(true);
            iconTemplateTransform.GetComponent<RecipeIconTemplateUI>().SetIconRecipe(recipeSO);
        }
    }
}
