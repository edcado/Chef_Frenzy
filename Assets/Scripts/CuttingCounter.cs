using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO cutKitchenObject;

    public override void Interact(Player player)
    {
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

    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject())
        {
            GetKitchenObject().Destroy();

            KitchenObject.SpawnKitchenObject(cutKitchenObject, this);
        }
    }
}
