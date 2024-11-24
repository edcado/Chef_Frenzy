using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private StoveCounter stoveCounter;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
    }

    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnchangeStateEventArgs e)
    {
        bool audioBool = e.state == StoveCounter.State.frying || e.state == StoveCounter.State.fried;
        if (audioBool)
        {
            audioSource.Play();
        }

        else
        {
            audioSource.Pause();
        }
    }
}
