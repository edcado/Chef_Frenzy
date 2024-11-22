using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DelyveryManager : MonoBehaviour
{
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;

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
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer <= 0f)
        {
            spawnRecipeTimer = spawnRecipeTimerMax;
            SpawnRecipe();
        }
    }

    public void SpawnRecipe()
    {
        if (waitingRecipeSOList.Count < waitingRecipesMax)
        {
            RecipeSO waitingRecipeSO = reciveListSO.recipeSOList[UnityEngine.Random.Range(0, reciveListSO.recipeSOList.Count)];
            Debug.Log(waitingRecipeSO.recipeName);
            waitingRecipeSOList.Add(waitingRecipeSO);
            OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
        }
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
                    if (waitingRecipeSOList.Count == 0)
                    {
                        SpawnRecipe();
                        spawnRecipeTimer = spawnRecipeTimerMax;
                    }

                    string plateDelivered = waitingRecipeSOList[i].recipeName;
                    Debug.Log("Se entrego " + plateDelivered );
                    succesfulRecipesAmount++;
                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    waitingRecipeSOList.RemoveAt(i);
                    SpawnRecipe();
                    spawnRecipeTimerMax = 0;

                    return;

                    //Introducir Netcode : Quien ha entregado el plato?
                }
            }
        }

        Debug.Log("No se ha entregado bien");
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
