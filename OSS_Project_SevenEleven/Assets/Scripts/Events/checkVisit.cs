using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkVisit : MonoBehaviour
{
    public GameObject[] barricade; // �� ��

    public List<GameObject> visit; //�湮�� �� ������ �迭

    public int visitnum; //�湮�ؾ��� ���� ��

    public bool isSound = false;

    public string openSound;

    private AudioManager theAudio;

    BGMManager bgm;

    private void Start()
    {
        theAudio = FindObjectOfType<AudioManager>();
        bgm = FindObjectOfType<BGMManager>();   
    }

    // Update is called once per frame
    void Update()
    {
        if(visit.Count == visitnum)
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
