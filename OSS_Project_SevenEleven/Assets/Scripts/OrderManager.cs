using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    //�̺�Ʈ ������ ���� ��ũ��Ʈ

    //Variables

    //Private
    private PlayerManager thePlayer; //�̺�Ʈ ���� Ű�Է� ó�� ����
    private List<MovingObject> characters;




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
    public void Move(string _name, string _dir) //Ư�� �������� �̵�
    {
        for (int i = 0; i < characters.Count; i++)
        {
            if(_name == characters[i].characterName)
            {
                characters[i].Move(_dir);
            }
        }
    }


    public void Turn(string _name, string _dir) //Ư�� �������� ȸ��
    {
        for (int i = 0; i < characters.Count; i++)
        {
            if (_name == characters[i].characterName)
            {
                characters[i].animator.SetFloat("DirX", 0f);
                characters[i].animator.SetFloat("DirY", 0f);
                switch (_dir)
                {
                    case "UP":
                        characters[i].animator.SetFloat("DirY",1f);
                        break;
                    case "DOWN":
                        characters[i].animator.SetFloat("DirY", -1f);
                        break;
                    case "RIGHT":
                        characters[i].animator.SetFloat("DirX", 1f);
                        break;
                    case "LEFT":
                        characters[i].animator.SetFloat("DirX", -1f);
                        break;
                }

            }
        }
    }

    public void SetTransparent(string _name) //����ȭ
    {
        for (int i = 0; i < characters.Count; i++)
        {
            if (_name == characters[i].characterName)
            {
                characters[i].gameObject.SetActive(false);
            }
        }
    }

    public void SetUnTransparent(string _name) //����ȭ ����
    {
        for (int i = 0; i < characters.Count; i++)
        {
            if (_name == characters[i].characterName)
            {
                characters[i].gameObject.SetActive(true);
            }
        }
    }
    
    public void SetThorought(string _name) //Layer ���
    {
        for (int i = 0; i < characters.Count; i++)
        {
            if (_name == characters[i].characterName)
            {
                characters[i].boxCollider.enabled = false;
            }
        }
    }

    public void SetUnThorought(string _name) //Layer ��� �Ұ���
    {
        for (int i = 0; i < characters.Count; i++)
        {
            if (_name == characters[i].characterName)
            {
                characters[i].boxCollider.enabled = true;
            }
        }
    }

    //ĳ���� �߰ݾ� �߰� ����




    // Update is called once per frame
    void Update()
    {
        
    }
}
