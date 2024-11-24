using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    public event EventHandler <OnIngredientAddedEventArgs> OnIngredientAdded;
    public static event EventHandler OnIngredientAdd;
    public class OnIngredientAddedEventArgs : EventArgs
    {
        public KitchenObjectSO kitchenObjectSO;
    }

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
            OnIngredientAdd?.Invoke(this, EventArgs.Empty);

            OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs
            {
                kitchenObjectSO = kitchenObjectSO
            });
            return true;
        }
       
    }

    public List<KitchenObjectSO> GetKitchenSOList()
    {
        return kitchenObjectSOList;
    }
}

    
