using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkVisit : MonoBehaviour
{
    public GameObject[] barricade; // �� ��

    public List<GameObject> visit; //�湮�� �� ������ �迭

    public int visitnum; //�湮�ؾ��� ���� ��

    // Update is called once per frame
    void Update()
    {
        if(visit.Count == visitnum)
        {
            for (int i = 0; i < barricade.Length; i++)
            {
                barricade[i].SetActive(false);
            }
        }
    }
}
