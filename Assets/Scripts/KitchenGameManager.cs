using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenGameManager : MonoBehaviour
{
    public static KitchenGameManager Instance { get; private set; }
    public enum States {waitingToStart, CountDown, Playing, GameOver }
    private States state;

    private float waitingToStartTimer = 1f;
    private float countDownTimer = 3f;
    private float playingTimer;
    [SerializeField] private float playingTimerMax = 10f;

   public event EventHandler OnStateChanged;

    private void Awake()
    {
        Instance = this;
        state = States.waitingToStart;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case States.waitingToStart:
                waitingToStartTimer -= Time.deltaTime;
                if (waitingToStartTimer < 0f) state = States.CountDown;
                OnStateChanged?.Invoke(this, EventArgs.Empty);              
                break;

            case States.CountDown:
                countDownTimer -= Time.deltaTime;
                if (countDownTimer < 0f)
                    state = States.Playing;
                playingTimer = playingTimerMax;
                OnStateChanged?.Invoke(this, EventArgs.Empty);

                break;

            case States.Playing:
                playingTimer -= Time.deltaTime;
                if (playingTimer < 0f)
                    state = States.GameOver;
                OnStateChanged?.Invoke(this, EventArgs.Empty);

                break;

            case States.GameOver:
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;
        }
        Debug.Log(state);
    }

    public bool isPlayingGame()
    {
        return state == States.Playing;
    }

    public bool isCountingDown()
    {
        return state == States.CountDown;
    }

    public float GetCountDownTimer()
    {
        return countDownTimer;
    }
    public bool isGameOver()
    {
        return state == States.GameOver;
    }

    public float GetGameTimerUI()
    {
        return 1 -(playingTimer / playingTimerMax);
    }
}
