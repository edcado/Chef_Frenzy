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
    [SerializeField] private UnityEngine.UI.Button button;

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
        button.gameObject.SetActive(true);
    }

    private void Hide()
    {
        recipesDeliveredNumber.gameObject.SetActive(false);
        backGroundImage.gameObject.SetActive(false);
        gameOverRecipesDelivered.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(false);
        button.gameObject.SetActive(false);
    }
}