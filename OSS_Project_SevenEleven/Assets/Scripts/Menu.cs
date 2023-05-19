using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{

    public static Menu instance;

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

    public GameObject go;
    public AudioManager theAudio;

    public string call_sound;
    public string cancel_sound;

    public OrderManager theOrder;

    private bool activated;               //�޴�â Ȱ��ȭ��Ȱ��ȭ ����


    public void Exit()          //����(���ø����̼�) ���Ḧ ���� �Լ�
    {
        Application.Quit();
    }



    public void Continue()   // �޴����� Resume��ư ������ �������� �簳
    {
        activated= false;
        go.SetActive(false);
        //theOrder.Move(); 
        theAudio.Play(cancel_sound);
    }




    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))  //ESC �Է��� ������ �޴� ȭ�� ON/OFF
        {
            activated = !activated;

            if (activated)
            {
                //theOrder.NotMove();       // �޴� ȭ���� ������ ĳ���͵��� ����
                go.SetActive(true);
                theAudio.Play(call_sound);
            }
            else
            {
                go.SetActive(false);
                theAudio.Play(cancel_sound);
                //theOrder.Move();          // ������ �簳�ϸ� �ٽ� ������
            }
        }

    }
}
