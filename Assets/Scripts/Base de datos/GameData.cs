using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData Instance;
    public string GameName;  // Almacena el Game Name del jugador

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // Hace que este objeto persista entre escenas
        }
        else
        {
            Destroy(gameObject);
        }
    }
}