using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CuttingCounter;

public class StoveCounter : BaseCounter
{
    public event EventHandler <OnchangeStateEventArgs> OnStateChanged; 
    public class OnchangeStateEventArgs : EventArgs
    {
        public State state;
    }

    [SerializeField] private FryingObjectSO[] fryingRecipeSOArray;
    [SerializeField] private BurningRecipeSO[] burnedRecipeSOArray;


    public enum State { idle, frying, fried, burned}

    public float fryingTimer;
    public float burnedTimer;
    private FryingObjectSO fryingObjectSO;
    private BurningRecipeSO burningRecipeSO;
    private State state;

    private void Start()
    {
        state = State.idle;
    }

    private void Update()
    {
        if (HasKitchenObject())
        {
            switch (state)
            {
                case State.idle:
                    break;

                case State.frying:
                    fryingTimer += Time.deltaTime;

                    if (fryingTimer > fryingObjectSO.fryTimerMax)
                    {                       
                        GetKitchenObject().Destroy();

                        KitchenObject.SpawnKitchenObject(fryingObjectSO.output, this);

                        Debug.Log("FRIED");
                        state = State.fried;
                        burnedTimer = 0;
                        burningRecipeSO = GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                        OnStateChanged?.Invoke(this, new OnchangeStateEventArgs
                        {
                            state = state
                        }); 
                    }
                    break;

                case State.fried:
                    burnedTimer += Time.deltaTime;

                    if (burnedTimer > burningRecipeSO.burnedTimerMax)
                    {
                        GetKitchenObject().Destroy();

                        KitchenObject.SpawnKitchenObject(burningRecipeSO.output, this);

                        state = State.burned;

                        OnStateChanged?.Invoke(this, new OnchangeStateEventArgs
                        {
                            state = state
                        });
                    }
                    break;

                case State.burned:
                    break;
            }

            Debug.Log(state);
        }
    }

    public override void Interact(Player player)
    {
        {
            {
                if (!HasKitchenObject())
                {
                    if (player.HasKitchenObject())
                    {
                        if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                        {
                            player.GetKitchenObject().SetKitchenObjectParent(this);

                            fryingObjectSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                            fryingTimer = 0f;
                            state = State.frying;

                            OnStateChanged?.Invoke(this, new OnchangeStateEventArgs
                            {
                                state = state
                            });
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
                        //Nothing happens
                    }

                    else
                    {
                        GetKitchenObject().SetKitchenObjectParent(player);

                        state = State.idle;

                        OnStateChanged?.Invoke(this, new OnchangeStateEventArgs
                        {
                            state = state
                        });
                    }
                }
            }
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
       FryingObjectSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        return fryingRecipeSO != null;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingObjectSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        if (fryingRecipeSO != null)
        {
            return fryingRecipeSO.output;
        }

        else
        {
            return null;
        }
    }

    private  FryingObjectSO GetFryingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {     
            foreach (FryingObjectSO fryingRecipeSO in fryingRecipeSOArray)
            {
                if (fryingRecipeSO.input == inputKitchenObjectSO)
                {
                    return fryingRecipeSO;
                }

            }
            return null;
    }

    private BurningRecipeSO GetBurningRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (BurningRecipeSO fryingRecipeSO in burnedRecipeSOArray)
        {
            if (fryingRecipeSO.input == inputKitchenObjectSO)
            {
                return fryingRecipeSO;
            }

        }
        return null;
    }
}
