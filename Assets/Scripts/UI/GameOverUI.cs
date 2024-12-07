using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipesDeliveredNumber;
    [SerializeField] private UnityEngine.UI.Image backGroundImage;
    [SerializeField] private TextMeshProUGUI gameOverRecipesDelivered;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private UnityEngine.UI.Button gameOverButton;

    private GameEnd_php gameEnd;
    private Player player;
    private string username;


    private void Awake()
    {
        gameOverButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.MainMenu);
        });
    }

    private void Start()
    {
        KitchenGameManager.Instance.OnStateChanged += KitchenGameManager_OnStateChanged;
        Hide();
    }

   

    private void KitchenGameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (KitchenGameManager.Instance.isGameOver())
        {
            Show();
            recipesDeliveredNumber.text = Mathf.Ceil(DelyveryManager.Instance.GetSuccesfulRecipesAmount()).ToString();
            username = PlayerPrefs.GetString("Username", username);
            //gameEnd.GameEnd(username, recipesDeliveredNumber.);
        }
        else
        {
            Hide();
        }

    }

    private void Show()
    {
        recipesDeliveredNumber.gameObject.SetActive(true);
        backGroundImage.gameObject.SetActive(true);
        gameOverRecipesDelivered.gameObject.SetActive(true);    
        gameOverText.gameObject.SetActive(true);  
        gameOverButton.gameObject.SetActive(true);
        gameOverButton.Select();
    }

    private void Hide()
    {
        recipesDeliveredNumber.gameObject.SetActive(false);
        backGroundImage.gameObject.SetActive(false);
        gameOverRecipesDelivered.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(false);
        gameOverButton.gameObject.SetActive(false);
    }
}
