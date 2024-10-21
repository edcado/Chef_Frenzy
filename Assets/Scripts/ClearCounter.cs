using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] KitchenObjectSO kitchenObjectSO;
    [SerializeField] Transform spawnPoint;

    private KitchenObject kitchenObject;
    private bool hasSpawned = false;
    public void Interact()
    {
        if (kitchenObject == null)
        {
            if (!hasSpawned)
            {
                Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.Prefab, spawnPoint);
                kitchenObjectTransform.localPosition = Vector3.zero;

                kitchenObjectTransform.GetComponent<KitchenObject>();
                if (kitchenObject != null)
                {
                    kitchenObject.SetClearCounter(this);
                }

                else
                {
                    Debug.Log("Falta Prefab");
                }

                hasSpawned = true;
            }
            
           
        }

        else
        {
            Debug.Log(kitchenObject.GetClearCounter());
        }
        

        
    }
}
