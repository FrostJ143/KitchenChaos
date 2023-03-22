using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class IconTemplateUI : MonoBehaviour
{
    [SerializeField] private Image image;
    
    public void ChangeIcon(KichenObjectsSO kichenObjectsSO)
    {
        image.sprite = kichenObjectsSO.sprite;
    }
}
