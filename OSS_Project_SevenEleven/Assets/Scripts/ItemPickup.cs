using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public int itemID; // �����ͺ��̽��� ItemID�� ���Ѵ�. ��ġ�ϴ� ��� �κ��丮�� �߰���
    public int _count;
    public string pickUpSound; // ������ ȹ��� ����

    public bool isPick = false;
    public GameObject displayImage; 

    /*private void OnTriggerStay2D(Collider2D collision)
    {   
        if (Input.GetKeyDown(KeyCode.X)) // ZŰ ������ ������ ȹ��
        {
            AudioManager.instance.Play(pickUpSound);
            Inventory.instance.GetAnItem(itemID, _count); // �κ��丮�� ȹ���� ������ �߰��ϴ� ����
            //Destroy(this.gameObject);
            this.gameObject.SetActive(false);
            isPick = true;
        }
    }*/

    private void ShowItemImage()
    {
        displayImage.SetActive(true);
        if (Input.GetKeyDown(KeyCode.Z))
        {
            displayImage.SetActive(false);
        }
    }

    private bool isPlayerOn = false;                        // �ڽ� �ݶ��̴��� �÷��̾ �����ִ��� Ȯ���ϴ� ����
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))                 //�÷��̾ �ڽ� �ݶ��̴� ���� ������
            isPlayerOn = true;
    }

    private void Start()
    {
        displayImage.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X)) 
        {
            if (isPlayerOn)                                     //XŰ ������ playeron�̸�
            {
                AudioManager.instance.Play(pickUpSound);
                Inventory.instance.GetAnItem(itemID, _count); // �κ��丮�� ȹ���� ������ �߰��ϴ� ����
                                                              //Destroy(this.gameObject);
                this.gameObject.SetActive(false);
                isPick = true;

                if (itemID == 10006)
                {
                    ShowItemImage();
                }
            }


        }

    }


}
