using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GamePausedUI : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button optionsButton;
    
    private void Awake()
    {
        resumeButton.onClick.AddListener(() => {
                GameManager.Instance.TogglePauseGame();
        });
        
        mainMenuButton.onClick.AddListener(() => {
                Loader.Load(Loader.Scene.MainMenuScene);
        });
        
        optionsButton.onClick.AddListener(() => {
                OptionsUI.Instance.Show();
        });
    }
    private void Start()
    {
        GameManager.Instance.OnPauseGame += GameManager_OnPauseGame;
        GameManager.Instance.OnUnPauseGame += GameManager_OnUnPauseGame;
        
        Hide();
    }
    private void GameManager_OnPauseGame(object sender, System.EventArgs e)
    {
        Show();
    }

    private void GameManager_OnUnPauseGame(object sender, System.EventArgs e)
    {
        Hide();
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
