using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePausedUI : MonoBehaviour
{
    [SerializeField] private Button optionsButton;
    public static GamePausedUI Instance { get; private set; }   

    private void Awake()
    {
        optionsButton.onClick.AddListener(() =>
        {
            AudioOptionsUI.Instance.Show();
        });

        Instance = this;    
    }
    private void Start()
    {
        KitchenGameManager.Instance.OnGamePaused += KitchenGameManager_OnGamePaused;
        KitchenGameManager.Instance.OnGameUnPaused += KitchenGameManager_OnGameUnPaused;
        Hide();
    }

    private void KitchenGameManager_OnGameUnPaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void KitchenGameManager_OnGamePaused(object sender, System.EventArgs e)
    {
        Show();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1.0f;
    }

    public void Resume()
    {
        KitchenGameManager.Instance.GamePaused();
    }
}