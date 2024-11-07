using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    private List<KitchenObjectSO> kitchenObjectSOList;
    [SerializeField] private List<KitchenObjectSO> validIngredientsSOList;

    private void Awake()
    {
        kitchenObjectSOList = new List<KitchenObjectSO>();  
    }

    public bool TryAddIngridient(KitchenObjectSO kitchenObjectSO)
    {
        if (!validIngredientsSOList.Contains(kitchenObjectSO))
        {
            return false;
        }

        if (kitchenObjectSOList.Contains(kitchenObjectSO))
        {           
            return false;
        }

        else
        {
            kitchenObjectSOList.Add(kitchenObjectSO);
            return true;
        }
       
    }
}

    
