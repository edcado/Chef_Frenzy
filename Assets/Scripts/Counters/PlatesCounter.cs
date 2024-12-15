using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemove;

    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;
    private float spawnPlatesTimer;
    private float spawnPlatesTimeMax = 4f;

    private NetworkVariable<int> spawnPlatesAmount = new NetworkVariable<int>(0);

    private int spawnPlatesAmountMax = 4;

    // Update is called once per frame
    void Update()
    {
        if (!IsServer)
        {
            return;
        }

        spawnPlatesTimer += Time.deltaTime;

        if (KitchenGameManager.Instance.isPlayingGame() && spawnPlatesTimer > spawnPlatesTimeMax)
        {
            spawnPlatesTimer = 0;

            if (spawnPlatesAmount.Value < spawnPlatesAmountMax)
            {
                SpawnPlateServerRpc();
            }
        }
    }

    [ServerRpc]
    private void SpawnPlateServerRpc()
    {
        spawnPlatesAmount.Value++;
        SpawnPlatesClientRpc(); // Actualiza visualmente para todos los clientes.
    }

    [ClientRpc]
    private void SpawnPlatesClientRpc()
    {
        OnPlateSpawned?.Invoke(this, EventArgs.Empty);
    }

    public override void Interact(Player player)
    {
        if (!HasKitchenObject() && !player.HasKitchenObject())
        {
            if (spawnPlatesAmount.Value > 0)
            {
                KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);
                InteractServerRpc();
            }
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void InteractServerRpc()
    {
        spawnPlatesAmount.Value--;
        InteractClientRpc();
    }

    [ClientRpc]
    private void InteractClientRpc()
    {
        OnPlateRemove?.Invoke(this, EventArgs.Empty);
    }
}


