using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI keyMoveUpText;
    [SerializeField] private TextMeshProUGUI keyMoveDownText;
    [SerializeField] private TextMeshProUGUI keyMoveRightText;
    [SerializeField] private TextMeshProUGUI keyMoveLeftText;
    [SerializeField] private TextMeshProUGUI keyInteracteText;
    [SerializeField] private TextMeshProUGUI keyAltInteracteText;
    [SerializeField] private TextMeshProUGUI keyPauseText;
    
    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;

        UpdateVisual();
    }
    
    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsWaitingToStartActive())
        {
            Hide();
        }
    }
    
    private void UpdateVisual()
    {
        keyMoveUpText.text = GameInput.Instance.GetKeyBinding(GameInput.Binding.Move_Up);
        keyMoveDownText.text = GameInput.Instance.GetKeyBinding(GameInput.Binding.Move_Down);
        keyMoveRightText.text = GameInput.Instance.GetKeyBinding(GameInput.Binding.Move_Right);
        keyMoveLeftText.text = GameInput.Instance.GetKeyBinding(GameInput.Binding.Move_Left);
        keyInteracteText.text = GameInput.Instance.GetKeyBinding(GameInput.Binding.Interacte);
        keyAltInteracteText.text = GameInput.Instance.GetKeyBinding(GameInput.Binding.Interacte_Alt);
        keyPauseText.text = GameInput.Instance.GetKeyBinding(GameInput.Binding.Pause);
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
