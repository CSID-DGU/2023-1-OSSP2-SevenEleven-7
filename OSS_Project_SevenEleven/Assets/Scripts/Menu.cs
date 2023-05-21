using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    public GameObject menu_obj;
    public AudioManager theAudio;

    public string call_sound;
    public string cancel_sound;

    public OrderManager theOrder;

    private bool activated;               //�޴�â Ȱ��ȭ��Ȱ��ȭ ����


    // �ٸ� ĵ���� UI ����� ����

    public GameObject savecanvas_obj;
    public GameObject inventory_canvas_obj;
    public GameObject load_canvas_obj;


    //

    public void Exit()          //����(���ø����̼�) ���Ḧ ���� �Լ�
    {
        Application.Quit();
    }



    public void Continue()   // �޴����� Resume��ư ������ �������� �簳
    {
        activated= false;
        menu_obj.SetActive(false);
        //theOrder.Move(); 
        theAudio.Play(cancel_sound);
    }


    public void Open_SaveCanvas()   // ���̺� ��ư������ savecanvas�� ���
    {
        theAudio.Play(call_sound);
        savecanvas_obj.SetActive(true);
    }

    public void Save_Canvas_Resume()   // ���̺� ���������� Resume��ư ������ �޴� ��������
    {
        savecanvas_obj.SetActive(false);
        theAudio.Play(cancel_sound);
    }

    public void Open_Inventory_Canvas()   // �κ��丮 ��ư������ �κ��丮canvas ���
    {
        menu_obj.SetActive(false);
        theAudio.Play(call_sound);
        inventory_canvas_obj.SetActive(true);
        
    }

    public void Inventory_Canvas_Resume()   // Inventory ���������� Resume��ư ������ �޴� ��������
    {
        inventory_canvas_obj.SetActive(false);
        theAudio.Play(cancel_sound);
    }

    public void Open_load_Canvas()   // �ε� ��ư������ �̾��ϱ� ������ ���
    {
        theAudio.Play(call_sound);
        load_canvas_obj.SetActive(true);
    }

    public void Load_Canvas_Resume()   // Load ���������� Resume��ư ������ �޴� ��������
    {
        load_canvas_obj.SetActive(false);
        theAudio.Play(cancel_sound);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))  //ESC �Է��� ������ �޴� ȭ�� ON/OFF
        {
            if(savecanvas_obj.activeSelf==false && inventory_canvas_obj.activeSelf == false
                && load_canvas_obj.activeSelf == false)        //  �޴� ĵ���� �� ������������
            {
                activated = !activated;

                if (activated)                  // activated������ 1�̸� �޴�â����
                {
                    //theOrder.NotMove();       // �޴� ȭ���� ������ ĳ���͵��� ����
                    menu_obj.SetActive(true);
                    theAudio.Play(call_sound);
                }
                else
                {
                    menu_obj.SetActive(false);
                    theAudio.Play(cancel_sound);
                    //theOrder.Move();          // ������ �簳�ϸ� �ٽ� ������
                }
            }
            else if(savecanvas_obj.activeSelf == true)   //�޴� ĵ�������� ���̺� ĵ������ ������� 
            {
                savecanvas_obj.SetActive(false);
                theAudio.Play(cancel_sound);
            }
            else if (inventory_canvas_obj.activeSelf == true)   //�޴� ĵ�������� �κ��丮 ĵ������ ������� 
            {
                inventory_canvas_obj.SetActive(false);
                theAudio.Play(cancel_sound);
            }
            else if (load_canvas_obj.activeSelf == true)   //�޴� ĵ�������� load ĵ������ ������� 
            {
                load_canvas_obj.SetActive(false);
                theAudio.Play(cancel_sound);
            }

        }

    }
}
