using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    //�̺�Ʈ ������ ���� ��ũ��Ʈ

    //Variables

    //Private
    private PlayerManager thePlayer; //�̺�Ʈ ���� Ű�Է� ó�� ����
    private List<MovingObject> characters;

    //Public

    


    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<PlayerManager>(); 
    }

    public void PreLoadCharacter() //�̺�Ʈ ���� ȣ���� �Լ�
    {
        characters = ToList();
    }

    public List<MovingObject> ToList()
    {
        List<MovingObject> tempList = new List<MovingObject>();
        MovingObject[] temp = FindObjectsOfType<MovingObject>(); //�÷��̾�� NPC�� ��� �迭 �ȿ� �� 

        for (int i = 0; i < temp.Length; i++)
        {
            tempList.Add(temp[i]);
        }

        return tempList;
    }

    public void Move(string _name, string _dir)
    {
        for (int i = 0; i < characters.Count; i++)
        {
            if(_name == characters[i].characterName)
            {
                characters[i].Move(_dir);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
