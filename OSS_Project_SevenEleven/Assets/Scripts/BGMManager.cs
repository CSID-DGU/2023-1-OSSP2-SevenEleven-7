using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
<<<<<<< HEAD
    static public BGMManager instance; //싱글톤

    public AudioClip[] clips; // 배경음악

    private AudioSource source;

    private void Awake() //Start보다 먼저 실행
    {
        //Scene 전환시 객체 파괴 방지 코드
=======

    //*사용방법*
    //collision등을 통해 각각의 함수를 사용하여 음악을 관리

    static public BGMManager instance;

    //Variables 

    //Public
    public AudioClip[] clips; //배경음악들

    //Private
    private AudioSource source;

    private WaitForSeconds waitTime = new WaitForSeconds(0.01f); //객체 한번만 생성

    private void Awake() //Start보다 먼저 실행
    {
        //Scene 전환시 객체 파괴 방지 코드
        //전활할 씬의 카메라 오브젝트는 삭제 할 것!
>>>>>>> 3dbd9091b9158362f6b70101383ee0e9539f4237

        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
<<<<<<< HEAD
        else
=======
        else if (instance != null)
>>>>>>> 3dbd9091b9158362f6b70101383ee0e9539f4237
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

<<<<<<< HEAD
=======

    //Method
    public void Play(int _playMusicTrack)
    {
        source.volume = 1f;
        source.clip = clips[_playMusicTrack];
        source.Play();
    }

    public void SetVolumn(float _volumn)
    {
        source.volume = _volumn;
    }

    public void Pause()
    {
        source.Pause();
    }

    public void Unpause()
    {
        source.UnPause();
    }

    public void Stop()
    {
        source.Stop();
    }

    public void FadeOutMusic()
    {
        StopAllCoroutines();
        StartCoroutine(FadeOutMusicCoroutine());
    }

    IEnumerator FadeOutMusicCoroutine()
    {
        for (float i = 1.0f; i>=0f; i-=0.01f)
        {
            source.volume = i;
            yield return waitTime;
        }
    }

    public void FadeInMusic()
    {
        StopAllCoroutines();
        StartCoroutine(FadeInMusicCoroutine());
    }

    IEnumerator FadeInMusicCoroutine()
    {
        for (float i = 0f; i <= 1f; i += 0.01f)
        {
            source.volume = i;
            yield return waitTime;
        }
    }
>>>>>>> 3dbd9091b9158362f6b70101383ee0e9539f4237
}
