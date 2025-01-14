using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateLobbyUI : MonoBehaviour
{
    [SerializeField] private Button createPublicButton;
    [SerializeField] private Button createPrivateButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private TMP_InputField nameLobbyInputField;

    private void Awake()
    {
        Hide();
        createPublicButton.onClick.AddListener(() =>
        {
            KitchenGameLobby.Instance.CreateLobby(nameLobbyInputField.text, false);
        });

        createPrivateButton.onClick.AddListener(() =>
        {
            KitchenGameLobby.Instance.CreateLobby(nameLobbyInputField.text, true);
        });

        closeButton.onClick.AddListener(() =>
        {
            KitchenGameLobby.Instance.CreateLobby(nameLobbyInputField.text, false);
            Hide();
        });
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true); 
    }
}
