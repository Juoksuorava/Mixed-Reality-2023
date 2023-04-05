using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI buttonText;
    public Slider timeSlider;

    public void ChangeColorDelay()
    {
        RotateSelf.wait = !RotateSelf.wait;
        if (RotateSelf.wait)
        {
            buttonText.text = "Delay of " + RotateSelf.waitTime + " seconds";
        }
        else
        {
            buttonText.text = "No delay";
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        buttonText.text = "No delay";
        timeSlider.value = 1f;
        ChangeDelayTime();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeDelayTime()
    {
        RotateSelf.waitTime = timeSlider.value;
        if (RotateSelf.wait)
        {
            buttonText.text = "Delay of " + RotateSelf.waitTime + " seconds";
        }
    }
}
