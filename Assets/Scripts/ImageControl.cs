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
    [SerializeField] private BaseCounter baseCounter;

    private void Start()
    {
        HideImage1();
        HideImage2();
        player.OnSelectedCounterChange += Player_OnSelectedCounterChange;
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

            if (e.selectedCounter as ClearCounter)

            {
                ShowImage1();
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
