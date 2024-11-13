using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemove;

    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;
    private float spawnPlatesTimer;
    private float spawnPlatesTimeMax = 4f;
    private int spawnPlatesAmount;
    private int spawnPlatesAmountMax = 4;

    // Update is called once per frame
    void Update()
    {

        spawnPlatesTimer += Time.deltaTime;

        if (spawnPlatesTimer > spawnPlatesTimeMax )
        {
            spawnPlatesTimer = 0;

            if (spawnPlatesAmount < spawnPlatesAmountMax)
            {
                spawnPlatesAmount++;

                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(Player player)
    {
       if (!HasKitchenObject() && !player.HasKitchenObject())
       {
            if (spawnPlatesAmount > 0)
            {
                spawnPlatesAmount--;
                KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);

                OnPlateRemove?.Invoke(this, EventArgs.Empty);
            }
       }
    }
}


