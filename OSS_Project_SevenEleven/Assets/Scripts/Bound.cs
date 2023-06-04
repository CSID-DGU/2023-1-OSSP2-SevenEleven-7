using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bound : MonoBehaviour
{
    // �� �̵��� �ٿ�带 �Ѱ��ֱ� ���� ��ũ��Ʈ

    //Variables

    //Private
    private BoxCollider2D bound;
    public string boundName; // ī�޶��� �ٿ��� �������ֱ� ���� ����.
    private CameraManager theCamera;

    // Start is called before the first frame update
    void Start()
    {
        bound = GetComponent<BoxCollider2D>();
        theCamera = FindObjectOfType<CameraManager>();
        theCamera.SetBound(bound);

        GameManager theGM = FindObjectOfType<GameManager>();
        theGM.LoadStart();
    }

    public void SetBound() // �ҷ����⿡�� ���Ǵ� �Լ�
    {
        if(theCamera != null)
        {
            theCamera.SetBound(bound);
        }
    }
}
