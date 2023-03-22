using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class DeliveryResultUI : MonoBehaviour
{
    private const string POP_UP = "PopUp";

    [SerializeField] private Image background;
    [SerializeField] private TextMeshProUGUI textDelivery;
    [SerializeField] private Image iconDelivery;
    [SerializeField] private Color colorSuccess;
    [SerializeField] private Color colorFail;
    [SerializeField] private Sprite iconSuccess;
    [SerializeField] private Sprite iconFail;
    
    private Animator animator;
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        DeliveryManager.Instance.OnSucceedDelivery += DeliveryManager_OnSucceedDelivery;
        DeliveryManager.Instance.OnFailedDelivery += DeliveryManager_OnFailedDelivery;
        
        gameObject.SetActive(false);
    }
    
    private void DeliveryManager_OnSucceedDelivery(object sender, System.EventArgs e)
    {
        gameObject.SetActive(true);
        background.color = colorSuccess;
        textDelivery.text = "DELIVERY\nSUCCESS";
        iconDelivery.sprite = iconSuccess;
        animator.SetTrigger(POP_UP);
    }
    
    private void DeliveryManager_OnFailedDelivery(object sender, System.EventArgs e)
    {
        gameObject.SetActive(true);
        background.color = colorFail;
        textDelivery.text = "DELIVERY\nFAIL";
        iconDelivery.sprite = iconFail;
        animator.SetTrigger(POP_UP);
    }
}
