using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System;

public class GameResultManager : MonoBehaviour
{

    public Text musicTitleUI;
    public Text scoreUI;
    public Text maxComboUI;
    public Image RankUI;

    // Start is called before the first frame update
    void Start()
    {
        musicTitleUI.text = PlayerInformation.musicTitle;
        scoreUI.text = "점수: " + (int)PlayerInformation.score;
        //숫자형태를 문자열로 바꾸기 위해선 앞에 ""를 더해주면된다
        maxComboUI.text = "최대 콤보: " + PlayerInformation.maxCombo;
        //리소스에서 비트(Beat) 텍스트 파일을 불러옵니다.
        TextAsset textAsset = Resources.Load<TextAsset>("Beats/" + PlayerInformation.selectedMusic);
        StringReader reader = new StringReader(textAsset.text);
        //첫번쨰 줄과 두번쨰 줄을 무시합니다.
        reader.ReadLine();
        reader.ReadLine();
        // 세번쨰 줄에 적힌 비트 정보(S랭크 점수 , A랭크 점수 , B랭크 점수)를 읽습니다.
        string beatInformation = reader.ReadLine();
        int scoreS = Convert.ToInt32(beatInformation.Split(' ')[3]);
        int scoreA = Convert.ToInt32(beatInformation.Split(' ')[4]);
        int scoreB = Convert.ToInt32(beatInformation.Split(' ')[5]);
        //성적에 맞는 랭크 이미지를 불러옵니다.
        if(PlayerInformation.score >= scoreS)
        {
            RankUI.sprite = Resources.Load<Sprite>("Sprites/Rank S");

        }
        else if (PlayerInformation.score >= scoreA)
        {
            RankUI.sprite = Resources.Load<Sprite>("Sprites/Rank A");

        }
        else if (PlayerInformation.score >= scoreB)
        {
            RankUI.sprite = Resources.Load<Sprite>("Sprites/Rank B");

        }
        else
        {
            RankUI.sprite = Resources.Load<Sprite>("Sprites/Rank C");
        }


    }

    public void Replay()
    {
        SceneManager.LoadScene("SongSelectScene");
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
