using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkVisit : MonoBehaviour
{
    static public checkVisit instance; //static���� ����� ������ ���� ����
    public GameObject[] barricade; // �� ��

    public List<GameObject> visit; //�湮�� �� ������ �迭

    public int visitnum; //�湮�ؾ��� ���� ��

    public int confirmvisitnum = 0;

    public bool isSound = false;

    public string openSound;

    private AudioManager theAudio;

    BGMManager bgm;

    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    private void Start()
    {
        theAudio = FindObjectOfType<AudioManager>();
        bgm = FindObjectOfType<BGMManager>();   
    }

    // Update is called once per frame
    void Update()
    {
        if(confirmvisitnum == visitnum)
        {
            if (isSound)
            {
                theAudio.Play(openSound);
                bgm.Stop();
            }
            for (int i = 0; i < barricade.Length; i++)
            {
                barricade[i].SetActive(false);
            }
            this.gameObject.SetActive(false);
        }
    }
}
