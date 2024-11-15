using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeliveryManagerSingleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipeTextName;

    public void SetRecipeName(RecipeSO recipeSO)
    {
        recipeTextName.text = recipeSO.recipeName;  
    }
}
