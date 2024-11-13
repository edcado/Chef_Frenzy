using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelyveryManager : MonoBehaviour
{
    [SerializeField] private ReciveListSO reciveListSO;
    private List<RecipeSO> waitingRecipeSOList;


    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipesMax = 4;

    private void Awake()
    {
        waitingRecipeSOList = new List<RecipeSO>();
    }

    private void Update()
    {
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer <= 0f)
        {
            spawnRecipeTimer = spawnRecipeTimerMax;
            if (waitingRecipeSOList.Count < waitingRecipesMax)
            {
            RecipeSO waitingRecipeSO = reciveListSO.recipeSOList[Random.Range(0, reciveListSO.recipeSOList.Count)];
                Debug.Log(waitingRecipeSO.recipeName);
            waitingRecipeSOList.Add(waitingRecipeSO);
            }
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
                    //
                    string plateDelivered = waitingRecipeSOList[i].recipeName;
                    Debug.Log("Se entrego " + plateDelivered );

                    waitingRecipeSOList.RemoveAt(i);
                }
            }
        }
    }

}
