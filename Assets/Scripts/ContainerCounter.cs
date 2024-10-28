using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    [SerializeField] KitchenObjectSO kitchenObjectSO;
    

    public override void Interact(Player player)
    {      
          Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.Prefab);
          kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(player);
    }

    
}
