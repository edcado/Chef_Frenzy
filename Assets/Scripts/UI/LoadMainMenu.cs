using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMainMenu : MonoBehaviour
{
    [SerializeField] PlayerInputs playerInputs;
    
    void Start()
    {
        playerInputs.onGameOverAction += PlayerInputs_onGameOverAction;
    }

    private void PlayerInputs_onGameOverAction(object sender, System.EventArgs e)
    {
        LoadToMainMenu();
    }


    public void LoadToMainMenu()
    {
        
        if (KitchenGameManager.Instance.isGameOver())
        {
            SceneManager.LoadScene("MainScene");         
        }
    }
}
