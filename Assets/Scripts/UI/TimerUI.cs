using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    [SerializeField] Image image;
    
    private void Update()
    {
        image.fillAmount = GameManager.Instance.GetPlayingTimerNormalized();
    }
}
