using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPS : MonoBehaviour
{
    [SerializeField] private Text FPStext;
    [SerializeField] private float delayTime;
    private int fpsNumber = 0;


    private void Start()
    {
        StartCoroutine(ShowFPS(delayTime));
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        fpsNumber = (int)(1f / Time.unscaledDeltaTime);
    }

    public IEnumerator ShowFPS(float delayTime)
    {
        label:;
        yield return new WaitForSeconds(delayTime);
        FPStext.text = fpsNumber.ToString();
        goto label;
    }
}
