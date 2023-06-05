using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Menu;

public class TitleButton : MonoBehaviour
{

    private bool first_new = true;
    private AudioManager theAudio;
    private PlayerManager thePlayerManager;
    private GameManager theGameManager;
    public GameObject theLoadUI;
    private GameObject theLogo;
    private GameObject theRun;
    private GameObject theRun2;


    private GameObject LoadBtn1;
    private GameObject LoadBtn2;
    private GameObject LoadBtn3;
    // Start is called before the first frame update
    void Start()
    {
        theAudio = FindObjectOfType<AudioManager>();
        thePlayerManager = FindObjectOfType<PlayerManager>();
        theGameManager= FindObjectOfType<GameManager>();
        theAudio.Play("TitleBGM");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void NewGame()                                   //ù��° ���� ����� �ٷ� ���̵� ,�װ� �ٽ� start������ ����Ʈ���̺����Ϸε�
    {
        theAudio.Play("select2");
        if (theGameManager.isFirstGameStart)
        {
            SceneManager.LoadScene("StartScene");
            first_new = false;
            theGameManager.isFirstGameStart = false;
        }
        else
        {
            thePlayerManager.LoadSaveDefault();
        }
    }
    public void OpenLoadUI()
    {
        theLogo = GameObject.Find("DDAYLogo");
        theLogo.SetActive(false);

        theRun = GameObject.Find("CharacterRun");
        theRun.SetActive(false);

        theRun2 = GameObject.Find("CharacterRun2");
        theRun2.SetActive(false);                           //�̹��� �������� ��ħ������ loadâ�������� off

        theAudio.Play("select1");
        theLoadUI.SetActive(true);

        LoadBtn1 = GameObject.Find("Load_Button1");
        LoadBtn2 = GameObject.Find("Load_Button2");
        LoadBtn3 = GameObject.Find("Load_Button3");         

        LoadBtn1.GetComponent<Button>().onClick.AddListener(thePlayerManager.LoadSave1);
        LoadBtn2.GetComponent<Button>().onClick.AddListener(thePlayerManager.LoadSave2);
        LoadBtn3.GetComponent<Button>().onClick.AddListener(thePlayerManager.LoadSave3);            //�ٸ����� �ִ� ��ũ��Ʈ �Լ� �����ϱ����� addlister�� ��Ŭ���Լ��߰�

        Slot_Refresh_BeforeOpenUI(1, "Load");
        Slot_Refresh_BeforeOpenUI(2, "Load");
        Slot_Refresh_BeforeOpenUI(3, "Load");              //�ε嵥���� UI ����ȭ�� ( �÷���Ÿ��,������ġ)
    }
    public void Slot_Refresh_BeforeOpenUI(int i, string slot_type)  // slot_type(in hieraarchy) -> Load or Save
    {
        GameObject slot = GameObject.Find("Title"+slot_type + "Slot" + i);
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
    public void CloseLoadUI()
    {
        theAudio.Play("cancel1");
        theLoadUI.SetActive(false);
        theLogo.SetActive(true);
        theRun.SetActive(true);
        theRun2.SetActive(true);
    }


    public void ExitGame()
    {
        Application.Quit();         //��������, �����Ϳ����� �ȸ���
    }

    public bool isFirstGame()
    {
        return first_new;           //�׽��ÿ�
    }
}
