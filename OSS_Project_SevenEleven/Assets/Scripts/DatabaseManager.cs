using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    static public DatabaseManager instance; //�̱���

    private void Awake() 
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(this.gameObject);
        }
    }

    public string[] var_name;
    public float[] var;

    public string[] switch_name;
    public bool[] switches; // ���� ������ ���� ����ġ.

    public List<Item> itemList = new List<Item>();

    // Start is called before the first frame update
    void Start()
    {
        itemList.Add(new Item(10001, "����", "���� �� ����", Item.ItemType.Quest));
        itemList.Add(new Item(10002, "�ϱ���", "ù��° �ϱ���", Item.ItemType.Quest));
        itemList.Add(new Item(10003, "����", "���̽����̽� ����", Item.ItemType.Quest));
    }

}
