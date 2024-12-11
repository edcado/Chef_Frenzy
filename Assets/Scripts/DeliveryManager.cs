using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Netcode;

public class DelyveryManager : NetworkBehaviour
{
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFail;

    [SerializeField] private ReciveListSO reciveListSO;
    private List<RecipeSO> waitingRecipeSOList;
    public static DelyveryManager Instance { get; private set;  }

    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipesMax = 4;

    public int succesfulRecipesAmount;

    private void Awake()
    {
        Instance = this;
        waitingRecipeSOList = new List<RecipeSO>();
    }


    private void Update()
    {
        if (!IsServer)
        {
            return;
        }
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer <= 0f)
        {
            spawnRecipeTimer = spawnRecipeTimerMax;
            SpawnRecipe();
        }
    }

    public void SpawnRecipe()
    {
        if (KitchenGameManager.Instance.isPlayingGame() && waitingRecipeSOList.Count < waitingRecipesMax)
        {
            int waitingRecipeIndex = UnityEngine.Random.Range(0, reciveListSO.recipeSOList.Count);
            SpawnNewWaitingRecipeCLientRpc(waitingRecipeIndex);
        }
    }


    [ClientRpc]
    private void SpawnNewWaitingRecipeCLientRpc(int waitingRecipeIndex)
    {
        RecipeSO waitingRecipeSO = reciveListSO.recipeSOList[waitingRecipeIndex];
        waitingRecipeSOList.Add(waitingRecipeSO);
        OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        for (int i = 0; i < waitingRecipeSOList.Count; i++)
        {
            RecipeSO waitingRecipeSO = waitingRecipeSOList[i];

            if (waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenSOList().Count)
            {
                bool plateContentsMatchesRecipe = true;
                foreach(KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList)
                {
                    bool ingredientFound = false;
                    foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenSOList())
                    {
                        if (plateKitchenObjectSO == recipeKitchenObjectSO)
                        {
                            //Coinciden el plato entregado con el plato requerido
                            ingredientFound = true;
                            break;
                        }
                    }
                    if (!ingredientFound)
                    {
                        //No se encontro el ingrediente en el plato proporcionado
                        plateContentsMatchesRecipe = false;
                    }
                }

                if (plateContentsMatchesRecipe)
                {
                    MatchDeliveryManagerCorrectUIServerRpc(i);
                    return;
                    //Introducir Netcode : Quien ha entregado el plato?
                }
            }
        }

        MatchDeliveryManagerInCorrectUIServerRpc();
    }

    [ServerRpc(RequireOwnership = false)]
    private void MatchDeliveryManagerInCorrectUIServerRpc()
    {
        MatchDeliveryManagerIncorrectUIClientRpc();
    }

    [ClientRpc]
    private void MatchDeliveryManagerIncorrectUIClientRpc()
    {
        OnRecipeFail?.Invoke(this, EventArgs.Empty);
    }




    [ServerRpc(RequireOwnership = false)]
    private void MatchDeliveryManagerCorrectUIServerRpc(int waitingRecipeSOListIndex)
    {
        MatchDeliveryManagerUICorrectClientRpc(waitingRecipeSOListIndex);
    }

    [ClientRpc] 
    private void MatchDeliveryManagerUICorrectClientRpc(int waitingRecipeSOListIndex)
    {
        if (waitingRecipeSOList.Count == 0)
        {
            SpawnRecipe();
            spawnRecipeTimer = spawnRecipeTimerMax;
        }


        succesfulRecipesAmount++;

        SpawnRecipe();
        spawnRecipeTimerMax = 0;

        waitingRecipeSOList.RemoveAt(waitingRecipeSOListIndex);
        OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
        OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
    }

    public List<RecipeSO> WaitingRecipeSOList()
    {
        return waitingRecipeSOList;
    }

    public int GetSuccesfulRecipesAmount()
    {
        return succesfulRecipesAmount;
    }

}
