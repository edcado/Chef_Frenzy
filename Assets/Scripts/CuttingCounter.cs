using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private CuttingObjectsSO[] cuttingObjectSOArray;

    public int cuttingProgress;

    public override void Interact(Player player)
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

    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
        {
            cuttingProgress++;

            CuttingObjectsSO cuttingObjectSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

            if (cuttingProgress >= cuttingObjectSO.maxCutProgress)
            {
                KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());

                GetKitchenObject().Destroy();

                KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
                cuttingProgress = 0;
            }
            
        }
    }

    public bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingObjectsSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
        return cuttingRecipeSO != null;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingObjectsSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
        if (cuttingRecipeSO != null)
        {
            return cuttingRecipeSO.output;
        }

        else
        {
            return null;
        }

        
    }   

    private CuttingObjectsSO GetCuttingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        {
            foreach (CuttingObjectsSO cuttingObjectSO in cuttingObjectSOArray)
            {
                if (cuttingObjectSO.input == inputKitchenObjectSO)
                {
                    return cuttingObjectSO;
                }

            }

            return null;
        }
    }
  
}
