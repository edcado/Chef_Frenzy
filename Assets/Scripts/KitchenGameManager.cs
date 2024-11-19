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
    private float playingTimer = 10f;


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
                if (waitingToStartTimer < 0f)
                    state = States.CountDown;
                break;

            case States.CountDown:
                countDownTimer -= Time.deltaTime;
                if (countDownTimer < 0f)
                    state = States.Playing;
                break;

            case States.Playing:
                playingTimer -= Time.deltaTime;
                if (playingTimer < 0f)
                    state = States.GameOver;
                break;

            case States.GameOver:
                break;
        }
        Debug.Log(state);
    }

    public bool isPlayingGame()
    {
        return state == States.Playing;
    }
}
