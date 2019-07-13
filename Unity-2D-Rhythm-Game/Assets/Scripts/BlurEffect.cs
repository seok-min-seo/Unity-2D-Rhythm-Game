using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlurEffect : MonoBehaviour
{
    public Image bgImage, blurImage;
    public float blurTime;

    public void StartBlurEffect()
    {
        blurImage.canvasRenderer.SetAlpha(0.0f);
        StartCoroutine(StartBlur());
    }


    IEnumerator StartBlur()
    {
        yield return new WaitForSeconds(0.4f);
        blurImage.CrossFadeAlpha(1.0f, blurTime, false);

   
    }
}
