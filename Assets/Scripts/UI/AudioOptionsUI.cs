using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AudioOptionsUI : MonoBehaviour
{
    [SerializeField] private Button musicButton;
    [SerializeField] private Button soundEffectsButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private TextMeshProUGUI musicText;
    [SerializeField] private TextMeshProUGUI SoundEffectsText;

    public static AudioOptionsUI Instance { get; private set; }

    private void Awake()
    {
        soundEffectsButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.ChangeVolume();
            UpdateSounds();
        });

        musicButton.onClick.AddListener(() =>
        {
            MusicManager.Instance.ChangeVolume();
            UpdateSounds();
        });

        closeButton.onClick.AddListener(() =>
        {
            Hide();
        });


        Instance = this;
    }

    private void Start()
    {
        UpdateSounds();
        Hide();
        KitchenGameManager.Instance.OnGameUnPaused += KitchenGameManager_OnGameUnPaused;
    }

    private void KitchenGameManager_OnGameUnPaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void UpdateSounds()
    {
        SoundManager.Instance.GetVolume();
        SoundEffectsText.text = "Sound Effects: " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f);  
        musicText.text = "Music: " + Mathf.Round(MusicManager.Instance.GetVolume() * 10f);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        GamePausedUI.Instance.Hide();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        GamePausedUI.Instance.Show();
    }

}
