using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSingle : MonoBehaviour
{
    static public NPCSingle instance; //static���� ����� ������ ���� ����
    // Start is called before the first frame update
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
}
