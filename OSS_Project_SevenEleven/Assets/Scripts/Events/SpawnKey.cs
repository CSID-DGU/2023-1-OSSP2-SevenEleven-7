using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnKey : MonoBehaviour
{
    public GameObject[] onObject; //��Ÿ�� Ű

    public List<GameObject> visit; //�湮�� �� ������ �迭

    public int visitnum; //�湮�ؾ��� ���� ��

    // Update is called once per frame
    void Update()
    {
        if (visit.Count == visitnum)
        {
            for (int i = 0; i < onObject.Length; i++)
            {
                onObject[i].SetActive(true);
            }
            this.gameObject.SetActive(false);
        }
    }
}
