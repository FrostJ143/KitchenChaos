using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private StoveCounter stoveCounter;
    
    private float playWarningSoundTimer;
    private bool bPlaySound = false;
    
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    
    private void Start()
    {
        stoveCounter.OnFrying += StoveCounter_OnFrying;
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
    }
    
    private void Update()
    {
        if (bPlaySound)
        {
            Debug.Log("Hello");
            playWarningSoundTimer -= Time.deltaTime;
            if (playWarningSoundTimer <= 0f)
            {
                float playWarningSoundTimerMax = .2f;
                playWarningSoundTimer = playWarningSoundTimerMax;
                SoundManager.Instance.PlayWarningSound(stoveCounter.transform.position);
            }
        }
    }
    
    private void StoveCounter_OnProgressChanged(object sender, IHasProgressBar.OnCuttingProgressEventArgs e)
    {
        float burnShowProgressAmount = .5f;
        bPlaySound = stoveCounter.IsInFriedState() && e.progressNormalized >= burnShowProgressAmount;
    }
    
    private void StoveCounter_OnFrying(object sender, StoveCounter.OnFryingEventArgs e)
    {
        bool playSound = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;
        if (playSound)
        {
            audioSource.Play();
        }
        else 
        {
            audioSource.Pause();
        }
    }
    
    
    
    
    
}
