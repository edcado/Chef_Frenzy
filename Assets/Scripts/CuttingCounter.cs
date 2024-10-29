using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private CuttingObjectsSO[] cuttingObjectSOArray;

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
            KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO()); 

            GetKitchenObject().Destroy();

            KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
        }
    }

    public bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (CuttingObjectsSO cuttingObjectSO in cuttingObjectSOArray)
        {
            if (cuttingObjectSO.input == inputKitchenObjectSO)
            {
                return true;
            }

        }

        return false;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach(CuttingObjectsSO cuttingObjectSO in cuttingObjectSOArray)
        {
            if (cuttingObjectSO.input == inputKitchenObjectSO)
            {
                return cuttingObjectSO.output;
            }
            
        }

        return null;
    }
  
}
