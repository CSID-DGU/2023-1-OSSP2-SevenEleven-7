using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSaveNLoad : MonoBehaviour
{
    // ���� ���� ���� �� �ε带 ���� ��ũ��Ʈ

    //For Initialization
    private PlayerManager thePlayer;
    private CameraManager theCamera;

    private GameObject VisitManager;
    private GameObject[] ChildVisitManager;
    private int CheckVisitLength;

    //Save N Load File
    public TestSaveFile[] testSaveFile;

    private int FileIndex;

    public void Start()
    {
        testSaveFile = new TestSaveFile[3]; //�� 3���� ���̺� ����
        thePlayer = FindObjectOfType<PlayerManager>();
        theCamera = FindObjectOfType<CameraManager>();
    }

    private void callSave()
    {
        testSaveFile[FileIndex].PlayerPos = thePlayer.transform.position;
        testSaveFile[FileIndex].currentBound = theCamera.bound;

        VisitManager = GameObject.Find("VisitManager");
        CheckVisitLength = VisitManager.transform.childCount;
        ChildVisitManager = new GameObject[CheckVisitLength];

        for (int i = 0; i < CheckVisitLength; i++)
        {
            ChildVisitManager[i] = VisitManager.transform.GetChild(i).gameObject;
        }

        foreach (GameObject obj in ChildVisitManager)
        {
            if (!obj.activeSelf) testSaveFile[FileIndex].isVisitCheck.Add(false);
            else testSaveFile[FileIndex].isVisitCheck.Add(true);
        }


    }

    private void callLoad()
    {
        thePlayer.transform.position = testSaveFile[FileIndex].PlayerPos;
        theCamera.bound = testSaveFile[FileIndex].currentBound;
    }
}
