using UnityEngine;

public class PlayerSessionManager : MonoBehaviour
{
    public static PlayerSessionManager Instance { get; private set; }

    public UserData userData;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject); // Mantener la instancia entre escenas
    }

    // M�todo para inicializar los datos del usuario desde el servidor
    public void InitializeUserData(UserData data)
    {
        userData = data;
    }

    // M�todo para actualizar datos al final de una partida
    public void UpdateGameStats(int gamesPlayed, int wins, int platesDelivered)
    {
        userData.GamesPlayed += gamesPlayed;
        userData.Wins += wins;
        userData.PlatesDelivered += platesDelivered;
    }

    // M�todo para sincronizar los datos con el servidor
    public void SaveDataToServer()
    {
        // Aqu� puedes implementar la l�gica para enviar los datos actualizados al servidor
        Debug.Log("Sincronizando datos con el servidor...");
    }
}
