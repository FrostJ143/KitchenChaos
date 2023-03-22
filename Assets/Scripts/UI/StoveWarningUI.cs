using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveWarningUI : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;

    private void Start()
    {
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
        
        Hide();
    }
    
    private void StoveCounter_OnProgressChanged(object sender, IHasProgressBar.OnCuttingProgressEventArgs e)
    {
        float burnShowProgressAmount = .5f;
        bool show = stoveCounter.IsInFriedState() && e.progressNormalized >= burnShowProgressAmount;

        if (show)
        {
            Show();
        }
        else 
        {
            Hide();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
    
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
