using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class SongSelectManager : MonoBehaviour
{
    public Image musicImageUI;
    public Text musicTitleUI;
    public Text bpmUI;

    private int musicIndex;
    private int musicCount = 4;

    private void UpdateSong(int musicIndex)
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.Stop();
        //리소스에서 비트(Beat) 텍스트 파일을 불러옵니다.
        TextAsset textAsset = Resources.Load<TextAsset>("Beats/" + musicIndex.ToString());
        StringReader stringReader = new StringReader(textAsset.text);
        //첫번째 줄에 적힌 곡 이름을 읽어서 UI를 업데이트 합니다
        musicTitleUI.text = stringReader.ReadLine();
        //두번째 줄은 읽기만 하고 아무 처리도 하지않습니다.
        stringReader.ReadLine();
        //세번째 줄에 적힌 BPM을 읽어서 UI를 업데이트합니다
        bpmUI.text = stringReader.ReadLine().Split(' ')[0];
        //리소스에서 비트 (Beat) 음악 파일을 불러와 재생합니다.
        AudioClip audioClip = Resources.Load<AudioClip>("Beats/" + musicIndex.ToString());
        audioSource.clip = audioClip;
        audioSource.Play();
        //리소스에서 비트(Beat)이미지 파일을 불러옵니다.
        musicImageUI.sprite = Resources.Load<Sprite>("Beats/" + musicIndex.ToString());

    }

    public void Right()
    {
        musicIndex = musicIndex + 1;
        if (musicIndex > musicCount) musicIndex = 1;
        UpdateSong(musicIndex);
    }

    public void Left()
    {
        musicIndex = musicIndex - 1;
        if (musicIndex < 1) musicIndex = musicCount;
        UpdateSong(musicIndex);
    }


    // Start is called before the first frame update
    void Start()
    {
        musicIndex = 1;
        UpdateSong(musicIndex);
    }

    public void GameStart()
    {
        PlayerInformation.selectedMusic = musicIndex.ToString();
        SceneManager.LoadScene("GameScene");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
