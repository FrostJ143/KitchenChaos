using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}
    
    public event EventHandler OnStateChanged;
    public event EventHandler OnPauseGame;
    public event EventHandler OnUnPauseGame;

    private enum State
    {
        WaitingToCountdow,
        WaitingToStart,
        Playing,
        GameOver,
    }
    private State state;
    
    private bool isPauseGame = false;
    // private float waitingToCountdowTimer = 1f;
    private float waitingToStartTimer = 3f;
    private float playingTimer;
    [SerializeField] public float playingTimerMax = 60f;
    
    private void Awake()
    {
        Instance = this;
        state = State.WaitingToCountdow;
    }
    
    private void Start()
    {
        GameInput.Instance.OnTogglePauseGame += GameInput_OnTogglePauseGame;
        GameInput.Instance.OnInteracte += GameInput_OnInteracte;
    }
    private void GameInput_OnTogglePauseGame(object sender, System.EventArgs e)
    {
        TogglePauseGame();
    }
    
    private void GameInput_OnInteracte(object sender, System.EventArgs e)
    {
        if (state == State.WaitingToCountdow)
        {
            state = State.WaitingToStart;
            OnStateChanged?.Invoke(this, EventArgs.Empty);
        }
    }
    
    private void Update()
    {

        switch(state)
        {
            case State.WaitingToCountdow:
                break;
            case State.WaitingToStart:
                waitingToStartTimer -= Time.deltaTime;
                if (waitingToStartTimer <= 0)
                {
                    state = State.Playing;
                    playingTimer = playingTimerMax;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.Playing:
                playingTimer -= Time.deltaTime;
                if (playingTimer <= 0)
                {
                    state = State.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GameOver:
                break;
        }
    }
    
    public bool IsPlaying()
    {
        return state == State.Playing;
    }
    
    public bool IsWaitingToStartActive()
    {
        return state == State.WaitingToStart;
    }

    public bool IsGameOver()
    {
        return state == State.GameOver;
    }
    
    public float GetWaitingToStartTimer()
    {
        return waitingToStartTimer;
    }
    
    public float GetPlayingTimerNormalized()
    {
        return (1 - (float)playingTimer / playingTimerMax);
    }
    
    public void TogglePauseGame()
    {
        if (!isPauseGame)
        {
            Time.timeScale = 0f;
            isPauseGame = true;
            OnPauseGame?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Time.timeScale = 1f;
            isPauseGame = false;
            OnUnPauseGame?.Invoke(this, EventArgs.Empty);
        }

    }
}
