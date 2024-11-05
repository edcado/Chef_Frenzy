using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    public event EventHandler OnPlateSpawned;

    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;
    private float spawnPlatesTimer;
    private float spawnPlatesTimeMax = 4f;
    private int spawnPlatesAmount;
    private int spawnPlatesAmountMax = 4;

    // Update is called once per frame
    void Update()
    {
        
        if (spawnPlatesAmount < spawnPlatesAmountMax)
        {
            spawnPlatesTimer += Time.deltaTime;

            if (spawnPlatesTimer > spawnPlatesTimeMax)
            {
                
                KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, this);

                
                spawnPlatesTimer = 0;

                
                spawnPlatesAmount++;
                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
