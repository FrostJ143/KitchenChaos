using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private Image barImage;
    [SerializeField] private GameObject hasProgressBarGameObject;
    
    private IHasProgressBar hasProgressBar;

    private void Start()
    {
        hasProgressBar = hasProgressBarGameObject.GetComponent<IHasProgressBar>();
        if (hasProgressBar == null)
        {
            Debug.LogError("There is no HasProgressBar interface!");
        }

        hasProgressBar.OnProgressChanged += hasProgressBar_OnProgressChanged;
        
        barImage.fillAmount = 0f;
        Hide();
    }
    
    private void hasProgressBar_OnProgressChanged(object sender, IHasProgressBar.OnCuttingProgressEventArgs e)
    {
        barImage.fillAmount = e.progressNormalized;
        
        if (barImage.fillAmount == 0f || barImage.fillAmount == 1f)
        {
            Hide();
        }
        else 
        {
            Show();
        }
    }
    
    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
