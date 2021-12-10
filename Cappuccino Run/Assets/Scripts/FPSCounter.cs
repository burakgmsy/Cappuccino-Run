using TMPro;
using UnityEngine;
public class FPSCounter : MonoBehaviour
{
    private int frameCounter = 0;
    private float timeCounter = 0.0f;
    private float refreshTime = 0.1f;
    [SerializeField]
    private TMP_Text framerateText;

    void Update()
    {
        if (timeCounter < refreshTime)
        {
            timeCounter += Time.deltaTime;
            frameCounter++;
        }
        else
        {
            float lastFramerate = frameCounter / timeCounter;
            frameCounter = 0;
            timeCounter = 0.0f;
            framerateText.text = "FPS: " + lastFramerate.ToString("n2");
        }
    }
}
