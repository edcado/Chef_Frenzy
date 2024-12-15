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
    public int spawnPlatesAmount;
    private int spawnPlatesAmountMax = 4;

    // Update is called once per frame
    void Update()
    {
        if (!IsServer)
        {
            return;
        }

        spawnPlatesTimer += Time.deltaTime;

        if (KitchenGameManager.Instance.isPlayingGame() && spawnPlatesTimer > spawnPlatesTimeMax )
        {
            spawnPlatesTimer = 0;

            if (spawnPlatesAmount < spawnPlatesAmountMax)
            {
                SpawnPlatesServerRpc();
            }
        }
    }

    [ServerRpc]
    private void SpawnPlatesServerRpc()
    {
        SpawnPlatesClientRpc();
    }

    [ClientRpc]
    private void SpawnPlatesClientRpc()
    {
        spawnPlatesAmount++;

        OnPlateSpawned?.Invoke(this, EventArgs.Empty);
    }

    public override void Interact(Player player)
    {
       if (!HasKitchenObject() && !player.HasKitchenObject())
       {
            if (spawnPlatesAmount > 0)
            {
                KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);
                InteractServerRpc();
            }
       }
    }

    [ServerRpc]
    private void InteractServerRpc()
    {
        InteractClientRpc();
    }

    [ClientRpc]
    private void InteractClientRpc()
    {
        spawnPlatesAmount--;
        OnPlateRemove?.Invoke(this, EventArgs.Empty);
    }
}


