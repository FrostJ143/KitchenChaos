using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStartUI : MonoBehaviour
{
    private const string NUMBER_POP_UP = "NumberPopUp";

    [SerializeField] private TextMeshProUGUI gameStartText;
    [SerializeField] private Animator animator;
    
    private int previousCountDownNumber;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        
        Hide();
    }
    
    private void Update()
    {
        int countDownNumber =  Mathf.CeilToInt(GameManager.Instance.GetWaitingToStartTimer());
        gameStartText.text = countDownNumber.ToString();

        if (countDownNumber != previousCountDownNumber)
        {
            previousCountDownNumber = countDownNumber;
            animator.SetTrigger(NUMBER_POP_UP);
            SoundManager.Instance.PlayCountDownSound();
        }
    }
    
    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsWaitingToStartActive())
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
