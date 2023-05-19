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
    public bool[] switches; // ���� ���������� ���� ����ġ.

    public List<Item> itemList = new List<Item>();



    // Start is called before the first frame update
    void Start()
    {
        itemList.Add(new Item(10001, "1��° ����", "���� �� ����", Item.ItemType.Quest));
        itemList.Add(new Item(10002, "2��° ����", "������ �� ����", Item.ItemType.Quest));
        itemList.Add(new Item(10003, "3��° ����", "���� �Ʒ� ����", Item.ItemType.Quest));
        itemList.Add(new Item(10004, "4��° ����", "������ �Ʒ� ����", Item.ItemType.Quest));
        itemList.Add(new Item(10005, "����", "��ü ����", Item.ItemType.Quest));

        itemList.Add(new Item(10006, "1��° �ϱ���", "1��° �ϱ���", Item.ItemType.Quest));
        itemList.Add(new Item(10007, "2��° �ϱ���", "2��° �ϱ���", Item.ItemType.Quest));
        itemList.Add(new Item(10008, "3��° �ϱ���", "3��° �ϱ���", Item.ItemType.Quest));
        itemList.Add(new Item(10009, "4��° �ϱ���", "4��° �ϱ���", Item.ItemType.Quest));
        itemList.Add(new Item(10010, "5��° �ϱ���", "5��° �ϱ���", Item.ItemType.Quest));
        itemList.Add(new Item(10011, "6��° �ϱ���", "6��° �ϱ���", Item.ItemType.Quest));
        itemList.Add(new Item(10012, "7��° �ϱ���", "7��° �ϱ���", Item.ItemType.Quest));
        itemList.Add(new Item(10013, "8��° �ϱ���", "8��° �ϱ���", Item.ItemType.Quest));
        itemList.Add(new Item(10014, "9��° �ϱ���", "9��° �ϱ���", Item.ItemType.Quest));
        itemList.Add(new Item(10015, "10��° �ϱ���", "10��° �ϱ���", Item.ItemType.Quest));
        itemList.Add(new Item(10016, "�ϱ���", "�ϱ���", Item.ItemType.Quest));

        itemList.Add(new Item(10017, "���̽����̽� ����", "���̽����̽��� ���� ����", Item.ItemType.Quest));
        itemList.Add(new Item(10018, "5�� ����� ����", "5�� ����� ����", Item.ItemType.Quest));

        itemList.Add(new Item(10019, "�л���", "�л���", Item.ItemType.Quest));
        itemList.Add(new Item(10020, "����", "����", Item.ItemType.Quest));
    }
}
