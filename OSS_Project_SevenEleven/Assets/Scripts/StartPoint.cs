using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{

    //Variables 

    //Public
    public string startPoint; // ���� �̵��Ǹ� �÷��̾ ������ ��ġ

    //Private
    private MovingObject thePlayer;
    private CameraManager theCamera; //ī�޶� �̵��� ���� ����

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<MovingObject>(); //���̶�Ű�� �ִ� ��� MovingObject ����
        theCamera = FindObjectOfType<CameraManager>(); //���̶�Ű�� �ִ� ��� CameraManager ����

        if (startPoint == thePlayer.currentMapName)
        {
            thePlayer.transform.position = this.transform.position; //�÷��̾� ��ġ ����
            theCamera.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, theCamera.transform.position.z);   
        }


    }
}