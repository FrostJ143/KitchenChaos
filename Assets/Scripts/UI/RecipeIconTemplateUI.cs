using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class RecipeIconTemplateUI : MonoBehaviour
{
    [SerializeField] private Transform iconContainer;
    [SerializeField] private Transform iconImage;
    [SerializeField] private TextMeshProUGUI recipeNameText;
    
    private void Awake()
    {
        iconImage.gameObject.SetActive(false);
    }
    
    public void SetIconRecipe(RecipeSO recipeSO)
    {
        recipeNameText.text = recipeSO.nameRecipe;

        foreach (Transform child in iconContainer)
        {
            if (child == iconImage) continue;
            Destroy(child.gameObject);
        }
        
        foreach (KichenObjectsSO kichenObjectsSO in recipeSO.ingredientsList)
        {
            Transform iconTransform = Instantiate(iconImage, iconContainer);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<Image>().sprite = kichenObjectsSO.sprite;
        }
    }
}
