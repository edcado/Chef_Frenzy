using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class TestingCharacterSelected : NetworkBehaviour
{
    public static TestingCharacterSelected Instance { get; private set; }

    private Dictionary<ulong, bool> playerReadyDictionary;

    public event EventHandler OnReadyChanged;

    private void Awake()
    {
        Instance = this;
        playerReadyDictionary = new Dictionary<ulong, bool>();  
    }

    public void SetPlayerReady()
    {
        SetPlayerReadyServerRpc();
    }

    [ServerRpc (RequireOwnership = false)]
    private void SetPlayerReadyServerRpc(ServerRpcParams serverRpcParams = default)
    {
        ulong senderClientId = serverRpcParams.Receive.SenderClientId;

        // Marca al cliente como listo
        playerReadyDictionary[senderClientId] = true;

        // Notifica a todos los clientes sobre este cambio
        SetPlayerReadyClientRpc(senderClientId);

        // Comprueba si todos los clientes están listos después de actualizar el estado
        if (AreAllClientsReady())
        {
            Loader.LoadNetwork(Loader.Scene.MainScene);
        }
    }

    private bool AreAllClientsReady()
    {
        foreach (ulong clientId in NetworkManager.Singleton.ConnectedClientsIds)
        {
            if (!playerReadyDictionary.ContainsKey(clientId) || !playerReadyDictionary[clientId])
            {
                return false;
            }
        }
        return true;
    }

    [ClientRpc]
    private void SetPlayerReadyClientRpc(ulong clientId)
    {
        playerReadyDictionary[clientId] = true;
        OnReadyChanged?.Invoke(this,EventArgs.Empty); 
    }

    public bool IsPlayerReady(ulong clientId)
    {
        return playerReadyDictionary.ContainsKey(clientId) && playerReadyDictionary[clientId];
    }
}
