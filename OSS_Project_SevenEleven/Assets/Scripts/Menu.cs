using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
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

    public void Exit()          //����(���ø����̼�) ���Ḧ ���� �Լ�
    {
        Application.Quit();
    }



    public void Continue()   // �޴����� Resume��ư ������ �������� �簳
    {
        activated= false;
        menu_obj.SetActive(false);
        theOrder.Move(); 
        theAudio.Play(cancel_sound);
    }


    public void Open_SaveCanvas()   // ���̺� ��ư������ savecanvas�� ���
    {
        theAudio.Play(call_sound);
        savecanvas_obj.SetActive(true);
        Slot_Refresh_BeforeOpenUI(1, "Save");
        Slot_Refresh_BeforeOpenUI(2, "Save");
        Slot_Refresh_BeforeOpenUI(3, "Save");

    }

    public void Save_Canvas_Resume()   // ���̺� ���������� Resume��ư ������ �޴� ��������
    {
        savecanvas_obj.SetActive(false);
        theAudio.Play(cancel_sound);
    }



    public class SaveData               //���̺� ���� UI�� json������
    {
        public string saved_map;
        public int playtime_minute;
        public int playtime_seconds;

    }



    public void SaveSlot_1_Modify()
    {
        SaveSlotModify(1);
    }
    public void SaveSlot_2_Modify()
    {
        SaveSlotModify(2);
    }
    public void SaveSlot_3_Modify()
    {
        SaveSlotModify(3);
    }

    public void SaveSlotModify(int i)     //���̺��ư1,2,3 �� ������ �� ���̺� ���Կ� ���� ĳ������ ��ġ , �÷��� Ÿ���� text�� ��ü
    {
        GameObject save_slot_1 = GameObject.Find("SaveSlot"+i);
        PlayerManager thePlayer = FindObjectOfType<PlayerManager>();

        double playtime = Math.Round(Time.time);
        int playtime_minute=(int)(playtime/60);
        int playtime_seconds = (int)(playtime % 60); //�÷���Ÿ�� �ʸ� ��,�ʷ� 

        save_slot_1.GetComponent<TextMeshProUGUI>().text = 
            "Saved Place : "+thePlayer.currentMapName +"\n Play Time : "+ playtime_minute+" Min " + playtime_seconds+" Sec";



        SaveData data = new SaveData();
        data.saved_map = thePlayer.currentMapName;
        data.playtime_minute = playtime_minute;
        data.playtime_seconds= playtime_seconds;
        string json=JsonUtility.ToJson(data);             // ���� �÷��̾��� ������ dataȭ                      

        string filename = "Save_UI_Slot" + i;   // Save_UI_Slot1,2,3  -> ���ϸ�
        string path = Application.dataPath + "/" + filename + ".Json";          //���̺� ������ jsonȭ ���Ѽ� ����
        File.WriteAllText(path, json);

    }


    public void Open_load_Canvas()   // �ε� ��ư������ �̾��ϱ� ������ ���
    {
        theAudio.Play(call_sound);
        load_canvas_obj.SetActive(true);
        Slot_Refresh_BeforeOpenUI(1, "Load");
        Slot_Refresh_BeforeOpenUI(2, "Load");
        Slot_Refresh_BeforeOpenUI(3, "Load");       //�̾��ϱ� ĵ������ ������ Refresh

    }

    public void Load_Canvas_Resume()   // Load ���������� Resume��ư ������ �޴� ��������
    {
        load_canvas_obj.SetActive(false);
        theAudio.Play(cancel_sound);
    }

    public void Slot_Refresh_BeforeOpenUI(int i,string slot_type)  // slot_type(in hieraarchy) -> Load or Save
    {
        GameObject slot = GameObject.Find(slot_type+"Slot" + i);
        string filename = "Save_UI_Slot" + i;   // Save_UI_Slot1  -> ���ϸ�
        string path = Application.dataPath + "/" + filename + ".Json";        //���̺� ������ path
        bool fileExists = File.Exists(path);                       
        if (fileExists)
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);           //���̺� ������ �����Ѵٸ� Json���� ������ ���Կ� ���ش�
            slot.GetComponent<TextMeshProUGUI>().text =
                 "Saved Place : " + data.saved_map + "\n Play Time : " + data.playtime_minute + " Min " + data.playtime_seconds + " Sec";
        }

    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))  //ESC �Է��� ������ �޴� ȭ�� ON/OFF
        {
            if(savecanvas_obj.activeSelf==false && load_canvas_obj.activeSelf == false)        //  �޴� ĵ���� �� ������������
            {
                activated = !activated;

                if (activated)                  // activated������ 1�̸� �޴�â����
                {
                    theOrder.NotMove();       // �޴� ȭ���� ������ ĳ���͵��� ����
                    menu_obj.SetActive(true);
                    theAudio.Play(call_sound);
                }
                else
                {
                    menu_obj.SetActive(false);
                    theAudio.Play(cancel_sound);
                    theOrder.Move();          // ������ �簳�ϸ� �ٽ� ������
                }
            }
            else if(savecanvas_obj.activeSelf == true)   //�޴� ĵ�������� ���̺� ĵ������ ������� 
            {
                savecanvas_obj.SetActive(false);
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
