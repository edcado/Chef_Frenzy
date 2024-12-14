using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class KitchenObject : NetworkBehaviour
{
    [SerializeField] public KitchenObjectSO kitchenObjectSO;

    private IKitchenObject KitchenObjectParent;

    private FollowTransform followTransform;

    private void Awake()
    {
        followTransform = GetComponent<FollowTransform>();
    }

    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kitchenObjectSO;
    }

    public void SetKitchenObjectParent(IKitchenObject kitchenObjectParent)
    {
        SetKitchenObjectServerRpc(kitchenObjectParent.GetNetworkObject());
    }


    [ServerRpc (RequireOwnership = false)]
    private void SetKitchenObjectServerRpc(NetworkObjectReference kitchenObjectParentNetworkObjectReference)
    {
        SetKitchenObjectClientRpc(kitchenObjectParentNetworkObjectReference);
    }


    [ClientRpc]
    private void SetKitchenObjectClientRpc(NetworkObjectReference kitchenObjectParentNetworkObjectReference)
    {
        kitchenObjectParentNetworkObjectReference.TryGet(out NetworkObject kitchenObjectParentNetworkObject);
        IKitchenObject kitchenObjectParent = kitchenObjectParentNetworkObject.GetComponent<IKitchenObject>();

        if (this.KitchenObjectParent != null)
        {
            this.KitchenObjectParent.ClearKitchenObject();
        }

        this.KitchenObjectParent = kitchenObjectParent;

        if (kitchenObjectParent.HasKitchenObject())
        {
            Debug.Log("counter has a counter already");
        }

        kitchenObjectParent.SetKitchenObject(this);


        followTransform.SetTargetTransform(kitchenObjectParent.GetKitchenObjectFollowTransform());
    }



    public IKitchenObject GetKitchenObjectParent()
    {
        return KitchenObjectParent;
    }

    public void Destroy()
    {
        KitchenObjectParent.ClearKitchenObject();
        Destroy(gameObject);
    }

    public static void SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObject kitchenObjectParent)
    {
        KitchenGameMultiplayer.Instance.SpawnKitchenObject(kitchenObjectSO, kitchenObjectParent);
    }

    public bool TryGetPlate(out PlateKitchenObject plateKitchenObject)
    {
        if (this is PlateKitchenObject)
        {
            plateKitchenObject = this as PlateKitchenObject;
            return true;
        }

        else
        {
            plateKitchenObject = null;
            return false;
        }
    }
}
