using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance {get; private set; }
    [SerializeField] private AudioClipSO audioClipSO;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        DelyveryManager.Instance.OnRecipeFail += DeliveryManager_OnRecipeFail;
        DelyveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        Player.Instance.OnPickUpSomething += Player_OnPickUpSomething;
        BaseCounter.OnDropSomething += BaseCounter_OnDropSomething;
        TrashCounter.OnAnyObjectTrash += TrashCounter_OnAnyObjectTrash;
        PlateKitchenObject.OnIngredientAdd += PlateKitchenObject_OnIngredientAdd;
    }

    private void PlateKitchenObject_OnIngredientAdd(object sender, System.EventArgs e)
    {
        PlaySound(audioClipSO.dropObject, Player.Instance.transform.position);
    }

    private void TrashCounter_OnAnyObjectTrash(object sender, System.EventArgs e)
    {
        TrashCounter trashCounter = sender as TrashCounter;
        PlaySound(audioClipSO.trash, trashCounter.transform.position);
    }

    private void BaseCounter_OnDropSomething(object sender, System.EventArgs e)
    {
        BaseCounter baseCounter = sender as BaseCounter;
        PlaySound(audioClipSO.dropObject, baseCounter.transform.position);
    }

    private void Player_OnPickUpSomething(object sender, System.EventArgs e)
    {
        PlaySound(audioClipSO.pickUpObject, Player.Instance.transform.position);    
    }

    private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e)
    {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySound(audioClipSO.chop, cuttingCounter.transform.position);
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, System.EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipSO.deliverySuccess, deliveryCounter.transform.position);
    }

    private void DeliveryManager_OnRecipeFail(object sender, System.EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipSO.deliveryFail, deliveryCounter.transform.position);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 100f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }

    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 100f)
    {
        PlaySound(audioClipArray[Random.Range(0, audioClipArray.Length)], position, volume);
    }

    public void PlayFootStepsSound(Vector3 position, float volume)
    {
        PlaySound(audioClipSO.footsteps, position, volume); 
    }
}