using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
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

    private DeliveryManager deliveryManager;
    private Player player;
    private string username;

    private void Awake()
    {
        gameOverButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.Shutdown();
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

            // Obtener las recetas entregadas por el cliente actual
            ulong clientId = NetworkManager.Singleton.LocalClientId;
            int recipesDelivered = DeliveryManager.Instance.GetPlayerSuccessfulRecipes(clientId);

            recipesDeliveredNumber.text = recipesDelivered.ToString();

            EndGame();
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

    private void EndGame()
    {
        ulong clientId = NetworkManager.Singleton.LocalClientId;

        // Obtener el username del clientId.
        string username = PlayerSessionManager.Instance.GetUsernameByClientId(clientId);

        // Obtener estadísticas del jugador.
        int platesDelivered = DeliveryManager.Instance.GetPlayerSuccessfulRecipes(clientId);
        int gamesPlayed = PlayerSessionManager.Instance.userData.GamesPlayed++;
        int wins = 0; // Dependiendo de si ganó o no.

        // Enviar los datos al servidor PHP.
        GameEnd_php gameEndPhp = FindObjectOfType<GameEnd_php>();
        gameEndPhp.SendPlayerData(username, platesDelivered, gamesPlayed, wins);
    }
}
