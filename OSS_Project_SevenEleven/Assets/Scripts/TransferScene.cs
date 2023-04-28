using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Scene ������ ���� ���̺귯��

public class TransferScene : MonoBehaviour
{

    //Build Settings���� ����ϱ� ���� Scene Load �� ��!!


    //Variables

    //Public 
    public string transferMapName;  //�̵��� ���� �̸�

    //Private
    private MovingObject thePlayer;

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<MovingObject>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            thePlayer.currentMapName = transferMapName; // ���� �̵� ������ �ε����ٸ� �̵��� ���� �̸��� Player������Ʈ�� �Ѱ���
            SceneManager.LoadScene(transferMapName); // transferMapName���� �̵�
        }
    }
}
