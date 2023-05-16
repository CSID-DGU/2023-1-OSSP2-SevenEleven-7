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

    /* public void UseItem(int _itemID) �Ҹ�ǰ�� �ִ� ��� ���Խ�Ű�� ���� �� �����ϴ�.(�Ҹ�ǰ�� ������ �����ص� �Ǵ� �ڵ�) ex. ���� 
    {
        switch (_itemID)
        {
            case 10015:
                Debug.Log("���̽����̽��� ���Ƚ��ϴ�.");
                break;            
        }
    }
    */

    // Start is called before the first frame update
    void Start()
    {
        itemList.Add(new Item(10001, "1��° ����", "���� �� ����", Item.ItemType.Quest));
        itemList.Add(new Item(10002, "2��° ����", "������ �� ����", Item.ItemType.Quest));
        itemList.Add(new Item(10003, "3��° ����", "���� �Ʒ� ����", Item.ItemType.Quest));
        itemList.Add(new Item(10004, "4��° ����", "������ �Ʒ� ����", Item.ItemType.Quest));

        itemList.Add(new Item(10005, "1��° �ϱ���", "1��° �ϱ���", Item.ItemType.Quest));
        itemList.Add(new Item(10006, "2��° �ϱ���", "2��° �ϱ���", Item.ItemType.Quest));
        itemList.Add(new Item(10007, "3��° �ϱ���", "3��° �ϱ���", Item.ItemType.Quest));
        itemList.Add(new Item(10008, "4��° �ϱ���", "4��° �ϱ���", Item.ItemType.Quest));
        itemList.Add(new Item(10009, "5��° �ϱ���", "5��° �ϱ���", Item.ItemType.Quest));
        itemList.Add(new Item(10010, "6��° �ϱ���", "6��° �ϱ���", Item.ItemType.Quest));
        itemList.Add(new Item(10011, "7��° �ϱ���", "7��° �ϱ���", Item.ItemType.Quest));
        itemList.Add(new Item(10012, "8��° �ϱ���", "8��° �ϱ���", Item.ItemType.Quest));
        itemList.Add(new Item(10013, "9��° �ϱ���", "9��° �ϱ���", Item.ItemType.Quest));
        itemList.Add(new Item(10014, "10��° �ϱ���", "10��° �ϱ���", Item.ItemType.Quest));

        itemList.Add(new Item(10015, "���̽����̽� ����", "���̽����̽��� ���� ����", Item.ItemType.Quest));
        //����κ��� ���Ŀ� �� �߰�
    }
}
