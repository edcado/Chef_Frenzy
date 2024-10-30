using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] Image progressBarImage;
    [SerializeField] CuttingCounter cuttingObject;

    // Start is called before the first frame update
    void Start()
    {
        cuttingObject.OnProgressChanged += CuttingObject_OnProgressChanged;

        progressBarImage.fillAmount = 0;

        Hide();
    }

    private void CuttingObject_OnProgressChanged(object sender, CuttingCounter.OnProgressChangedEventsArgs e)
    {
        progressBarImage.fillAmount = e.progressNormalized;

        if (e.progressNormalized == 0 || e.progressNormalized == 1)
        {
            Hide();
        }

        else
        {
            Show();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
