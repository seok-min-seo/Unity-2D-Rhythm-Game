using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //싱글톤 형태로 처리

    public static GameManager instance { get; set; }
    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
    }

    public float notespeed;

    public GameObject scoreUI;
    private float score;
    private Text scoreText; //스코어 텍스트를 화면ㅇ ㅔ보여주기

    public GameObject comboUI; //내부적으로 콤보를 처리해주기
    private int combo;
    private Text comboText; //콤보텍스트
    private Animator comboAnimator;  // 콤보가 추가 될때마다 애니메이터를 보여줘야하기떄문

    public enum judges {  NONE = 0, BAD, GOOD, PERFECT, MISS };
    public GameObject judgeUI;
    private Sprite[] judgeSprites;  //Resources 폴더에서 불러와서 가지고있다가
    private Image judgementSpriteRenderer; //이미지 렌더러를 이용해서 불러옴
    private Animator judgementSpriteAnimator; 
    


    public GameObject[] trails;
    private SpriteRenderer[] trailSpriteRenderers;

    //음악 변수
    private AudioSource audioSource;
    private string music = "Drops of H20";

    //음악을 실행하는 함수입니다.
    void MusicStart()
    {
        //리소스에서 비트(Beat) 음악 파일을 불러와 재생합니다.
        AudioClip audioClip = Resources.Load<AudioClip>("Beats/" + music);
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.Play();
    }


    // Start is called before the first frame update
    void Start()
    {
        Invoke("MusicStart", 2);
        judgementSpriteRenderer = judgeUI.GetComponent<Image>();
        judgementSpriteAnimator = judgeUI.GetComponent<Animator>();
        scoreText = scoreUI.GetComponent<Text>();
        comboText = comboUI.GetComponent<Text>();
        comboAnimator = comboUI.GetComponent<Animator>();

        //판정 결과를 보여주는 스프라이트 이미지를 미리 초기화합니다.
        judgeSprites = new Sprite[4];
        judgeSprites[0] = Resources.Load<Sprite>("Sprites/Bad");
        judgeSprites[1] = Resources.Load<Sprite>("Sprites/Good");
        judgeSprites[2] = Resources.Load<Sprite>("Sprites/Miss");
        judgeSprites[3] = Resources.Load<Sprite>("Sprites/Perfect");
        //Load함수를 쓰면 리솔시스에 있는 폴더를 사용할수가있다
        //역시 비쥬얼씨



        trailSpriteRenderers = new SpriteRenderer[trails.Length];
        for(int i = 0; i < trails.Length; i++)
        {
            trailSpriteRenderers[i] = trails[i].GetComponent<SpriteRenderer>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //사용자가 입력한 키에 해당하는 라인을 빛나게 처리합니다.
        if (Input.GetKey(KeyCode.D)) ShineTrail(0);
        if (Input.GetKey(KeyCode.F)) ShineTrail(1);
        if (Input.GetKey(KeyCode.J)) ShineTrail(2);
        if (Input.GetKey(KeyCode.K)) ShineTrail(3);
        //한번 빛나게 된 라인은 반복적으로 다시 어둡게 처리합니다.
        for(int i = 0; i < trailSpriteRenderers.Length; i++)
        {
            Color color = trailSpriteRenderers[i].color;
            color.a -= 0.01f;
            trailSpriteRenderers[i].color = color;

        }
    }

    public void ShineTrail(int index)
    {
        Color color = trailSpriteRenderers[index].color;
        color.a = 0.32f;
        trailSpriteRenderers[index].color = color;
    }

    //노트 판정 이후에 판정 결과를 화면에 보여줍니다.
    void showJudgement()
    {
        //점수 이미지를 보여줍니다.
        string scoreFormat = "000000";
        scoreText.text = score.ToString(scoreFormat);
        //판정 이미지를 보여줍니다.
        judgementSpriteAnimator.SetTrigger("Show");
       
       
        //콤보가 2 이상일 때만 콤보 이미지를 보여줍니다.
        if(combo >= 2)
        {
            comboText.text = "COMBO " + combo.ToString();
            comboAnimator.SetTrigger("Show");
        }
    }

    //노트 판정을 진행합니다

    public void processJudge(judges judge, int noteType)
    {
        if (judge == judges.NONE) return;
        //MISS판정을 받은 경우 콤보를 종료하고, 점수를 많이 깎습니다.
        if(judge == judges.MISS)
        {
            judgementSpriteRenderer.sprite = judgeSprites[2];
            
            combo = 0;
            if (score >= 15) score -= 15;
            if (score <= 0) score = 0;
        }
        //BAD판정을 받은경우 아무 일도 일어나지 않습니다.
        else if (judge == judges.BAD)
        {
            judgementSpriteRenderer.sprite = judgeSprites[0];
            combo = 0;
        }

        //퍼펙트 혹은 GOOD 판정을 받은 경우 콤보 및 점수를 올립니다.
        else
        {
            if(judge == judges.PERFECT)
            {
                judgementSpriteRenderer.sprite = judgeSprites[3];
                score += 20;
            }
            else if(judge == judges.GOOD)
            {
                judgementSpriteRenderer.sprite = judgeSprites[1];
                score += 15;
            }
            combo += 1;
            score += (float)combo * 0.1f;
        }
        showJudgement();
       
    }
}
