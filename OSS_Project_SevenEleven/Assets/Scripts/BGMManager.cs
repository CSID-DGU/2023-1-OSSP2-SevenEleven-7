using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    static public BGMManager instance; //�̱���

    public AudioClip[] clips; // �������

    private AudioSource source;

    private void Awake() //Start���� ���� ����
    {
        //Scene ��ȯ�� ��ü �ı� ���� �ڵ�

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

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

}
