using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] PlayerInputs playerInputs;
    private bool canStart = false;
    
    void Start()
    {
        
    }

    private void PlayerInputs_onMainMenuQuit(object sender, System.EventArgs e)
    {
        QuitGame();
    }

    private void PlayerInputs_onMainMenuLoadIn(object sender, System.EventArgs e)
    {
        LoadPlayScene();
    }

    public void LoadPlayScene()
    {
        if (!canStart)
        {
            Loader.Load(Loader.Scene.MainScene);
            canStart = true;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
