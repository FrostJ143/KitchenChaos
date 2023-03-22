using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.InputSystem;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    private const string PLAYER_PREFS_KEY_BINDINGS = "Keybindings";
    public static GameInput Instance {get; private set;}
    
    public enum Binding {
        Move_Up,
        Move_Down,
        Move_Right,
        Move_Left,
        Interacte,
        Interacte_Alt,
        Pause,
    }

    private PlayerInputAction playerInputAction;
    
    public event EventHandler OnInteracte;
    
    public event EventHandler OnAlternateInteracte;
    
    public event EventHandler OnTogglePauseGame;

    private void Awake()
    {
        Instance = this;
        playerInputAction = new PlayerInputAction();
        
        // Load keybindings right after initilize playerInputAction and before enable playerInputAction
        if (PlayerPrefs.HasKey(PLAYER_PREFS_KEY_BINDINGS))
        {
            playerInputAction.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_PREFS_KEY_BINDINGS));
        }

        playerInputAction.Player.Enable();
        
        playerInputAction.Player.Interacte.performed += Interacte_performed;
        playerInputAction.Player.AlternateInteracte.performed += AlternateInteracte_performed;
        playerInputAction.Player.TogglePauseGame.performed += TogglePauseGame_performed;
        
    }
    
    private void OnDestroy()
    {
        playerInputAction.Player.Interacte.performed -= Interacte_performed;
        playerInputAction.Player.AlternateInteracte.performed -= AlternateInteracte_performed;
        playerInputAction.Player.TogglePauseGame.performed -= TogglePauseGame_performed;
        
        playerInputAction.Dispose();
    }
    
    private void Interacte_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteracte?.Invoke(this, EventArgs.Empty);
    }
    
    private void AlternateInteracte_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnAlternateInteracte?.Invoke(this, EventArgs.Empty);
    }
    
    private void TogglePauseGame_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnTogglePauseGame?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputAction.Player.Move.ReadValue<Vector2>();
        
        return inputVector.normalized;
    }
    
    public String GetKeyBinding(Binding binding)
    {
        string text;
        switch (binding) 
        {
            default:
            case Binding.Move_Up:
                text =  playerInputAction.Player.Move.bindings[1].ToDisplayString();
                break;
            case Binding.Move_Down:
                text = playerInputAction.Player.Move.bindings[2].ToDisplayString();
                break;
            case Binding.Move_Right:
                text =  playerInputAction.Player.Move.bindings[3].ToDisplayString();
                break;
            case Binding.Move_Left:
                text =  playerInputAction.Player.Move.bindings[4].ToDisplayString();
                break;
            case Binding.Interacte:
                text =  playerInputAction.Player.Interacte.bindings[0].ToDisplayString();
                break;
            case Binding.Interacte_Alt:
                text =  playerInputAction.Player.AlternateInteracte.bindings[0].ToDisplayString();
                break;
            case Binding.Pause:
                text =  playerInputAction.Player.TogglePauseGame.bindings[0].ToDisplayString();
                break;
        }
        
        if (text == "Escape") 
        {
            text = "Esc";
        }
        
        return text;
    }
    
    public void RebindingKey(Binding binding, Action action)
    {
        playerInputAction.Player.Disable();
        InputAction inputAction;
        int bindingIndex;

        switch (binding)
        {
            default:
            case Binding.Move_Up:
                inputAction = playerInputAction.Player.Move;
                bindingIndex = 1;
                break;
            case Binding.Move_Down:
                inputAction = playerInputAction.Player.Move;
                bindingIndex = 2;
                break;
            case Binding.Move_Right:
                inputAction = playerInputAction.Player.Move;
                bindingIndex = 3;
                break;
            case Binding.Move_Left:
                inputAction = playerInputAction.Player.Move;
                bindingIndex = 4;
                break;
            case Binding.Interacte:
                inputAction = playerInputAction.Player.Interacte;
                bindingIndex = 0;
                break;
            case Binding.Interacte_Alt:
                inputAction = playerInputAction.Player.AlternateInteracte;
                bindingIndex = 0;
                break;
            case Binding.Pause:
                inputAction = playerInputAction.Player.TogglePauseGame;
                bindingIndex = 0;
                break;
        }

        inputAction.PerformInteractiveRebinding(bindingIndex)
        .OnComplete(callback => {
            callback.Dispose();
            playerInputAction.Player.Enable();
            action();
            
            PlayerPrefs.SetString(PLAYER_PREFS_KEY_BINDINGS, playerInputAction.SaveBindingOverridesAsJson());
            PlayerPrefs.Save();
        })
        .Start();
        
    }
}
