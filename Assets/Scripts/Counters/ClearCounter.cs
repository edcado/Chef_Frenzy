using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] KitchenObjectSO kitchenObjectSO;
    

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);
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