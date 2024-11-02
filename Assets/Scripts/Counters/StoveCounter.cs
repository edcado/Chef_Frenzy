using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CuttingCounter;

public class StoveCounter : BaseCounter
{
    [SerializeField] private FryingObjectSO[] fryingRecipeSOArray;

    public override void Interact(Player player)
    {
        {
            {
                if (!HasKitchenObject())
                {
                    if (player.HasKitchenObject())
                    {
                        if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                        {
                            player.GetKitchenObject().SetKitchenObjectParent(this);
                            
                        }
                    }

                    else
                    {

                    }
                }

                else
                {
                    if (player.HasKitchenObject())
                    {
                        //Nothing happens
                    }

                    else
                    {
                        GetKitchenObject().SetKitchenObjectParent(player);
                    }
                }
            }
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
       FryingObjectSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        return fryingRecipeSO != null;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingObjectSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        if (fryingRecipeSO != null)
        {
            return fryingRecipeSO.output;
        }

        else
        {
            return null;
        }
    }

    private  FryingObjectSO GetFryingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        
            foreach (FryingObjectSO fryingRecipeSO in fryingRecipeSOArray)
            {
                if (fryingRecipeSO.input == inputKitchenObjectSO)
                {
                    return fryingRecipeSO;
                }

            }

            return null;
        
    }
}
