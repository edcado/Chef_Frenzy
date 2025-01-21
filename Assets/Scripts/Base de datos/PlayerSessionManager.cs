using UnityEngine;
using Unity.Netcode;
using System.Collections.Generic;

public class PlayerSessionManager : MonoBehaviour
{
    public static PlayerSessionManager Instance { get; private set; }

    private Dictionary<ulong, string> clientIdToUsername = new Dictionary<ulong, string>();

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

    [ServerRpc(RequireOwnership = false)]
    public void RegisterUsernameServerRpc(string username, ServerRpcParams serverRpcParams = default)
    {
        ulong clientId = serverRpcParams.Receive.SenderClientId;

        if (!clientIdToUsername.ContainsKey(clientId))
        {
            clientIdToUsername.Add(clientId, username);
            Debug.Log($"Registrado username '{username}' para clientId '{clientId}'");
        }
    }

    public string GetUsernameByClientId(ulong clientId)
    {
        if (clientIdToUsername.TryGetValue(clientId, out string username))
        {
            return username;
        }
        return "Unknown";
    }


    // Método para inicializar los datos del usuario desde el servidor
    public void InitializeUserData(UserData data)
    {
        userData = data;
    }


}
