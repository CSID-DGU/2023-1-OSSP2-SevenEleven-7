using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnKey : MonoBehaviour
{
    public GameObject key; //��Ÿ�� Ű

    public List<GameObject> visit; //�湮�� �� ������ �迭

    public int visitnum; //�湮�ؾ��� ���� ��

    // Update is called once per frame
    void Update()
    {
        if (visit.Count == visitnum)
        {
            key.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}
