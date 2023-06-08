using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestSaveNLoad : MonoBehaviour
{
    // ���� ���� ���� �� �ε带 ���� ��ũ��Ʈ

    //For Initialization
    public PlayerManager thePlayer;
    public CameraManager theCamera;

    public DatabaseManager theDatabase; //
    public Inventory theInven; //
    public List<string> item_id__should_destroy;//

    public checkVisit[] checkVisits;

    public SpawnKey[] checkKeys;

    public GameObject spawnManager;
    public GameObject[] checkPickforSpawn;

    //Save N Load File
    public TestSaveFile[] testSaveFile;
    public int item_count;
    public int FileIndex;

    public void Start()
    {
        testSaveFile = new TestSaveFile[4]; //�� 3���� ���̺� ����  //4��° ���̺������� �����ϱ�뼼�̺�����

        for (int i = 0; i <testSaveFile.Length; i++)
        {
            testSaveFile[i] = GetComponent<TestSaveFile>();
        }

        thePlayer = FindObjectOfType<PlayerManager>();
        theCamera = FindObjectOfType<CameraManager>();

        theDatabase = FindObjectOfType<DatabaseManager>();//
        theInven = FindObjectOfType<Inventory>();//

        checkVisits = FindObjectsOfType<checkVisit>();
        checkKeys = FindObjectsOfType<SpawnKey>();
        
    }

    private void callSave()
    {
        testSaveFile[FileIndex].PlayerPos = thePlayer.transform.position;
        testSaveFile[FileIndex].CameraPos = theCamera.transform.position;
        testSaveFile[FileIndex].currentBound = theCamera.bound;
        
        //VisitManager
        for (int i = 0; i < checkVisits.Length; i++)
        {
            testSaveFile[FileIndex].confirmVisit.Add(checkVisits[i].confirmvisitnum);
        }

        //KeyManager
        for (int i = 0; i < checkKeys.Length; i++)
        {
            testSaveFile[FileIndex].confirmKeySpawn.Add(checkKeys[i].visit);
        }

        //SpawnManager
        spawnManager = GameObject.Find("SpawnManager");
        checkPickforSpawn = new GameObject[spawnManager.transform.childCount];

        for (int i = 0; i < checkPickforSpawn.Length; i++)
        {
            checkPickforSpawn[i] = spawnManager.transform.GetChild(i).gameObject;
        }

        foreach (GameObject obj in checkPickforSpawn)
        {
            if (!obj.activeSelf) testSaveFile[FileIndex].confirmPickforSpawn.Add(false);
            else testSaveFile[FileIndex].confirmPickforSpawn.Add(true);
        }

        testSaveFile[FileIndex].playerItemInventory.Clear();//
        testSaveFile[FileIndex].playerItemInventoryCount.Clear();//

        List<Item> itemList = theInven.SaveItem();//

        for (int i = 0; i < itemList.Count; i++)//
        {
            testSaveFile[FileIndex].playerItemInventory.Add(itemList[i].itemID);//
            testSaveFile[FileIndex].playerItemInventoryCount.Add(itemList[i].itemCount);//
        }
    }

    private void callLoad()
    {
        thePlayer.transform.position = testSaveFile[FileIndex].PlayerPos;
        theCamera.bound = testSaveFile[FileIndex].currentBound;
        theCamera.minBound = testSaveFile[FileIndex].currentBound.bounds.min;
        theCamera.maxBound = testSaveFile[FileIndex].currentBound.bounds.max;
        theCamera.transform.position = testSaveFile[FileIndex].CameraPos;
        for (int i = 0; i < checkVisits.Length; i++)
        {
            //���Ͽ� �ε����� �´� confirmvisit���� �ҷ���
            checkVisits[i].confirmvisitnum = testSaveFile[FileIndex].confirmVisit[i];
        }

        for (int i = 0; i < checkKeys.Length; i++)
        {
            checkKeys[i].visit = testSaveFile[FileIndex].confirmKeySpawn[i];
        }

        for (int i = 0; i < checkPickforSpawn.Length; i++)
        {
            checkPickforSpawn[i].SetActive(testSaveFile[FileIndex].confirmPickforSpawn[i]);
        }


        List<Item> itemList = new List<Item>();

        for (int i = 0; i < testSaveFile[FileIndex].playerItemInventory.Count; i++)
        {
            for (int x = 0; x < theDatabase.itemList.Count; x++)
            {
                if (testSaveFile[FileIndex].playerItemInventory[i] == theDatabase.itemList[x].itemID)
                {
                    itemList.Add(theDatabase.itemList[x]);
                }
            }
        }

        for (int i = 0; i < testSaveFile[FileIndex].playerItemInventoryCount.Count; i++)
        {
            itemList[i].itemCount = testSaveFile[FileIndex].playerItemInventoryCount[i];
        }

        theInven.LoadItem(itemList);

        item_count = 0;
        for (int i = 0; i < testSaveFile[FileIndex].playerItemInventoryCount.Count; i++)
        {
            item_id__should_destroy.Add((testSaveFile[FileIndex].playerItemInventory[i]).ToString());
            item_count++;
        }
    }



    public void callTestSave1()
    {
        FileIndex = 0;
        callSave();
    }

    public void callTestSave2()
    {
        FileIndex = 1;
        callSave();
    }

    public void callTestSave3()
    {
        FileIndex = 2;
        callSave();
    }


    public void callTestLoad1()
    {
        FileIndex = 0;
        callLoad();
    }

    public void callTestLoad2()
    {
        FileIndex = 1;
        callLoad();
    }

    public void callTestLoad3()
    {
        FileIndex = 2;
        callLoad();
    }


    public void callTestLoadFromAnotherScene1()
    {
        SceneManager.LoadScene("StartScene");
        FileIndex = 0;
        callLoad();
    }

    public void callTestLoadFromAnotherScene2()
    {
        SceneManager.LoadScene("StartScene");
        FileIndex = 1;
        callLoad();
    }
    public void callTestLoadFromAnotherScene3()
    {
        SceneManager.LoadScene("StartScene");
        FileIndex = 2;
        callLoad();
    }

    public void MakeDeafultSaveFile()       //����Ʈ���̺����ϻ���
    {
        FileIndex = 3;
        callSave();
        Debug.Log("����Ʈ ���� ���� �ϰ� �ε����� " + FileIndex);
    }

    public void CallNewGame()               //���ε��� ���̺����Ϸε�
    {
        FileIndex = 3;
        callLoad();
        Debug.Log("����Ʈ ���� �ε� �ϰ� �ε����� " + FileIndex);
    }

}