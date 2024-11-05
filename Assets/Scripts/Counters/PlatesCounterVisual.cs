using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
    [SerializeField] private PlatesCounter platesCounter;
    [SerializeField] private Transform visualSpawnPrefab;
    [SerializeField] private Transform counterTopPoint;

    private void Start()
    {
        platesCounter.OnPlateSpawned += PlatesCounter_OnPlateSpawned;
    }

    private void PlatesCounter_OnPlateSpawned(object sender, System.EventArgs e)
    {
       Transform plateVisualTransform = Instantiate(visualSpawnPrefab, counterTopPoint);
    }
}
