using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnKey : MonoBehaviour
{
    public GameObject[] onObject; //��Ÿ�� Ű

    public int visit; //�湮�� �� ������ ����

    public int visitnum; //�湮�ؾ��� ���� ��

    // Update is called once per frame
    void Update()
    {
        if (visit == visitnum)
        {
            for (int i = 0; i < onObject.Length; i++)
            {
                onObject[i].SetActive(true);
            }
            this.gameObject.SetActive(false);
        }
    }
}
