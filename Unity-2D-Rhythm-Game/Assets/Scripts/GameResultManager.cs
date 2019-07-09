using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameResultManager : MonoBehaviour
{

    public Text musicTitleUI;
    public Text scoreUI;
    public Text maxComboUI;

    // Start is called before the first frame update
    void Start()
    {
        musicTitleUI.text = PlayerInformation.musicTitle;
        scoreUI.text = "" + PlayerInformation.score;
        //숫자형태를 문자열로 바꾸기 위해선 앞에 ""를 더해주면된다
        maxComboUI.text = "" + PlayerInformation.maxCombo;

        Debug.Log(PlayerInformation.maxCombo);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
