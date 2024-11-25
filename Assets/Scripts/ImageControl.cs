using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageControl : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Image image;
    [SerializeField] private Image image2;
    [SerializeField] private PlatesCounter platesCounter;
    [SerializeField] private StoveCounter stoveCounter;

    private void Start()
    {
        HideImage1();
        HideImage2();
        player.OnSelectedCounterChange += Player_OnSelectedCounterChange;
        player.OnNotCounter += Player_OnNotCounter;
        platesCounter.OnPlateSpawned += PlatesCounter_OnPlateSpawned;
        stoveCounter.Onfrying += StoveCounter_Onfrying;
        stoveCounter.OnFried += StoveCounter_OnFried;
        stoveCounter.OnBurned += StoveCounter_OnBurned;
    }

    private void StoveCounter_OnBurned(object sender, EventArgs e)
    {
        ShowImage1();
    }

    private void StoveCounter_OnFried(object sender, EventArgs e)
    {
        ShowImage1();
    }

    private void StoveCounter_Onfrying(object sender, EventArgs e)
    {
        ShowImage1();
    }

    private void PlatesCounter_OnPlateSpawned(object sender, EventArgs e)
    {
        ShowImage1();
    }

    private void Player_OnNotCounter(object sender, EventArgs e)
    {
        HideImage1();
        HideImage2();
    }

    private void Player_OnSelectedCounterChange(object sender, Player.OnSelectedCounterChangeEventArgs e)
    {
        if (e.selectedCounter != null)
        {
            if (e.selectedCounter is CuttingCounter cuttingCounter)
            {
                cuttingCounter.OnCanCut += CuttingCounter_OnCanCut;
                cuttingCounter.OnCutFinished += CuttingCounter_OnCutFinished;

                if (player.HasKitchenObject())
                {
                    ShowImage1();
                }
                else
                {
                    HideImage1();
                }

                if (cuttingCounter.HasKitchenObject() && cuttingCounter.HasRecipeWithInput(cuttingCounter.GetKitchenObject().GetKitchenObjectSO()))
                {
                    Debug.Log("CanCut");
                }
            }


            if (e.selectedCounter is ContainerCounter containerCounter)
            {
                if (!containerCounter.HasKitchenObject())
                {
                    ShowImage1();
                }
                containerCounter.OnObjectPicked += ContainerCounter_OnObjectPicked;
            }

            if (e.selectedCounter is ClearCounter clearCounter)

            {
                if (!clearCounter.HasKitchenObject())
                {
                    if (player.HasKitchenObject())
                    {
                        ShowImage1();
                    }
                }

                if (clearCounter.HasKitchenObject())
                {
                    if (!player.HasKitchenObject())
                    {
                        ShowImage1();
                    }
                }
                
            }

            if (e.selectedCounter is PlatesCounter platesCounter)
            {
                if (!player.HasKitchenObject() && platesCounter.spawnPlatesAmount >= 1)
                ShowImage1();
            }

            if (e.selectedCounter is StoveCounter stoveCounter)
            {
                if (player.HasKitchenObject())
                {
                    KitchenObjectSO playerKitchenObjectSO = player.GetKitchenObject().GetKitchenObjectSO();

                    bool hasValidRecipe = stoveCounter.HasRecipeWithInput(playerKitchenObjectSO);

                    if (hasValidRecipe)
                    {
                        ShowImage1();
                    }
                    
                }

                if (!player.HasKitchenObject())
                {
                    ShowImage1();
                }

                if (!stoveCounter.HasKitchenObject() && !player.HasKitchenObject())
                {
                    HideImage1();
                }

                else
                {
                    Debug.Log("El jugador no tiene ningún objeto.");
                }
            }

            if (e.selectedCounter is DeliveryCounter deliveryCounter)
            {
                if (player.HasKitchenObject() && player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    ShowImage1();
                }
                else
                {
                    HideImage1();
                }
            }
        }

    }



    private void ContainerCounter_OnObjectPicked(object sender, EventArgs e)
    {
        HideImage1();
    }

    private void CuttingCounter_OnCutFinished(object sender, EventArgs e)
    {
        HideImage2();
    }

    private void CuttingCounter_OnCanCut(object sender, System.EventArgs e)
    {
        CuttingCounter cuttingCounter = sender as CuttingCounter;

        if (cuttingCounter.HasKitchenObject() && cuttingCounter.HasRecipeWithInput(cuttingCounter.GetKitchenObject().GetKitchenObjectSO()))
        {
            ShowImage2();
        }
        else
        {
            HideImage2();
        }
    }

    private void HideImage1()
    {
        image.gameObject.SetActive(false);  
    }

    private void ShowImage1()
    {
        image.gameObject.SetActive(true);
    }

    private void HideImage2()
    {
        image2.gameObject.SetActive(false);
    }
    private void ShowImage2()
    {
        image2.gameObject.SetActive(true);
    }
}
