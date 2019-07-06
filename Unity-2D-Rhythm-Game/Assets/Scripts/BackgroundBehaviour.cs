using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundBehaviour : MonoBehaviour
{

    public GameObject gameBackground;
    private SpriteRenderer gameBackgroundSpriteRenderer;


    // Start is called before the first frame update
    void Start()
    {
        gameBackgroundSpriteRenderer = gameBackground.GetComponent<SpriteRenderer>();
        StartCoroutine(FadeOut(gameBackgroundSpriteRenderer, 0.005f));
    }

    IEnumerator FadeOut (SpriteRenderer spriteRenderer, float amount)
    {
        Color color = spriteRenderer.color;
        while(color.a > 0.0f) // 현재 알파값(불투명도)값이 존재한다면 어마운트만큼 알파캆을 감소시킴
        {
            color.a -= amount;
            spriteRenderer.color = color;
            yield return new WaitForSeconds(amount);
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
