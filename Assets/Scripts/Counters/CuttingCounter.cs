using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgress
{
    public event EventHandler<IHasProgress.OnProgressChangedEventsArgs> OnProgressChanged;
    public event EventHandler OnCanCut;
    public event EventHandler OnCutFinished;

    public event EventHandler onCut;
    public static event EventHandler OnAnyCut;

    new public static void ResetStaticData()
    {
        OnAnyCut = null;    
    }

    [SerializeField] public CuttingObjectsSO[] cuttingObjectSOArray;

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
                        cuttingProgress = 0;

                        CuttingObjectsSO cuttingObjectSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventsArgs
                        {
                            progressNormalized = (float)cuttingProgress / cuttingObjectSO.maxCutProgress
                        });

                        OnCanCut?.Invoke(this, EventArgs.Empty);    
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
                    if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                    {

                        if (plateKitchenObject.TryAddIngridient(GetKitchenObject().GetKitchenObjectSO()))
                        {
                            GetKitchenObject().Destroy();
                            OnCanCut?.Invoke(this, EventArgs.Empty);
                        }
                    }
                }

                else
                {
                    GetKitchenObject().SetKitchenObjectParent(player);
                    OnCanCut?.Invoke(this, EventArgs.Empty);
                }
            }
        }
    }

    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
        {
            cuttingProgress++;

            onCut?.Invoke(this, EventArgs.Empty);
            OnAnyCut?.Invoke(this, EventArgs.Empty);

            CuttingObjectsSO cuttingObjectSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventsArgs
            {
                progressNormalized = (float)cuttingProgress / cuttingObjectSO.maxCutProgress
            });

            if (cuttingProgress >= cuttingObjectSO.maxCutProgress)
            {
                KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());

                GetKitchenObject().Destroy();

                KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
                

               
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
            OnCutFinished?.Invoke(this, EventArgs.Empty);
            return cuttingRecipeSO.output;
        }

        else
        {
            return null;
        }

        
    }   

    public CuttingObjectsSO GetCuttingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
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
