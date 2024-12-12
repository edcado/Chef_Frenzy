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
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    
                   if (plateKitchenObject.TryAddIngridient(GetKitchenObject().GetKitchenObjectSO()))
                   {
                        player.GetKitchenObject().Destroy();

                   }
                }

                else
                {
                    if (GetKitchenObject().TryGetPlate(out plateKitchenObject))
                    {
                        if (plateKitchenObject.TryAddIngridient(player.GetKitchenObject().GetKitchenObjectSO()))
                        {
                            player.GetKitchenObject().Destroy();

                        }

                    }
                }
            }

            else
            {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

   
}
