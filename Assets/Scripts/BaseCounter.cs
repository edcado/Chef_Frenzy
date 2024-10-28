using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObject
{
    [SerializeField] Transform spawnPoint;
    private KitchenObject kitchenObject;


    private void Start()
    {
        
        spawnPoint.position = new Vector3(spawnPoint.position.x, spawnPoint.position.y, spawnPoint.position.z - 0.920f);
    }
    public virtual void Interact(Player player)
    {

    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return spawnPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
