using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI Instance;
   [SerializeField] private Button soundEffectsButton;
   [SerializeField] private Button musicButton;
   [SerializeField] private Button closeButton;
   [SerializeField] private Button moveUpBtn;
   [SerializeField] private Button moveDownBtn;
   [SerializeField] private Button moveRightBtn;
   [SerializeField] private Button moveLeftBtn;
   [SerializeField] private Button interacteBtn;
   [SerializeField] private Button interacteAltBtn;
   [SerializeField] private Button pauseBtn;
   
   [SerializeField] private TextMeshProUGUI soundEffectsText;
   [SerializeField] private TextMeshProUGUI musicText;
   [SerializeField] private TextMeshProUGUI moveUpText;
   [SerializeField] private TextMeshProUGUI moveDownText;
   [SerializeField] private TextMeshProUGUI moveRightText;
   [SerializeField] private TextMeshProUGUI moveLeftText;
   [SerializeField] private TextMeshProUGUI interacteText;
   [SerializeField] private TextMeshProUGUI interacteAltText;
   [SerializeField] private TextMeshProUGUI pauseText;
   
   [SerializeField] private Transform rebindUI;
   
   
   private void Awake()
   {
    Instance = this;

    soundEffectsButton.onClick.AddListener(() => {
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();
    });
    
    musicButton.onClick.AddListener(() => {
            MusicManager.Instance.ChangeVolume();
            UpdateVisual();
    });
    
    closeButton.onClick.AddListener(() => {
            Hide();
    });
    
    moveUpBtn.onClick.AddListener(() => {RebindingKey(GameInput.Binding.Move_Up);});
    moveDownBtn.onClick.AddListener(() => {RebindingKey(GameInput.Binding.Move_Down);});
    moveRightBtn.onClick.AddListener(() => {RebindingKey(GameInput.Binding.Move_Right);});
    moveLeftBtn.onClick.AddListener(() => {RebindingKey(GameInput.Binding.Move_Left);});
    interacteBtn.onClick.AddListener(() => {RebindingKey(GameInput.Binding.Interacte);});
    interacteAltBtn.onClick.AddListener(() => {RebindingKey(GameInput.Binding.Interacte_Alt);});
    pauseBtn.onClick.AddListener(() => {RebindingKey(GameInput.Binding.Pause);});
   }
   
   private void Start()
    {
        GameManager.Instance.OnUnPauseGame += GameManager_OnUnPauseGame;
        
        UpdateVisual();

        Hide();
        HideRebindUI();
    }
   
   private void GameManager_OnUnPauseGame(object sender, System.EventArgs e)
   {
        Hide();
   }
   
   private void UpdateVisual()
   {
       soundEffectsText.text = "Sound Effects: " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f);
       musicText.text = "Music: " + Mathf.Round(MusicManager.Instance.GetVolume() * 10f);
       
       moveUpText.text = GameInput.Instance.GetKeyBinding(GameInput.Binding.Move_Up);
       moveDownText.text = GameInput.Instance.GetKeyBinding(GameInput.Binding.Move_Down);
       moveRightText.text = GameInput.Instance.GetKeyBinding(GameInput.Binding.Move_Right);
       moveLeftText.text = GameInput.Instance.GetKeyBinding(GameInput.Binding.Move_Left);
       interacteText.text = GameInput.Instance.GetKeyBinding(GameInput.Binding.Interacte);
       interacteAltText.text = GameInput.Instance.GetKeyBinding(GameInput.Binding.Interacte_Alt);
       pauseText.text = GameInput.Instance.GetKeyBinding(GameInput.Binding.Pause);
   }
   
   public void Show()
   {
    gameObject.SetActive(true);
   }

   public void Hide()
   {
    gameObject.SetActive(false);
   }
   
   private void RebindingKey(GameInput.Binding binding)
   {
        GameInput.Instance.RebindingKey(binding, () => {
                HideRebindUI();
                UpdateVisual();
        });
        ShowRebindUI();
   }
   
   private void ShowRebindUI()
   {
        rebindUI.gameObject.SetActive(true);
   }
   
   private void HideRebindUI()
   {
        rebindUI.gameObject.SetActive(false);
   }
}
