using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveNLoad : MonoBehaviour
{
    [System.Serializable]
    public class Data // ��� ���̺� ��ϵ��� ���� Ŭ����
    {
        public float playerX;
        public float playerY;
        public float playerZ;

        public List<int> playerItemInventory; // ������ �ִ� �������� ���̵��� ����
        public List<int> playerItemInventoryCount; 

        public string mapName; // ĳ���Ͱ� ��� �ʿ� �־�����
        public string sceneName; // ĳ���Ͱ� ��� ���� �־�����

        public List<bool> swList;
        public List<string> swNameList;
        public List<string> varNameList;
        public List<float> varNumberList;
    }

    private PlayerManager thePlayer; // �÷��̾��� ��ġ���� �˱����� ����
    private DatabaseManager theDatabase;
    private Inventory theInven;

    public Data data;

    private Vector3 vector; // vector�� �÷��̾��� ��ġ�� ��� playerX,playerY,playerZ�� �ְ� �ҷ��� ����


    public void CallSave() // ���̺갡 �̷������ ����
    {
        theDatabase = FindObjectOfType<DatabaseManager>();
        thePlayer = FindObjectOfType<PlayerManager>();
        theInven = FindObjectOfType<Inventory>();
    }

    public void CallLoad() // �ε尡 �̷������ ����
    {

    }
}
